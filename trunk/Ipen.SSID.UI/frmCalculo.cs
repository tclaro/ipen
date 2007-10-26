using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ZedGraph;
using System.Configuration;

namespace Ipen.SSID.UI
{
    public partial class Form1 : Form
    {

        public int idModeloAberto;
        private ArrayList ModeloCompartimental = new ArrayList();
        private CompartimentalModel.CaixasCollection TodosCompartimentos = new CompartimentalModel.CaixasCollection();

        int n, indice;
        double[,] sum, a, R, term, b;
        double[] xt, xo, u;
        double[,] q, qi;
        double max, lam, terr;
        int Tempo;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {

            
            int Final =0 , Passo =0;
            double MeiaVida =0 ;
            try
            {
                Final = Convert.ToInt32(txtTempo.Text);
                Passo = Convert.ToInt32(txtPasso.Text);
                MeiaVida = Convert.ToDouble(txtMeiaVida.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conversão\n" + ex.Message, "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Muda cursor pra ampulheta
            this.Cursor = Cursors.WaitCursor;
            DateTime startTime = DateTime.Now;

            Final = Final / Passo;
            Tempo = 0;
            
            if (MeiaVida > 0)
                lam = Math.Log(2) / MeiaVida; //lambda R
            else
                lam = 0;

            terr = 1E-10;
            double QuantAnt = 0;

            LerModelo(idModeloAberto);
            Init();

            StringBuilder str = new StringBuilder();

            Object[] ListaDeParesDePontos = new Object[n];
            str.Append ("Tempo");

            foreach (CompartimentalModel.Caixas C in TodosCompartimentos)
            {
                if (C.Acompanhar)
                {
                    PointPairList list = new PointPairList();
                    ListaDeParesDePontos[(int)C.Tag] = list;
                    str.Append("\t");
                    str.Append(C.Nome);
                }
            }

            str.Append("\t");
            str.Append("Total");
            str.Append("\n");

            for (int T = 0; T <= Final + 1; T++)
            {

                Calculo();


                double SomaCompartimentos = 0;
                str.Append(Tempo.ToString());

                foreach (CompartimentalModel.Caixas C in TodosCompartimentos)
                {
                    indice = (int)C.Tag;

                    SomaCompartimentos += xt[indice];

                    if (C.Acompanhar)
                    {
                        
                        double valorInstanteCompartimento = 0d;

                        if (C.Eliminacao)
                        {
                            valorInstanteCompartimento = xt[indice] - QuantAnt;
                            QuantAnt = xt[indice];
                        }
                        else
                            valorInstanteCompartimento = xt[indice];

                        str.Append("\t");
                        str.Append(valorInstanteCompartimento.ToString("e10"));

                        ((PointPairList)ListaDeParesDePontos[indice]).Add(T, valorInstanteCompartimento);


                        
                    }
                }

                str.Append("\t");
                str.Append(SomaCompartimentos.ToString("e10"));
                str.Append("\n");
                Tempo = Tempo + Passo;
                
            }

            this.Cursor = Cursors.Default;
            DateTime stopTime = DateTime.Now;
            TimeSpan Duration = stopTime - startTime;
            lblTempoDecorrido.Text = Duration.Hours.ToString("00") + ":" + Duration.Minutes.ToString("00") + ":" + Duration.Seconds.ToString("00") + ":" + Duration.Milliseconds.ToString("000");


            txtSaida.Text = str.ToString();

            GraphPane pane = CreateChart(ListaDeParesDePontos, zedGraphControl1);
            GraphPane paneCopia = pane.Clone();
            frmGrafico FormGrafico = new frmGrafico();
            FormGrafico.CreateChart(paneCopia);
            FormGrafico.Show();

        }
        
        private GraphPane CreateChart(Object[] coisas, ZedGraphControl zgc)
        {
            
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles and axis labels
            myPane.Title.Text = "Análise Compartimental";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Quantidade";

            
            for (int i =1; i<n;i++)
            {
                PointPairList MinhaLista = (PointPairList)coisas[i];
                CompartimentalModel.Caixas C = TodosCompartimentos[i-1];

                if (C.Acompanhar)
                {
                    //LineItem myCurve2 = myPane.AddCurve("Compartimento " + i.ToString(), MinhaLista, RetornarCor(i), SymbolType.None);
                    LineItem minhaCurva = myPane.AddCurve(C.Nome, MinhaLista, C.BackColor, SymbolType.None);
                    // Fill the area under the curve with a white-red gradient at 45 degrees
                    //minhaCurva.Line.Fill = new Fill(C.BackColor, Color.White, 45F);
                    // Make the symbols opaque by filling them with white
                    //minhaCurva.Symbol.Fill = new Fill(Color.White);
                }
            }

            // Set the XAxis type
            myPane.XAxis.Type = AxisType.Log;
            myPane.YAxis.Type = AxisType.Log;


            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            return myPane;
        }

        private void Init()
        {
           
            sum = new double[n, n];
            a = new double[n, n];
            term = new double[n, n];
            b = new double[n, n];

            xt = new double[n];
            xo = new double[n];
            u = new double[n];

            q = new double[2 * n, 2 * n];
            qi = new double[2 * n, 2 * n];
        }

        private void Calculo()
        {
           
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    sum[i, j] = 0;
                    a[i, j] = R[j, i];
                }
            }

            for (int i = 1; i < n; i++)
            {
                a[i, i] = -lam;
                for (int k = 1; k < n; k++)
                    if (k != i)
                        a[i, i] = a[i, i] - R[i, k];
            }

            for (int i = 1; i < n; i++)
            {
                xo[i] = R[i, i];
                sum[i, i] = 1;
                term[i, i] = 1;
            }

            for (int i = 1; i < n; i++)
                for (int j = 1; j < n; j++)
                {
                    q[i, j] = a[i, j];
                    a[i, j] = a[i, j] * Tempo;
                }

            max = 0;

            for (int i = 1; i < n; i++)
                if (a[i, i] < max)
                    max = a[i, i];

            int iz;
            for (iz = 0; iz <= 1000; iz++)
                if (-max / (double)Math.Exp(Math.Log(2) * iz) < 0.2)
                    goto Fora;

        Fora:

            for (int i = 1; i < n; i++)
                for (int j = 1; j < n; j++)
                    a[i, j] = a[i, j] / (double)(Math.Exp(Math.Log(2) * iz));

            for (int ir = 1; ir <= 10000; ir++)
            {
                for (int j = 1; j < n; j++)
                    for (int i = 1; i < n; i++)
                    {
                        b[i, j] = 0;
                        for (int k = 1; k < n; k++)
                            b[i, j] = b[i, j] + term[i, k] * a[k, j];
                    }

                for (int i = 1; i < n; i++)
                    for (int j = 1; j < n; j++)
                    {
                        term[i, j] = b[i, j] / ir;
                        sum[i, j] = sum[i, j] + term[i, j];
                    }

                for (int i = 1; i < n; i++)
                    for (int j = 1; j < n; j++)
                    {
                        if (sum[i, j] != 0)
                            if (term[i, j] / sum[i, j] > terr)
                                goto volta;
                    }
                goto exit;

            volta:
                int xxx = 0;
            }
        exit:

            for (int id = 1; id < iz + 1; id++)
            {
                for (int i = 1; i < n; i++)
                    for (int j = 1; j < n; j++)
                    {
                        a[i, j] = 0;
                        for (int k = 1; k < n; k++)
                            a[i, j] = a[i, j] + sum[i, k] * sum[k, j];
                    }

                for (int i = 1; i < n; i++)
                    for (int j = 1; j < n; j++)
                        sum[i, j] = a[i, j];
            }

            if (lam != 0)
                Inversao();

            for (int i = 1; i < n; i++)
            {
                xt[i] = 0;
                for (int k = 1; k < n; k++)
                    xt[i] = xt[i] + sum[i, k] * xo[k];
            }

            if (lam != 0)
            {
                for (int i = 1; i < n; i++)
                    sum[i, i] = sum[i, i] - 1;

                for (int i = 1; i < n; i++)
                    for (int j = 1; j < n; j++)
                    {
                        b[i, j] = 0;
                        for (int id = 1; id < n; id++)
                            b[i, j] = b[i, j] + qi[i, id] * sum[id, j];
                    }

                for (int i = 1; i < n; i++)
                {
                    u[i] = 0;
                    for (int j = 1; j < n; j++)
                        u[i] = u[i] + b[i, j] * xo[j];
                    u[i] = u[i] * lam;
                }
            }
        }

