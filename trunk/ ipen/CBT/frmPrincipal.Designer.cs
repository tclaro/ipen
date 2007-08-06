using System.Windows.Forms;
namespace CBT
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.mnu = new System.Windows.Forms.MenuStrip();
            this.mnuArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArquivoSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoSalvarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArquivoVisualizarImpressao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoImprimir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArquivoSair = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditarLocalizar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditarPropriedades = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditarExcluir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInserir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInserirCaixa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInserirLinha = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFerramentas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFerramentasPersonalizar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFerramentasOpcoes = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.configurarBancoDeDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArquivo,
            this.mnuEditar,
            this.mnuInserir,
            this.mnuFerramentas,
            this.cadastrarToolStripMenuItem});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(851, 24);
            this.mnu.TabIndex = 1;
            this.mnu.Text = "menuStrip1";
            // 
            // mnuArquivo
            // 
            this.mnuArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArquivoNovo,
            this.mnuArquivoAbrir,
            this.toolStripSeparator,
            this.mnuArquivoSalvar,
            this.mnuArquivoSalvarComo,
            this.toolStripSeparator1,
            this.mnuArquivoVisualizarImpressao,
            this.mnuArquivoImprimir,
            this.toolStripSeparator2,
            this.mnuArquivoSair});
            this.mnuArquivo.Name = "mnuArquivo";
            this.mnuArquivo.Size = new System.Drawing.Size(56, 20);
            this.mnuArquivo.Text = "&Arquivo";
            // 
            // mnuArquivoNovo
            // 
            this.mnuArquivoNovo.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoNovo.Image")));
            this.mnuArquivoNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoNovo.Name = "mnuArquivoNovo";
            this.mnuArquivoNovo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuArquivoNovo.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoNovo.Text = "&Novo";
            this.mnuArquivoNovo.Click += new System.EventHandler(this.mnuArquivoNovo_Click);
            // 
            // mnuArquivoAbrir
            // 
            this.mnuArquivoAbrir.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoAbrir.Image")));
            this.mnuArquivoAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoAbrir.Name = "mnuArquivoAbrir";
            this.mnuArquivoAbrir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuArquivoAbrir.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoAbrir.Text = "&Abrir...";
            this.mnuArquivoAbrir.Click += new System.EventHandler(this.mnuArquivoAbrir_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuArquivoSalvar
            // 
            this.mnuArquivoSalvar.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoSalvar.Image")));
            this.mnuArquivoSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoSalvar.Name = "mnuArquivoSalvar";
            this.mnuArquivoSalvar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuArquivoSalvar.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoSalvar.Text = "&Salvar";
            this.mnuArquivoSalvar.Click += new System.EventHandler(this.mnuArquivoSalvar_Click);
            // 
            // mnuArquivoSalvarComo
            // 
            this.mnuArquivoSalvarComo.Name = "mnuArquivoSalvarComo";
            this.mnuArquivoSalvarComo.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoSalvarComo.Text = "Salvar &como...";
            this.mnuArquivoSalvarComo.Click += new System.EventHandler(this.mnuArquivoSalvarComo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuArquivoVisualizarImpressao
            // 
            this.mnuArquivoVisualizarImpressao.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoVisualizarImpressao.Image")));
            this.mnuArquivoVisualizarImpressao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoVisualizarImpressao.Name = "mnuArquivoVisualizarImpressao";
            this.mnuArquivoVisualizarImpressao.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoVisualizarImpressao.Text = "Visuali&zar impressão";
            this.mnuArquivoVisualizarImpressao.Click += new System.EventHandler(this.mnuArquivoVisualizarImpressao_Click);
            // 
            // mnuArquivoImprimir
            // 
            this.mnuArquivoImprimir.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoImprimir.Image")));
            this.mnuArquivoImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoImprimir.Name = "mnuArquivoImprimir";
            this.mnuArquivoImprimir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuArquivoImprimir.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoImprimir.Text = "&Imprimir...";
            this.mnuArquivoImprimir.Click += new System.EventHandler(this.mnuArquivoImprimir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuArquivoSair
            // 
            this.mnuArquivoSair.Name = "mnuArquivoSair";
            this.mnuArquivoSair.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoSair.Text = "Sai&r";
            this.mnuArquivoSair.Click += new System.EventHandler(this.mnuArquivoSair_Click);
            // 
            // mnuEditar
            // 
            this.mnuEditar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditarLocalizar,
            this.toolStripMenuItem1,
            this.mnuEditarPropriedades,
            this.mnuEditarExcluir});
            this.mnuEditar.Name = "mnuEditar";
            this.mnuEditar.Size = new System.Drawing.Size(47, 20);
            this.mnuEditar.Text = "&Editar";
            // 
            // mnuEditarLocalizar
            // 
            this.mnuEditarLocalizar.Name = "mnuEditarLocalizar";
            this.mnuEditarLocalizar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuEditarLocalizar.Size = new System.Drawing.Size(164, 22);
            this.mnuEditarLocalizar.Text = "Localizar";
            this.mnuEditarLocalizar.Click += new System.EventHandler(this.mnuEditarLocalizar_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuEditarPropriedades
            // 
            this.mnuEditarPropriedades.Name = "mnuEditarPropriedades";
            this.mnuEditarPropriedades.Size = new System.Drawing.Size(164, 22);
            this.mnuEditarPropriedades.Text = "&Propriedades...";
            this.mnuEditarPropriedades.Click += new System.EventHandler(this.mnuEditarPropriedades_Click);
            // 
            // mnuEditarExcluir
            // 
            this.mnuEditarExcluir.Name = "mnuEditarExcluir";
            this.mnuEditarExcluir.Size = new System.Drawing.Size(164, 22);
            this.mnuEditarExcluir.Text = "Excluir";
            this.mnuEditarExcluir.Click += new System.EventHandler(this.mnuEditarExcluir_Click);
            // 
            // mnuInserir
            // 
            this.mnuInserir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInserirCaixa,
            this.mnuInserirLinha});
            this.mnuInserir.Name = "mnuInserir";
            this.mnuInserir.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.mnuInserir.Size = new System.Drawing.Size(50, 20);
            this.mnuInserir.Text = "&Inserir";
            // 
            // mnuInserirCaixa
            // 
            this.mnuInserirCaixa.Name = "mnuInserirCaixa";
            this.mnuInserirCaixa.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.C)));
            this.mnuInserirCaixa.Size = new System.Drawing.Size(172, 22);
            this.mnuInserirCaixa.Text = "&Caixa";
            this.mnuInserirCaixa.Click += new System.EventHandler(this.mnuInserirCaixa_Click);
            // 
            // mnuInserirLinha
            // 
            this.mnuInserirLinha.Name = "mnuInserirLinha";
            this.mnuInserirLinha.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.L)));
            this.mnuInserirLinha.Size = new System.Drawing.Size(172, 22);
            this.mnuInserirLinha.Text = "&Linha";
            this.mnuInserirLinha.Click += new System.EventHandler(this.mnuInserirLinha_Click);
            // 
            // mnuFerramentas
            // 
            this.mnuFerramentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFerramentasPersonalizar,
            this.mnuFerramentasOpcoes,
            this.configurarBancoDeDadosToolStripMenuItem});
            this.mnuFerramentas.Name = "mnuFerramentas";
            this.mnuFerramentas.Size = new System.Drawing.Size(80, 20);
            this.mnuFerramentas.Text = "Ferra&mentas";
            // 
            // mnuFerramentasPersonalizar
            // 
            this.mnuFerramentasPersonalizar.Name = "mnuFerramentasPersonalizar";
            this.mnuFerramentasPersonalizar.Size = new System.Drawing.Size(228, 22);
            this.mnuFerramentasPersonalizar.Text = "Personali&zar...";
            this.mnuFerramentasPersonalizar.Click += new System.EventHandler(this.mnuFerramentasPersonalizar_Click);
            // 
            // mnuFerramentasOpcoes
            // 
            this.mnuFerramentasOpcoes.Name = "mnuFerramentasOpcoes";
            this.mnuFerramentasOpcoes.Size = new System.Drawing.Size(228, 22);
            this.mnuFerramentasOpcoes.Text = "&Opções...";
            this.mnuFerramentasOpcoes.Click += new System.EventHandler(this.mnuFerramentasOpcoes_Click);
            // 
            // cadastrarToolStripMenuItem
            // 
            this.cadastrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelosToolStripMenuItem});
            this.cadastrarToolStripMenuItem.Name = "cadastrarToolStripMenuItem";
            this.cadastrarToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.cadastrarToolStripMenuItem.Text = "Cadastrar";
            // 
            // modelosToolStripMenuItem
            // 
            this.modelosToolStripMenuItem.Name = "modelosToolStripMenuItem";
            this.modelosToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.modelosToolStripMenuItem.Text = "Modelos";
            this.modelosToolStripMenuItem.Click += new System.EventHandler(this.modelosToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 590);
            this.panel1.TabIndex = 2;
            // 
            // configurarBancoDeDadosToolStripMenuItem
            // 
            this.configurarBancoDeDadosToolStripMenuItem.Name = "configurarBancoDeDadosToolStripMenuItem";
            this.configurarBancoDeDadosToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.configurarBancoDeDadosToolStripMenuItem.Text = "Configurar Banco de Dados...";
            this.configurarBancoDeDadosToolStripMenuItem.Click += new System.EventHandler(this.configurarBancoDeDadosToolStripMenuItem_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 614);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnu);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modelos Compartimentais";
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivo;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoNovo;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoSalvar;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoSalvarComo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoVisualizarImpressao;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoSair;
        private System.Windows.Forms.ToolStripMenuItem mnuEditar;
        private System.Windows.Forms.ToolStripMenuItem mnuEditarLocalizar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditarPropriedades;
        private System.Windows.Forms.ToolStripMenuItem mnuEditarExcluir;
        private System.Windows.Forms.ToolStripMenuItem mnuInserir;
        private System.Windows.Forms.ToolStripMenuItem mnuInserirCaixa;
        private System.Windows.Forms.ToolStripMenuItem mnuInserirLinha;
        private System.Windows.Forms.ToolStripMenuItem mnuFerramentas;
        private System.Windows.Forms.ToolStripMenuItem mnuFerramentasPersonalizar;
        private System.Windows.Forms.ToolStripMenuItem mnuFerramentasOpcoes;
        private System.Windows.Forms.ToolStripMenuItem cadastrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelosToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem configurarBancoDeDadosToolStripMenuItem;
    }
}