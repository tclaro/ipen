using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Ipen.SSID.UI
{
    public partial class frmGrafico : Form
    {
        public frmGrafico()
        {
            InitializeComponent();
        }

        private void SetSize()
        {
            zg1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zg1.Size = new Size(ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }

        

        public void CreateChart(GraphPane pane)
        {
            ZedGraphControl zgc = zg1;
            GraphPane myPane = pane;
            zgc.GraphPane = myPane;
            
            zgc.IsShowPointValues = true;

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();

            SetSize();
        }

        private void frmGrafico_Resize(object sender, EventArgs e)
        {
            SetSize();
        }
    }
}