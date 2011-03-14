using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ipen.CompartimentalModel;


namespace Ipen.SSID.UI
{
    public partial class frmModelos : Form
    {
        frmCalculo FormOrigem;

        public frmModelos(string Arquivo, frmCalculo F)
        {
            InitializeComponent();
            carregarModelos(Arquivo);
            FormOrigem = F;
        }

        
        private void carregarModelos(string Arquivo)
        {
            Modelos modelo = new Modelos();
            Configuracoes.Arquivo = Arquivo;
            DataTable dt = DataBD.SelecionarModelos();
            dgvModelos.DataSource = null;
            dgvModelos.DataSource = dt;
            dgvModelos.Columns["idModelo"].HeaderText = "Código";
            dgvModelos.Columns["nmModelo"].HeaderText = "Nome";
            dgvModelos.Columns["dtAlteracao"].HeaderText = "Última Alteração";
            dgvModelos.Columns["Descricao"].HeaderText = "Descrição";
            dgvModelos.Columns["nmTipoModelo"].HeaderText = "Tipo";
            dgvModelos.Columns["meiaVida"].HeaderText = "Meia Vida";
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Pega o código da linha selecionada
            int idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;
            //FormOrigem.LerModelo(idModelo);
            FormOrigem.idModeloAberto = idModelo;
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}