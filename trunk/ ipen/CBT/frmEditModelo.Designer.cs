namespace CBT
{
    partial class frmEditModelo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlModelo = new System.Windows.Forms.TabControl();
            this.tabPageCompart = new System.Windows.Forms.TabPage();
            this.lblFracao = new System.Windows.Forms.Label();
            this.txtFracao = new System.Windows.Forms.TextBox();
            this.chkIncorporacao = new System.Windows.Forms.CheckBox();
            this.cmdLimparComp = new System.Windows.Forms.Button();
            this.lstCompartimentos = new System.Windows.Forms.ListBox();
            this.lblCompAdded = new System.Windows.Forms.Label();
            this.btnRemComp = new System.Windows.Forms.Button();
            this.btnAddComp = new System.Windows.Forms.Button();
            this.chkEliminacao = new System.Windows.Forms.CheckBox();
            this.btnCorComp = new System.Windows.Forms.Button();
            this.chkAcompanhar = new System.Windows.Forms.CheckBox();
            this.lblCorComp = new System.Windows.Forms.Label();
            this.lblNomeComp = new System.Windows.Forms.Label();
            this.txtCompartimento = new System.Windows.Forms.TextBox();
            this.tabPageLigacoes = new System.Windows.Forms.TabPage();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblSeta = new System.Windows.Forms.Label();
            this.btnCorLig = new System.Windows.Forms.Button();
            this.lblCorLig = new System.Windows.Forms.Label();
            this.cmdLimparLig = new System.Windows.Forms.Button();
            this.btnRemLig = new System.Windows.Forms.Button();
            this.btnAddLig = new System.Windows.Forms.Button();
            this.lvwLigacoes = new System.Windows.Forms.ListView();
            this.txtValorAB = new System.Windows.Forms.TextBox();
            this.cboCompartB = new System.Windows.Forms.ComboBox();
            this.cboCompartA = new System.Windows.Forms.ComboBox();
            this.dlgCor = new System.Windows.Forms.ColorDialog();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.btnGamb = new System.Windows.Forms.Button();
            this.toolTipComp = new System.Windows.Forms.ToolTip(this.components);
            this.lblContador = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.tabControlModelo.SuspendLayout();
            this.tabPageCompart.SuspendLayout();
            this.tabPageLigacoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlModelo
            // 
            this.tabControlModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlModelo.Controls.Add(this.tabPageCompart);
            this.tabControlModelo.Controls.Add(this.tabPageLigacoes);
            this.tabControlModelo.Location = new System.Drawing.Point(12, 38);
            this.tabControlModelo.Name = "tabControlModelo";
            this.tabControlModelo.SelectedIndex = 0;
            this.tabControlModelo.Size = new System.Drawing.Size(608, 294);
            this.tabControlModelo.TabIndex = 4;
            this.tabControlModelo.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlModelo_Selected);
            // 
            // tabPageCompart
            // 
            this.tabPageCompart.Controls.Add(this.lblFracao);
            this.tabPageCompart.Controls.Add(this.txtFracao);
            this.tabPageCompart.Controls.Add(this.chkIncorporacao);
            this.tabPageCompart.Controls.Add(this.cmdLimparComp);
            this.tabPageCompart.Controls.Add(this.lstCompartimentos);
            this.tabPageCompart.Controls.Add(this.lblCompAdded);
            this.tabPageCompart.Controls.Add(this.btnRemComp);
            this.tabPageCompart.Controls.Add(this.btnAddComp);
            this.tabPageCompart.Controls.Add(this.chkEliminacao);
            this.tabPageCompart.Controls.Add(this.btnCorComp);
            this.tabPageCompart.Controls.Add(this.chkAcompanhar);
            this.tabPageCompart.Controls.Add(this.lblCorComp);
            this.tabPageCompart.Controls.Add(this.lblNomeComp);
            this.tabPageCompart.Controls.Add(this.txtCompartimento);
            this.tabPageCompart.Location = new System.Drawing.Point(4, 22);
            this.tabPageCompart.Name = "tabPageCompart";
            this.tabPageCompart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCompart.Size = new System.Drawing.Size(600, 268);
            this.tabPageCompart.TabIndex = 0;
            this.tabPageCompart.Text = "Compartimentos";
            this.tabPageCompart.UseVisualStyleBackColor = true;
            // 
            // lblFracao
            // 
            this.lblFracao.AutoSize = true;
            this.lblFracao.Enabled = false;
            this.lblFracao.Location = new System.Drawing.Point(44, 184);
            this.lblFracao.Name = "lblFracao";
            this.lblFracao.Size = new System.Drawing.Size(43, 13);
            this.lblFracao.TabIndex = 14;
            this.lblFracao.Text = "&Fração:";
            // 
            // txtFracao
            // 
            this.txtFracao.Enabled = false;
            this.txtFracao.Location = new System.Drawing.Point(93, 181);
            this.txtFracao.Name = "txtFracao";
            this.txtFracao.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFracao.Size = new System.Drawing.Size(75, 20);
            this.txtFracao.TabIndex = 13;
            this.txtFracao.Text = "0";
            // 
            // chkIncorporacao
            // 
            this.chkIncorporacao.AutoSize = true;
            this.chkIncorporacao.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncorporacao.Location = new System.Drawing.Point(20, 158);
            this.chkIncorporacao.Name = "chkIncorporacao";
            this.chkIncorporacao.Size = new System.Drawing.Size(89, 17);
            this.chkIncorporacao.TabIndex = 12;
            this.chkIncorporacao.Text = "&Incorporação";
            this.chkIncorporacao.UseVisualStyleBackColor = true;
            this.chkIncorporacao.CheckedChanged += new System.EventHandler(this.chkIncorporacao_CheckedChanged);
            // 
            // cmdLimparComp
            // 
            this.cmdLimparComp.Location = new System.Drawing.Point(297, 158);
            this.cmdLimparComp.Name = "cmdLimparComp";
            this.cmdLimparComp.Size = new System.Drawing.Size(75, 23);
            this.cmdLimparComp.TabIndex = 11;
            this.cmdLimparComp.Text = "&Limpar";
            this.cmdLimparComp.UseVisualStyleBackColor = true;
            this.cmdLimparComp.Click += new System.EventHandler(this.cmdLimparComp_Click);
            // 
            // lstCompartimentos
            // 
            this.lstCompartimentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCompartimentos.FormattingEnabled = true;
            this.lstCompartimentos.Location = new System.Drawing.Point(378, 31);
            this.lstCompartimentos.Name = "lstCompartimentos";
            this.lstCompartimentos.Size = new System.Drawing.Size(205, 212);
            this.lstCompartimentos.TabIndex = 10;
            this.toolTipComp.SetToolTip(this.lstCompartimentos, "Duplo clique para alterar um compartimento");
            this.lstCompartimentos.DoubleClick += new System.EventHandler(this.lstCompartimentos_DoubleClick);
            // 
            // lblCompAdded
            // 
            this.lblCompAdded.AutoSize = true;
            this.lblCompAdded.Location = new System.Drawing.Point(375, 13);
            this.lblCompAdded.Name = "lblCompAdded";
            this.lblCompAdded.Size = new System.Drawing.Size(143, 13);
            this.lblCompAdded.TabIndex = 8;
            this.lblCompAdded.Text = "Compartimentos Adicionados";
            // 
            // btnRemComp
            // 
            this.btnRemComp.Location = new System.Drawing.Point(297, 129);
            this.btnRemComp.Name = "btnRemComp";
            this.btnRemComp.Size = new System.Drawing.Size(75, 23);
            this.btnRemComp.TabIndex = 7;
            this.btnRemComp.Text = "&Remover";
            this.btnRemComp.UseVisualStyleBackColor = true;
            this.btnRemComp.Click += new System.EventHandler(this.btnRemComp_Click);
            // 
            // btnAddComp
            // 
            this.btnAddComp.Location = new System.Drawing.Point(297, 100);
            this.btnAddComp.Name = "btnAddComp";
            this.btnAddComp.Size = new System.Drawing.Size(75, 23);
            this.btnAddComp.TabIndex = 6;
            this.btnAddComp.Text = "&Adicionar";
            this.btnAddComp.UseVisualStyleBackColor = true;
            this.btnAddComp.Click += new System.EventHandler(this.btnAddComp_Click);
            // 
            // chkEliminacao
            // 
            this.chkEliminacao.AutoSize = true;
            this.chkEliminacao.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEliminacao.Location = new System.Drawing.Point(32, 129);
            this.chkEliminacao.Name = "chkEliminacao";
            this.chkEliminacao.Size = new System.Drawing.Size(77, 17);
            this.chkEliminacao.TabIndex = 5;
            this.chkEliminacao.Text = "&Eliminação";
            this.chkEliminacao.UseVisualStyleBackColor = true;
            // 
            // btnCorComp
            // 
            this.btnCorComp.Location = new System.Drawing.Point(96, 61);
            this.btnCorComp.Name = "btnCorComp";
            this.btnCorComp.Size = new System.Drawing.Size(30, 23);
            this.btnCorComp.TabIndex = 3;
            this.btnCorComp.Click += new System.EventHandler(this.btnCorComp_Click);
            // 
            // chkAcompanhar
            // 
            this.chkAcompanhar.AutoSize = true;
            this.chkAcompanhar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAcompanhar.Location = new System.Drawing.Point(23, 100);
            this.chkAcompanhar.Name = "chkAcompanhar";
            this.chkAcompanhar.Size = new System.Drawing.Size(86, 17);
            this.chkAcompanhar.TabIndex = 4;
            this.chkAcompanhar.Text = "Acompanhar";
            this.chkAcompanhar.UseVisualStyleBackColor = true;
            this.chkAcompanhar.CheckedChanged += new System.EventHandler(this.chkAcompanhar_CheckedChanged);
            // 
            // lblCorComp
            // 
            this.lblCorComp.AutoSize = true;
            this.lblCorComp.Location = new System.Drawing.Point(64, 66);
            this.lblCorComp.Name = "lblCorComp";
            this.lblCorComp.Size = new System.Drawing.Size(26, 13);
            this.lblCorComp.TabIndex = 2;
            this.lblCorComp.Text = "&Cor:";
            // 
            // lblNomeComp
            // 
            this.lblNomeComp.AutoSize = true;
            this.lblNomeComp.Location = new System.Drawing.Point(52, 31);
            this.lblNomeComp.Name = "lblNomeComp";
            this.lblNomeComp.Size = new System.Drawing.Size(38, 13);
            this.lblNomeComp.TabIndex = 0;
            this.lblNomeComp.Text = "&Nome:";
            // 
            // txtCompartimento
            // 
            this.txtCompartimento.Location = new System.Drawing.Point(96, 28);
            this.txtCompartimento.Name = "txtCompartimento";
            this.txtCompartimento.Size = new System.Drawing.Size(246, 20);
            this.txtCompartimento.TabIndex = 1;
            // 
            // tabPageLigacoes
            // 
            this.tabPageLigacoes.Controls.Add(this.lblValor);
            this.tabPageLigacoes.Controls.Add(this.lblSeta);
            this.tabPageLigacoes.Controls.Add(this.btnCorLig);
            this.tabPageLigacoes.Controls.Add(this.lblCorLig);
            this.tabPageLigacoes.Controls.Add(this.cmdLimparLig);
            this.tabPageLigacoes.Controls.Add(this.btnRemLig);
            this.tabPageLigacoes.Controls.Add(this.btnAddLig);
            this.tabPageLigacoes.Controls.Add(this.lvwLigacoes);
            this.tabPageLigacoes.Controls.Add(this.txtValorAB);
            this.tabPageLigacoes.Controls.Add(this.cboCompartB);
            this.tabPageLigacoes.Controls.Add(this.cboCompartA);
            this.tabPageLigacoes.Location = new System.Drawing.Point(4, 22);
            this.tabPageLigacoes.Name = "tabPageLigacoes";
            this.tabPageLigacoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLigacoes.Size = new System.Drawing.Size(600, 268);
            this.tabPageLigacoes.TabIndex = 1;
            this.tabPageLigacoes.Text = "Ligações";
            this.tabPageLigacoes.UseVisualStyleBackColor = true;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(111, 68);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(34, 13);
            this.lblValor.TabIndex = 3;
            this.lblValor.Text = "&Valor:";
            // 
            // lblSeta
            // 
            this.lblSeta.AutoSize = true;
            this.lblSeta.Location = new System.Drawing.Point(186, 39);
            this.lblSeta.Name = "lblSeta";
            this.lblSeta.Size = new System.Drawing.Size(49, 13);
            this.lblSeta.TabIndex = 12;
            this.lblSeta.Text = "------------>";
            // 
            // btnCorLig
            // 
            this.btnCorLig.Location = new System.Drawing.Point(370, 63);
            this.btnCorLig.Name = "btnCorLig";
            this.btnCorLig.Size = new System.Drawing.Size(30, 23);
            this.btnCorLig.TabIndex = 6;
            this.btnCorLig.Click += new System.EventHandler(this.btnCorLig_Click);
            // 
            // lblCorLig
            // 
            this.lblCorLig.AutoSize = true;
            this.lblCorLig.Location = new System.Drawing.Point(338, 68);
            this.lblCorLig.Name = "lblCorLig";
            this.lblCorLig.Size = new System.Drawing.Size(26, 13);
            this.lblCorLig.TabIndex = 5;
            this.lblCorLig.Text = "&Cor:";
            // 
            // cmdLimparLig
            // 
            this.cmdLimparLig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLimparLig.Location = new System.Drawing.Point(510, 77);
            this.cmdLimparLig.Name = "cmdLimparLig";
            this.cmdLimparLig.Size = new System.Drawing.Size(75, 23);
            this.cmdLimparLig.TabIndex = 10;
            this.cmdLimparLig.Text = "&Limpar";
            this.cmdLimparLig.UseVisualStyleBackColor = true;
            this.cmdLimparLig.Click += new System.EventHandler(this.cmdLimparLig_Click);
            // 
            // btnRemLig
            // 
            this.btnRemLig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemLig.Location = new System.Drawing.Point(510, 48);
            this.btnRemLig.Name = "btnRemLig";
            this.btnRemLig.Size = new System.Drawing.Size(75, 23);
            this.btnRemLig.TabIndex = 9;
            this.btnRemLig.Text = "Remover";
            this.btnRemLig.UseVisualStyleBackColor = true;
            this.btnRemLig.Click += new System.EventHandler(this.btnRemLig_Click);
            // 
            // btnAddLig
            // 
            this.btnAddLig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLig.Location = new System.Drawing.Point(510, 19);
            this.btnAddLig.Name = "btnAddLig";
            this.btnAddLig.Size = new System.Drawing.Size(75, 23);
            this.btnAddLig.TabIndex = 8;
            this.btnAddLig.Text = "Adicionar";
            this.btnAddLig.UseVisualStyleBackColor = true;
            this.btnAddLig.Click += new System.EventHandler(this.btnAddLig_Click);
            // 
            // lvwLigacoes
            // 
            this.lvwLigacoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwLigacoes.Location = new System.Drawing.Point(11, 107);
            this.lvwLigacoes.Name = "lvwLigacoes";
            this.lvwLigacoes.Size = new System.Drawing.Size(574, 145);
            this.lvwLigacoes.TabIndex = 11;
            this.toolTipComp.SetToolTip(this.lvwLigacoes, "Duplo Clique para alterar uma ligação");
            this.lvwLigacoes.UseCompatibleStateImageBehavior = false;
            this.lvwLigacoes.DoubleClick += new System.EventHandler(this.lvwLigacoes_DoubleClick);
            // 
            // txtValorAB
            // 
            this.txtValorAB.Location = new System.Drawing.Point(151, 63);
            this.txtValorAB.Name = "txtValorAB";
            this.txtValorAB.Size = new System.Drawing.Size(109, 20);
            this.txtValorAB.TabIndex = 4;
            // 
            // cboCompartB
            // 
            this.cboCompartB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompartB.FormattingEnabled = true;
            this.cboCompartB.Location = new System.Drawing.Point(241, 36);
            this.cboCompartB.Name = "cboCompartB";
            this.cboCompartB.Size = new System.Drawing.Size(159, 21);
            this.cboCompartB.TabIndex = 2;
            this.cboCompartB.SelectionChangeCommitted += new System.EventHandler(this.cboCompartimentoAouB_SelectionChangeCommitted);
            // 
            // cboCompartA
            // 
            this.cboCompartA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompartA.FormattingEnabled = true;
            this.cboCompartA.Location = new System.Drawing.Point(21, 36);
            this.cboCompartA.Name = "cboCompartA";
            this.cboCompartA.Size = new System.Drawing.Size(159, 21);
            this.cboCompartA.TabIndex = 0;
            this.cboCompartA.SelectionChangeCommitted += new System.EventHandler(this.cboCompartimentoAouB_SelectionChangeCommitted);
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(78, 11);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(130, 20);
            this.txtNome.TabIndex = 1;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(34, 14);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalvar.Location = new System.Drawing.Point(12, 338);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Location = new System.Drawing.Point(545, 338);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 6;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(290, 14);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(58, 13);
            this.lblDescricao.TabIndex = 2;
            this.lblDescricao.Text = "Descrição:";
            // 
            // btnGamb
            // 
            this.btnGamb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGamb.Location = new System.Drawing.Point(266, 380);
            this.btnGamb.Name = "btnGamb";
            this.btnGamb.Size = new System.Drawing.Size(122, 34);
            this.btnGamb.TabIndex = 7;
            this.btnGamb.Text = "Gambiarra Oculta";
            this.btnGamb.UseVisualStyleBackColor = true;
            this.btnGamb.Click += new System.EventHandler(this.btnGamb_Click);
            // 
            // lblContador
            // 
            this.lblContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblContador.AutoSize = true;
            this.lblContador.Location = new System.Drawing.Point(106, 343);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(0, 13);
            this.lblContador.TabIndex = 8;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(354, 6);
            this.txtDescricao.MaxLength = 200;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(261, 42);
            this.txtDescricao.TabIndex = 3;
            // 
            // frmEditModelo
            // 
            this.AcceptButton = this.btnGamb;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 371);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblContador);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.tabControlModelo);
            this.Controls.Add(this.btnGamb);
            this.MinimumSize = new System.Drawing.Size(640, 398);
            this.Name = "frmEditModelo";
            this.Text = "Edição Modelos";
            this.Load += new System.EventHandler(this.frmEditModelo_Load);
            this.tabControlModelo.ResumeLayout(false);
            this.tabPageCompart.ResumeLayout(false);
            this.tabPageCompart.PerformLayout();
            this.tabPageLigacoes.ResumeLayout(false);
            this.tabPageLigacoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlModelo;
        private System.Windows.Forms.TabPage tabPageCompart;
        private System.Windows.Forms.TabPage tabPageLigacoes;
        private System.Windows.Forms.TextBox txtCompartimento;
        private System.Windows.Forms.CheckBox chkAcompanhar;
        private System.Windows.Forms.Label lblCorComp;
        private System.Windows.Forms.Label lblNomeComp;
        private System.Windows.Forms.Button btnCorComp;
        private System.Windows.Forms.ColorDialog dlgCor;
        private System.Windows.Forms.CheckBox chkEliminacao;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.Button btnAddComp;
        private System.Windows.Forms.Button btnRemComp;
        private System.Windows.Forms.Label lblCompAdded;
        private System.Windows.Forms.ComboBox cboCompartA;
        private System.Windows.Forms.ComboBox cboCompartB;
        private System.Windows.Forms.TextBox txtValorAB;
        private System.Windows.Forms.Button btnRemLig;
        private System.Windows.Forms.Button btnAddLig;
        private System.Windows.Forms.ListView lvwLigacoes;
        private System.Windows.Forms.ListBox lstCompartimentos;
        private System.Windows.Forms.Button btnGamb;
        private System.Windows.Forms.Button cmdLimparComp;
        private System.Windows.Forms.Button cmdLimparLig;
        private System.Windows.Forms.Button btnCorLig;
        private System.Windows.Forms.Label lblCorLig;
        private System.Windows.Forms.ToolTip toolTipComp;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Label lblSeta;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblFracao;
        private System.Windows.Forms.TextBox txtFracao;
        private System.Windows.Forms.CheckBox chkIncorporacao;
    }
}