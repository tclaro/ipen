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
            this.splitContainer1.Panel2.Controls.Add(this.PnlCanvas);
            this.splitContainer1.Panel1.Enabled = false;
            AjustarRotulos();

            btnCorComp.BackColor = Caixas.CorPadrao;
            btnCorLig.BackColor = Linhas.CorPadrao;

            ConfigurarListView();

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

                //Não funciona... pq precisa passar pelo método "IncluirCaixa()"
                //this.PnlCanvas.SistemaCompartimental.Caixas = interfaceXML.Caixas;
                
                foreach (Caixas cx in interfaceXML.Caixas)
                    this.PnlCanvas.IncluirCaixa(cx);

                //aqui funciona
                //this.PnlCanvas.SistemaCompartimental.Linhas = interfaceXML.Linhas;

                foreach (Linhas ln in interfaceXML.Linhas)
                    this.PnlCanvas.IncluirLinha(ln);

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
            LerTipoModelosMdb();

        }

        private void LerTipoModelosMdb()
        {
            DataTable dt = DataBD.SelecionarTipos();

            cboTipo.DataSource = dt;
            cboTipo.DisplayMember = "nmTipoModelo";
            cboTipo.ValueMember = "idTipoModelo";
            
        }

        private void exibirRótuloDeTransferênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Exibir = !exibirRótuloDeTransferênciasToolStripMenuItem.Checked;
            exibirRótuloDeTransferênciasToolStripMenuItem.Checked = Exibir;
            CompartimentalModel.Configuracoes.ExibirRotulos = Exibir;
            this.PnlCanvas.Refresh();
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
            frmModelos Fmodelo = new frmModelos();
            DialogResult dlgForm = Fmodelo.ShowDialog();

            if (dlgForm == DialogResult.OK)
            {
                frmEditModelo Fedit = new frmEditModelo();
                Fedit.ShowDialog();
            }
            else if (dlgForm == DialogResult.Yes)
            {
                frmEditModelo Fedit = new frmEditModelo(Fmodelo.idModelo);
                Fedit.ShowDialog();
            }
        }

        private void modelosGraficamenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuracoes.Arquivo == null)
            {
                MessageBox.Show("Antes, use a opção \"Configurar banco de dados\" no menu ferramentas", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmModelos Fmodelo = new frmModelos();
            DialogResult dlgForm = Fmodelo.ShowDialog();

            
            if (dlgForm == DialogResult.OK)
            {
                //novo
                LimparColecao();
            }
            
            else if (dlgForm == DialogResult.Yes)
            {
                //alterar
    
            }
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

        #region novo menu

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modelo = new Modelos();
            this.splitContainer1.Panel1.Enabled = true;
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuracoes.Arquivo == null)
            {
                MessageBox.Show("Antes, use a opção \"Configurar banco de dados\" no menu ferramentas", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frmModelos Fmodelo = new frmModelos();
            DialogResult dlgForm = Fmodelo.ShowDialog();

            if (dlgForm == DialogResult.Yes)
            {
                this.splitContainer1.Panel1.Enabled = true;
                Modelo = DataBD.SelecionarModelos(Fmodelo.idModelo);
                CarregarTela();
                LimparTelaLigacao();
            }
        }
        #endregion

        #region Código do form editar modelo

        private Modelos Modelo;

        private Caixas CaixaAlterando;

        private CaixasCollection colCaixasA = new CaixasCollection();
        private CaixasCollection colCaixasB = new CaixasCollection();

        #region Métodos dos Compartimentos

        private void lstCompartimentos_DoubleClick(object sender, EventArgs e)
        {
            int indice = lstCompartimentos.SelectedIndex;
            if (indice < 0)
                return;

            CaixaAlterando = (Caixas)lstCompartimentos.SelectedItem;

            txtCompartimento.Text = CaixaAlterando.Nome;
            chkAcompanhar.Checked = CaixaAlterando.Acompanhar;
            chkEliminacao.Checked = CaixaAlterando.Eliminacao;
            btnCorComp.BackColor = CaixaAlterando.BackColor;
            chkIncorporacao.Checked = CaixaAlterando.Incorporacao;
            txtFracao.Text = CaixaAlterando.Fracao.ToString();
            btnAddComp.Text = "&Alterar";
        }

        private void cmdLimparComp_Click(object sender, EventArgs e)
        {

            LimparTelaCompartimento();
        }

        private void btnCorComp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog dlgCor = new ColorDialog();
            dlgCor.Color = this.btnCorComp.BackColor;
            dlgCor.ShowDialog();
            this.btnCorComp.BackColor = dlgCor.Color;
        }

        private void btnAddComp_Click(object sender, EventArgs e)
        {
            string NomeComp = txtCompartimento.Text.Trim();
            bool ModoAlterar = (btnAddComp.Text == "&Alterar");

            if (NomeComp == "")
            {
                MessageBox.Show("Informe o nome do compartimento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCompartimento.Focus();
                return;
            }

            System.Drawing.Color Cor = btnCorComp.BackColor;
            bool Acompanhar = chkAcompanhar.Checked;
            bool Eliminacao = chkEliminacao.Checked;
            bool Incorporacao = chkIncorporacao.Checked;
            double Fracao = double.Parse(txtFracao.Text);

            foreach (Caixas item in Modelo.Colecao.Caixas)
                if (item.Nome == NomeComp)
                {
                    if (!(ModoAlterar && item.Numero == CaixaAlterando.Numero))
                    {
                        MessageBox.Show("Ja existe um compartimento chamado \"" + NomeComp + "\"", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCompartimento.Focus();
                        return;
                    }
                }

            if (ModoAlterar)
            {
                CaixaAlterando.Acompanhar = Acompanhar;
                CaixaAlterando.Eliminacao = Eliminacao;
                CaixaAlterando.Nome = NomeComp;
                CaixaAlterando.BackColor = Cor;
                CaixaAlterando.Incorporacao = Incorporacao;
                CaixaAlterando.Fracao = Fracao;
            }
            else
            {
                Caixas caixa = new Caixas(NomeComp, Cor, Acompanhar, Eliminacao, Incorporacao, Fracao);
                Modelo.Colecao.Caixas.Add(caixa);
            }
            RefazBind();
            LimparTelaCompartimento();
        }

        private void LimparTelaCompartimento()
        {
            //Reset na tab do compartimento
            txtCompartimento.Text = "";
            chkAcompanhar.Checked = false;
            chkEliminacao.Checked = false;
            chkIncorporacao.Checked = false;
            txtFracao.Text = "0";
            txtCompartimento.Focus();
            btnCorComp.BackColor = Caixas.CorPadrao;
            btnAddComp.Text = "&Adicionar";
            CaixaAlterando = null;
        }

        private void btnRemComp_Click(object sender, EventArgs e)
        {
            int Item = lstCompartimentos.SelectedIndex;
            if (Item < 0)
                return;

            Caixas CaixaRemovendo = (Caixas)lstCompartimentos.SelectedItem;

            int quantLigacoes = Modelo.Colecao.ContarLigacoesAssociadas(CaixaRemovendo);
            if ( quantLigacoes > 0)
            {
                DialogResult resposta = MessageBox.Show("Existem " + quantLigacoes.ToString() + " ligações associadas a este compartimento que também serão removidas\nDeseja continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta == DialogResult.No)
                    return;

                Modelo.Colecao.Linhas.Remove(CaixaRemovendo);
            }

            Modelo.Colecao.Caixas.RemoveAt(Item);
            RefazBind();
            AtualizarListView();
        }

        #endregion

        #region Métodos das Ligações
        
        private void ConfigurarListView()
        {
            lvwLigacoes.FullRowSelect = true;
            lvwLigacoes.View = View.Details;
            lvwLigacoes.MultiSelect = false;
            lvwLigacoes.HideSelection = true;
            lvwLigacoes.LabelEdit = false;
            lvwLigacoes.LabelWrap = false;
            lvwLigacoes.Columns.Add("", 0);
            lvwLigacoes.Columns.Add("Compartimento Saída", 185, HorizontalAlignment.Right);
            lvwLigacoes.Columns.Add("", 30, HorizontalAlignment.Center);
            lvwLigacoes.Columns.Add("Compartimento Entrada", 185, HorizontalAlignment.Left);
            lvwLigacoes.Columns.Add("Valor de Transferência", 150, HorizontalAlignment.Left);
        }

        private void LimparTelaLigacao()
        {
            btnCorLig.BackColor = Linhas.CorPadrao;
            txtValorAB.Text = "";
            cboCompartA.SelectedItem = null;
            cboCompartB.SelectedItem = null;
            cboCompartA.Focus();
        }

        private void btnAddLig_Click(object sender, EventArgs e)
        {
            Linhas Ligacao = null;

            if (!ChecarLigacoesValidas())
                return;

            float Valor;
            if (!float.TryParse(txtValorAB.Text, out Valor))
            {
                MessageBox.Show("Valor " + txtValorAB.Text + " Inválido", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtValorAB.Focus();
                return;
            }

            Linhas LinhaExistente = Modelo.Colecao.ObterLinhaPorCaixas((Caixas)cboCompartA.SelectedItem, (Caixas)cboCompartB.SelectedItem);
            
            //linha existente?
            if (LinhaExistente != null)
            {
                //Existe no Mesmo Sentido?
                if ((((Caixas)cboCompartA.SelectedItem).Nome == LinhaExistente.CaixaInicio.Nome 
                    && LinhaExistente.DirecaoDaLinha == Linhas.Direcao.InicioParaFim) 
                    || LinhaExistente.DirecaoDaLinha == Linhas.Direcao.Ambos)
                {
                    //Alterar?
                    DialogResult Resposta = MessageBox.Show("Já existe uma ligação entre esses compartimentos, deseja alterá-lo?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Resposta == DialogResult.No)
                        //Não Alterar
                        return; //Fim
                    else
                    {//Sim, alterar!

                        //A linha esta no mesmo sentido da tela?
                        if (LinhaExistente.CaixaInicio.Nome == ((Caixas)cboCompartA.SelectedItem).Nome)
                            LinhaExistente.ValorAB = Valor;
                        else
                            LinhaExistente.ValorBA = Valor;
                    }
                }
                else
                {
                    //Existe apenas em Sentido diferente
                    LinhaExistente.DirecaoDaLinha = Linhas.Direcao.Ambos;
                    LinhaExistente.ValorBA = Valor;
                }
                
                LinhaExistente.BackColor = btnCorLig.BackColor;
            }
            else
            {
                //Nova Linha
                Ligacao = new Linhas();
                Ligacao.CaixaInicio = (Caixas)cboCompartA.SelectedItem;
                Ligacao.CaixaFim = (Caixas)cboCompartB.SelectedItem;
                Ligacao.BackColor = btnCorLig.BackColor;
                Ligacao.DirecaoDaLinha = Linhas.Direcao.InicioParaFim;
                Ligacao.ValorAB = Valor;
                Modelo.Colecao.Linhas.Add(Ligacao);
            }

            AtualizarListView();
            LimparTelaLigacao();

        }

        private void AtualizarListView()
        {
            lvwLigacoes.Items.Clear();
            ListViewItem item;

            foreach (Linhas ln in Modelo.Colecao.Linhas)
            {

                if (ln.DirecaoDaLinha == Linhas.Direcao.InicioParaFim || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                {
                    item = lvwLigacoes.Items.Add("");
                    item.SubItems.Add(ln.CaixaInicio.Nome);
                    item.SubItems.Add("--->");
                    item.SubItems.Add(ln.CaixaFim.Nome);
                    item.SubItems.Add(ln.ValorAB.ToString("E02"));
                    item.Tag = ln;
                }
                if (ln.DirecaoDaLinha == Linhas.Direcao.FimParaInicio || ln.DirecaoDaLinha == Linhas.Direcao.Ambos)
                {
                    item = lvwLigacoes.Items.Add("");
                    item.SubItems.Add(ln.CaixaFim.Nome);
                    item.SubItems.Add("--->");
                    item.SubItems.Add(ln.CaixaInicio.Nome);
                    item.SubItems.Add(ln.ValorBA.ToString("E02"));
                    item.Tag = ln;
                }
            }

            AtualizarContador();
        }
        private void lvwLigacoes_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = lvwLigacoes.SelectedItems[0];

            if (item == null)
                return;

            Linhas Ln = (Linhas)item.Tag;
            if (Ln.CaixaInicio.Nome == item.SubItems[1].Text)
            {
                cboCompartA.SelectedValue = Ln.CaixaInicio.Numero;
                cboCompartB.SelectedValue = Ln.CaixaFim.Numero;
                txtValorAB.Text = Ln.ValorAB.ToString();
            }
            else
            {
                cboCompartA.SelectedValue = Ln.CaixaFim.Numero;
                cboCompartB.SelectedValue = Ln.CaixaInicio.Numero;
                txtValorAB.Text = Ln.ValorBA.ToString();
            }
        }

        private void btnCorLig_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog dlgCor = new ColorDialog();
            dlgCor.Color = btnCorLig.BackColor;
            dlgCor.ShowDialog();
            btnCorLig.BackColor = dlgCor.Color;
        }

        private void cmdLimparLig_Click(object sender, EventArgs e)
        {
            LimparTelaLigacao();
        }

        private void btnRemLig_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection Items = lvwLigacoes.SelectedItems;
            if (Items == null || Items.Count < 1)
            {
                MessageBox.Show("Selecione abaixo a ligação que deseja remover", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ListViewItem Item = Items[0];
            Linhas Ln = (Linhas)Item.Tag;

            Linhas LinhaExistente = Modelo.Colecao.ObterLinhaPorCaixas(Ln.CaixaInicio, Ln.CaixaFim);
            if (LinhaExistente == null)
            {
                MessageBox.Show("Unexpected ERROR!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Ln.DirecaoDaLinha != Linhas.Direcao.Ambos)
                Modelo.Colecao.Linhas.Remove(Ln);
            else
                if (Ln.CaixaInicio.Nome == Item.SubItems[1].Text)
                    Ln.DirecaoDaLinha = Linhas.Direcao.FimParaInicio;
                else
                    Ln.DirecaoDaLinha = Linhas.Direcao.InicioParaFim;

            AtualizarListView();
        }

        private bool ChecarLigacoesValidas()
        {

            if (cboCompartA.SelectedItem == null || cboCompartB.SelectedItem == null)
            {
                MessageBox.Show("Selecione dois compartimentos para estabelecer uma ligação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCompartA.Focus();
                return false;
            }

            if (cboCompartA.SelectedItem == cboCompartB.SelectedItem)
            {
                MessageBox.Show("Selecione compartimentos diferentes para estabelecer uma ligação", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCompartA.Focus();
                return false;
            }

            if (txtValorAB.Text == "")
            {
                MessageBox.Show("Informe o valor de transferência para a ligação A->B", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtValorAB.Focus();
                return false;
            }
 
            return true;
        }

        private void cboCompartimentoAouB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Linhas LinhaExistente = Modelo.Colecao.ObterLinhaPorCaixas((Caixas)cboCompartA.SelectedItem, (Caixas)cboCompartB.SelectedItem);
            if (LinhaExistente != null)
            {
                if (((Caixas)cboCompartA.SelectedItem).Nome == LinhaExistente.CaixaInicio.Nome)
                    txtValorAB.Text = LinhaExistente.ValorAB.ToString();
                else
                    txtValorAB.Text = LinhaExistente.ValorBA.ToString();
                btnCorLig.BackColor = LinhaExistente.BackColor;
            }
            else
            {
                txtValorAB.Text = "";
                btnCorLig.BackColor = Linhas.CorPadrao;
            }
        }

        #endregion

        #region Métodos Genéricos do Form

        /// <summary>
        /// Só é executado se chegou nesse form pelo "Alterar", então o objeto Modelo ja tem coisa lá
        /// </summary>
        private void CarregarTela()
        {
            //Ei, sr. modelo, pega la suas caixas e linhas...
            Modelo.PreencherCaixasLinhas();

            //Agora preenche a list e as 2 combos com as caixas...
            RefazBind();

            //e a listview com as linhas!
            AtualizarListView();

            //Nome do modelo no alto da tela por favor...
            txtNome.Text = Modelo.nmModelo;
            txtDescricao.Text = Modelo.Descricao;

        }

        private void SincronizarColecoes()
        {
            /* Clona a collection de compartimentos para fazer bind 
            nas comboboxes de ligacões */
            colCaixasA.Clear();
            colCaixasB.Clear();

            foreach (Caixas C in Modelo.Colecao.Caixas)
                this.colCaixasA.Add(C);

            foreach (Caixas C in Modelo.Colecao.Caixas)
                this.colCaixasB.Add(C);
        }

        private void RefazBind()
        {
            SincronizarColecoes();

            lstCompartimentos.DataSource = null;
            lstCompartimentos.DataSource = Modelo.Colecao.Caixas;
            lstCompartimentos.DisplayMember = "Nome";
            lstCompartimentos.ValueMember = "Numero";
            cboCompartA.DataSource = null;
            cboCompartA.DataSource = this.colCaixasA;
            cboCompartA.DisplayMember = "Nome";
            cboCompartA.ValueMember = "Numero";
            cboCompartB.DataSource = null;
            cboCompartB.DataSource = this.colCaixasB;
            cboCompartB.DisplayMember = "Nome";
            cboCompartB.ValueMember = "Numero";

            AtualizarContador();
        }

        private void frmEditModelo_Load(object sender, EventArgs e)
        {
            btnCorComp.BackColor = Caixas.CorPadrao;
            btnCorLig.BackColor = Linhas.CorPadrao;

            ConfigurarListView();
          
        }

        private void btnGamb_Click(object sender, EventArgs e)
        {
            //O botão default está escondido, e desempenha ação diferente
            //de acordo com a tab que está selecionada
            if (tabControlModelo.SelectedTab.Text == "Compartimentos")
                btnAddComp_Click(sender, e);
            else
                btnAddLig_Click(sender, e);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AtualizarContador()
        {// Temporariamente desligado

            //if (tabControlModelo.SelectedTab.Name == "tabPageCompart")
            //    lblContador.Text = "Total: " + Modelo.Colecao.Caixas.Count.ToString() + " compartimentos";
            //else
            //    lblContador.Text = "Total: " + lvwLigacoes.Items.Count.ToString() + " transferências em " + Modelo.Colecao.Linhas.Count.ToString() + " ligações";
        }

        private void tabControlModelo_Selected(object sender, TabControlEventArgs e)
        {
            AtualizarContador();

            //Mudou de aba, limpamos a tela e principalmente os objetos "alterando"
            //pra nao correr risco de mudar algo na outra e voltar pra terminar
            LimparTelaLigacao();
            LimparTelaCompartimento();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            this.Modelo.nmModelo = txtNome.Text;
            this.Modelo.Descricao = txtDescricao.Text;
            DataBD.GravarModelo(this.Modelo);
            this.Close();
        }

        private void chkIncorporacao_CheckedChanged(object sender, EventArgs e)
        {
            bool Habilitado = chkIncorporacao.Checked;
            lblFracao.Enabled = Habilitado;
            txtFracao.Enabled = Habilitado;
            if (!Habilitado)
                txtFracao.Text = "0";
        }

        #endregion

     

        #endregion

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == this.Modelo.nmModelo)
            {
                MessageBox.Show("Altere o nome do modelo para criar uma cópia dele", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            this.Modelo.idModelo = 0;
            this.Modelo.dtCriacao = System.DateTime.Now;
            Salvar();
        }

        private void Salvar()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite um nome para este modelo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Modelo.nmModelo = txtNome.Text;
            this.Modelo.Descricao = txtDescricao.Text;
            DataBD.GravarModelo(this.Modelo);
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            this.PnlCanvas.SistemaCompartimental = this.Modelo.Colecao;
            foreach (Caixas cx in this.Modelo.Colecao.Caixas)
            {
                this.PnlCanvas.Controls.Add(cx);
                cx.BringToFront();
                this.PnlCanvas.VerificarCaixasSobrepostas(cx);
                this.PnlCanvas.Refresh();
            }
        }
    }
}