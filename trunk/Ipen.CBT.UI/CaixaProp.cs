using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CompartimentalModel;

namespace CBT
{
	/// <summary>
	/// Summary description for CaixaProp.
	/// </summary>
	public class CaixaProp : System.Windows.Forms.Form
	{
		#region Campos
        private Caixas _caixaPropriedades;
        private int Numero;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCor;
		private System.Windows.Forms.Button btnCancelar;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblNome;
		private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ColorDialog dlgCor;
        private System.Windows.Forms.CheckBox chkAcompanhar;
        private CheckBox chkEliminacao;
        private CheckBox chkIncorporacao;
        private Label lblFracao;
        private TextBox txtFracao;
        
		private System.ComponentModel.Container components = null;
		#endregion

		#region Construtor
		public CaixaProp(string Nome, System.Drawing.Color CorDaLinha, bool Acompanhar, bool Eliminacao)
		{
			InitializeComponent();
			this.txtNome.Text = Nome;
			this.btnCor.BackColor = CorDaLinha;
            chkAcompanhar.Checked = Acompanhar;
            chkEliminacao.Checked = Eliminacao;
		}
		public CaixaProp(Caixas cx)
		{
			InitializeComponent();
            this._caixaPropriedades = cx;
            if (_caixaPropriedades == null)
                _caixaPropriedades = new Caixas("[Sem Título]", Caixas.CorPadrao, false, false);

            PreencherRegistro();
 		}
        private void PreencherRegistro()
        {
            this.txtNome.Text = _caixaPropriedades.Nome;
            Numero = _caixaPropriedades.Numero;
            this.dlgCor.Color = _caixaPropriedades.BackColor;
            this.btnCor.BackColor = this.dlgCor.Color;
            this.chkAcompanhar.Checked = _caixaPropriedades.Acompanhar;
            this.chkEliminacao.Checked = _caixaPropriedades.Eliminacao;
            this.chkIncorporacao.Checked = _caixaPropriedades.Incorporacao;
            this.txtFracao.Text = _caixaPropriedades.Fracao.ToString();
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCor = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.dlgCor = new System.Windows.Forms.ColorDialog();
            this.chkAcompanhar = new System.Windows.Forms.CheckBox();
            this.chkEliminacao = new System.Windows.Forms.CheckBox();
            this.chkIncorporacao = new System.Windows.Forms.CheckBox();
            this.lblFracao = new System.Windows.Forms.Label();
            this.txtFracao = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(195, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Cor:";
            // 
            // btnCor
            // 
            this.btnCor.Location = new System.Drawing.Point(195, 24);
            this.btnCor.Name = "btnCor";
            this.btnCor.Size = new System.Drawing.Size(32, 21);
            this.btnCor.TabIndex = 1;
            this.btnCor.Click += new System.EventHandler(this.btnCor_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(145, 117);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(65, 117);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(8, 8);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(176, 16);
            this.lblNome.TabIndex = 9;
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(8, 24);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(181, 21);
            this.txtNome.TabIndex = 0;
            // 
            // chkAcompanhar
            // 
            this.chkAcompanhar.AutoSize = true;
            this.chkAcompanhar.Location = new System.Drawing.Point(8, 60);
            this.chkAcompanhar.Name = "chkAcompanhar";
            this.chkAcompanhar.Size = new System.Drawing.Size(86, 17);
            this.chkAcompanhar.TabIndex = 2;
            this.chkAcompanhar.Text = "Acompanhar";
            this.chkAcompanhar.UseVisualStyleBackColor = true;
            // 
            // chkEliminacao
            // 
            this.chkEliminacao.AutoSize = true;
            this.chkEliminacao.Location = new System.Drawing.Point(8, 83);
            this.chkEliminacao.Name = "chkEliminacao";
            this.chkEliminacao.Size = new System.Drawing.Size(75, 17);
            this.chkEliminacao.TabIndex = 12;
            this.chkEliminacao.Text = "Eliminação";
            this.chkEliminacao.UseVisualStyleBackColor = true;
            // 
            // chkIncorporacao
            // 
            this.chkIncorporacao.AutoSize = true;
            this.chkIncorporacao.Location = new System.Drawing.Point(107, 60);
            this.chkIncorporacao.Name = "chkIncorporacao";
            this.chkIncorporacao.Size = new System.Drawing.Size(90, 17);
            this.chkIncorporacao.TabIndex = 13;
            this.chkIncorporacao.Text = "Incorporacao";
            this.chkIncorporacao.UseVisualStyleBackColor = true;
            // 
            // lblFracao
            // 
            this.lblFracao.AutoSize = true;
            this.lblFracao.Location = new System.Drawing.Point(104, 84);
            this.lblFracao.Name = "lblFracao";
            this.lblFracao.Size = new System.Drawing.Size(40, 13);
            this.lblFracao.TabIndex = 14;
            this.lblFracao.Text = "Fração";
            // 
            // txtFracao
            // 
            this.txtFracao.Location = new System.Drawing.Point(150, 81);
            this.txtFracao.Name = "txtFracao";
            this.txtFracao.Size = new System.Drawing.Size(53, 21);
            this.txtFracao.TabIndex = 15;
            // 
            // CaixaProp
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(232, 152);
            this.Controls.Add(this.txtFracao);
            this.Controls.Add(this.lblFracao);
            this.Controls.Add(this.chkIncorporacao);
            this.Controls.Add(this.chkEliminacao);
            this.Controls.Add(this.chkAcompanhar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCor);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CaixaProp";
            this.Opacity = 0.85;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caixa";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Métodos de Eventos
		private void btnCor_Click(object sender, System.EventArgs e)
		{
			this.dlgCor.Color = this.btnCor.BackColor;
			this.dlgCor.ShowDialog();
			this.btnCor.BackColor = this.dlgCor.Color;
		}
        private void btnOK_Click(object sender, EventArgs e)
        {
            this._caixaPropriedades.Nome = this.txtNome.Text;
            this._caixaPropriedades.BackColor = this.dlgCor.Color;
            this._caixaPropriedades.Acompanhar = this.chkAcompanhar.Checked;
            this._caixaPropriedades.Eliminacao = this.chkEliminacao.Checked;
            this._caixaPropriedades.Incorporacao = this.chkIncorporacao.Checked;
            this._caixaPropriedades.Fracao = double.Parse(this.txtFracao.Text);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion

		#region Acesso aos Campos
        public int NumeroDaCaixa
        {
            get
            {
                return Numero;
            }
        }
		public string NomeDaCaixa
		{
			get
			{
				if (this.txtNome.Text.Trim().Length > 0)
					return this.txtNome.Text.Trim();
				else
					return "[Sem título]";
			}
		}
		public System.Drawing.Color CorDaCaixa
		{
			get
			{
				return this.btnCor.BackColor;
			}
		}

        public bool Acompanhar
        {
            get
            {
                return this.chkAcompanhar.Checked;
            }
        }

        public bool Eliminacao
        {
            get
            {
                return this.chkEliminacao.Checked;
            }
        }
        public Caixas CaixaPropriedades
        {
            get { return _caixaPropriedades; }
            set { _caixaPropriedades = value; }
        }

		#endregion
	}
}
