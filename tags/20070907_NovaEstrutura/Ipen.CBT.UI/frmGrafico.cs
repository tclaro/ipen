using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ipen.CompartimentalModel;

namespace Ipen.CBT.UI
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmGrafico : System.Windows.Forms.Form
    {
        private IContainer components;
        private string _ArquivoAberto = "";

        #region Campos

        private MenuStrip mnu;
        private ToolStripMenuItem mnuArquivo;
        private ToolStripMenuItem mnuArquivoNovo;
        private ToolStripMenuItem mnuArquivoAbrir;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem mnuArquivoSalvar;
        private ToolStripMenuItem mnuArquivoSalvarComo;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuArquivoImprimir;
        private ToolStripMenuItem mnuArquivoVisualizarImpressao;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuArquivoSair;
        private ToolStripMenuItem mnuEditar;
        private ToolStripMenuItem mnuFerramentas;
        private ToolStripMenuItem mnuFerramentasPersonalizar;
        private ToolStripMenuItem mnuFerramentasOpcoes;
        private ToolStripMenuItem mnuEditarPropriedades;
        private ToolStripMenuItem mnuEditarExcluir;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuEditarLocalizar;
        private ToolStripMenuItem mnuInserir;
        private ToolStripMenuItem mnuInserirCaixa;
        private ToolStripMenuItem mnuInserirLinha;
        private ContextMenuStrip mnuContextual;
        private ToolStripMenuItem mnuContextualPropriedades;
        private ToolStripMenuItem mnuContextualExcluir;
        private Desktop canvas;
        private ToolStripMenuItem cadastrarToolStripMenuItem;
        private ToolStripMenuItem modelosToolStripMenuItem;
        #endregion

        #region Construtor
        public frmGrafico()
        {
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGrafico));
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
            this.mnuContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContextualPropriedades = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextualExcluir = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas = new Ipen.CBT.UI.Desktop();
            this.mnu.SuspendLayout();
            this.mnuContextual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
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
            this.mnu.Size = new System.Drawing.Size(777, 24);
            this.mnu.TabIndex = 0;
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
            // 
            // mnuArquivoImprimir
            // 
            this.mnuArquivoImprimir.Image = ((System.Drawing.Image)(resources.GetObject("mnuArquivoImprimir.Image")));
            this.mnuArquivoImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuArquivoImprimir.Name = "mnuArquivoImprimir";
            this.mnuArquivoImprimir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuArquivoImprimir.Size = new System.Drawing.Size(180, 22);
            this.mnuArquivoImprimir.Text = "&Imprimir...";
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
            this.mnuFerramentasOpcoes});
            this.mnuFerramentas.Name = "mnuFerramentas";
            this.mnuFerramentas.Size = new System.Drawing.Size(80, 20);
            this.mnuFerramentas.Text = "Ferra&mentas";
            // 
            // mnuFerramentasPersonalizar
            // 
            this.mnuFerramentasPersonalizar.Name = "mnuFerramentasPersonalizar";
            this.mnuFerramentasPersonalizar.Size = new System.Drawing.Size(155, 22);
            this.mnuFerramentasPersonalizar.Text = "Personali&zar...";
            // 
            // mnuFerramentasOpcoes
            // 
            this.mnuFerramentasOpcoes.Name = "mnuFerramentasOpcoes";
            this.mnuFerramentasOpcoes.Size = new System.Drawing.Size(155, 22);
            this.mnuFerramentasOpcoes.Text = "&Opções...";
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
            // mnuContextual
            // 
            this.mnuContextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextualPropriedades,
            this.mnuContextualExcluir});
            this.mnuContextual.Name = "mnuContextual";
            this.mnuContextual.Size = new System.Drawing.Size(149, 48);
            this.mnuContextual.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContextual_Opening);
            // 
            // mnuContextualPropriedades
            // 
            this.mnuContextualPropriedades.Name = "mnuContextualPropriedades";
            this.mnuContextualPropriedades.Size = new System.Drawing.Size(148, 22);
            this.mnuContextualPropriedades.Text = "&Propriedades";
            this.mnuContextualPropriedades.Click += new System.EventHandler(this.mnuContextualPropriedades_Click);
            // 
            // mnuContextualExcluir
            // 
            this.mnuContextualExcluir.Name = "mnuContextualExcluir";
            this.mnuContextualExcluir.Size = new System.Drawing.Size(148, 22);
            this.mnuContextualExcluir.Text = "&Excluir";
            this.mnuContextualExcluir.Click += new System.EventHandler(this.mnuContextualExcluir_Click);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Ivory;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.ContextMenuStrip = this.mnuContextual;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 24);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(777, 497);
            this.canvas.TabIndex = 3;
            this.canvas.TabStop = false;
            // 
            // frmGrafico
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(777, 521);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.mnu);
            this.MainMenuStrip = this.mnu;
            this.Name = "frmGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modelos Compartimentais";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.mnuContextual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region Métodos públicos
        public void ReverMenus()
        {
            if (this.canvas.BoxRequest)
                this.mnuInserirLinha.Text = "Cancelar adição";
            else
                this.mnuInserirLinha.Text = "&Linha";
        }

        #endregion

        #region Métodos privados

        private void SolicitarNovaCaixa()
        {
            CaixaProp novacaixaForm = new CaixaProp("[Sem título]", Caixas.CorPadrao, false, false);
            System.Windows.Forms.DialogResult dlgResultado = novacaixaForm.ShowDialog();
            if (dlgResultado == System.Windows.Forms.DialogResult.Cancel)
                return;
            //int Numero = novacaixaForm.NumeroDaCaixa;
            string Titulo = novacaixaForm.NomeDaCaixa;
            System.Drawing.Color corDaCaixa = novacaixaForm.CorDaCaixa;
            bool Acompanhar = novacaixaForm.Acompanhar;
            bool Eliminacao = novacaixaForm.Eliminacao;
            novacaixaForm.Dispose();
            novacaixaForm = null;
            this.canvas.AdicionarCaixa(Titulo, corDaCaixa, Acompanhar, Eliminacao);
            this.Refresh();
        }

        private void SolicitarNovaLinha()
        {
            if (!this.canvas.BoxRequest)
            {
                this.canvas.SolicitarCaixa(true);
                ReverMenus();
            }
            else
            {
                this.canvas.SolicitarCaixa(false);
                ReverMenus();
            }
        }
        private void EditarObjeto()
        {
            if (this.canvas.LinhaSelecionada != null)
                this.canvas.EditarLinha(this.canvas.LinhaSelecionada);
            if (this.canvas.CaixaSelecionada != null)
                this.canvas.EditarCaixa(this.canvas.CaixaSelecionada);
        }
        private void ExcluirObjeto()
        {
            if (this.canvas.ObjetoSelecionado != null)
            {
                string objetotipo = "";
                if (this.canvas.ObjetoSelecionado is Caixas)
                    objetotipo = "caixa e todas as linhas associadas";
                if (this.canvas.ObjetoSelecionado is Linhas)
                    objetotipo = "linha";
                if (MessageBox.Show("Você tem certeza que deseja excluir essa " + objetotipo + "?", "Exclusão de " + objetotipo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    this.canvas.ExcluirSelecao();
            }
        }
        #endregion

        #region Métodos de Eventos
        private void mnuInserirCaixa_Click(object sender, EventArgs e)
        {
            SolicitarNovaCaixa();
        }
        private void mnuInserirLinha_Click(object sender, EventArgs e)
        {
            SolicitarNovaLinha();
        }
        private void mnuEditarPropriedades_Click(object sender, EventArgs e)
        {
            EditarObjeto();
        }
        private void mnuEditarExcluir_Click(object sender, EventArgs e)
        {
            ExcluirObjeto();
        }
        private void mnuContextualPropriedades_Click(object sender, EventArgs e)
        {
            EditarObjeto();
        }
        private void mnuContextualExcluir_Click(object sender, EventArgs e)
        {
            ExcluirObjeto();
        }
        private void mnuContextual_Opening(object sender, CancelEventArgs e)
        {
            if (this.canvas.ObjetoSelecionado is Caixas)
            {
                this.mnuContextualPropriedades.Text = "&Propriedades da caixa";
                this.mnuContextualExcluir.Text = "&Excluir a caixa";
                e.Cancel = false;
            }
            if (this.canvas.ObjetoSelecionado is Linhas)
            {
                this.mnuContextualPropriedades.Text = "&Propriedades da linha";
                this.mnuContextualExcluir.Text = "&Excluir a linha";
                e.Cancel = false;
            }
            if (this.canvas.ObjetoSelecionado == null)
            {
                e.Cancel = true;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete && this.canvas.ObjetoSelecionado != null)
                ExcluirObjeto();
        }

        #endregion

        private void mnuArquivoAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "c:\\";
            openFile.DefaultExt = "xml";
            openFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            openFile.RestoreDirectory = true;
            //openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ArquivoAberto = openFile.FileName;
                DataXML interfaceXML = new DataXML(ArquivoAberto);
                interfaceXML.ImportarXML();
                if (this.canvas.TodasAsCaixas == null)
                    this.canvas.TodasAsCaixas = new CaixasCollection();
                if (this.canvas.TodasAsLinhas == null)
                    this.canvas.TodasAsLinhas = new LinhasCollection();
                this.canvas.TodasAsCaixas.Clear();
                this.canvas.TodasAsLinhas.Clear();

                this.canvas.TodasAsCaixas.AddRange(interfaceXML.Caixas);
                this.canvas.TodasAsLinhas.AddRange(interfaceXML.Linhas);
            }
        }

        private void mnuArquivoNovo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ao iniciar um novo modelo, todas as alterações no modelo atual que não foram salvas, serão perdidas.\nDeseja continuar?", "Novo modelo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.canvas.LimparTudo();
                this.Refresh();
                ArquivoAberto = "";
            }
        }

        private void mnuArquivoSalvarComo_Click(object sender, EventArgs e)
        {
            string nomeArquivo = "";
            nomeArquivo = SolicitarNomeArquivo();
            if (nomeArquivo != "")
            {
                ArquivoAberto = nomeArquivo;
                DataXML interfaceXML = new DataXML(ArquivoAberto);
                interfaceXML.Caixas = this.canvas.TodasAsCaixas;
                interfaceXML.Linhas = this.canvas.TodasAsLinhas;
                interfaceXML.ExportarXML();
            }
        }

        public string ArquivoAberto
        {
            get
            {
                return this._ArquivoAberto;
            }
            set
            {
                this._ArquivoAberto = value;
                this.Text = "Modelos Compartimentais " + _ArquivoAberto;
            }
        }

        private void mnuArquivoSalvar_Click(object sender, EventArgs e)
        {
            string nomeArquivo = "";
            if (ArquivoAberto == "")
            {
                nomeArquivo = SolicitarNomeArquivo();
                ArquivoAberto = nomeArquivo;
            }
            if (ArquivoAberto != "")
            {
                ArquivoAberto = nomeArquivo;
                DataXML interfaceXML = new DataXML(ArquivoAberto);
                interfaceXML.Caixas = this.canvas.TodasAsCaixas;
                interfaceXML.Linhas = this.canvas.TodasAsLinhas;
                interfaceXML.ExportarXML();
            }
        }

        private string SolicitarNomeArquivo()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = "c:\\";
            saveFile.DefaultExt = "xml";
            saveFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == DialogResult.OK)
                return saveFile.FileName;
            else
                return "";
        }

        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmModelos F = new frmModelos();
            F.Show();
        }
    }
}