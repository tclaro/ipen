using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ipen.CompartimentalModel;

namespace Ipen.CBT.UI
{
	/// <summary>
	/// Summary description for LinhaProp.
	/// </summary>
	public class LinhaProp : System.Windows.Forms.Form
	{
		#region Campos
        private Linhas _linhaPropriedades;
        private System.Windows.Forms.TextBox txtNome;
		private System.Windows.Forms.Label lblNome;
		private System.Windows.Forms.ColorDialog dlgCor;
		private System.Windows.Forms.Label lblCor;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnCor;
        private Label lblDirecao;
        private Label lblCaixaInicio;
        private Label lblCaixaFim;
        private Button btnDirecao;
        private Label lblValor1;
        private TextBox txtValorAB;
        private Label lblValor2;
        private TextBox txtValorBA;
		private System.ComponentModel.Container components = null;
		#endregion

		#region Construtor
		public LinhaProp(System.Drawing.Color CorDaLinha, Caixas cxInicio, Caixas cxFim)
		{
			InitializeComponent();
            if (cxInicio != null && cxFim != null)
                _linhaPropriedades = new Linhas(cxInicio, cxFim, CorDaLinha, Linhas.Direcao.InicioParaFim, 0, 0);
            ConstrutorAuxiliar();
        }
		public LinhaProp(Linhas ln)
		{
			InitializeComponent();
            this._linhaPropriedades = ln;
            ConstrutorAuxiliar();
        }
        private void ConstrutorAuxiliar()
        {
            if (_linhaPropriedades == null)
                _linhaPropriedades = new Linhas(null, null, Linhas.CorPadrao, Linhas.Direcao.InicioParaFim, 0, 0);

            PreencherRegistro();
        }
        private void PreencherRegistro()
        {
            this.txtNome.Text = _linhaPropriedades.Nome;
            this.dlgCor.Color = _linhaPropriedades.ForeColor;
            this.btnCor.BackColor = this.dlgCor.Color;
            this.lblCaixaInicio.Text = _linhaPropriedades.CaixaInicio != null ? _linhaPropriedades.CaixaInicio.Numero.ToString() : string.Empty;
            this.lblCaixaFim.Text = _linhaPropriedades.CaixaFim != null ? _linhaPropriedades.CaixaFim.Numero.ToString() : string.Empty;
            this.txtValorAB.Text = _linhaPropriedades.ValorAB.ToString();
            this.txtValorBA.Text = _linhaPropriedades.ValorBA.ToString();
            switch (_linhaPropriedades.DirecaoDaLinha)
            {
                case Linhas.Direcao.InicioParaFim:
                    this.btnDirecao.Text = "-->";
                    this.txtValorAB.Enabled = true;
                    this.txtValorBA.Enabled = false;
                    break;
                case Linhas.Direcao.FimParaInicio:
                    this.btnDirecao.Text = "<--";
                    this.txtValorAB.Enabled = false;
                    this.txtValorBA.Enabled = true;
                    break;
                case Linhas.Direcao.Ambos:
                    this.btnDirecao.Text = "<-->";
                    this.txtValorAB.Enabled = true;
                    this.txtValorBA.Enabled = true;
                    break;
            }
            RefazerNome();
        }
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dlgCor = new System.Windows.Forms.ColorDialog();
            this.btnCor = new System.Windows.Forms.Button();
            this.lblCor = new System.Windows.Forms.Label();
            this.lblDirecao = new System.Windows.Forms.Label();
            this.lblCaixaInicio = new System.Windows.Forms.Label();
            this.lblCaixaFim = new System.Windows.Forms.Label();
            this.btnDirecao = new System.Windows.Forms.Button();
            this.lblValor1 = new System.Windows.Forms.Label();
            this.txtValorAB = new System.Windows.Forms.TextBox();
            this.lblValor2 = new System.Windows.Forms.Label();
            this.txtValorBA = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.Location = new System.Drawing.Point(8, 24);
            this.txtNome.Name = "txtNome";
            this.txtNome.ReadOnly = true;
            this.txtNome.Size = new System.Drawing.Size(181, 21);
            this.txtNome.TabIndex = 0;
            this.txtNome.TabStop = false;
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(8, 8);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(80, 16);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "Nome:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(74, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(152, 157);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            // 
            // dlgCor
            // 
            this.dlgCor.Color = System.Drawing.Color.Maroon;
            // 
            // btnCor
            // 
            this.btnCor.Location = new System.Drawing.Point(195, 24);
            this.btnCor.Name = "btnCor";
            this.btnCor.Size = new System.Drawing.Size(32, 21);
            this.btnCor.TabIndex = 0;
            this.btnCor.Click += new System.EventHandler(this.btnCor_Click);
            // 
            // lblCor
            // 
            this.lblCor.Location = new System.Drawing.Point(195, 8);
            this.lblCor.Name = "lblCor";
            this.lblCor.Size = new System.Drawing.Size(32, 16);
            this.lblCor.TabIndex = 5;
            this.lblCor.Text = "Cor:";
            // 
            // lblDirecao
            // 
            this.lblDirecao.AutoSize = true;
            this.lblDirecao.Location = new System.Drawing.Point(8, 100);
            this.lblDirecao.Name = "lblDirecao";
            this.lblDirecao.Size = new System.Drawing.Size(89, 13);
            this.lblDirecao.TabIndex = 8;
            this.lblDirecao.Text = "Direção do fluxo:";
            // 
            // lblCaixaInicio
            // 
            this.lblCaixaInicio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaixaInicio.Location = new System.Drawing.Point(8, 117);
            this.lblCaixaInicio.Name = "lblCaixaInicio";
            this.lblCaixaInicio.Size = new System.Drawing.Size(80, 19);
            this.lblCaixaInicio.TabIndex = 9;
            this.lblCaixaInicio.Text = "K0";
            this.lblCaixaInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaixaFim
            // 
            this.lblCaixaFim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCaixaFim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaixaFim.Location = new System.Drawing.Point(147, 117);
            this.lblCaixaFim.Name = "lblCaixaFim";
            this.lblCaixaFim.Size = new System.Drawing.Size(80, 19);
            this.lblCaixaFim.TabIndex = 10;
            this.lblCaixaFim.Text = "K0";
            this.lblCaixaFim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDirecao
            // 
            this.btnDirecao.Location = new System.Drawing.Point(96, 115);
            this.btnDirecao.Name = "btnDirecao";
            this.btnDirecao.Size = new System.Drawing.Size(43, 23);
            this.btnDirecao.TabIndex = 3;
            this.btnDirecao.Text = "-->";
            this.btnDirecao.Click += new System.EventHandler(this.btnDirecao_Click);
            // 
            // lblValor1
            // 
            this.lblValor1.Location = new System.Drawing.Point(8, 51);
            this.lblValor1.Name = "lblValor1";
            this.lblValor1.Size = new System.Drawing.Size(80, 16);
            this.lblValor1.TabIndex = 13;
            this.lblValor1.Text = "Valor     -->";
            // 
            // txtValorAB
            // 
            this.txtValorAB.BackColor = System.Drawing.Color.White;
            this.txtValorAB.Location = new System.Drawing.Point(8, 67);
            this.txtValorAB.Name = "txtValorAB";
            this.txtValorAB.Size = new System.Drawing.Size(107, 21);
            this.txtValorAB.TabIndex = 1;
            this.txtValorAB.TextChanged += new System.EventHandler(this.txtValorAB_TextChanged);
            // 
            // lblValor2
            // 
            this.lblValor2.Location = new System.Drawing.Point(120, 51);
            this.lblValor2.Name = "lblValor2";
            this.lblValor2.Size = new System.Drawing.Size(80, 16);
            this.lblValor2.TabIndex = 15;
            this.lblValor2.Text = "Valor     <--";
            // 
            // txtValorBA
            // 
            this.txtValorBA.BackColor = System.Drawing.Color.White;
            this.txtValorBA.Enabled = false;
            this.txtValorBA.Location = new System.Drawing.Point(120, 67);
            this.txtValorBA.Name = "txtValorBA";
            this.txtValorBA.Size = new System.Drawing.Size(107, 21);
            this.txtValorBA.TabIndex = 2;
            this.txtValorBA.TextChanged += new System.EventHandler(this.txtValorBA_TextChanged);
            // 
            // LinhaProp
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(234, 191);
            this.ControlBox = false;
            this.Controls.Add(this.lblValor2);
            this.Controls.Add(this.txtValorBA);
            this.Controls.Add(this.lblValor1);
            this.Controls.Add(this.txtValorAB);
            this.Controls.Add(this.btnDirecao);
            this.Controls.Add(this.lblCaixaFim);
            this.Controls.Add(this.lblCaixaInicio);
            this.Controls.Add(this.lblDirecao);
            this.Controls.Add(this.lblCor);
            this.Controls.Add(this.btnCor);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LinhaProp";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Linha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinhaProp_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

