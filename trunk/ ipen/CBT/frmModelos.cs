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
    public partial class frmModelos : Form
    {
        public frmModelos()
        {
            InitializeComponent();
            carregarModelos();
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
            bool modoGrafico = chkModoGrafico.Checked;
            if (!modoGrafico)
            {
                frmEditModelo F = new frmEditModelo();
                F.ShowDialog();
                carregarModelos();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Pega o código da linha selecionada
            int idModelo = (int)dgvModelos.CurrentRow.Cells["idModelo"].Value;

            //Passa pro form ja trazer o modelo carregado
            frmEditModelo F = new frmEditModelo(idModelo);
            F.ShowDialog();
            carregarModelos();
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