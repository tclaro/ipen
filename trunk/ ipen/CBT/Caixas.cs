using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace CBT
{
	/// <summary>
	/// Summary description for Caixas.
	/// </summary>
	public class Caixas : System.Windows.Forms.Control
    {
        #region Campos
        private Bitmap _backBuffer;
        private bool _Arrastando;
        private Point _DragPoint;
		private bool _EstaSelecionado;
        private string _Nome;
        private int _Numero;
        private bool _Acompanhar;
        private bool _Eliminacao;
        private bool _Incorporacao;
        private double _Fracao;
        private System.Drawing.Point _PontoCentral;
		#endregion

		#region Construtor
        public Caixas() : this(0, "[Sem Título]", new Point(0,0), CorPadrao, false, false, false, 0) { }
        public Caixas(string Texto, System.Drawing.Color Cor, bool Acompanhar, bool Eliminacao) : this(0, Texto, new Point(0, 0), Cor, Acompanhar, Eliminacao, false, 0) { }
        public Caixas(string Texto, System.Drawing.Color Cor, bool Acompanhar, bool Eliminacao, bool Incorporacao, double Fracao) : this(0, Texto, new Point(0, 0), Cor, Acompanhar, Eliminacao, Incorporacao, Fracao) { }
        public Caixas(int Numero, string Texto, System.Drawing.Point Posicao, System.Drawing.Color Cor) : this(Numero, Texto, new Point(0, 0), Cor, false, false, false, 0) { }
        public Caixas(int Numero, string Texto, System.Drawing.Point Posicao, System.Drawing.Color Cor, bool Acompanhar, bool Eliminacao): this(Numero, Texto, Posicao, Cor, Acompanhar, Eliminacao, false, 0){ }
        public Caixas(int Numero, string Texto, System.Drawing.Point Posicao, System.Drawing.Color Cor, bool Acompanhar, bool Eliminacao, bool Incorporacao, double Fracao)
		{

            _Arrastando = false;
            _DragPoint = new Point();
            _Numero = Numero;
            _Nome = Texto;
            BackColor = Cor;
            _EstaSelecionado = false;
            _Acompanhar = Acompanhar;
            _Eliminacao = Eliminacao;
            _Incorporacao = Incorporacao;
            _Fracao = Fracao;
            this.Location = Posicao;
            this.Font = new Font("Tahoma", 8F, FontStyle.Regular);
            this.Size = this.DefaultSize;
            VerificarTamanho();
            RecalcularValoresPosicao();
        }
        #endregion

        #region Propriedades públicas

        #region Override na base
        [DefaultValue(typeof(Size), "60,50")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; VerificarTamanho(); }
        }
        public new Size DefaultSize
        {
            get { return new Size(60, 50); }
        }
        public new Color ForeColor
        {
            get { return DrawingUtils.ColorFromAhsb(255, this.BackColor.GetHue(), this.BackColor.GetSaturation(), (this.BackColor.GetBrightness() >= 0.65F || this.BackColor.GetBrightness() <= 0.35F) ? 1F - this.BackColor.GetBrightness() : (this.BackColor.GetBrightness() > 0.5F) ? 0F : 1F); }
        }
        public new Color DefaultBackColor
        {
            get { return Caixas.CorPadrao; }
        }
        #endregion

        #region Novas propriedades
        public static readonly Color CorPadrao = Color.SteelBlue;
        public int Numero
        {
            get { return _Numero; }
            set
            {
                _Numero = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }
        public Bitmap BackBuffer
        {
            get { return _backBuffer; }
            set { _backBuffer = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set
            {
                _Nome = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
                VerificarTamanho();
            }
        }
        public bool EstaSelecionado
        {
            get { return this._EstaSelecionado; }
            set
            {
                this._EstaSelecionado = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }
        public bool Acompanhar
        {
            get { return _Acompanhar; }
            set
            {
                _Acompanhar = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }
        public bool Eliminacao
        {
            get { return _Eliminacao; }
            set
            {
                _Eliminacao = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }
        public bool Incorporacao
        {
            get { return _Incorporacao; }
            set
            {
                _Incorporacao = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }
        public double Fracao
        {
            get { return _Fracao; }
            set
            {
                _Fracao = value;
                BoxEventArgs be = new BoxEventArgs();
                be.Box = this;
                be.EventType = BoxEventArgs.BoxEventTypes.PropertyChanged;
                OnPropertyChanged(be);
            }
        }

        public System.Drawing.Point[] PontosExtremos
        {
            get
            {
                Array ptos = Array.CreateInstance(typeof(System.Drawing.Point), 4);
                System.Drawing.Point PtoA = new System.Drawing.Point(this.Left, this.Top);
                System.Drawing.Point PtoB = new System.Drawing.Point(this.Left + this.Width, this.Top);
                System.Drawing.Point PtoC = new System.Drawing.Point(this.Left, this.Top + this.Height);
                System.Drawing.Point PtoD = new System.Drawing.Point(this.Left + this.Width, this.Top + this.Height);
                ptos.SetValue(PtoA, 0);
                ptos.SetValue(PtoB, 1);
                ptos.SetValue(PtoC, 2);
                ptos.SetValue(PtoD, 3);
                return (System.Drawing.Point[])ptos;
            }
        }
        public System.Drawing.Point PontoCentral
        {
            get
            {
                //return new Point(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
                return _PontoCentral;
            }
        }
        #endregion

        #endregion

        #region Novos Eventos
        public class BoxEventArgs : EventArgs
        {
            private Caixas _box;
            private BoxEventTypes _eventType;
            public BoxEventArgs() { }
            public Caixas Box { get { return _box; } set { _box = value; } }
            public BoxEventTypes EventType { get { return _eventType; } set { _eventType = value; } }
            public enum BoxEventTypes
            {
                Deleted, Moved, PropertyChanged
            }
        }
        public delegate void CaixaEventHandler(BoxEventArgs be);
        public event CaixaEventHandler Moved;
        public event CaixaEventHandler Deleted;
        public event CaixaEventHandler PropertyChanged;
        protected void OnMoved(BoxEventArgs be)
        {
            if (Moved != null)
                Moved(be);
        }
        protected void OnDeleted(BoxEventArgs be)
        {
            if (Deleted != null)
                Deleted(be);
        }
        protected void OnPropertyChanged(BoxEventArgs be)
        {
            DestruirBuffer();
            if (PropertyChanged != null)
                PropertyChanged(be);
        }
        #endregion

        #region Override de métodos de eventos
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _Arrastando = true;
                _DragPoint.X = e.X;
                _DragPoint.Y = e.Y;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _Arrastando = false;
            _DragPoint.X = 0;
            _DragPoint.Y = 0;

            if (this.Left < 0)
                this.Left = 0;
            if (this.Top < 0)
                this.Top = 0;
            BoxEventArgs be = new BoxEventArgs();
            be.Box = this;
            be.EventType = BoxEventArgs.BoxEventTypes.Moved;
            OnMoved(be);
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_Arrastando)
            {
                this.Left = this.Left + e.X - _DragPoint.X;
                this.Top = this.Top + e.Y - _DragPoint.Y;
            }
            base.OnMouseMove(e);

        }
        protected override void OnResize(EventArgs e)
        {
            DestruirBuffer();
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            RecalcularValoresPosicao();
            Desenhar();
            e.Graphics.DrawImage(BackBuffer, 0, 0);
            
        }
        private void Desenhar()
        {
            if (BackBuffer == null)
            {
                #region Desenhar
                BackBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                Graphics g = Graphics.FromImage(BackBuffer);
                g.Clear((this.Parent == null) ? Color.Transparent : this.Parent.BackColor);

                #region Cria e configura o gráfico e as variáveis
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                Color gradientTop = DrawingUtils.ColorFromAhsb(255, this.BackColor.GetHue(), this.BackColor.GetSaturation() * 0.9F, this.BackColor.GetBrightness() * 0.5F);
                Color gradientBottom = DrawingUtils.ColorFromAhsb(255, this.BackColor.GetHue(), this.BackColor.GetSaturation() * 1.2F, this.BackColor.GetBrightness() * 1.2F);
                #endregion

                #region Retângulo arredondado externo em Gradiente
                Rectangle outerRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                using (GraphicsPath outerPath = DrawingUtils.RoundedRectangle(outerRect, 5, 0))
                {
                    using (LinearGradientBrush outerBrush = new LinearGradientBrush(outerRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
                    {
                        g.FillPath(outerBrush, outerPath);
                    }
                    using (Pen outlinePen = new Pen(gradientTop))
                    {
                        g.DrawPath(outlinePen, outerPath);
                    }
                }
                #endregion

                #region Luz superior em Gradiente
                // Paint the highlight rounded rectangle
                Rectangle innerRect = new Rectangle(0, -1, this.Width - 1, this.Height / 2 - 2);
                using (GraphicsPath innerPath = DrawingUtils.RoundedRectangle(innerRect, 5, 2))
                {
                    using (LinearGradientBrush innerBrush = new LinearGradientBrush(innerRect, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), LinearGradientMode.Vertical))
                    {
                        g.FillPath(innerBrush, innerPath);
                    }
                }
                #endregion

                #region Texto
                SizeF tamanho = g.MeasureString(this.Nome, this.Font);
                g.DrawString(this.Numero.ToString(), this.Font, Brushes.White, 2, 2);
                g.DrawString(this.Numero.ToString(), this.Font, Brushes.Black, 1, 1);
                g.DrawString(this.Nome, this.Font, Brushes.Gray, this.Width / 2 - tamanho.Width / 2 + 1, this.Height / 2 - tamanho.Height / 2 + 1);
                g.DrawString(this.Nome, this.Font, new SolidBrush(this.ForeColor), this.Width / 2 - tamanho.Width / 2, this.Height / 2 - tamanho.Height / 2);
                #endregion

                if (EstaSelecionado)
                {
                    using (LinearGradientBrush br = new LinearGradientBrush(new Point(1, 0), new Point(this.ClientSize.Width / 2, 0), Color.FromArgb(30, Color.White), Color.FromArgb(90, Color.White)))
                    {
                        g.FillRectangle(br, 0, 0, this.ClientSize.Width / 2, this.ClientSize.Height);
                    }
                    using (LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(this.ClientSize.Width / 2, 0), Color.FromArgb(90, Color.White), Color.FromArgb(30, Color.White)))
                    {
                        g.FillRectangle(br, this.ClientSize.Width / 2, 0, this.ClientSize.Width / 2, this.ClientSize.Height);
                    }
                }
                g.Dispose();
                #endregion
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Don't allow the background to paint
            //base.OnPaintBackground(pevent);
        }
        #endregion

        #region Métodos públicos
        public bool PontoNessaCaixa(System.Drawing.Point pto)
        {
            if (pto.X >= this.Left && pto.X <= (this.Left + this.Width) &&
                pto.Y >= this.Top && pto.Y <= (this.Top + this.Height))
                return true;
            else
                return false;
        }
        public void Delete()
        {
            BoxEventArgs be = new BoxEventArgs();
            be.Box = this;
            be.EventType = BoxEventArgs.BoxEventTypes.Deleted;
            OnDeleted(be);
            this.Dispose();
        }
        #endregion

        #region Métodos internos
        private void VerificarTamanho()
        {
            if (this.Width < System.Windows.Forms.TextRenderer.MeasureText(this._Nome, this.Font).Width + 10)
                this.Width = System.Windows.Forms.TextRenderer.MeasureText(this._Nome, this.Font).Width + 10;
        }

        private void RecalcularValoresPosicao()
        {
            _PontoCentral = new Point(this.Left + (this.Width / 2), this.Top + (this.Height / 2));
        }

        private void DestruirBuffer()
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
                BackBuffer = null;
            }
            this.Invalidate();
        }
        #endregion
    }
}