        #region Métodos
        private Linhas.Direcao DirecaoDeString(string Seta)
        {
            switch (Seta)
            {
                case "-->":
                    return Linhas.Direcao.InicioParaFim;
                case "<--":
                    return Linhas.Direcao.FimParaInicio;
                case "<-->":
                    return Linhas.Direcao.Ambos;
            }
            return Linhas.Direcao.InicioParaFim;
        }
        private void RefazerNome()
        {
            this.txtNome.Text = Linhas.SugerirNome(DirecaoDeString(this.btnDirecao.Text), this.lblCaixaInicio.Text, this.lblCaixaFim.Text, this.ValorAB, this.ValorBA);
        }
        #endregion

        #region Métodos de Eventos
        private void btnCor_Click(object sender, System.EventArgs e)
		{
			this.dlgCor.Color = this.btnCor.BackColor;
			this.dlgCor.ShowDialog();
			this.btnCor.BackColor = this.dlgCor.Color;
		}
        private void btnDirecao_Click(object sender, EventArgs e)
        {
            if (this.btnDirecao.Text == "-->")
            {
                this.btnDirecao.Text = "<--";
                this.txtValorAB.Enabled = false;
                this.txtValorBA.Enabled = true;
                RefazerNome();
            }
            else if (this.btnDirecao.Text == "<--")
            {
                this.btnDirecao.Text = "<-->";
                this.txtValorAB.Enabled = true;
                this.txtValorBA.Enabled = true;
                RefazerNome();
            }
            else if (this.btnDirecao.Text == "<-->")
            {
                this.btnDirecao.Text = "-->";
                this.txtValorAB.Enabled = true;
                this.txtValorBA.Enabled = false;
                RefazerNome();
            }
        }
        private void txtValorAB_TextChanged(object sender, EventArgs e)
        {
            RefazerNome();
        }
        private void txtValorBA_TextChanged(object sender, EventArgs e)
        {
            RefazerNome();
        }
		#endregion