        private void Inversao()
        {
            for (int i1 = 1; i1 < n; i1++)
            {
                for (int j1 = n; j1 < 2 * n; j1++)
                    q[i1, j1] = 0;
                q[i1, i1 + (n - 1)] = 1;
            }

            for (int ip1 = 1; ip1 < n; ip1++)
            {
                for (int k1 = 1; k1 < n; k1++)
                {

                    qi[ip1, k1] = q[ip1, k1 + ip1] / (q[ip1, ip1]);
                    qi[ip1, k1] = qi[ip1, k1];
                }

                for (int j1 = 1; j1 < n; j1++)
                {
                    if (ip1 == j1)
                        goto desvio;
                    for (int k1 = 1; k1 < n; k1++)

                        qi[j1, k1] = q[j1, k1 + ip1] - q[j1, ip1] * qi[ip1, k1];

                desvio:
                    int xxx = 1;
                }
                for (int j1 = 1; j1 < n; j1++)
                    for (int k1 = 1; k1 < n; k1++)
                        q[j1, k1 + ip1] = qi[j1, k1];
            }
        }
        
        public void ImportarArquivo(string Arquivo)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(Arquivo, System.Data.XmlReadMode.ReadSchema);
        }

        public void LerModelo(int idModelo)
        {
            CompartimentalModel.Modelos Modelo = new CompartimentalModel.Modelos();
            Modelo.idModelo = idModelo;
            Modelo.PreencherCaixasLinhas();
            
            int Tamanho = Modelo.Colecao.Caixas.Count + 1;  //+ 1 pq o elemento zero nao é utilizado

            n = Tamanho;
            R = new double[n, n];

            TodosCompartimentos.Clear();
            int Contador = 1;

            foreach (CompartimentalModel.Caixas Caixa in Modelo.Colecao.Caixas)
            {
                Caixa.Tag = Contador++;

                if (Caixa.Incorporacao)
                    R[(int)Caixa.Tag, (int)Caixa.Tag] = Caixa.Fracao;

                TodosCompartimentos.Add(Caixa);
            }
            TodosCompartimentos.TrimExcess();

            foreach (CompartimentalModel.Linhas Linha in Modelo.Colecao.Linhas)
            {
                if (Linha.ValorAB != 0)
                    R[RetornarID(Linha.CaixaInicio.Numero, TodosCompartimentos), RetornarID(Linha.CaixaFim.Numero, TodosCompartimentos)] = Linha.ValorAB;
                if (Linha.ValorBA != 0)
                    R[RetornarID(Linha.CaixaFim.Numero, TodosCompartimentos), RetornarID(Linha.CaixaInicio.Numero, TodosCompartimentos)] = Linha.ValorBA;
            }
        }

