using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CompartimentalModel
{
	/// <summary>
	/// Summary description for Linhas.
	/// </summary>
    public class Linhas
	{
        public static readonly System.Drawing.Color CorPadrao = Color.DarkRed;

		#region Campos
		private bool _EstaSelecionado;
		private Caixas _CaixaInicio;
		private Caixas _CaixaFim;
		private Linhas.Direcao _Direcao;
		private float _ValorAB;
		private float _ValorBA;
        private Color backColor;
        private Color foreColor;
        private Font font;
        #endregion

		#region Construtor
        public Linhas() : this(null, null, Linhas.CorPadrao, Direcao.InicioParaFim, 0F, 0F)
        {
        }
		public Linhas(Caixas CaixaInicio, Caixas CaixaFim, System.Drawing.Color CorDaLinha, Linhas.Direcao Fluxo, float ValorAB, float ValorBA)
		{
            this.BackColor = Color.Transparent;
            _EstaSelecionado = false;
            this.ForeColor = CorDaLinha;
            this.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Italic);
            this.CaixaInicio = CaixaInicio;
            this.CaixaFim = CaixaFim;
            _Direcao = Fluxo;
            switch (_Direcao)
            {
                case Direcao.InicioParaFim:
                    _ValorAB = ValorAB;
                    _ValorBA = 0;
                    break;
                case Direcao.FimParaInicio:
                    _ValorAB = 0;
                    _ValorBA = ValorBA;
                    break;
                case Direcao.Ambos:
                    _ValorAB = ValorAB;
                    _ValorBA = ValorBA;
                    break;
            }
        }
		#endregion

		#region Métodos públicos
		public bool PontoNessaLinha(System.Drawing.Point pto)
		{
			float m = this.CoeficienteAngular;
			int DeltaX = pto.X - this.PontoInicio.X;
			int yf = pto.Y;
			int yi = this.PontoInicio.Y;

			int MenorX = PontoInicio.X < PontoFim.X ? PontoInicio.X : PontoFim.X;
			int MaiorX = PontoInicio.X > PontoFim.X ? PontoInicio.X : PontoFim.X;
			int MenorY = PontoInicio.Y < PontoFim.Y ? PontoInicio.Y : PontoFim.Y;
			int MaiorY = PontoInicio.Y > PontoFim.Y ? PontoInicio.Y : PontoFim.Y;

			bool Retorno = (m * DeltaX) + yi + 5 > yf;
			Retorno &=     (m * DeltaX) + yi - 5 < yf;
			Retorno &=     pto.X >= MenorX;
			Retorno &=     pto.X <= MaiorX;
			Retorno &=     pto.Y >= MenorY;
			Retorno &=     pto.Y <= MaiorY;

			return Retorno;
		}
        public int XdeY(int Y)
        {
            if (this.CoeficienteAngular == 0)
                return 0;

            int Retorno = (int)((Y - this.PontoInicio.Y) / this.CoeficienteAngular) + PontoInicio.X;
            return Retorno;
        }
        public int YdeX(int X)
        {
            if (this.CoeficienteAngular == float.PositiveInfinity || this.CoeficienteAngular == float.NegativeInfinity)
                return 0;

            int Retorno = (int)((X - this.PontoInicio.X) * this.CoeficienteAngular) + PontoInicio.Y;
            return Retorno;
        }

		public static string SugerirNome(Linhas.Direcao fluxo, string cx1, string cx2, float vl1, float vl2)
		{
            string Valor1 = vl1.ToString("0.00e+00");
            string Valor2 = vl2.ToString("0.00e+00");

			switch (fluxo)
			{
				case Direcao.InicioParaFim:
					return "K" + cx1 + "," + cx2 + " = " + Valor1;
				case Direcao.FimParaInicio:
					return "K" + cx2 + "," + cx1 + " = " + Valor2;
				case Direcao.Ambos:
					return "K" + cx1 + "," + cx2 + " = " + Valor1 + "  &&  " + "K" + cx2 + "," + cx1 + " = " + Valor2;
			}
			return "";
		}
		public static string SugerirNomeAB(Linhas.Direcao fluxo, string cx1, string cx2, float vl1, float vl2)
		{
            string Valor1 = vl1.ToString("0.00e+00");
			if (fluxo == Linhas.Direcao.InicioParaFim || fluxo == Linhas.Direcao.Ambos)
				return "K" + cx1 + "," + cx2 + " = " + Valor1;
			return "";
		}
		public static string SugerirNomeBA(Linhas.Direcao fluxo, string cx1, string cx2, float vl1, float vl2)
		{
            string Valor2 = vl2.ToString("0.00e+00");
			if (fluxo == Linhas.Direcao.InicioParaFim || fluxo == Linhas.Direcao.Ambos)
                return "K" + cx2 + "," + cx1 + " = " + Valor2;
			return "";
		}
		#endregion

		#region Enums
		public enum Direcao : byte
		{
			InicioParaFim = 1,
			FimParaInicio = 2,
			Ambos = 3
		}
		#endregion

		#region Acesso aos Campos
		public System.Drawing.Point PontoInicio
		{
			get
			{
				return CaixaInicio == null ? new Point(0,0) : new Point(CaixaInicio.PontoCentral.X, CaixaInicio.PontoCentral.Y);
			}
		}
        public System.Drawing.Point PontoFim
		{
			get
			{
                return CaixaFim == null ? new Point(0, 0) : new Point(CaixaFim.PontoCentral.X, CaixaFim.PontoCentral.Y);
            }
		}
		public Caixas CaixaInicio
		{
			get
			{
				return _CaixaInicio;
			}
            set
            {
                _CaixaInicio = value;
            }
		}
		public Caixas CaixaFim
		{
			get
			{
				return _CaixaFim;
			}
            set
            {
                _CaixaFim = value;
            }
        }
		public System.Drawing.Point PontoCentral
		{
			get { return new Point((this.PontoFim.X + this.PontoInicio.X) / 2, (this.PontoFim.Y + this.PontoInicio.Y) / 2); }
		}
		public bool EstaSelecionado
		{
			get
			{
				return this._EstaSelecionado;
			}
			set
			{
				this._EstaSelecionado = value;
			}
		}
		public float CoeficienteAngular
		{
			get { return (float)(this.PontoFim.Y - this.PontoInicio.Y) / (float)(this.PontoFim.X - this.PontoInicio.X); }
		}
		public string Nome
		{
			get
			{
                if (this.CaixaInicio == null || this.CaixaFim == null)
                    return SugerirNome(this.DirecaoDaLinha, "0", "0", this.ValorAB, this.ValorBA);
                else
				    return SugerirNome(this.DirecaoDaLinha, this.CaixaInicio.Numero.ToString(), this.CaixaFim.Numero.ToString(), this.ValorAB, this.ValorBA);
			}
		}
		public string NomeAB
		{
			get
			{
				return Linhas.SugerirNomeAB(this._Direcao, this._CaixaInicio.Numero.ToString(), this._CaixaFim.Numero.ToString(), this._ValorAB, this._ValorBA);
			}
		}
		public string NomeBA
		{
			get
			{
				return Linhas.SugerirNomeBA(this._Direcao, this._CaixaInicio.Numero.ToString(), this._CaixaFim.Numero.ToString(), this._ValorAB, this._ValorBA);
			}
		}
        public Linhas.Direcao DirecaoDaLinha
        {
            get
            {
                return _Direcao;
            }
            set
            {
                _Direcao = value;
            }
        }
        public float ValorAB
        {
            get
            {
                if (_Direcao == Direcao.FimParaInicio)
                    this._ValorAB = 0;
                return this._ValorAB;
            }
            set
            {
                if (_Direcao == Direcao.FimParaInicio)
                    this._ValorAB = 0;
                else
                    this._ValorAB = value;
            }
        }
        public float ValorBA
        {
            get
            {
                if (_Direcao == Direcao.InicioParaFim)
                    this._ValorBA = 0;
                return this._ValorBA;
            }
            set
            {
                if (_Direcao == Direcao.InicioParaFim)
                    this._ValorBA = 0;
                else
                    this._ValorBA = value;
            }
        }
		public System.Drawing.Point PontoTercoInicio
		{
			get
			{
				int TercoDoCaminhoX = ((PontoFim.X - PontoInicio.X) / 3);
				int TercoDoCaminhoY = ((PontoFim.Y - PontoInicio.Y) / 3);
				return new System.Drawing.Point(PontoInicio.X + TercoDoCaminhoX, PontoInicio.Y + TercoDoCaminhoY);
			}
		}
        public System.Drawing.Point PontoTercoFim
		{
			get
			{
				int TercoDoCaminhoX = ((PontoFim.X - PontoInicio.X) / 3);
				int TercoDoCaminhoY = ((PontoFim.Y - PontoInicio.Y) / 3);
				return new System.Drawing.Point(PontoFim.X - TercoDoCaminhoX, PontoFim.Y - TercoDoCaminhoY);
			}
		}
        public Color BackColor { get { return backColor; } set { backColor = value; } }
        public Color ForeColor { get { return foreColor; } set { foreColor = value; } }
        public Font Font { get { return font; } set { font = value; } }
        #endregion

        /*
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //#region DesenharLinhas
            //e.Graphics.DrawLine(new Pen(this.ForeColor), this.PontoInicio, this.PontoFim);
            //if (this.DirecaoDaLinha != Linhas.Direcao.Ambos)
            //{
            //    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(this.Nome, this.Font);
            //    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (this.PontoCentral.X - tamanho.Width / 2) - 1, (this.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
            //    e.Graphics.DrawString(this.Nome, this.Font, new System.Drawing.SolidBrush(this.ForeColor), this.PontoCentral.X - tamanho.Width / 2, this.PontoCentral.Y - tamanho.Height / 2);
            //    e.Graphics.DrawRectangle(new Pen(this.ForeColor), (this.PontoCentral.X - tamanho.Width / 2) - 1, (this.PontoCentral.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
            //}
            //else
            //{
            //    System.Drawing.SizeF tamanho = e.Graphics.MeasureString(this.NomeAB, this.Font);
            //    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (this.PontoTercoInicio.X - tamanho.Width / 2) - 1, (this.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);
            //    e.Graphics.DrawString(this.NomeAB, this.Font, new System.Drawing.SolidBrush(this.ForeColor), this.PontoTercoInicio.X - tamanho.Width / 2, this.PontoTercoInicio.Y - tamanho.Height / 2);
            //    e.Graphics.DrawRectangle(new Pen(this.ForeColor), (this.PontoTercoInicio.X - tamanho.Width / 2) - 1, (this.PontoTercoInicio.Y - tamanho.Height / 2) - 1, tamanho.Width + 2, tamanho.Height + 2);

            //    System.Drawing.SizeF tamanho2 = e.Graphics.MeasureString(this.NomeBA, this.Font);
            //    e.Graphics.FillRectangle(System.Drawing.Brushes.White, (this.PontoTercoFim.X - tamanho2.Width / 2) - 1, (this.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
            //    e.Graphics.DrawString(this.NomeBA, this.Font, new System.Drawing.SolidBrush(this.ForeColor), this.PontoTercoFim.X - tamanho2.Width / 2, this.PontoTercoFim.Y - tamanho2.Height / 2);
            //    e.Graphics.DrawRectangle(new Pen(this.ForeColor), (this.PontoTercoFim.X - tamanho2.Width / 2) - 1, (this.PontoTercoFim.Y - tamanho2.Height / 2) - 1, tamanho2.Width + 2, tamanho2.Height + 2);
            //}
            //if (this.DirecaoDaLinha == Linhas.Direcao.InicioParaFim || this.DirecaoDaLinha == Linhas.Direcao.Ambos)
            //    DesenharSetaDirecao(this.CaixaFim, this, this.CaixaInicio, e);
            //if (this.DirecaoDaLinha == Linhas.Direcao.FimParaInicio || this.DirecaoDaLinha == Linhas.Direcao.Ambos)
            //    DesenharSetaDirecao(this.CaixaInicio, this, this.CaixaFim, e);
            //#endregion
            //base.OnPaint(e);
        }
        private void DesenharSetaDirecao(Caixas Caixa, Linhas ln, Caixas OutraCaixa, System.Windows.Forms.PaintEventArgs e)
        {

            //Seta na parte superior do compartimento
            if (ln.XdeY(Caixa.Top) >= Caixa.Left && ln.XdeY(Caixa.Top) <= Caixa.Right && OutraCaixa.PontoCentral.Y < Caixa.PontoCentral.Y)
                e.Graphics.FillEllipse(System.Drawing.Brushes.Red, ln.XdeY(Caixa.Top) - 3, Caixa.Top - 3, 6, 6);

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

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
       */
    }
}