		#region Acesso aos Campos
		public string NomeDaLinha
		{
			get
			{
				if (this.txtNome.Text.Trim().Length > 0)
					return this.txtNome.Text.Trim();
				else
					return "[Sem título]";
			}
		}
		public System.Drawing.Color CorDaLinha
		{
			get
			{
				return this.btnCor.BackColor;
			}
		}
        public Linhas.Direcao Direcao
        {
            get
            {
                if (this.btnDirecao.Text == "-->")
                    return Linhas.Direcao.InicioParaFim;
                else if (this.btnDirecao.Text == "<--")
                    return Linhas.Direcao.FimParaInicio;
                else
                    return Linhas.Direcao.Ambos;
            }
        }
        public float ValorAB
        {
            get
            {
                float Retorno = 0F;
                try
                {
                    Retorno = float.Parse(this.txtValorAB.Text);
                }
                catch
                {
                    Retorno = 0F;
                }
                return Retorno;
            }
        }
        public float ValorBA
        {
            get
            {
                float Retorno = 0F;
                try
                {
                    Retorno = float.Parse(this.txtValorBA.Text);
                }
                catch
                {
                    Retorno = 0F;
                }
                return Retorno;
            }
        }
        public Linhas LinhaPropriedades { get { return this._linhaPropriedades; } set { this._linhaPropriedades = value; } }
        #endregion

        private void LinhaProp_FormClosing(object sender, FormClosingEventArgs e)
        {
            float abVal = 0, baVal = 0;
            if ((_linhaPropriedades.CaixaFim == null || _linhaPropriedades.CaixaInicio == null || !float.TryParse(this.txtValorAB.Text, out abVal) || !float.TryParse(this.txtValorBA.Text, out baVal)) && this.DialogResult == DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }

            this.LinhaPropriedades.ValorAB = abVal;
            this.LinhaPropriedades.ValorBA = baVal;
            //this.LinhaPropriedades.Nome = this.txtNome.Text;
            this.LinhaPropriedades.BackColor = this.dlgCor.Color;
            this.LinhaPropriedades.DirecaoDaLinha = DirecaoDeString(this.btnDirecao.Text);
        }
	}
}
