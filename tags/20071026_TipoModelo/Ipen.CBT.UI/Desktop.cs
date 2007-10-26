using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Ipen.CompartimentalModel;

namespace Ipen.CBT.UI
{
	/// <summary>
	/// Summary description for Desktop.
	/// </summary>
	public class Desktop : System.Windows.Forms.PictureBox
	{
		#region Campos
		public CaixasCollection TodasAsCaixas = new CaixasCollection();
		public LinhasCollection TodasAsLinhas = new LinhasCollection();
		private Caixas CaixaArrastada;
		private System.Drawing.Point PontoReferencia;
		private bool EstaSolicitandoCaixa;
		private Caixas CaixaSolicitada1;
		private Caixas CaixaSolicitada2;
		#endregion

		#region Construtor
		public Desktop()
		{
			//
			// TODO: Add constructor logic here
			//
			EstaSolicitandoCaixa = false;
			CaixaArrastada = null;
			PontoReferencia = new System.Drawing.Point(0, 0);
			CaixaSolicitada1 = null;
			CaixaSolicitada2 = null;
		}

		#endregion

        #region Métodos públicos
        public Caixas ObterCaixaPorClique(System.Drawing.Point pto)
        {
            foreach (Caixas cx in TodasAsCaixas)
            {
                if (cx.PontoNessaCaixa(pto))
                    return cx;
            }

            return null;
        }
        public Caixas ObterCaixaPorClique(System.Drawing.Point[] ptos, Caixas CaixaNaoVerificada)
        {
            foreach (Caixas cx in TodasAsCaixas)
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
        public Linhas ObterLinhaPorClique(System.Drawing.Point pto)
        {
            foreach (Linhas lna in TodasAsLinhas)
            {
                if (lna.PontoNessaLinha(pto))
                    return lna;
            }

            return null;
        }
        public void AdicionarCaixa(string Titulo, System.Drawing.Color corCaixa, bool Acompanhar, bool Eliminacao)
        {
            DesmarcarTodos();
            //Caixas cx = new Caixas(Numero, Titulo, new System.Drawing.Point(4, 5), corCaixa);
            Caixas cx = new Caixas(TodasAsCaixas.Count + 1, Titulo, new System.Drawing.Point(4, 5), corCaixa, Acompanhar, Eliminacao);
            VerificarCaixaForaDaTela(cx);
            VerificarCaixasSobrepostas(cx);
            TodasAsCaixas.Add(cx);
            this.Refresh();
        }
        public void AdicionarLinha(Caixas cx1, Caixas cx2, System.Drawing.Color corLinha, Linhas.Direcao Fluxo, float ValorAB, float ValorBA)
        {
            DesmarcarTodos();
            Linhas ln = new Linhas(cx1, cx2, corLinha, Fluxo, ValorAB, ValorBA);
            TodasAsLinhas.Add(ln);
            this.Refresh();
        }
        public void DesmarcarTodos()
        {
            foreach (Linhas lna in TodasAsLinhas)
                lna.EstaSelecionado = false;
            if (!EstaSolicitandoCaixa)
            {
                foreach (Caixas cxa in TodasAsCaixas)
                    cxa.EstaSelecionado = false;
            }
            this.Refresh();
        }
        public void EditarCaixa(Caixas cx)
        {
            CaixaProp novacaixaForm = new CaixaProp(cx);
            System.Windows.Forms.DialogResult dlgResultado = novacaixaForm.ShowDialog();
            if (dlgResultado == System.Windows.Forms.DialogResult.Cancel)
            {
                this.SolicitarCaixa(false);
                this.Refresh();
                ((frmGrafico)this.Parent).ReverMenus();
                return;
            }
            int Numero = novacaixaForm.NumeroDaCaixa;
            string Titulo = novacaixaForm.NomeDaCaixa;
            System.Drawing.Color corDaCaixa = novacaixaForm.CorDaCaixa;
            bool Acompanhar = novacaixaForm.Acompanhar;
            bool Eliminacao = novacaixaForm.Eliminacao;
            novacaixaForm.Dispose();
            novacaixaForm = null;
            cx.BackColor = corDaCaixa;
            cx.Numero = Numero;
            cx.Nome = Titulo;
            cx.Acompanhar = Acompanhar;
            cx.Eliminacao = Eliminacao;
            foreach (Linhas ln in TodasAsLinhas)
            {
             //   if (ln.CaixaInicio == cx || ln.CaixaFim == cx)
                    //ln.Nome = Linhas.SugerirNome(ln.DirecaoDaLinha, ln.CaixaInicio.Numero.ToString(), ln.CaixaFim.Numero.ToString(), ln.ValorAB, ln.ValorBA);
            }
            SolicitarCaixa(false);
            this.Refresh();
            ((frmGrafico)this.Parent).ReverMenus();
        }
        public void EditarLinha(Linhas ln)
        {
            LinhaProp novalinhaForm = new LinhaProp(ln);
            System.Windows.Forms.DialogResult dlgResultado = novalinhaForm.ShowDialog();
            if (dlgResultado == System.Windows.Forms.DialogResult.Cancel)
            {
                this.SolicitarCaixa(false);
                this.Refresh();
                ((frmGrafico)this.Parent).ReverMenus();
                return;
            }
            //string Titulo = novalinhaForm.NomeDaLinha;
            System.Drawing.Color corDaLinha = novalinhaForm.CorDaLinha;
            Linhas.Direcao fluxo = novalinhaForm.Direcao;
            float valorAB = novalinhaForm.ValorAB;
            float valorBA = novalinhaForm.ValorBA;
            novalinhaForm.Dispose();
            novalinhaForm = null;
            ln.BackColor = corDaLinha;
            //ln.Nome = Titulo;
            ln.DirecaoDaLinha = fluxo;
            ln.ValorAB = valorAB;
            ln.ValorBA = valorBA;
            SolicitarCaixa(false);
            this.Refresh();
            ((frmGrafico)this.Parent).ReverMenus();
        }
        public void ExcluirSelecao()
        {
            object Selecao = this.ObjetoSelecionado;
            if (Selecao is Caixas)
            {
                foreach (Caixas cx in TodasAsCaixas)
                {
                    if (Selecao == cx)
                    {
                        LinhasCollection TodasAsLinhasBkp = new LinhasCollection();
                        TodasAsLinhasBkp.AddRange(TodasAsLinhas);
                        foreach (Linhas ln in TodasAsLinhasBkp)
                        {
                            if (cx == ln.CaixaInicio || cx == ln.CaixaFim)
                            {
                                if (TodasAsLinhas.Contains(ln))
                                    TodasAsLinhas.Remove(ln);
                            }
                        }
                        TodasAsLinhasBkp.Clear();
                        TodasAsLinhasBkp = null;
                        TodasAsCaixas.Remove(cx);
                        this.Refresh();
                        return;
                    }
                }
            }
            if (Selecao is Linhas)
            {
                foreach (Linhas ln in TodasAsLinhas)
                {
                    if (Selecao == ln)
                    {
                        TodasAsLinhas.Remove(ln);
                        this.Refresh();
                        return;
                    }
                }
            }
        }
        public void SolicitarCaixa(bool Solicitar)
        {
            DesmarcarTodos();
            if (!Solicitar)
            {
                EstaSolicitandoCaixa = false;
                this.Cursor = System.Windows.Forms.Cursors.Default;
                CaixaSolicitada1 = null;
                CaixaSolicitada2 = null;
            }
            else
            {
                EstaSolicitandoCaixa = true;
                this.Cursor = System.Windows.Forms.Cursors.Cross;
            }
            DesmarcarTodos();
            this.Refresh();
        }
        public void RealinharConexoes()
        {
            foreach (Linhas ln in TodasAsLinhas)
            {
                //ln.Realinhar();
            }
        }
        public void VerificarCaixaForaDaTela(Caixas cx)
        {
            if (cx.Top < 0)
                cx.Top = 0;
            if (cx.Left < 0)
                cx.Left = 0;
            if (cx.Left + cx.Width > this.Width)
            {
                cx.Top += cx.Height;
                cx.Left = 0;
            }
            this.RealinharConexoes();
            this.Refresh();
        }
        public void VerificarCaixasSobrepostas()
        {
            foreach (Caixas cx in TodasAsCaixas)
            {
                while (ObterCaixaPorClique(cx.PontosExtremos, cx) != null)
                {
                    cx.Left += 5;
                    this.RealinharConexoes();
                    //Refresh no Loop: Mais animação
                    //this.Refresh();
                }
                // Refresh no final: Mais velocidade
                this.Refresh();
            }
        }

        public void LimparTudo()
        {
            this.TodasAsCaixas.Clear();
            this.TodasAsLinhas.Clear();
        }

        public void VerificarCaixasSobrepostas(Caixas cx)
        {
            while (ObterCaixaPorClique(cx.PontosExtremos, cx) != null)
            {
                cx.Left += 5;
                this.RealinharConexoes();
                //Refresh no Loop: Mais animação
                this.Refresh();
            }
            // Refresh no final: Mais velocidade
            //this.Refresh();
        }
        #endregion

        #region Métodos de Eventos
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            DesmarcarTodos();

            if (!EstaSolicitandoCaixa)
            {
                System.Drawing.Point pto = new System.Drawing.Point(e.X, e.Y);
                Caixas cxa = this.ObterCaixaPorClique(pto);
                if (cxa != null)
                {
                    cxa.EstaSelecionado = true;
                    this.Refresh();
                    CaixaArrastada = cxa;
                    PontoReferencia.X = e.X - cxa.Left;
                    PontoReferencia.Y = e.Y - cxa.Top;
                }
                else
                {
                    Linhas linha = this.ObterLinhaPorClique(pto);
                    if (linha != null)
                    {
                        linha.EstaSelecionado = true;
                        this.Refresh();
                    }
                }
            }
            else
            {
                System.Drawing.Point pto = new System.Drawing.Point(e.X, e.Y);
                Caixas cxa = this.ObterCaixaPorClique(pto);
                if (cxa != null)
                {
                    if (this.CaixaSolicitada1 == null)
                    {
                        this.CaixaSolicitada1 = cxa;
                        this.CaixaSolicitada1.EstaSelecionado = true;
                        this.Refresh();
                    }
                    else
                    {
                        if (this.CaixaSolicitada1 != cxa && this.CaixaSolicitada2 == null)
                            this.CaixaSolicitada2 = cxa;
                    }
                }

                if (this.CaixaSolicitada1 != null && this.CaixaSolicitada2 != null && this.CaixaSolicitada1 != this.CaixaSolicitada2)
                {
                    foreach (Linhas alllines in TodasAsLinhas)
                    {
                        if ((alllines.CaixaInicio == CaixaSolicitada1 && alllines.CaixaFim == CaixaSolicitada2) || (alllines.CaixaInicio == CaixaSolicitada2 && alllines.CaixaFim == CaixaSolicitada1))
                        {
                            this.SolicitarCaixa(false);
                            this.Refresh();
                            ((frmGrafico)this.Parent).ReverMenus();
                            return;
                        }
                    }

                    LinhaProp novalinhaForm = new LinhaProp(/*"K" + this.CaixaSolicitada1.Nome + this.CaixaSolicitada2.Nome + " = 0", */System.Drawing.Color.Maroon, this.CaixaSolicitada1, this.CaixaSolicitada2);
                    System.Windows.Forms.DialogResult dlgResultado = novalinhaForm.ShowDialog();
                    if (dlgResultado == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.SolicitarCaixa(false);
                        this.Refresh();
                        ((frmGrafico)this.Parent).ReverMenus();
                        return;
                    }
                    string Titulo = novalinhaForm.NomeDaLinha;
                    System.Drawing.Color corDaLinha = novalinhaForm.CorDaLinha;
                    Linhas.Direcao fluxo = novalinhaForm.Direcao;
                    float valorAB = novalinhaForm.ValorAB;
                    float valorBA = novalinhaForm.ValorBA;
                    novalinhaForm.Dispose();
                    novalinhaForm = null;
                    this.AdicionarLinha(CaixaSolicitada1, CaixaSolicitada2, corDaLinha, fluxo, valorAB, valorBA);
                    this.CaixaSolicitada1.EstaSelecionado = false;
                    this.CaixaSolicitada2.EstaSelecionado = false;
                    this.CaixaSolicitada1 = null;
                    this.CaixaSolicitada2 = null;
                    SolicitarCaixa(false);
                    this.Refresh();
                    ((frmGrafico)this.Parent).ReverMenus();
                }
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (!EstaSolicitandoCaixa)
            {
                if (CaixaArrastada == null)
                {
                    base.OnMouseUp(e);
                    return;
                }

                CaixaArrastada.Left = e.X - PontoReferencia.X;
                CaixaArrastada.Top = e.Y - PontoReferencia.Y;
                RealinharConexoes();
                this.Refresh();
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (!EstaSolicitandoCaixa)
            {
                if (CaixaArrastada == null)
                {
                    base.OnMouseUp(e);
                    return;
                }
                VerificarCaixaForaDaTela(CaixaArrastada);
                VerificarCaixasSobrepostas(CaixaArrastada);
                CaixaArrastada = null;
                PontoReferencia.X = 0;
                PontoReferencia.Y = 0;
            }
            base.OnMouseUp(e);
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            foreach (Caixas cx in TodasAsCaixas)
            {
                if (cx.EstaSelecionado)
                    EditarCaixa(cx);
            }
            foreach (Linhas ln in TodasAsLinhas)
            {
                if (ln.EstaSelecionado)
                    EditarLinha(ln);
            }
            base.OnDoubleClick(e);
        }
        #endregion

        #region DesenharObjetos
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            #region Fontes
            System.Drawing.Font letra = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Italic);
            System.Drawing.Font letrabold = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Regular);
            #endregion

