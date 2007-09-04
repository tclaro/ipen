using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CompartimentalModel;

namespace CBT
{
    public partial class frmEditModelo : Form
    {

        private Modelos Modelo;
        private Caixas CaixaAlterando;

        private CaixasCollection colCaixasA = new CaixasCollection();
        private CaixasCollection colCaixasB = new CaixasCollection();

        #region Construtores
        public frmEditModelo()
        {
            InitializeComponent();
            Modelo = new Modelos();
        }

        public frmEditModelo(int idModelo)
        {
            InitializeComponent();
            Modelo = DataBD.SelecionarModelos(idModelo);
            CarregarTela();
            LimparTelaLigacao();
        }
        #endregion

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
            this.dlgCor.Color = this.btnCorComp.BackColor;
            this.dlgCor.ShowDialog();
            this.btnCorComp.BackColor = this.dlgCor.Color;
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
        {
            if (tabControlModelo.SelectedTab.Name == "tabPageCompart")
                lblContador.Text = "Total: " + Modelo.Colecao.Caixas.Count.ToString() + " compartimentos";
            else
                lblContador.Text = "Total: " + lvwLigacoes.Items.Count.ToString() + " transferências em " + Modelo.Colecao.Linhas.Count.ToString() + " ligações";
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

        private void chkAcompanhar_CheckedChanged(object sender, EventArgs e)
        {

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




    }
}