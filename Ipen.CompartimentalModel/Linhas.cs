using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ipen.CompartimentalModel
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

        //Cache de MeasureString para os rótulos desta linha. Duas posições
        //("slots"), porque uma ligação bidirecional ("Ambos") mede dois
        //textos (NomeAB e NomeBA) no mesmo frame - com um único slot, cada
        //chamada invalidaria o cache da outra e nada seria reaproveitado.
        private string _rotuloCacheTexto1, _rotuloCacheChaveFonte1;
        private SizeF _rotuloCacheTamanho1;
        private string _rotuloCacheTexto2, _rotuloCacheChaveFonte2;
        private SizeF _rotuloCacheTamanho2;
        #endregion

		#region Construtor
        public Linhas() : this(null, null, Linhas.CorPadrao, Direcao.InicioParaFim, 0F, 0F)
        {
        }
		public Linhas(Caixas CaixaInicio, Caixas CaixaFim, System.Drawing.Color CorDaLinha, Linhas.Direcao Fluxo, float ValorAB, float ValorBA)
		{
            //Painel.OnPaint desenha a linha com BackColor, não ForeColor.
            //Deixar BackColor = Transparent aqui (como era antes) fazia
            //linhas importadas de XML via Reservatorio.ImportarArquivo
            //nascerem invisíveis, pois esse caminho nunca reatribui BackColor
            //depois de construir a Linhas - diferente do caminho via banco
            //(DataBD.PreencherLinhas), que sempre o faz logo em seguida.
            //Nada no projeto depende de BackColor == Transparent como estado
            //inicial "não definido".
            this.BackColor = CorDaLinha;
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
        /// <summary>
        /// Equivalente a g.MeasureString(texto, this.Font), mas cacheado: só
        /// remede quando o texto ou a fonte mudam. Nome/NomeAB/NomeBA são
        /// derivados de Numero e dos valores, então comparar a string já é
        /// uma chave de invalidação confiável.
        /// </summary>
        public SizeF MedirRotulo(Graphics g, string texto)
        {
            string chaveFonte = this.font.Name + "|" + this.font.Size + "|" + this.font.Style;

            if (texto == _rotuloCacheTexto1 && chaveFonte == _rotuloCacheChaveFonte1)
                return _rotuloCacheTamanho1;
            if (texto == _rotuloCacheTexto2 && chaveFonte == _rotuloCacheChaveFonte2)
                return _rotuloCacheTamanho2;

            SizeF tamanho = g.MeasureString(texto, this.font);

            //O slot 1 (mais recente) vira o slot 2, e o resultado novo ocupa
            //o slot 1 - um LRU de duas posições.
            _rotuloCacheTexto2 = _rotuloCacheTexto1;
            _rotuloCacheChaveFonte2 = _rotuloCacheChaveFonte1;
            _rotuloCacheTamanho2 = _rotuloCacheTamanho1;

            _rotuloCacheTexto1 = texto;
            _rotuloCacheChaveFonte1 = chaveFonte;
            _rotuloCacheTamanho1 = tamanho;

            return tamanho;
        }

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
            return XdeY(Y, this.CoeficienteAngular);
        }
        /// <summary>
        /// Igual a XdeY(int), mas recebe o coeficiente angular já calculado -
        /// para quem (como DesenharSetaDirecao) já o tem em mãos e chama isto
        /// várias vezes por linha por frame.
        /// </summary>
        public int XdeY(int Y, float m)
        {
            if (m == 0)
                return 0;

            int Retorno = (int)((Y - this.PontoInicio.Y) / m) + PontoInicio.X;
            return Retorno;
        }
        public int YdeX(int X)
        {
            return YdeX(X, this.CoeficienteAngular);
        }
        /// <summary>
        /// Igual a YdeX(int), mas recebe o coeficiente angular já calculado.
        /// </summary>
        public int YdeX(int X, float m)
        {
            if (m == float.PositiveInfinity || m == float.NegativeInfinity)
                return 0;

            int Retorno = (int)((X - this.PontoInicio.X) * m) + PontoInicio.Y;
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
				return CaixaInicio == null ? new Point(0,0) : CaixaInicio.PontoCentral;
			}
		}
        public System.Drawing.Point PontoFim
		{
			get
			{
                return CaixaFim == null ? new Point(0, 0) : CaixaFim.PontoCentral;
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
            get {
                return (float)(this.PontoFim.Y - this.PontoInicio.Y) / (float)(this.PontoFim.X - this.PontoInicio.X);
            }
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
     
    }
}
