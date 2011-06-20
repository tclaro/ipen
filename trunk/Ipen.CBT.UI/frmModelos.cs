using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ipen.CompartimentalModel;


namespace Ipen.CBT.UI
{
    public partial class frmModelos : Form
    {
        int _idModelo = 0;

        public frmModelos()
        {
            InitializeComponent();
            carregarModelos();
        }

        public int idModelo
        {
            get
            {
                return _idModelo;
            }
        }

        
        private void carregarModelos()
        {
            //Modelos modelo = new Modelos();
            DataTable dt = DataBD.SelecionarModelos();
            dgvModelos.DataSource = null;
            dgvModelos.DataSource = dt;
            dgvModelos.Columns["idModelo"].HeaderText = "Código";
            dgvModelos.Columns["idModelo"].Width = 50;
            dgvModelos.Columns["nmModelo"].HeaderText = "Nome";
            dgvModelos.Columns["dtAlteracao"].HeaderText = "Última Alteração";
            dgvModelos.Columns["Descricao"].HeaderText = "Descrição";
            dgvModelos.Columns["nmTipoModelo"].HeaderText = "Tipo";
            dgvModelos.Columns["meiaVida"].HeaderText = "Meia Vida";
            dgvModelos.Columns["meiaVida"].Width = 90;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Pega o código da linha selecionada
            _idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Pega o código da linha selecionada
            int idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;
            string nmModelo = dgvModelos.CurrentRow.Cells["nmModelo"].Value.ToString();

            DialogResult Resposta = MessageBox.Show("Tem certeza que deseja excluir o modelo " + nmModelo + "?\nEsta operação não poderá ser desfeita", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Resposta == DialogResult.No)
                return;

            DataBD.RemoverModelo(idModelo);
            carregarModelos();
        }
    }
}