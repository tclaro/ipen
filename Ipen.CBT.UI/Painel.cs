using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Ipen.CompartimentalModel;

namespace Ipen.CBT.UI
{
    public class Painel : Panel
    {
        #region Campos
        Sistema _sistemaCompartimental;

        //Linhas conectadas à caixa sendo arrastada no momento (montada uma vez
        //no mouse-down, não recalculada a cada movimento) e a região suja do
        //último frame, para que só a área afetada pelo arrasto seja invalidada
        //em vez do painel inteiro.
        private System.Collections.Generic.List<Linhas> _linhasDaCaixaArrastada;
        private Rectangle _regiaoSujaAnterior = Rectangle.Empty;
        private const int MargemRegiaoSuja = 150; //cobre espessura de traço, seta e rótulo
        #endregion

        #region Construtor
        public Painel()
        {
            //Evita que o WinForms apague o fundo antes de pintar (causa do "flick"
            //ao arrastar um compartimento) e faz este painel se pintar num buffer
            //fora da tela antes de apresentar.
            SetStyle(ControlStyles.AllPaintingInWmPaint
                   | ControlStyles.UserPaint
                   | ControlStyles.OptimizedDoubleBuffer, true);

            InitializeHelper();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //WS_EX_COMPOSITED: faz o Windows compor este painel e todos os
                //seus controles filhos (as Caixas, que são Controls próprios)
                //fora da tela antes de apresentar. É isto - e não apenas
                //DoubleBuffered/OptimizedDoubleBuffer - que elimina o flicker
                //quando há muitos controles filhos, porque DoubleBuffered só
                //bufferiza a pintura do próprio painel, não a dos filhos.
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        void InitializeHelper()
        {
            _sistemaCompartimental = Sistema.getInstance();
            _sistemaCompartimental.BoxMouseDown += new MouseEventHandler(_sistemaCompartimental_BoxMouseDown);
            _sistemaCompartimental.BoxDeleted += new Caixas.CaixaEventHandler(_sistemaCompartimental_BoxDeleted);
            _sistemaCompartimental.BoxDoubleClick += new EventHandler(_sistemaCompartimental_BoxDoubleClick);
            _sistemaCompartimental.BoxKeyDown += new KeyEventHandler(_sistemaCompartimental_BoxKeyDown);
            _sistemaCompartimental.BoxMoved += new Caixas.CaixaEventHandler(_sistemaCompartimental_BoxMoved);
            _sistemaCompartimental.BoxPropertyChanged += new Caixas.CaixaEventHandler(_sistemaCompartimental_BoxPropertyChanged);
            _sistemaCompartimental.BoxMove += new EventHandler(_sistemaCompartimental_BoxMove);
        }
         
        #endregion

        #region Propriedades públicas
        public Sistema SistemaCompartimental
        {
            get { return _sistemaCompartimental; }
            set { _sistemaCompartimental = value; }
        }
        #endregion

        #region Métodos públicos
        public void IncluirCaixa(Caixas cx)
        {
            //_sistemaCompartimental.Caixas.Add(cx);
            this.Controls.Add(cx);
            cx.BringToFront();
            this.VerificarCaixasSobrepostas(cx);
            //this.Refresh();
        }
        public void IncluirLinha(Linhas ln)
        {

            this._sistemaCompartimental.Linhas.Add(ln);
            this.Invalidate();
        }

        public void DesmarcarTudo()
        {
            foreach (Caixas cx in this._sistemaCompartimental.Caixas)
                cx.EstaSelecionado = false;

            foreach (Linhas ln in this._sistemaCompartimental.Linhas)
                ln.EstaSelecionado = false;

            
        }
        #endregion

        #region Novos Eventos
        public delegate void BoxModifyRequestHandle(Caixas cx);
        public event BoxModifyRequestHandle BoxModifyRequest;

        protected void OnBoxModifyRequest(Caixas cx)
        {
            if (BoxModifyRequest != null)
                BoxModifyRequest(cx);
        }
        #endregion

        #region Métodos de eventos
        void _sistemaCompartimental_BoxPropertyChanged(Caixas.BoxEventArgs be)
        {
            this.Invalidate();

            //Mudar seleção não deveria reposicionar caixas: só verifica
            //sobreposição quando a propriedade alterada não foi a seleção.
            if (be.EventType != Caixas.BoxEventArgs.BoxEventTypes.SelectionChanged)
                this.VerificarCaixasSobrepostas(be.Box);
        }
        void _sistemaCompartimental_BoxMove(object sender, EventArgs e)
        {
            //sender é a própria Caixas que se moveu (preservado pela cadeia
            //CaixasCollection -> Sistema -> Painel).
            if (_linhasDaCaixaArrastada == null || _linhasDaCaixaArrastada.Count == 0)
            {
                //Sem lista construída (ex.: movimento programático fora de um
                //arrasto de mouse) - cai no comportamento seguro de sempre.
                this.Invalidate();
                return;
            }

            Rectangle regiao = Rectangle.Empty;
            foreach (Linhas ln in _linhasDaCaixaArrastada)
            {
                Rectangle limites = LimitesDaLinha(ln);
                regiao = regiao.IsEmpty ? limites : Rectangle.Union(regiao, limites);
            }

            if (!_regiaoSujaAnterior.IsEmpty)
                regiao = Rectangle.Union(regiao, _regiaoSujaAnterior);

            _regiaoSujaAnterior = regiao;

            //false: não desce a invalidação para os controles filhos (as 48
            //Caixas) - eles não mudaram, e o próprio WinForms já cuida de
            //repintar o filho que se moveu.
            this.Invalidate(regiao, false);
        }
        void _sistemaCompartimental_BoxMoved(Caixas.BoxEventArgs be)
        {
            this.VerificarCaixasSobrepostas(be.Box);
            _linhasDaCaixaArrastada = null;
            _regiaoSujaAnterior = Rectangle.Empty;
        }

        private Rectangle LimitesDaLinha(Linhas ln)
        {
            Point a = ln.PontoInicio;
            Point b = ln.PontoFim;
            int x = Math.Min(a.X, b.X) - MargemRegiaoSuja;
            int y = Math.Min(a.Y, b.Y) - MargemRegiaoSuja;
            int w = Math.Abs(a.X - b.X) + MargemRegiaoSuja * 2;
            int h = Math.Abs(a.Y - b.Y) + MargemRegiaoSuja * 2;
            return new Rectangle(x, y, w, h);
        }
        void _sistemaCompartimental_BoxKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is Caixas))
                return;
            Caixas cx = (Caixas)sender;
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dlgResposta = MessageBox.Show(string.Format("Tem certeza que deseja excluir o compartimento {0} ({1}) e todas as suas ligações?", cx.Numero, cx.Nome), "Exclusão de compartimento", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlgResposta == DialogResult.Yes)
                    _sistemaCompartimental.Caixas.Remove(cx);
            }
        }
        void _sistemaCompartimental_BoxDoubleClick(object sender, EventArgs e)
        {
            if (!(sender is Caixas))
                return;
            Caixas cx = (Caixas)sender;
            OnBoxModifyRequest(cx);
        }

        void _sistemaCompartimental_BoxDeleted(Caixas.BoxEventArgs be)
        {
        }

        void _sistemaCompartimental_BoxMouseDown(object sender, MouseEventArgs e)
        {
            this.DesmarcarTudo();

            if (!(sender is Caixas))
                return;
            Caixas cx = (Caixas)sender;
            cx.BringToFront();
            cx.EstaSelecionado = true;

            //Monta, uma única vez por possível arrasto (não a cada
            //mouse-move), a lista de linhas conectadas a esta caixa. É o que
            //permite invalidar só a região afetada durante o arrasto em vez
            //do painel inteiro.
            _linhasDaCaixaArrastada = new System.Collections.Generic.List<Linhas>();
            foreach (Linhas ln in _sistemaCompartimental.Linhas)
                if (ln.CaixaInicio == cx || ln.CaixaFim == cx)
                    _linhasDaCaixaArrastada.Add(ln);
            _regiaoSujaAnterior = Rectangle.Empty;

            OnBoxModifyRequest(cx);
        }

        protected override void OnClick(EventArgs e)
        {
            this.DesmarcarTudo();

            base.OnClick(e);
        }
        #endregion

        #region Métodos internos
        public void VerificarCaixasSobrepostas(Caixas cx)
        {
            while (ObterCaixaPorClique(cx.PontosExtremos, cx) != null)
                cx.Left += 5;
        }
        private Caixas ObterCaixaPorClique(System.Drawing.Point pto)
        {
            foreach (Caixas cx in this._sistemaCompartimental.Caixas)
                if (cx.PontoNessaCaixa(pto))
                    return cx;

            return null;
        }
        private Caixas ObterCaixaPorClique(System.Drawing.Point[] ptos, Caixas CaixaNaoVerificada)
        {
            foreach (Caixas cx in this._sistemaCompartimental.Caixas)
            {
                if (cx == CaixaNaoVerificada)
                    continue;
                foreach (System.Drawing.Point pto in ptos)
                {
                    if (cx.PontoNessaCaixa(pto))
                        return cx;
                }
                System.Drawing.Point[] ptos2 = cx.PontosExtremos;
                foreach (System.Drawing.Point pto in ptos2)
                {
                    if (CaixaNaoVerificada.PontoNessaCaixa(pto))
                        return cx;
                }
            }

            return null;
        }
        #endregion

        //Objetos GDI+ reaproveitados entre linhas e entre frames (mutados por
        //propriedade em vez de recriados) - o laço de pintura original criava
        //Pen/SolidBrush/GraphicsPath novos a cada linha e nunca os descartava
        //(vazamento de handles GDI, agravado durante o arrasto).
        private readonly Pen _penLinha = new Pen(Color.Black);
        private readonly Pen _penRotulo = new Pen(Color.Black);
        private readonly SolidBrush _brushRotulo = new SolidBrush(Color.Black);
        private readonly SolidBrush _brushSeta = new SolidBrush(Color.Black);
        private readonly GraphicsPath _pathSeta = new GraphicsPath();

        protected override void OnPaint(PaintEventArgs e)
        {
            //this.SuspendLayout();

            #region Configurar gráficos

            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = InterpolationMode.Default;
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion


            #region Desenhar Linhas
            foreach (Linhas ln in _sistemaCompartimental.Linhas)
            {
                //G-2: uma auto-ligação sem os dois extremos resolvidos (ex.:
                //vinda de um XML malformado) não tem o que desenhar.
                if (ln.CaixaInicio == null || ln.CaixaFim == null)
                    continue;

                //Pula por completo linhas fora da região sendo repintada.
                //Combinado com a invalidação por região do arrasto, o custo
                //do frame passa a ser proporcional às linhas realmente
                //visíveis/afetadas, não ao total do modelo.
                if (!e.ClipRectangle.IntersectsWith(LimitesDaLinha(ln)))
                    continue;

                int EspessuraDaLinha = 1;
                Brush CorDoRotulo = System.Drawing.Brushes.White;

                if (ln.CaixaInicio.EstaSelecionado || ln.CaixaFim.EstaSelecionado)
                {
                    EspessuraDaLinha = 3;
                    CorDoRotulo = Brushes.PapayaWhip;
                }

                //Desenha se não estivermos restringindo às ligações do
                //selecionado, ou se esta linha tocar um compartimento
                //selecionado.
                if (!Configuracoes.ExibirApenasLigacoesSelecionadas || (ln.CaixaInicio.EstaSelecionado || ln.CaixaFim.EstaSelecionado))
                {
                    //Coeficiente angular calculado uma vez e reaproveitado
                    //pelas setas (cada seta relia PontoInicio/PontoFim até 8x).
                    float m = ln.CoeficienteAngular;

                    //Desenha a seta
                    if (Configuracoes.ExibirSetas)
                    {
                        if (ln.DirecaoDaLinha == Linhas.Direcao.InicioParaFim || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                            DesenharSetaDirecao(ln.CaixaFim, ln, ln.CaixaInicio, e.Graphics, m);
                        if (ln.DirecaoDaLinha == Linhas.Direcao.FimParaInicio || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                            DesenharSetaDirecao(ln.CaixaInicio, ln, ln.CaixaFim, e.Graphics, m);
                    }

                    //Desenha a linha
                    _penLinha.Color = ln.BackColor;
                    _penLinha.Width = EspessuraDaLinha;
                    e.Graphics.DrawLine(_penLinha, ln.PontoInicio, ln.PontoFim);

                    //Tentativa com curve no lugar de line
                   //e.Graphics.DrawCurve(new Pen(ln.ForeColor, EspessuraDaLinha), new Point[2]{ln.PontoInicio, ln.PontoFim},0 );

                    //Rótulo da Linha
                    if (Configuracoes.ExibirRotulos)
                    {
                        _brushRotulo.Color = ln.BackColor;
                        _penRotulo.Color = ln.BackColor;

                        if (ln.DirecaoDaLinha != Linhas.Direcao.Ambos)
                        {
                            System.Drawing.SizeF tamanho = ln.MedirRotulo(e.Graphics, ln.Nome);
                            e.Graphics.FillRectangle(CorDoRotulo, (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                            e.Graphics.DrawString(ln.Nome, ln.Font, _brushRotulo, ln.PontoCentral.X - tamanho.Width / 2, ln.PontoCentral.Y - tamanho.Height / 2);
                            e.Graphics.DrawRectangle(_penRotulo, (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                        }
                        else
                        {
                            System.Drawing.SizeF tamanho = ln.MedirRotulo(e.Graphics, ln.NomeAB);
                            e.Graphics.FillRectangle(CorDoRotulo, (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                            e.Graphics.DrawString(ln.NomeAB, ln.Font, _brushRotulo, ln.PontoTercoInicio.X - tamanho.Width / 2, ln.PontoTercoInicio.Y - tamanho.Height / 2);
                            e.Graphics.DrawRectangle(_penRotulo, (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);

                            System.Drawing.SizeF tamanho2 = ln.MedirRotulo(e.Graphics, ln.NomeBA);
                            e.Graphics.FillRectangle(CorDoRotulo, (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                            e.Graphics.DrawString(ln.NomeBA, ln.Font, _brushRotulo, ln.PontoTercoFim.X - tamanho2.Width / 2, ln.PontoTercoFim.Y - tamanho2.Height / 2);
                            e.Graphics.DrawRectangle(_penRotulo, (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                        }

                    }
                }
            }
            #endregion

            //this.ResumeLayout();

            //Não há nada de base para pintar (UserPaint está ligado e este
            //painel não delega a nenhum outro handler de OnPaint).
            //base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _penLinha.Dispose();
                _penRotulo.Dispose();
                _brushRotulo.Dispose();
                _brushSeta.Dispose();
                _pathSeta.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //SuspendLayout/ResumeLayout não têm efeito sobre pintura; foram
            //removidos daqui. Com AllPaintingInWmPaint ligado, o fundo já é
            //pintado dentro do buffer, não direto na tela.
            base.OnPaintBackground(e);
        }
        
        private void DesenharSetaDirecao(Caixas Caixa, Linhas ln, Caixas OutraCaixa, Graphics g, float m)
        {
            _brushSeta.Color = ln.BackColor;

            //ln.XdeY(Caixa.Top) é o mesmo valor nas quatro comparações das
            //duas primeiras condições (seta de cima e seta de baixo ambas
            //testam contra a interseção da linha no Y do topo da caixa) -
            //calculado uma única vez em vez de até 4x por chamada.
            int xTop = ln.XdeY(Caixa.Top, m);

            //Idem para ln.YdeX(Caixa.Left), reaproveitado pelas duas
            //condições de seta lateral (a de baixo também compara contra o
            //X do lado esquerdo - comportamento original preservado).
            int yLeft = ln.YdeX(Caixa.Left, m);

            //Seta na parte superior do compartimento
            if (xTop > Caixa.Left && xTop <= Caixa.Right && OutraCaixa.PontoCentral.Y < Caixa.PontoCentral.Y)
            {
                Point pontaDaSeta = new Point(xTop, Caixa.Top);
                Point verticeDireita = new Point(pontaDaSeta.X + 4, pontaDaSeta.Y - 14);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 4, pontaDaSeta.Y - 14);
                _pathSeta.Reset();
                _pathSeta.AddLine(pontaDaSeta, verticeDireita);
                _pathSeta.AddLine(verticeDireita, verticeEsquerda);
                _pathSeta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(_brushSeta, _pathSeta);
            }

            //Seta na parte inferior
            if (xTop >= Caixa.Left && xTop < Caixa.Right && OutraCaixa.PontoCentral.Y > Caixa.PontoCentral.Y)
            {
                int xBottom = ln.XdeY(Caixa.Bottom, m);
                Point pontaDaSeta = new Point(xBottom, Caixa.Bottom);
                Point verticeDireita = new Point(pontaDaSeta.X + 4, pontaDaSeta.Y + 14);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 4, pontaDaSeta.Y + 14);
                _pathSeta.Reset();
                _pathSeta.AddLine(pontaDaSeta, verticeDireita);
                _pathSeta.AddLine(verticeDireita, verticeEsquerda);
                _pathSeta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(_brushSeta, _pathSeta);
            }

            //Seta na lateral esquerda
            if (yLeft >= Caixa.Top && yLeft < Caixa.Bottom && OutraCaixa.PontoCentral.X < Caixa.PontoCentral.X)
            {
                Point pontaDaSeta = new Point(Caixa.Left, yLeft);
                Point verticeDireita = new Point(pontaDaSeta.X - 14, pontaDaSeta.Y + 4);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 14, pontaDaSeta.Y - 4);
                _pathSeta.Reset();
                _pathSeta.AddLine(pontaDaSeta, verticeDireita);
                _pathSeta.AddLine(verticeDireita, verticeEsquerda);
                _pathSeta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(_brushSeta, _pathSeta);
            }

            //Seta na lateral direita
            if (yLeft >= Caixa.Top && yLeft <= Caixa.Bottom && OutraCaixa.PontoCentral.X > Caixa.PontoCentral.X)
            {
                int yRight = ln.YdeX(Caixa.Right, m);
                Point pontaDaSeta = new Point(Caixa.Right, yRight);
                Point verticeDireita = new Point(pontaDaSeta.X + 14, pontaDaSeta.Y + 4);
                Point verticeEsquerda = new Point(pontaDaSeta.X + 14, pontaDaSeta.Y - 4);
                _pathSeta.Reset();
                _pathSeta.AddLine(pontaDaSeta, verticeDireita);
                _pathSeta.AddLine(verticeDireita, verticeEsquerda);
                _pathSeta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(_brushSeta, _pathSeta);
            }
        }
    }
}
