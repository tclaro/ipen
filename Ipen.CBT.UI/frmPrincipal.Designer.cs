using System.Windows.Forms;
namespace Ipen.CBT.UI
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
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.exibirRótuloDeTransferênciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurarBancoDeDadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelosGraficamenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.tabControlModelo = new System.Windows.Forms.TabControl();
            this.tabPageCompart = new System.Windows.Forms.TabPage();
            this.btnSync = new System.Windows.Forms.Button();
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.btnCorLig = new System.Windows.Forms.Button();
            this.lblCorLig = new System.Windows.Forms.Label();
            this.cmdLimparLig = new System.Windows.Forms.Button();
            this.btnRemLig = new System.Windows.Forms.Button();
            this.btnAddLig = new System.Windows.Forms.Button();
            this.lvwLigacoes = new System.Windows.Forms.ListView();
            this.txtValorAB = new System.Windows.Forms.TextBox();
            this.cboCompartB = new System.Windows.Forms.ComboBox();
            this.cboCompartA = new System.Windows.Forms.ComboBox();
            this.btnGamb = new System.Windows.Forms.Button();
            this.mnu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlModelo.SuspendLayout();
            this.tabPageCompart.SuspendLayout();
            this.tabPageLigacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArquivo,
            this.arquivoToolStripMenuItem,
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
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.salvarComoToolStripMenuItem,
            this.exportarToolStripMenuItem,
            this.importarToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo2";
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.novoToolStripMenuItem.Text = "Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvarToolStripMenuItem_Click);
            // 
            // salvarComoToolStripMenuItem
            // 
            this.salvarComoToolStripMenuItem.Name = "salvarComoToolStripMenuItem";
            this.salvarComoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salvarComoToolStripMenuItem.Text = "Salvar Como";
            this.salvarComoToolStripMenuItem.Click += new System.EventHandler(this.salvarComoToolStripMenuItem_Click);
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportarToolStripMenuItem.Text = "Exportar";
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importarToolStripMenuItem.Text = "Importar";
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
            this.mnuFerramentasOpcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exibirRótuloDeTransferênciasToolStripMenuItem});
            this.mnuFerramentasOpcoes.Name = "mnuFerramentasOpcoes";
            this.mnuFerramentasOpcoes.Size = new System.Drawing.Size(228, 22);
            this.mnuFerramentasOpcoes.Text = "&Opções";
            this.mnuFerramentasOpcoes.Click += new System.EventHandler(this.mnuFerramentasOpcoes_Click);
            // 
            // exibirRótuloDeTransferênciasToolStripMenuItem
            // 
            this.exibirRótuloDeTransferênciasToolStripMenuItem.Checked = true;
            this.exibirRótuloDeTransferênciasToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exibirRótuloDeTransferênciasToolStripMenuItem.Name = "exibirRótuloDeTransferênciasToolStripMenuItem";
            this.exibirRótuloDeTransferênciasToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.exibirRótuloDeTransferênciasToolStripMenuItem.Text = "Exibir Rótulo de Transferências";
            this.exibirRótuloDeTransferênciasToolStripMenuItem.Click += new System.EventHandler(this.exibirRótuloDeTransferênciasToolStripMenuItem_Click);
            // 
            // configurarBancoDeDadosToolStripMenuItem
            // 
            this.configurarBancoDeDadosToolStripMenuItem.Name = "configurarBancoDeDadosToolStripMenuItem";
            this.configurarBancoDeDadosToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.configurarBancoDeDadosToolStripMenuItem.Text = "Configurar Banco de Dados...";
            this.configurarBancoDeDadosToolStripMenuItem.Click += new System.EventHandler(this.configurarBancoDeDadosToolStripMenuItem_Click);
            // 
            // cadastrarToolStripMenuItem
            // 
            this.cadastrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelosToolStripMenuItem,
            this.modelosGraficamenteToolStripMenuItem});
            this.cadastrarToolStripMenuItem.Name = "cadastrarToolStripMenuItem";
            this.cadastrarToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.cadastrarToolStripMenuItem.Text = "Cadastrar";
            // 
            // modelosToolStripMenuItem
            // 
            this.modelosToolStripMenuItem.Name = "modelosToolStripMenuItem";
            this.modelosToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.modelosToolStripMenuItem.Text = "Modelos modo texto";
            this.modelosToolStripMenuItem.Click += new System.EventHandler(this.modelosToolStripMenuItem_Click);
            // 
            // modelosGraficamenteToolStripMenuItem
            // 
            this.modelosGraficamenteToolStripMenuItem.Name = "modelosGraficamenteToolStripMenuItem";
            this.modelosGraficamenteToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.modelosGraficamenteToolStripMenuItem.Text = "Modelos Graficamente";
            this.modelosGraficamenteToolStripMenuItem.Click += new System.EventHandler(this.modelosGraficamenteToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 590);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboTipo);
            this.splitContainer1.Panel1.Controls.Add(this.lblTipo);
            this.splitContainer1.Panel1.Controls.Add(this.txtDescricao);
            this.splitContainer1.Panel1.Controls.Add(this.lblDescricao);
            this.splitContainer1.Panel1.Controls.Add(this.lblNome);
            this.splitContainer1.Panel1.Controls.Add(this.txtNome);
            this.splitContainer1.Panel1.Controls.Add(this.tabControlModelo);
            this.splitContainer1.Panel1.Controls.Add(this.btnGamb);
            this.splitContainer1.Panel1MinSize = 176;
            this.splitContainer1.Size = new System.Drawing.Size(851, 590);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 3;
            // 
            // cboTipo
            // 
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(245, 3);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(159, 21);
            this.cboTipo.TabIndex = 44;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(212, 6);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(31, 13);
            this.lblTipo.TabIndex = 43;
            this.lblTipo.Text = "Tipo:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(472, 3);
            this.txtDescricao.MaxLength = 200;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(355, 43);
            this.txtDescricao.TabIndex = 42;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(413, 7);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(58, 13);
            this.lblDescricao.TabIndex = 41;
            this.lblDescricao.Text = "Descrição:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(15, 6);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 39;
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(59, 4);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(147, 20);
            this.txtNome.TabIndex = 40;
            // 
            // tabControlModelo
            // 
            this.tabControlModelo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlModelo.Controls.Add(this.tabPageCompart);
            this.tabControlModelo.Controls.Add(this.tabPageLigacoes);
            this.tabControlModelo.Location = new System.Drawing.Point(1, 30);
            this.tabControlModelo.Name = "tabControlModelo";
            this.tabControlModelo.SelectedIndex = 0;
            this.tabControlModelo.Size = new System.Drawing.Size(847, 139);
            this.tabControlModelo.TabIndex = 38;
            // 
            // tabPageCompart
            // 
            this.tabPageCompart.Controls.Add(this.btnSync);
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
            this.tabPageCompart.Size = new System.Drawing.Size(839, 113);
            this.tabPageCompart.TabIndex = 0;
            this.tabPageCompart.Text = "Compartimentos";
            this.tabPageCompart.UseVisualStyleBackColor = true;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(262, 59);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(49, 30);
            this.btnSync.TabIndex = 43;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // lblFracao
            // 
            this.lblFracao.AutoSize = true;
            this.lblFracao.Enabled = false;
            this.lblFracao.Location = new System.Drawing.Point(119, 77);
            this.lblFracao.Name = "lblFracao";
            this.lblFracao.Size = new System.Drawing.Size(43, 13);
            this.lblFracao.TabIndex = 42;
            this.lblFracao.Text = "&Fração:";
            // 
            // txtFracao
            // 
            this.txtFracao.Enabled = false;
            this.txtFracao.Location = new System.Drawing.Point(168, 74);
            this.txtFracao.Name = "txtFracao";
            this.txtFracao.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFracao.Size = new System.Drawing.Size(75, 20);
            this.txtFracao.TabIndex = 41;
            this.txtFracao.Text = "0";
            // 
            // chkIncorporacao
            // 
            this.chkIncorporacao.AutoSize = true;
            this.chkIncorporacao.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncorporacao.Location = new System.Drawing.Point(6, 76);
            this.chkIncorporacao.Name = "chkIncorporacao";
            this.chkIncorporacao.Size = new System.Drawing.Size(89, 17);
            this.chkIncorporacao.TabIndex = 40;
            this.chkIncorporacao.Text = "&Incorporação";
            this.chkIncorporacao.UseVisualStyleBackColor = true;
            this.chkIncorporacao.CheckedChanged += new System.EventHandler(this.chkIncorporacao_CheckedChanged);
            // 
            // cmdLimparComp
            // 
            this.cmdLimparComp.Location = new System.Drawing.Point(320, 79);
            this.cmdLimparComp.Name = "cmdLimparComp";
            this.cmdLimparComp.Size = new System.Drawing.Size(75, 23);
            this.cmdLimparComp.TabIndex = 39;
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
            this.lstCompartimentos.Location = new System.Drawing.Point(410, 19);
            this.lstCompartimentos.Name = "lstCompartimentos";
            this.lstCompartimentos.Size = new System.Drawing.Size(423, 82);
            this.lstCompartimentos.TabIndex = 38;
            this.lstCompartimentos.DoubleClick += new System.EventHandler(this.lstCompartimentos_DoubleClick);
            // 
            // lblCompAdded
            // 
            this.lblCompAdded.AutoSize = true;
            this.lblCompAdded.Location = new System.Drawing.Point(407, 3);
            this.lblCompAdded.Name = "lblCompAdded";
            this.lblCompAdded.Size = new System.Drawing.Size(143, 13);
            this.lblCompAdded.TabIndex = 37;
            this.lblCompAdded.Text = "Compartimentos Adicionados";
            // 
            // btnRemComp
            // 
            this.btnRemComp.Location = new System.Drawing.Point(320, 49);
            this.btnRemComp.Name = "btnRemComp";
            this.btnRemComp.Size = new System.Drawing.Size(75, 23);
            this.btnRemComp.TabIndex = 36;
            this.btnRemComp.Text = "&Remover";
            this.btnRemComp.UseVisualStyleBackColor = true;
            this.btnRemComp.Click += new System.EventHandler(this.btnRemComp_Click);
            // 
            // btnAddComp
            // 
            this.btnAddComp.Location = new System.Drawing.Point(320, 20);
            this.btnAddComp.Name = "btnAddComp";
            this.btnAddComp.Size = new System.Drawing.Size(75, 23);
            this.btnAddComp.TabIndex = 35;
            this.btnAddComp.Text = "&Adicionar";
            this.btnAddComp.UseVisualStyleBackColor = true;
            this.btnAddComp.Click += new System.EventHandler(this.btnAddComp_Click);
            // 
            // chkEliminacao
            // 
            this.chkEliminacao.AutoSize = true;
            this.chkEliminacao.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEliminacao.Location = new System.Drawing.Point(107, 53);
            this.chkEliminacao.Name = "chkEliminacao";
            this.chkEliminacao.Size = new System.Drawing.Size(77, 17);
            this.chkEliminacao.TabIndex = 34;
            this.chkEliminacao.Text = "&Eliminação";
            this.chkEliminacao.UseVisualStyleBackColor = true;
            // 
            // btnCorComp
            // 
            this.btnCorComp.Location = new System.Drawing.Point(269, 20);
            this.btnCorComp.Name = "btnCorComp";
            this.btnCorComp.Size = new System.Drawing.Size(28, 20);
            this.btnCorComp.TabIndex = 32;
            this.btnCorComp.Click += new System.EventHandler(this.btnCorComp_Click);
            // 
            // chkAcompanhar
            // 
            this.chkAcompanhar.AutoSize = true;
            this.chkAcompanhar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAcompanhar.Location = new System.Drawing.Point(9, 53);
            this.chkAcompanhar.Name = "chkAcompanhar";
            this.chkAcompanhar.Size = new System.Drawing.Size(86, 17);
            this.chkAcompanhar.TabIndex = 33;
            this.chkAcompanhar.Text = "Acompanhar";
            this.chkAcompanhar.UseVisualStyleBackColor = true;
            // 
            // lblCorComp
            // 
            this.lblCorComp.AutoSize = true;
            this.lblCorComp.Location = new System.Drawing.Point(238, 23);
            this.lblCorComp.Name = "lblCorComp";
            this.lblCorComp.Size = new System.Drawing.Size(26, 13);
            this.lblCorComp.TabIndex = 31;
            this.lblCorComp.Text = "&Cor:";
            // 
            // lblNomeComp
            // 
            this.lblNomeComp.AutoSize = true;
            this.lblNomeComp.Location = new System.Drawing.Point(40, 24);
            this.lblNomeComp.Name = "lblNomeComp";
            this.lblNomeComp.Size = new System.Drawing.Size(38, 13);
            this.lblNomeComp.TabIndex = 29;
            this.lblNomeComp.Text = "&Nome:";
            // 
            // txtCompartimento
            // 
            this.txtCompartimento.Location = new System.Drawing.Point(84, 20);
            this.txtCompartimento.Name = "txtCompartimento";
            this.txtCompartimento.Size = new System.Drawing.Size(140, 20);
            this.txtCompartimento.TabIndex = 30;
            // 
            // tabPageLigacoes
            // 
            this.tabPageLigacoes.Controls.Add(this.pictureBox1);
            this.tabPageLigacoes.Controls.Add(this.lblValor);
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
            this.tabPageLigacoes.Size = new System.Drawing.Size(839, 113);
            this.tabPageLigacoes.TabIndex = 1;
            this.tabPageLigacoes.Text = "Ligações";
            this.tabPageLigacoes.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(58, 36);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(34, 13);
            this.lblValor.TabIndex = 3;
            this.lblValor.Text = "&Valor:";
            // 
            // btnCorLig
            // 
            this.btnCorLig.Location = new System.Drawing.Point(98, 59);
            this.btnCorLig.Name = "btnCorLig";
            this.btnCorLig.Size = new System.Drawing.Size(28, 20);
            this.btnCorLig.TabIndex = 6;
            this.btnCorLig.Click += new System.EventHandler(this.btnCorLig_Click);
            // 
            // lblCorLig
            // 
            this.lblCorLig.AutoSize = true;
            this.lblCorLig.Location = new System.Drawing.Point(66, 59);
            this.lblCorLig.Name = "lblCorLig";
            this.lblCorLig.Size = new System.Drawing.Size(26, 13);
            this.lblCorLig.TabIndex = 5;
            this.lblCorLig.Text = "&Cor:";
            // 
            // cmdLimparLig
            // 
            this.cmdLimparLig.Location = new System.Drawing.Point(188, 74);
            this.cmdLimparLig.Name = "cmdLimparLig";
            this.cmdLimparLig.Size = new System.Drawing.Size(75, 23);
            this.cmdLimparLig.TabIndex = 10;
            this.cmdLimparLig.Text = "&Limpar";
            this.cmdLimparLig.UseVisualStyleBackColor = true;
            this.cmdLimparLig.Click += new System.EventHandler(this.cmdLimparLig_Click);
            // 
            // btnRemLig
            // 
            this.btnRemLig.Location = new System.Drawing.Point(188, 45);
            this.btnRemLig.Name = "btnRemLig";
            this.btnRemLig.Size = new System.Drawing.Size(75, 23);
            this.btnRemLig.TabIndex = 9;
            this.btnRemLig.Text = "Remover";
            this.btnRemLig.UseVisualStyleBackColor = true;
            this.btnRemLig.Click += new System.EventHandler(this.btnRemLig_Click);
            // 
            // btnAddLig
            // 
            this.btnAddLig.Location = new System.Drawing.Point(188, 15);
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
            this.lvwLigacoes.Location = new System.Drawing.Point(269, 6);
            this.lvwLigacoes.Name = "lvwLigacoes";
            this.lvwLigacoes.Size = new System.Drawing.Size(562, 100);
            this.lvwLigacoes.TabIndex = 11;
            this.lvwLigacoes.UseCompatibleStateImageBehavior = false;
            this.lvwLigacoes.Click += new System.EventHandler(this.lvwLigacoes_DoubleClick);
            // 
            // txtValorAB
            // 
            this.txtValorAB.Location = new System.Drawing.Point(98, 33);
            this.txtValorAB.Name = "txtValorAB";
            this.txtValorAB.Size = new System.Drawing.Size(72, 20);
            this.txtValorAB.TabIndex = 4;
            // 
            // cboCompartB
            // 
            this.cboCompartB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompartB.FormattingEnabled = true;
            this.cboCompartB.Location = new System.Drawing.Point(11, 88);
            this.cboCompartB.Name = "cboCompartB";
            this.cboCompartB.Size = new System.Drawing.Size(159, 21);
            this.cboCompartB.TabIndex = 2;
            this.cboCompartB.SelectionChangeCommitted += new System.EventHandler(this.cboCompartimentoAouB_SelectionChangeCommitted);
            // 
            // cboCompartA
            // 
            this.cboCompartA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompartA.FormattingEnabled = true;
            this.cboCompartA.Location = new System.Drawing.Point(11, 8);
            this.cboCompartA.Name = "cboCompartA";
            this.cboCompartA.Size = new System.Drawing.Size(159, 21);
            this.cboCompartA.TabIndex = 0;
            this.cboCompartA.SelectionChangeCommitted += new System.EventHandler(this.cboCompartimentoAouB_SelectionChangeCommitted);
            // 
            // btnGamb
            // 
            this.btnGamb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGamb.Location = new System.Drawing.Point(514, 114);
            this.btnGamb.Name = "btnGamb";
            this.btnGamb.Size = new System.Drawing.Size(122, 34);
            this.btnGamb.TabIndex = 45;
            this.btnGamb.Text = "Gambiarra Oculta";
            this.btnGamb.UseVisualStyleBackColor = true;
            this.btnGamb.Click += new System.EventHandler(this.btnGamb_Click);
            // 
            // frmPrincipal
            // 
            this.AcceptButton = this.btnGamb;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 614);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modelos Compartimentais";
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlModelo.ResumeLayout(false);
            this.tabPageCompart.ResumeLayout(false);
            this.tabPageCompart.PerformLayout();
            this.tabPageLigacoes.ResumeLayout(false);
            this.tabPageLigacoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private ToolStripMenuItem exibirRótuloDeTransferênciasToolStripMenuItem;
        private ToolStripMenuItem modelosGraficamenteToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem novoToolStripMenuItem;
        private ToolStripMenuItem salvarToolStripMenuItem;
        private ToolStripMenuItem salvarComoToolStripMenuItem;
        private ToolStripMenuItem exportarToolStripMenuItem;
        private ToolStripMenuItem importarToolStripMenuItem;
        private ComboBox cboTipo;
        private Label lblTipo;
        private TextBox txtDescricao;
        private Label lblDescricao;
        private Label lblNome;
        private TextBox txtNome;
        private TabControl tabControlModelo;
        private TabPage tabPageCompart;
        private Label lblFracao;
        private TextBox txtFracao;
        private CheckBox chkIncorporacao;
        private Button cmdLimparComp;
        private ListBox lstCompartimentos;
        private Label lblCompAdded;
        private Button btnRemComp;
        private Button btnAddComp;
        private CheckBox chkEliminacao;
        private Button btnCorComp;
        private CheckBox chkAcompanhar;
        private Label lblCorComp;
        private Label lblNomeComp;
        private TextBox txtCompartimento;
        private TabPage tabPageLigacoes;
        private PictureBox pictureBox1;
        private Label lblValor;
        private Button btnCorLig;
        private Label lblCorLig;
        private Button cmdLimparLig;
        private Button btnRemLig;
        private Button btnAddLig;
        private ListView lvwLigacoes;
        private TextBox txtValorAB;
        private ComboBox cboCompartB;
        private ComboBox cboCompartA;
        private Button btnGamb;
        private Button btnSync;
    }
}