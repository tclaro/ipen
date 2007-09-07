using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace SSID
{
    public partial class frmGrafico : Form
    {
        public frmGrafico()
        {
            InitializeComponent();
        }

        private void frmGrafico_Load(object sender, EventArgs e)
        {
            // Setup the graph
            //CreateChart(zg1);
            // Size the control to fill the form with a margin
            //SetSize();
        }

        private void SetSize()
        {
            zg1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
            zg1.Size = new Size(ClientRectangle.Width - 20,
                                    ClientRectangle.Height - 20);
        }

        

        public void CreateChart(GraphPane pane)
        {
            ZedGraphControl zgc = zg1;
            GraphPane myPane = pane;
            zgc.GraphPane = myPane;
            zgc.IsShowCursorValues = true;

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