        /*
        public void LerDataSet(DataSet ds)
        {
            int Tamanho = ds.Tables["TableCaixas"].Rows.Count + 1;  //+ 1 pq o elemento zero nao é utilizado

            n = Tamanho;
            R = new double[n, n];
            R[1, 1] = 1; //Plasma é o 4

            ModeloCompartimental.Clear();
            int Contador = 1;
            foreach (System.Data.DataRow dr in ds.Tables["TableCaixas"].Rows)
            {
                Compartimento C = new Compartimento((int)dr["Numero"], Contador++, (string)dr["Nome"], System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]), (bool)dr["Acompanhar"]);
                ModeloCompartimental.Add(C);
            }
            ModeloCompartimental.TrimToSize();

            foreach (System.Data.DataRow dr in ds.Tables["TableLinhas"].Rows)
            {
                double ValorAB = Convert.ToDouble(dr["ValorAB"]);
                double ValorBA = Convert.ToDouble(dr["ValorBA"]);
                if (ValorAB != 0)
                    //Problema aqui
                    R[RetornarID((int)dr["CaixaInicio"], ModeloCompartimental), RetornarID((int)dr["CaixaFim"], ModeloCompartimental)] = ValorAB;
                if (ValorBA != 0)
                    R[RetornarID((int)dr["CaixaFim"], ModeloCompartimental), RetornarID((int)dr["CaixaInicio"], ModeloCompartimental)] = ValorBA;
            }
        }
        */
         
        private int RetornarID(int NumeroCaixa, CompartimentalModel.CaixasCollection Colecao)
        {
            foreach (CompartimentalModel.Caixas C in Colecao)
            {
                if (C.Numero == NumeroCaixa)
                    return (int)C.Tag;
            }
            return 0;
        }

        private void carregarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caminhoInicial = LerSettings("XMLPath");

            if (caminhoInicial == "")
                caminhoInicial = "c:\\";

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = caminhoInicial;
            openFile.DefaultExt = "xml";
            openFile.Filter = "Extensible Markup Language (*.xml)|*.xml";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ImportarArquivo(openFile.FileName);
                if (openFile.FileName != caminhoInicial)
                    GravarSettings("XMLPath", openFile.FileName);

                btnCalcular.Enabled = true;
            }
        }

        private void carregarMDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caminhoInicial = LerSettings("MDBPath");

            if (caminhoInicial == "")
                caminhoInicial = "c:\\";

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = caminhoInicial;
            openFile.DefaultExt = "mdb";
            openFile.Filter = "Microsoft Office Access (*.mdb)|*.mdb";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (openFile.FileName != caminhoInicial)
                    GravarSettings("MDBPath", openFile.FileName);
                frmModelos F = new frmModelos(openFile.FileName, this);
                F.ShowDialog();
                btnCalcular.Enabled = true;
            }
        }

        private void sairDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string LerSettings(string Chave)
        {
            return ConfigurationManager.AppSettings[Chave];
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
