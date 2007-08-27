using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CBT
{
    public class Painel : Panel
    {
        #region Campos
        Sistema _sistemaCompartimental;
        #endregion

        #region Construtor
        public Painel()
        {
            InitializeHelper();
        }
        void InitializeHelper()
        {
            _sistemaCompartimental = new Sistema();
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
            this._sistemaCompartimental.Caixas.Add(cx);
            this.Controls.Add(cx);
            cx.BringToFront();
            this.VerificarCaixasSobrepostas(cx);
            this.Refresh();
        }
        public void IncluirLinha(Linhas ln)
        {
            this._sistemaCompartimental.Linhas.Add(ln);
            this.Refresh();
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
            this.Refresh();
            this.VerificarCaixasSobrepostas(be.Box);
        }
        void _sistemaCompartimental_BoxMove(object sender, EventArgs e)
        {
            this.Refresh();
        }
        void _sistemaCompartimental_BoxMoved(Caixas.BoxEventArgs be)
        {
            this.VerificarCaixasSobrepostas(be.Box);
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
                    this._sistemaCompartimental.Caixas.Remove(cx);
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
        }
        protected override void OnClick(EventArgs e)
        {
            this.DesmarcarTudo();
            base.OnClick(e);
        }
        #endregion

        #region Métodos internos
        private void VerificarCaixasSobrepostas(Caixas cx)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            #region Configurar gráficos
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion
            
            
            #region Desenhar Linhas
            foreach (Linhas ln in this.SistemaCompartimental.Linhas)
            {
                //Desenha a seta
                if (ln.DirecaoDaLinha == Linhas.Direcao.InicioParaFim || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                    DesenharSetaDirecao(ln.CaixaFim, ln, ln.CaixaInicio, e.Graphics);
                if (ln.DirecaoDaLinha == Linhas.Direcao.FimParaInicio || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                    DesenharSetaDirecao(ln.CaixaInicio, ln, ln.CaixaFim, e.Graphics);

                //Desenha a linha
                e.Graphics.DrawLine(new Pen(ln.ForeColor), ln.PontoInicio, ln.PontoFim);

                //Label da Linha
                if (ln.DirecaoDaLinha != Linhas.Direcao.Ambos)
                {
                    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(ln.Nome, ln.Font);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                    e.Graphics.DrawString(ln.Nome, ln.Font, new System.Drawing.SolidBrush(ln.ForeColor), ln.PontoCentral.X - tamanho.Width / 2, ln.PontoCentral.Y - tamanho.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.ForeColor), (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                }
                else
                {
                    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(ln.NomeAB, ln.Font);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                    e.Graphics.DrawString(ln.NomeAB, ln.Font, new System.Drawing.SolidBrush(ln.ForeColor), ln.PontoTercoInicio.X - tamanho.Width / 2, ln.PontoTercoInicio.Y - tamanho.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.ForeColor), (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);

                    System.Drawing.SizeF tamanho2 = e.Graphics.MeasureString(ln.NomeBA, ln.Font);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                    e.Graphics.DrawString(ln.NomeBA, ln.Font, new System.Drawing.SolidBrush(ln.ForeColor), ln.PontoTercoFim.X - tamanho2.Width / 2, ln.PontoTercoFim.Y - tamanho2.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.ForeColor), (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                }
                break;
            }
            #endregion
                   
            //base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }
        
        private void DesenharSetaDirecao(Caixas Caixa, Linhas ln, Caixas OutraCaixa, Graphics g)
        {
            //Seta na parte superior do compartimento
            if (ln.XdeY(Caixa.Top) >= Caixa.Left && ln.XdeY(Caixa.Top) <= Caixa.Right && OutraCaixa.PontoCentral.Y < Caixa.PontoCentral.Y)
            {
                Point pontaDaSeta = new Point(ln.XdeY(Caixa.Top), Caixa.Top);
                Point verticeDireita = new Point(pontaDaSeta.X + 4, pontaDaSeta.Y - 14);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 4, pontaDaSeta.Y - 14);
                GraphicsPath setaCompleta = new GraphicsPath();
                setaCompleta.AddLine(pontaDaSeta, verticeDireita);
                setaCompleta.AddLine(verticeDireita, verticeEsquerda);
                setaCompleta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(new SolidBrush(ln.ForeColor), setaCompleta);
            }

            //Seta na parte inferior 
            if (ln.XdeY(Caixa.Top) >= Caixa.Left && ln.XdeY(Caixa.Top) <= Caixa.Right && OutraCaixa.PontoCentral.Y > Caixa.PontoCentral.Y)
            {
                Point pontaDaSeta = new Point(ln.XdeY(Caixa.Bottom), Caixa.Bottom);
                Point verticeDireita = new Point(pontaDaSeta.X + 4, pontaDaSeta.Y + 14);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 4, pontaDaSeta.Y + 14);
                GraphicsPath setaCompleta = new GraphicsPath();
                setaCompleta.AddLine(pontaDaSeta, verticeDireita);
                setaCompleta.AddLine(verticeDireita, verticeEsquerda);
                setaCompleta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(new SolidBrush(ln.ForeColor), setaCompleta);
            }

            //Seta na lateral esquerda
            if (ln.YdeX(Caixa.Left) >= Caixa.Top && ln.YdeX(Caixa.Left) <= Caixa.Bottom && OutraCaixa.PontoCentral.X < Caixa.PontoCentral.X)
            {
                Point pontaDaSeta = new Point(Caixa.Left, ln.YdeX(Caixa.Left));
                Point verticeDireita = new Point(pontaDaSeta.X - 14, pontaDaSeta.Y + 4);
                Point verticeEsquerda = new Point(pontaDaSeta.X - 14, pontaDaSeta.Y - 4);
                GraphicsPath setaCompleta = new GraphicsPath();
                setaCompleta.AddLine(pontaDaSeta, verticeDireita);
                setaCompleta.AddLine(verticeDireita, verticeEsquerda);
                setaCompleta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(new SolidBrush(ln.ForeColor), setaCompleta);
            }

            //Seta na lateral direita
            if (ln.YdeX(Caixa.Left) >= Caixa.Top && ln.YdeX(Caixa.Left) <= Caixa.Bottom && OutraCaixa.PontoCentral.X > Caixa.PontoCentral.X)
            {
                Point pontaDaSeta = new Point(Caixa.Right, ln.YdeX(Caixa.Right));
                Point verticeDireita = new Point(pontaDaSeta.X + 14, pontaDaSeta.Y + 4);
                Point verticeEsquerda = new Point(pontaDaSeta.X + 14, pontaDaSeta.Y - 4);
                GraphicsPath setaCompleta = new GraphicsPath();
                setaCompleta.AddLine(pontaDaSeta, verticeDireita);
                setaCompleta.AddLine(verticeDireita, verticeEsquerda);
                setaCompleta.AddLine(verticeEsquerda, pontaDaSeta);
                g.FillPath(new SolidBrush(ln.ForeColor), setaCompleta);
            }
        }
    }
}
