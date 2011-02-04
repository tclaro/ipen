namespace Ipen.SSID.UI
{
    partial class frmCalculo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalculo));
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtTempo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPasso = new System.Windows.Forms.TextBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modelosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarMDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGraficoSeparado = new System.Windows.Forms.ToolStripMenuItem();
            this.métodoDeSoluçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBirchall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRungeKutta5 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRungeKutta45 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdamsMoulton = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairDoSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTempoDecorrido = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSalvarHTML = new System.Windows.Forms.Button();
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblMetodo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMeiaVida = new System.Windows.Forms.Label();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalcular
            // 
            this.btnCalcular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcular.Enabled = false;
            this.btnCalcular.Location = new System.Drawing.Point(639, 40);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 40);
            this.btnCalcular.TabIndex = 0;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtTempo
            // 
            this.txtTempo.Location = new System.Drawing.Point(98, 40);
            this.txtTempo.Name = "txtTempo";
            this.txtTempo.Size = new System.Drawing.Size(100, 20);
            this.txtTempo.TabIndex = 5;
            this.txtTempo.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tempo (dias):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Passo:";
            // 
            // txtPasso
            // 
            this.txtPasso.Location = new System.Drawing.Point(98, 66);
            this.txtPasso.Name = "txtPasso";
            this.txtPasso.Size = new System.Drawing.Size(100, 20);
            this.txtPasso.TabIndex = 7;
            this.txtPasso.Text = "1";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(207, 2);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(26, 22);
            this.zedGraphControl1.TabIndex = 11;
            this.zedGraphControl1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelosToolStripMenuItem,
            this.configuraçõesToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(758, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modelosToolStripMenuItem
            // 
            this.modelosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarXMLToolStripMenuItem,
            this.carregarMDBToolStripMenuItem});
            this.modelosToolStripMenuItem.Name = "modelosToolStripMenuItem";
            this.modelosToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.modelosToolStripMenuItem.Text = "Modelos";
            // 
            // carregarXMLToolStripMenuItem
            // 
            this.carregarXMLToolStripMenuItem.Name = "carregarXMLToolStripMenuItem";
            this.carregarXMLToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.carregarXMLToolStripMenuItem.Text = "Carregar XML...";
            this.carregarXMLToolStripMenuItem.Click += new System.EventHandler(this.carregarXMLToolStripMenuItem_Click);
            // 
            // carregarMDBToolStripMenuItem
            // 
            this.carregarMDBToolStripMenuItem.Name = "carregarMDBToolStripMenuItem";
            this.carregarMDBToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.carregarMDBToolStripMenuItem.Text = "Carregar MDB...";
            this.carregarMDBToolStripMenuItem.Click += new System.EventHandler(this.carregarMDBToolStripMenuItem_Click);
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGraficoSeparado,
            this.métodoDeSoluçãoToolStripMenuItem});
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            // 
            // mnuGraficoSeparado
            // 
            this.mnuGraficoSeparado.Name = "mnuGraficoSeparado";
            this.mnuGraficoSeparado.Size = new System.Drawing.Size(245, 22);
            this.mnuGraficoSeparado.Text = "Gerar gráfico em janela separada";
            this.mnuGraficoSeparado.Click += new System.EventHandler(this.mnuGraficoSeparado_Click);
            // 
            // métodoDeSoluçãoToolStripMenuItem
            // 
            this.métodoDeSoluçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBirchall,
            this.mnuRungeKutta5,
            this.mnuRungeKutta45,
            this.mnuAdamsMoulton});
            this.métodoDeSoluçãoToolStripMenuItem.Name = "métodoDeSoluçãoToolStripMenuItem";
            this.métodoDeSoluçãoToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.métodoDeSoluçãoToolStripMenuItem.Text = "Método de Solução";
            // 
            // mnuBirchall
            // 
            this.mnuBirchall.Name = "mnuBirchall";
            this.mnuBirchall.Size = new System.Drawing.Size(161, 22);
            this.mnuBirchall.Text = "Birchall";
            this.mnuBirchall.Click += new System.EventHandler(this.mnuBirchall_Click);
            // 
            // mnuRungeKutta5
            // 
            this.mnuRungeKutta5.Name = "mnuRungeKutta5";
            this.mnuRungeKutta5.Size = new System.Drawing.Size(161, 22);
            this.mnuRungeKutta5.Text = "Runge-Kutta 5";
            this.mnuRungeKutta5.Click += new System.EventHandler(this.mnuRungeKutta5_Click);
            // 
            // mnuRungeKutta45
            // 
            this.mnuRungeKutta45.Checked = true;
            this.mnuRungeKutta45.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRungeKutta45.Name = "mnuRungeKutta45";
            this.mnuRungeKutta45.Size = new System.Drawing.Size(161, 22);
            this.mnuRungeKutta45.Text = "Runge-Kutta 45";
            this.mnuRungeKutta45.Click += new System.EventHandler(this.mnuRungeKutta45_Click);
            // 
            // mnuAdamsMoulton
            // 
            this.mnuAdamsMoulton.Name = "mnuAdamsMoulton";
            this.mnuAdamsMoulton.Size = new System.Drawing.Size(161, 22);
            this.mnuAdamsMoulton.Text = "Adams-Moulton";
            this.mnuAdamsMoulton.Click += new System.EventHandler(this.mnuAdamsMoulton_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairDoSistemaToolStripMenuItem});
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.sairToolStripMenuItem.Text = "Sair";
            // 
            // sairDoSistemaToolStripMenuItem
            // 
            this.sairDoSistemaToolStripMenuItem.Name = "sairDoSistemaToolStripMenuItem";
            this.sairDoSistemaToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.sairDoSistemaToolStripMenuItem.Text = "Sair do Sistema";
            this.sairDoSistemaToolStripMenuItem.Click += new System.EventHandler(this.sairDoSistemaToolStripMenuItem_Click);
            // 
            // lblTempoDecorrido
            // 
            this.lblTempoDecorrido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTempoDecorrido.AutoSize = true;
            this.lblTempoDecorrido.Location = new System.Drawing.Point(644, 106);
            this.lblTempoDecorrido.Name = "lblTempoDecorrido";
            this.lblTempoDecorrido.Size = new System.Drawing.Size(70, 13);
            this.lblTempoDecorrido.TabIndex = 15;
            this.lblTempoDecorrido.Text = "00:00:00:000";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(613, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tempo de Processamento";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 128);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(738, 352);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSalvarHTML);
            this.tabPage1.Controls.Add(this.Browser);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(730, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Relatório";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSalvarHTML
            // 
            this.btnSalvarHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvarHTML.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSalvarHTML.BackgroundImage")));
            this.btnSalvarHTML.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalvarHTML.Location = new System.Drawing.Point(637, 6);
            this.btnSalvarHTML.Name = "btnSalvarHTML";
            this.btnSalvarHTML.Size = new System.Drawing.Size(63, 62);
            this.btnSalvarHTML.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnSalvarHTML, "Salvar em arquivo HTML");
            this.btnSalvarHTML.UseVisualStyleBackColor = true;
            this.btnSalvarHTML.Visible = false;
            this.btnSalvarHTML.Click += new System.EventHandler(this.btnSalvarHTML_Click);
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.Location = new System.Drawing.Point(3, 3);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(724, 320);
            this.Browser.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.zg1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(730, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gráfico";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // zg1
            // 
            this.zg1.Location = new System.Drawing.Point(46, 24);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            this.zg1.Size = new System.Drawing.Size(308, 265);
            this.zg1.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // lblMetodo
            // 
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Location = new System.Drawing.Point(341, 40);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(82, 13);
            this.lblMetodo.TabIndex = 24;
            this.lblMetodo.Text = "Runge-Kutta 45";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Método Selecionado: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(224, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Modelo:";
            // 
            // lblMeiaVida
            // 
            this.lblMeiaVida.AutoSize = true;
            this.lblMeiaVida.Location = new System.Drawing.Point(476, 40);
            this.lblMeiaVida.Name = "lblMeiaVida";
            this.lblMeiaVida.Size = new System.Drawing.Size(57, 13);
            this.lblMeiaVida.TabIndex = 27;
            this.lblMeiaVida.Text = "Meia Vida:";
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(275, 66);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(108, 13);
            this.lblModelo.TabIndex = 28;
            this.lblModelo.Text = "Selecione um modelo";
            // 
            // lblDescricao
            // 
            this.lblDescricao.Location = new System.Drawing.Point(275, 88);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(306, 44);
            this.lblDescricao.TabIndex = 29;
            // 
            // frmCalculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 492);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.lblMeiaVida);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMetodo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTempoDecorrido);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPasso);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTempo);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(591, 465);
            this.Name = "frmCalculo";
            this.Text = " Smart Software for Internal Dosimetry";
            this.Resize += new System.EventHandler(this.frmCalculo_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.TextBox txtTempo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPasso;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modelosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarMDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairDoSistemaToolStripMenuItem;
        private System.Windows.Forms.Label lblTempoDecorrido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.TabPage tabPage2;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGraficoSeparado;
        private System.Windows.Forms.Button btnSalvarHTML;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem métodoDeSoluçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuBirchall;
        private System.Windows.Forms.ToolStripMenuItem mnuRungeKutta5;
        private System.Windows.Forms.ToolStripMenuItem mnuRungeKutta45;
        private System.Windows.Forms.ToolStripMenuItem mnuAdamsMoulton;
        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMeiaVida;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblDescricao;
    }
}