            #region DesenharLinhas
            foreach (Linhas ln in TodasAsLinhas)
            {
                //e.Graphics.DrawLine(new Pen(ln.BackColor), ln.PontoInicio, ln.PontoFim);
                if (ln.DirecaoDaLinha != Linhas.Direcao.Ambos)
                {
                    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(ln.Nome, letra);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                    e.Graphics.DrawString(ln.Nome, letra, new System.Drawing.SolidBrush(ln.BackColor), ln.PontoCentral.X - tamanho.Width / 2, ln.PontoCentral.Y - tamanho.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.BackColor), (ln.PontoCentral.X - tamanho.Width / 2) - 1, (ln.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                }
                else
                {
                    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(ln.NomeAB, letra);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
                    e.Graphics.DrawString(ln.NomeAB, letra, new System.Drawing.SolidBrush(ln.BackColor), ln.PontoTercoInicio.X - tamanho.Width / 2, ln.PontoTercoInicio.Y - tamanho.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.BackColor), (ln.PontoTercoInicio.X - tamanho.Width / 2) - 1, (ln.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);

                    System.Drawing.SizeF tamanho2 = e.Graphics.MeasureString(ln.NomeBA, letra);
                    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                    e.Graphics.DrawString(ln.NomeBA, letra, new System.Drawing.SolidBrush(ln.BackColor), ln.PontoTercoFim.X - tamanho2.Width / 2, ln.PontoTercoFim.Y - tamanho2.Height / 2);
                    e.Graphics.DrawRectangle(new Pen(ln.BackColor), (ln.PontoTercoFim.X - tamanho2.Width / 2) - 1, (ln.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
                }
                if (ln.DirecaoDaLinha == Linhas.Direcao.InicioParaFim || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                    DesenharSetaDirecao(ln.CaixaFim, ln, ln.CaixaInicio, e);
                if (ln.DirecaoDaLinha == Linhas.Direcao.FimParaInicio || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                    DesenharSetaDirecao(ln.CaixaInicio, ln, ln.CaixaFim, e);
            }
            #endregion
            #region DesenharCaixas
            foreach (Caixas cx in TodasAsCaixas)
            {
                Color corBase = cx.BackColor;
                Color gradientTop = DrawingUtils.ColorFromAhsb(255, corBase.GetHue(), corBase.GetSaturation() * 0.9F, corBase.GetBrightness() * 0.5F);
                Color gradientBottom = DrawingUtils.ColorFromAhsb(255, corBase.GetHue(), corBase.GetSaturation() * 1.2F, corBase.GetBrightness() * 1.2F);
                Color corFonte = Color.Transparent;
                corFonte = DrawingUtils.ColorFromAhsb(255, corBase.GetHue(), corBase.GetSaturation(), (corBase.GetBrightness() >= 0.65F || corBase.GetBrightness() <= 0.35F) ? 1F - corBase.GetBrightness() : (corBase.GetBrightness() > 0.5F) ? 0F : 1F);
                System.Drawing.SizeF tamanho = e.Graphics.MeasureString(cx.Nome, letrabold);
                if (tamanho.Width + 10 > cx.Width)
                    cx.Width = (int) tamanho.Width + 10;

                #region Retângulo arredondado externo em Gradiente
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle outerRect = new Rectangle(cx.Left, cx.Top, cx.Width - 1, cx.Height - 1);
                using (GraphicsPath outerPath = DrawingUtils.RoundedRectangle(outerRect, 5, 0))
                {
                    using (LinearGradientBrush outerBrush = new LinearGradientBrush(outerRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillPath(outerBrush, outerPath);
                    }
                    if (cx.EstaSelecionado)
                    {
                        using (Pen outlinePen = new Pen(gradientTop, 3F))
                        {
                            e.Graphics.DrawPath(outlinePen, outerPath);
                        }
                    }
                    else
                    {
                        using (Pen outlinePen = new Pen(gradientTop))
                        {
                            e.Graphics.DrawPath(outlinePen, outerPath);
                        }
                    }
                }
                #endregion

                #region Luz superior em Gradiente
                // Paint the highlight rounded rectangle
                Rectangle innerRect = new Rectangle(cx.Left, cx.Top - 1, cx.Width - 1, cx.Height / 2 - 2);
                using (GraphicsPath innerPath = DrawingUtils.RoundedRectangle(innerRect, 5, 2))
                {
                    using (LinearGradientBrush innerBrush = new LinearGradientBrush(innerRect, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillPath(innerBrush, innerPath);
                    }
                }
                #endregion

                #region Texto
                e.Graphics.DrawString(cx.Numero.ToString(), letrabold, Brushes.White, cx.Left + 2, cx.Top + 2);
                e.Graphics.DrawString(cx.Numero.ToString(), letrabold, Brushes.Black, cx.Left + 1, cx.Top + 1);
                e.Graphics.DrawString(cx.Nome, letrabold, Brushes.Gray, (cx.PontoCentral.X - (tamanho.Width / 2)) + 1, (cx.PontoCentral.Y - (tamanho.Height / 2)) + 1);
                e.Graphics.DrawString(cx.Nome, letrabold, new SolidBrush(corFonte), cx.PontoCentral.X - (tamanho.Width / 2), cx.PontoCentral.Y - (tamanho.Height / 2));
                #endregion
            }
            #endregion

            base.OnPaint(e);
        }
        #endregion

        private void DesenharSetaDirecao(Caixas Caixa, Linhas ln, Caixas OutraCaixa, System.Windows.Forms.PaintEventArgs e)
        {

            //Seta na parte superior do compartimento
            if (ln.XdeY(Caixa.Top) >= Caixa.Left && ln.XdeY(Caixa.Top) <= Caixa.Right && OutraCaixa.PontoCentral.Y < Caixa.PontoCentral.Y)
            {
                e.Graphics.FillEllipse(System.Drawing.Brushes.Red, ln.XdeY(Caixa.Top) - 3, Caixa.Top - 3, 6, 6);
            }
            //Seta na parte inferior 
            if (ln.XdeY(Caixa.Top) >= Caixa.Left && ln.XdeY(Caixa.Top) <= Caixa.Right && OutraCaixa.PontoCentral.Y > Caixa.PontoCentral.Y)
                e.Graphics.FillEllipse(System.Drawing.Brushes.Red, ln.XdeY(Caixa.Bottom) - 3, Caixa.Bottom - 3, 6, 6);

            //Seta na lateral esquerda
            if (ln.YdeX(Caixa.Left) >= Caixa.Top && ln.YdeX(Caixa.Left) <= Caixa.Bottom && OutraCaixa.PontoCentral.X < Caixa.PontoCentral.X)
                e.Graphics.FillEllipse(System.Drawing.Brushes.Red, Caixa.Left - 3, ln.YdeX(Caixa.Left) - 3, 6, 6);

            //Seta na lateral direita
            if (ln.YdeX(Caixa.Left) >= Caixa.Top && ln.YdeX(Caixa.Left) <= Caixa.Bottom && OutraCaixa.PontoCentral.X > Caixa.PontoCentral.X)
                e.Graphics.FillEllipse(System.Drawing.Brushes.Red, Caixa.Right - 3, ln.YdeX(Caixa.Right) - 3, 6, 6);
        }

        #region Acesso aos Campos
        public bool BoxRequest
        {
            get
            {
                return this.EstaSolicitandoCaixa;
            }
        }
        public Caixas CaixaSelecionada
        {
            get
            {
                foreach (Caixas cx in TodasAsCaixas)
                {
                    if (cx.EstaSelecionado)
                        return cx;
                }
                return null;
            }
        }
        public Linhas LinhaSelecionada
        {
            get
            {
                foreach (Linhas ln in TodasAsLinhas)
                {
                    if (ln.EstaSelecionado)
                        return ln;
                }
                return null;
            }
        }

        public object ObjetoSelecionado
        {
            get
            {
                foreach (Caixas cx in TodasAsCaixas)
                {
                    if (cx.EstaSelecionado)
                        return cx;
                }
                foreach (Linhas ln in TodasAsLinhas)
                {
                    if (ln.EstaSelecionado)
                        return ln;
                }
                return null;
            }
        }
        #endregion
    }
}