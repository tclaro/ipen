namespace Ipen.SSID.UI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtSaida = new System.Windows.Forms.RichTextBox();
            this.txtTempo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPasso = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMeiaVida = new System.Windows.Forms.TextBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modelosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarMDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairDoSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTempoDecorrido = new System.Windows.Forms.Label();
            this.rdoPascal = new System.Windows.Forms.RadioButton();
            this.rdoKutta5 = new System.Windows.Forms.RadioButton();
            this.rdoKutta45 = new System.Windows.Forms.RadioButton();
            this.rdoAdams = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalcular
            // 
            this.btnCalcular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalcular.Enabled = false;
            this.btnCalcular.Location = new System.Drawing.Point(466, 29);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 23);
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
            this.txtSaida.Size = new System.Drawing.Size(546, 294);
            this.txtSaida.TabIndex = 4;
            this.txtSaida.Text = "";
            this.txtSaida.WordWrap = false;
            // 
            // txtTempo
            // 
            this.txtTempo.Location = new System.Drawing.Point(129, 51);
            this.txtTempo.Name = "txtTempo";
            this.txtTempo.Size = new System.Drawing.Size(100, 20);
            this.txtTempo.TabIndex = 5;
            this.txtTempo.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tempo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Passo:";
            // 
            // txtPasso
            // 
            this.txtPasso.Location = new System.Drawing.Point(129, 77);
            this.txtPasso.Name = "txtPasso";
            this.txtPasso.Size = new System.Drawing.Size(100, 20);
            this.txtPasso.TabIndex = 7;
            this.txtPasso.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Meia Vida Radioativa:";
            // 
            // txtMeiaVida
            // 
            this.txtMeiaVida.Location = new System.Drawing.Point(129, 103);
            this.txtMeiaVida.Name = "txtMeiaVida";
            this.txtMeiaVida.Size = new System.Drawing.Size(100, 20);
            this.txtMeiaVida.TabIndex = 9;
            this.txtMeiaVida.Text = "0";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(207, 2);
            this.zedGraphControl1.Name = "zedGraphControl1";
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
            this.menuStrip1.Size = new System.Drawing.Size(569, 24);
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
            this.lblTempoDecorrido.AutoSize = true;
            this.lblTempoDecorrido.Location = new System.Drawing.Point(471, 84);
            this.lblTempoDecorrido.Name = "lblTempoDecorrido";
            this.lblTempoDecorrido.Size = new System.Drawing.Size(70, 13);
            this.lblTempoDecorrido.TabIndex = 15;
            this.lblTempoDecorrido.Text = "00:00:00:000";
            // 
            // rdoPascal
            // 
            this.rdoPascal.AutoSize = true;
            this.rdoPascal.Checked = true;
            this.rdoPascal.Location = new System.Drawing.Point(309, 32);
            this.rdoPascal.Name = "rdoPascal";
            this.rdoPascal.Size = new System.Drawing.Size(57, 17);
            this.rdoPascal.TabIndex = 17;
            this.rdoPascal.TabStop = true;
            this.rdoPascal.Text = "Pascal";
            this.rdoPascal.UseVisualStyleBackColor = true;
            // 
            // rdoKutta5
            // 
            this.rdoKutta5.AutoSize = true;
            this.rdoKutta5.Location = new System.Drawing.Point(309, 55);
            this.rdoKutta5.Name = "rdoKutta5";
            this.rdoKutta5.Size = new System.Drawing.Size(94, 17);
            this.rdoKutta5.TabIndex = 18;
            this.rdoKutta5.Text = "Runge-Kutta 5";
            this.rdoKutta5.UseVisualStyleBackColor = true;
            // 
            // rdoKutta45
            // 
            this.rdoKutta45.AutoSize = true;
            this.rdoKutta45.Location = new System.Drawing.Point(309, 78);
            this.rdoKutta45.Name = "rdoKutta45";
            this.rdoKutta45.Size = new System.Drawing.Size(100, 17);
            this.rdoKutta45.TabIndex = 19;
            this.rdoKutta45.Text = "Runge-Kutta 45";
            this.rdoKutta45.UseVisualStyleBackColor = true;
            // 
            // rdoAdams
            // 
            this.rdoAdams.AutoSize = true;
            this.rdoAdams.Location = new System.Drawing.Point(309, 100);
            this.rdoAdams.Name = "rdoAdams";
            this.rdoAdams.Size = new System.Drawing.Size(101, 17);
            this.rdoAdams.TabIndex = 20;
            this.rdoAdams.Text = "Adams-Moulton ";
            this.rdoAdams.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 438);
            this.Controls.Add(this.rdoAdams);
            this.Controls.Add(this.rdoKutta45);
            this.Controls.Add(this.rdoKutta5);
            this.Controls.Add(this.rdoPascal);
            this.Controls.Add(this.lblTempoDecorrido);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMeiaVida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPasso);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTempo);
            this.Controls.Add(this.txtSaida);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SSID";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMeiaVida;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modelosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarMDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairDoSistemaToolStripMenuItem;
        private System.Windows.Forms.Label lblTempoDecorrido;
        private System.Windows.Forms.RadioButton rdoPascal;
        private System.Windows.Forms.RadioButton rdoKutta5;
        private System.Windows.Forms.RadioButton rdoKutta45;
        private System.Windows.Forms.RadioButton rdoAdams;
    }
}

