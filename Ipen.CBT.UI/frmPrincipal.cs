using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Ipen.CompartimentalModel;

namespace Ipen.CBT.UI
{
    public partial class frmPrincipal : Form
    {
        private Painel PnlCanvas;
        private StatusPossiveis statusAtual;
        private Caixas criaLinha1;
        private Caixas criaLinha2;
        
        private string _ArquivoAberto = "";

        private enum StatusPossiveis
        {
            Normal,
            SolicitandoLinhaA,
            SolicitandoLinhaB
        }

        public frmPrincipal()
        {
            InitializeComponent();
            statusAtual = StatusPossiveis.Normal;
            PnlCanvas = new Painel();
            PnlCanvas.AutoScroll = true;
            PnlCanvas.BackColor = System.Drawing.Color.Ivory;
            PnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlCanvas.BoxModifyRequest += new Painel.BoxModifyRequestHandle(pnlCanvas_BoxModifyRequest);
            PnlCanvas.SistemaCompartimental.BoxClick += new EventHandler(SistemaCompartimental_BoxClick);
            this.panel1.Controls.Add(this.PnlCanvas);
            AjustarRotulos();
        }

        private void AjustarRotulos()
        {
            bool Exibir = Convert.ToBoolean(LerSettings("Rotulos"));
            exibirRótuloDeTransferênciasToolStripMenuItem.Checked = Exibir;
            Configuracoes.ExibirRotulos = Exibir;
        }

        #region Métodos de eventos
        void pnlCanvas_BoxModifyRequest(Caixas cx)
        {
            AlterarCaixa(cx);
        }
        void SistemaCompartimental_BoxClick(object sender, EventArgs e)
        {
            if (sender is Caixas && statusAtual != StatusPossiveis.Normal)
                SolicitarNovaLinha(sender as Caixas);
        }
        #endregion

        #region Comandos de menus
        private void mnuArquivoNovo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ao iniciar um novo modelo, todas as alterações no modelo atual que não foram salvas, serão perdidas.\nDeseja continuar?", "Novo modelo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                LimparColecao();
                ArquivoAberto = "";
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



        private void mnuArquivoAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = LerSettings("XMLPath");
            openFile.DefaultExt = "xml";
            openFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                LimparColecao();

                ArquivoAberto = openFile.FileName;
                GravarSettings("XMLPath", openFile.FileName);

                DataXML interfaceXML = new DataXML(ArquivoAberto);
                //DataXML interfaceXML = new DataXML(ArquivoAberto, this.PnlCanvas.SistemaCompartimental.Linhas, this.PnlCanvas.SistemaCompartimental.Caixas);
                
                interfaceXML.ImportarXML();

                //Não funciona... 
                //this.PnlCanvas.SistemaCompartimental.Caixas = interfaceXML.Caixas;
                
                foreach (Caixas cx in interfaceXML.Caixas)
                    this.PnlCanvas.IncluirCaixa(cx);

                //aqui funciona
                this.PnlCanvas.SistemaCompartimental.Linhas = interfaceXML.Linhas;

                this.PnlCanvas.Refresh();
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
            Salvar(ArquivoAberto);
        }

