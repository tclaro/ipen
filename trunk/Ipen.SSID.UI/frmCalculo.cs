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
using DotNumerics.ODE;

namespace Ipen.SSID.UI
{
    public partial class frmCalculo : Form
    {

        public int idModeloAberto;
        CompartimentalModel.Modelos ModeloAberto;
        private ArrayList ModeloCompartimental = new ArrayList();
        private CompartimentalModel.CaixasCollection TodosCompartimentos = new CompartimentalModel.CaixasCollection();

        int n, indice;
        double[,] sum, a, R, term, b;
        double[] xt, xo, u;
        double[,] q, qi;
        double max, lam, terr;
        int Tempo;

        public frmCalculo()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {

            if (!rdoBirchall.Checked)
            {
                ResolverOutrosMetodos();
                return;
            }


            int Final = 0, Passo = 0;
            double MeiaVida = 0;
            try
            {
                Final = Convert.ToInt32(txtTempo.Text);
                Passo = Convert.ToInt32(txtPasso.Text);
                
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
            LerModelo(idModeloAberto);
            MeiaVida = ModeloAberto.meiaVida;

            if (MeiaVida > 0)
                lam = Math.Log(2) / MeiaVida; //lambda R
            else
                lam = 0;

            terr = 1E-10;
            double QuantAnt = 0;


            PreencherMatrizR();
            Init();

            StringBuilder str = new StringBuilder();

            Object[] ListaDeParesDePontos = new Object[n];
            str.Append("Tempo");

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

            GraphPane pane = CreateChart(ListaDeParesDePontos, zedGraphControl1, false);
            GraphPane paneCopia = pane.Clone();
            frmGrafico FormGrafico = new frmGrafico();
            FormGrafico.CreateChart(paneCopia);
            FormGrafico.Show();

        }

        private GraphPane CreateChart(Object[] coisas, ZedGraphControl zgc, bool ZeroBased)
        {

            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles and axis labels
            myPane.Title.Text = "Análise Compartimental";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Quantidade";

            int Limite;
            if (ZeroBased)
                Limite = coisas.GetLength(0)+1;
            else
                Limite = coisas.GetLength(0);

            for (int i = 1; i < Limite; i++)
            {
                CompartimentalModel.Caixas C ;
                PointPairList MinhaLista;
                if (ZeroBased)
                {
                    MinhaLista = (PointPairList)coisas[i-1];
                    C = TodosCompartimentos[i-1];
                }
                else
                {
                    
                    MinhaLista = (PointPairList)coisas[i];
                    C = TodosCompartimentos[i-1];
                }
                
                
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
            ModeloAberto = new CompartimentalModel.Modelos();
            ModeloAberto.idModelo = idModelo;
            ModeloAberto = CompartimentalModel.DataBD.SelecionarModelos(idModelo);
            ModeloAberto.PreencherCaixasLinhas();

            TodosCompartimentos.Clear();
        }

        public void PreencherMatrizR()
        {
            int Tamanho = ModeloAberto.Colecao.Caixas.Count + 1;  //+ 1 pq o elemento zero nao é utilizado

            n = Tamanho;
            R = new double[n, n];
            int Contador = 1;

            foreach (CompartimentalModel.Caixas Caixa in ModeloAberto.Colecao.Caixas)
            {
                Caixa.Tag = Contador++;

                if (Caixa.Incorporacao)
                    R[(int)Caixa.Tag, (int)Caixa.Tag] = Caixa.Fracao;

                TodosCompartimentos.Add(Caixa);
            }
            TodosCompartimentos.TrimExcess();

            foreach (CompartimentalModel.Linhas Linha in ModeloAberto.Colecao.Linhas)
            {
                if (Linha.ValorAB != 0)
                    R[RetornarID(Linha.CaixaInicio.Numero, TodosCompartimentos), RetornarID(Linha.CaixaFim.Numero, TodosCompartimentos)] = Linha.ValorAB;
                if (Linha.ValorBA != 0)
                    R[RetornarID(Linha.CaixaFim.Numero, TodosCompartimentos), RetornarID(Linha.CaixaInicio.Numero, TodosCompartimentos)] = Linha.ValorBA;
            }


        }

        public void PreencherMatrizR(bool OutrosMetodos)
        {
            int Tamanho = ModeloAberto.Colecao.Caixas.Count;
            R = new double[Tamanho, Tamanho];
            
            int contador = 0;
            foreach (CompartimentalModel.Caixas Caixa in ModeloAberto.Colecao.Caixas)
            {
                Caixa.Tag = contador++;
                TodosCompartimentos.Add(Caixa);
            }
            TodosCompartimentos.TrimExcess();


            foreach (CompartimentalModel.Caixas Caixa in ModeloAberto.Colecao.Caixas)
            {
                foreach (CompartimentalModel.Linhas Linha in ModeloAberto.Colecao.Linhas)
                {
                    int l = RetornarID(Caixa.Numero, TodosCompartimentos);

                    if (Linha.CaixaInicio == Caixa)
                    {
                        int c = RetornarID(Linha.CaixaFim.Numero, TodosCompartimentos);
                        if (Linha.ValorAB != 0)
                            R[l, l] += Linha.ValorAB;
                        if (Linha.ValorBA != 0)
                            R[l, c] = Linha.ValorBA;
                    }
                    
                    if (Linha.CaixaFim == Caixa)
                    {
                        int c = RetornarID(Linha.CaixaInicio.Numero, TodosCompartimentos);

                        if (Linha.ValorBA != 0)
                            R[l, l] += Linha.ValorBA;
                        if (Linha.ValorAB != 0)
                            R[l, c] = Linha.ValorAB;
                    }
                    
                }
            }

            for (int i = 0; i <= R.GetUpperBound(0); i++)
                R[i, i] = R[i, i] * -1;
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

        #region Interface
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
        #endregion

        private void SolveRungeKutta()
        {
            double Final = 0, Passo = 0;
            double Inicio = 0;
            double MeiaVida = 0;
            double QuantAnt = 0;
            int QuantFuncoes = 0;
            try
            {
                Final = Convert.ToDouble(txtTempo.Text);
                Passo = Convert.ToDouble(txtPasso.Text);
                MeiaVida = ModeloAberto.meiaVida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conversão\n" + ex.Message, "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            StringBuilder str = new StringBuilder();
                        
            double[] y0 = new double[ModeloAberto.Colecao.Caixas.Count];
            foreach (CompartimentalModel.Caixas Caixa in ModeloAberto.Colecao.Caixas)
            {
                y0[(int)Caixa.Tag] = Caixa.Fracao;
                QuantFuncoes++;
                
            }
            str.Append ("\n");

            Object[] ListaDeParesDePontos = new Object[QuantFuncoes];

            foreach (CompartimentalModel.Caixas Caixa in ModeloAberto.Colecao.Caixas)
            {
                if (Caixa.Acompanhar)
                {
                    PointPairList list = new PointPairList();
                    ListaDeParesDePontos[(int)Caixa.Tag] = list;
                    str.Append(Caixa.Nome + "\t");
                }
            }
                        
            OdeFunction YDot = new OdeFunction(MontarEquacao);
            double[,] sol;

            if (rdoKutta5.Checked)
                sol = ResolverPorKutta5(YDot, QuantFuncoes, y0, Inicio, Final, Passo);
            else if (rdoKutta45.Checked)
                sol = ResolverPorKutta45(YDot, QuantFuncoes, y0, Inicio, Final, Passo);
            else
                sol = ResolverPorAdamsM(YDot, QuantFuncoes, y0, Inicio, Final, Passo);

            txtSaida.Clear();

            for (int i = 0; i < sol.GetLength(0); i++)
            {
                double soma =0;
                str.Append("\nT = " + sol[i, 0].ToString());
               
                for (int c = 1; c < sol.GetLength(1); c++)
                {
                    CompartimentalModel.Caixas Caixa = ModeloAberto.Colecao.Caixas[c-1];
                    soma += sol[i, c];
                    if (Caixa.Acompanhar)
                    {
                        double valorInstanteCompartimento = 0d;

                        if (Caixa.Eliminacao)
                        {
                            valorInstanteCompartimento = sol[i, c] - QuantAnt;
                            QuantAnt = sol[i, c];
                        }
                        else
                            valorInstanteCompartimento = sol[i, c];

                        str.Append("\t" + valorInstanteCompartimento.ToString("e10"));
                        ((PointPairList)ListaDeParesDePontos[(int)Caixa.Tag]).Add(sol[i, 0], valorInstanteCompartimento);
                    }
                }
                str.Append("\t soma = " + soma.ToString("e10"));
            }
            txtSaida.Text = str.ToString();

            GraphPane pane = CreateChart(ListaDeParesDePontos, zedGraphControl1,true);
            GraphPane paneCopia = pane.Clone();
            frmGrafico FormGrafico = new frmGrafico();
            FormGrafico.CreateChart(paneCopia);
            FormGrafico.Show();
        }

        private double[,] ResolverPorKutta5(OdeFunction YDot, int QuantidadeFuncoes, double[] EstadoInicial, double Inicio, double Final,double Passo)
        {
            OdeImplicitRungeKutta5 rungeKutta = new OdeImplicitRungeKutta5(YDot, QuantidadeFuncoes);
            double[,] sol;
            sol = rungeKutta.Solve(EstadoInicial, Inicio, Passo, Final);
            return sol;
        }

        private double[,] ResolverPorKutta45(OdeFunction YDot, int QuantidadeFuncoes, double[] EstadoInicial, double Inicio, double Final, double Passo)
        {
            OdeExplicitRungeKutta45 rungeKutta = new OdeExplicitRungeKutta45(YDot, QuantidadeFuncoes);
            double[,] sol;
            sol = rungeKutta.Solve(EstadoInicial, Inicio, Passo, Final);
            return sol;
        }

        private double[,] ResolverPorAdamsM(OdeFunction YDot, int QuantidadeFuncoes, double[] EstadoInicial, double Inicio, double Final, double Passo)
        {
            OdeAdamsMoulton Adams = new OdeAdamsMoulton(YDot, QuantidadeFuncoes);
            double[,] sol;
            sol = Adams.Solve(EstadoInicial, Inicio, Passo, Final);
            return sol;
        }

        private double[] MontarEquacao(double T, double[] Y)
        {

            int Tamanho = R.GetLength(0);

            double[] ydot = new double[Tamanho];

           
            for (int l = 0; l < Tamanho; l++)
                for (int c = 0; c < Tamanho; c++)
                    ydot[l] += R[l,c] * Y[c];

            return ydot;
        }


        private void ResolverOutrosMetodos()
        {
            LerModelo(idModeloAberto);
            PreencherMatrizR(true);

            //Muda cursor pra ampulheta
            this.Cursor = Cursors.WaitCursor;
            DateTime startTime = DateTime.Now;

            SolveRungeKutta();

            this.Cursor = Cursors.Default;
            DateTime stopTime = DateTime.Now;
            TimeSpan Duration = stopTime - startTime;
            lblTempoDecorrido.Text = Duration.Hours.ToString("00") + ":" + Duration.Minutes.ToString("00") + ":" + Duration.Seconds.ToString("00") + ":" + Duration.Milliseconds.ToString("000");
        }
    }
}

 