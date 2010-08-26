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
            this.txtSaida = new System.Windows.Forms.RichTextBox();
            this.txtTempo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPasso = new System.Windows.Forms.TextBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modelosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarMDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairDoSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTempoDecorrido = new System.Windows.Forms.Label();
            this.rdoBirchall = new System.Windows.Forms.RadioButton();
            this.rdoKutta5 = new System.Windows.Forms.RadioButton();
            this.rdoKutta45 = new System.Windows.Forms.RadioButton();
            this.rdoAdams = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalcular
            // 
            this.btnCalcular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcular.Enabled = false;
            this.btnCalcular.Location = new System.Drawing.Point(464, 40);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 40);
            this.btnCalcular.TabIndex = 0;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtSaida
            // 
            this.txtSaida.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaida.Location = new System.Drawing.Point(12, 132);
            this.txtSaida.Name = "txtSaida";
            this.txtSaida.Size = new System.Drawing.Size(560, 294);
            this.txtSaida.TabIndex = 4;
            this.txtSaida.Text = "";
            this.txtSaida.WordWrap = false;
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
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(26, 22);
            this.zedGraphControl1.TabIndex = 11;
            this.zedGraphControl1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelosToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(583, 24);
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
            this.lblTempoDecorrido.Location = new System.Drawing.Point(469, 106);
            this.lblTempoDecorrido.Name = "lblTempoDecorrido";
            this.lblTempoDecorrido.Size = new System.Drawing.Size(70, 13);
            this.lblTempoDecorrido.TabIndex = 15;
            this.lblTempoDecorrido.Text = "00:00:00:000";
            // 
            // rdoBirchall
            // 
            this.rdoBirchall.AutoSize = true;
            this.rdoBirchall.Checked = true;
            this.rdoBirchall.Location = new System.Drawing.Point(31, 24);
            this.rdoBirchall.Name = "rdoBirchall";
            this.rdoBirchall.Size = new System.Drawing.Size(59, 17);
            this.rdoBirchall.TabIndex = 17;
            this.rdoBirchall.TabStop = true;
            this.rdoBirchall.Text = "Birchall";
            this.rdoBirchall.UseVisualStyleBackColor = true;
            // 
            // rdoKutta5
            // 
            this.rdoKutta5.AutoSize = true;
            this.rdoKutta5.Location = new System.Drawing.Point(31, 43);
            this.rdoKutta5.Name = "rdoKutta5";
            this.rdoKutta5.Size = new System.Drawing.Size(94, 17);
            this.rdoKutta5.TabIndex = 18;
            this.rdoKutta5.Text = "Runge-Kutta 5";
            this.rdoKutta5.UseVisualStyleBackColor = true;
            // 
            // rdoKutta45
            // 
            this.rdoKutta45.AutoSize = true;
            this.rdoKutta45.Location = new System.Drawing.Point(31, 63);
            this.rdoKutta45.Name = "rdoKutta45";
            this.rdoKutta45.Size = new System.Drawing.Size(100, 17);
            this.rdoKutta45.TabIndex = 19;
            this.rdoKutta45.Text = "Runge-Kutta 45";
            this.rdoKutta45.UseVisualStyleBackColor = true;
            // 
            // rdoAdams
            // 
            this.rdoAdams.AutoSize = true;
            this.rdoAdams.Location = new System.Drawing.Point(31, 81);
            this.rdoAdams.Name = "rdoAdams";
            this.rdoAdams.Size = new System.Drawing.Size(101, 17);
            this.rdoAdams.TabIndex = 20;
            this.rdoAdams.Text = "Adams-Moulton ";
            this.rdoAdams.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoAdams);
            this.groupBox1.Controls.Add(this.rdoKutta45);
            this.groupBox1.Controls.Add(this.rdoKutta5);
            this.groupBox1.Controls.Add(this.rdoBirchall);
            this.groupBox1.Location = new System.Drawing.Point(260, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 103);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Método de Solução";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(438, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tempo de Processamento";
            // 
            // frmCalculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 438);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTempoDecorrido);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPasso);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTempo);
            this.Controls.Add(this.txtSaida);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(591, 465);
            this.Name = "frmCalculo";
            this.Text = " ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.RichTextBox txtSaida;
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
        private System.Windows.Forms.RadioButton rdoBirchall;
        private System.Windows.Forms.RadioButton rdoKutta5;
        private System.Windows.Forms.RadioButton rdoKutta45;
        private System.Windows.Forms.RadioButton rdoAdams;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}

