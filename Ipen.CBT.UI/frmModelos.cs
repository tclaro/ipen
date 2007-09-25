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
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Pega o c�digo da linha selecionada
            _idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Pega o c�digo da linha selecionada
            int idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;
            string nmModelo = dgvModelos.CurrentRow.Cells["nmModelo"].Value.ToString();

            DialogResult Resposta = MessageBox.Show("Tem certeza que deseja excluir o modelo " + nmModelo + "?\nEsta opera��o n�o poder� ser desfeita", "Aten��o", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Resposta == DialogResult.No)
                return;

            DataBD.RemoverModelo(idModelo);
            carregarModelos();

        }
    }
}