        private string SolicitarNomeArquivo()
        {
            string caminhoInicial = LerSettings("XMLPath");
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = caminhoInicial;
            saveFile.DefaultExt = "xml";
            saveFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                GravarSettings("XMLPath", saveFile.FileName);
                return saveFile.FileName;
            }
            else
                return "";
        }

        private void Salvar(string Caminho)
        {
            if (Caminho != "")
            {
                ArquivoAberto = Caminho;
                DataXML interfaceXML = new DataXML(ArquivoAberto);
                interfaceXML.Caixas = this.PnlCanvas.SistemaCompartimental.Caixas;
                interfaceXML.Linhas = this.PnlCanvas.SistemaCompartimental.Linhas;
                interfaceXML.ExportarXML();
            }
        }

        private void mnuArquivoSalvarComo_Click(object sender, EventArgs e)
        {
            string nomeArquivo = "";
            nomeArquivo = SolicitarNomeArquivo();
            Salvar(nomeArquivo);
        }

        private void configurarBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caminhoInicial = LerSettings("MDBPath");

            if (caminhoInicial == "")
                caminhoInicial = "c:\\";

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = caminhoInicial;
            openFile.DefaultExt = "mdb";
            openFile.Filter = "Microsoft Office Access (*.mdb)|*.mdb";
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Configuracoes.Arquivo = openFile.FileName;
                if (openFile.FileName != caminhoInicial)
                    GravarSettings("MDBPath", openFile.FileName);
            }
        }

        private void exibirRótuloDeTransferênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Exibir = !exibirRótuloDeTransferênciasToolStripMenuItem.Checked;
            exibirRótuloDeTransferênciasToolStripMenuItem.Checked = Exibir;
            CompartimentalModel.Configuracoes.ExibirRotulos = Exibir;
            GravarSettings("Rotulos", Exibir.ToString());
        }

        private void mnuArquivoVisualizarImpressao_Click(object sender, EventArgs e)
        {

        }

        private void mnuArquivoImprimir_Click(object sender, EventArgs e)
        {

        }

        private void mnuArquivoSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuEditarLocalizar_Click(object sender, EventArgs e)
        {

        }

        private void mnuEditarPropriedades_Click(object sender, EventArgs e)
        {

        }

        private void mnuEditarExcluir_Click(object sender, EventArgs e)
        {

        }

        private void mnuInserirCaixa_Click(object sender, EventArgs e)
        {
            SolicitarNovaCaixa();
        }

        private void mnuInserirLinha_Click(object sender, EventArgs e)
        {
            SolicitarNovaLinha(null);
        }

        private void mnuFerramentasPersonalizar_Click(object sender, EventArgs e)
        {

        }

        private void mnuFerramentasOpcoes_Click(object sender, EventArgs e)
        {

        }

        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuracoes.Arquivo == null)
            {
                MessageBox.Show("Antes, use a opção \"Configurar banco de dados\" no menu ferramentas", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmModelos F = new frmModelos();
            F.Show();
        }
        #endregion

        /// <summary>
        /// Limpa a coleção e a tela
        /// </summary>
        private void LimparColecao()
        {
            //Limpa a colecao e a tela?
            this.PnlCanvas.SistemaCompartimental.Clear();
            this.PnlCanvas.Controls.Clear();
            this.PnlCanvas.Refresh();
        }
        
        private void SolicitarNovaCaixa()
        {
            this.PnlCanvas.DesmarcarTudo();
            CaixaProp novacaixaForm = new CaixaProp(null);
            System.Windows.Forms.DialogResult dlgResultado = novacaixaForm.ShowDialog();
            if (dlgResultado != System.Windows.Forms.DialogResult.OK)
            {
                novacaixaForm.Dispose();
                novacaixaForm = null;
                return;
            }
            this.PnlCanvas.IncluirCaixa(novacaixaForm.CaixaPropriedades);
            novacaixaForm.Dispose();
            novacaixaForm = null;
        }
        private void AlterarCaixa(Caixas cx)
        {
            this.PnlCanvas.DesmarcarTudo();

            CaixaProp altcaixaForm = new CaixaProp(cx);
            altcaixaForm.ShowDialog();
            altcaixaForm.Dispose();
            altcaixaForm = null;
        }

        private void OperacaoNormal()
        {
            criaLinha1 = null;
            criaLinha2 = null;
            statusAtual = StatusPossiveis.Normal;
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }
        private void SolicitarNovaLinha(Caixas cx)
        {
            switch (statusAtual)
            {
                case StatusPossiveis.SolicitandoLinhaA:
                    if (cx != null)
                    {
                        criaLinha1 = cx;
                        statusAtual = StatusPossiveis.SolicitandoLinhaB;
                    }
                    else
                    {
                        OperacaoNormal();
                    }
                    break;
                case StatusPossiveis.SolicitandoLinhaB:
                    if (cx != null && cx != criaLinha1)
                    {
                        criaLinha2 = cx;
                        this.PnlCanvas.DesmarcarTudo();

                        // Criar linha
                        LinhaProp novalinhaForm = new LinhaProp(Linhas.CorPadrao, criaLinha1, criaLinha2);
                        System.Windows.Forms.DialogResult dlgResultado = novalinhaForm.ShowDialog();
                        if (dlgResultado != System.Windows.Forms.DialogResult.OK)
                        {
                            novalinhaForm.Dispose();
                            novalinhaForm = null;
                            return;
                        }
                        this.PnlCanvas.IncluirLinha(novalinhaForm.LinhaPropriedades);
                        novalinhaForm.Dispose();
                        novalinhaForm = null;
                        OperacaoNormal();
                    }
                    else if (cx == null)
                    {
                        OperacaoNormal();
                    }
                    break;
                case StatusPossiveis.Normal:
                default:
                    criaLinha1 = null;
                    criaLinha2 = null;
                    statusAtual = StatusPossiveis.SolicitandoLinhaA;
                    this.PnlCanvas.DesmarcarTudo();
                    Cursor.Current = Cursors.Cross;
                    break;
            }
        }



        private string LerSettings(string Chave)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Chave];
        }

        private void GravarSettings(string Chave, string Valor)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[Chave].Value = Valor;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }



    }
}