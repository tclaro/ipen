using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Pascal
{
    public partial class Form1 : Form
    {

//   CONST
        const int tamanho = 8;

        const int n=24;
        const float  DNP = 0.30f,
                DTB = 0.08f,
                DP = 0.25f,
                llst = 24.0f,
                LSI = 6.0f,
                LULI = 1.8f,
                LLLI = 1.0f;
//VAR
        int NO,QD,RN,TEMPO;
        float LA,LB,LC,LD,LE,LH,LF,LG,LI,LJ,LS ;
        float FA,FB,FC,FD,FE,FH,FF,FG,FI,FJ;
        float LBL, LK, LP, F1, FU, FP, TR;
        float C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12;

        char CLASSE;
        string[] org = new string[7];
        float[] LO = new float[7],
                        FO = new float[7],
                        C = new float[21];


  // Area de entrada de dados        
  
//Type
        //float matriz = new ArrayList();
        //ArrayList vetor = new ArrayList();
        //ArrayList dmatriz = new ArrayList();
        float[,] sum = new float[ n, n ],
            a = new float[n, n],
            R = new float[n, n],
            term = new float[n, n],
            b = new float[n, n];

        float[] xt = new float[n],
                xo = new float[n],
                u = new float[n];
        
        float[,] q = new float[2*n,2*n],
                qi = new float[2*n,2*n];

  int i,j,k,iz,ir,i1,ip1,k1           ;
  int j1,id,t,f,pp,w,cod,cont         ;
  float xtot,xxant,xant,d,max,reduc   ;
  //h,m,s,s100                      : word;
  float elapsed1,elapsed2             ;
  float lam,terr                            ;
  string mfile                           ;
  string tempot,ppt                      ;
  char opcao                           ;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            RN = 1;
            CLASSE = Convert.ToChar("d");
            tempot = "12";
            ppt = "1";

            cont = 1;
            if (tempot == "")
                f = 100;
            else
                f = Convert.ToInt32(tempot);

            pp = Convert.ToInt32(ppt);
            if (pp == 0)
                pp = 10;
            
            //gettime(h,m,s,s100);
            //elapsed1:=3600*h+60*m+s+s100/100;

            f = f / pp;
            t = 0;

            xant=0;
            xtot=0;
            xxant=0;

            Init();
            StringBuilder str = new StringBuilder();

            //for (w = 0;w <= f + 1; w++)
            for (w = 0; w <= 1; w++)
            {
                Calculo();
                    
                str.Append(t.ToString());
                str.Append("\t");
                str.Append(((float)(xt[17] - xxant)).ToString());
                str.Append("\n");
                
                xxant = xt[17];
                t = t + pp;
                
            }
            txtSaida.Text = str.ToString();
        }

        private void Init()
        {
            if ((CLASSE == 'd') || (CLASSE == 'D'))
            {
                FA=0.5f; LA=69.31f;
                FB=0.5f; LB=69.31f;
                FC=0.95f;LC=69.31f;
                FD=0.05f;LD=3.466f;
                FE=0.8f; LE=1.386f;
                FF=0.0f; LF=0.0f;
                FG=0.0f; LG=0.0f;
                FH=0.2f; LH=1.386f;
                FI=1.0f; LI=1.386f;
                FJ=0.0f; LJ=0.0f;
            }
            /*
            if ((CLASSE == 'w') || (CLASSE == 'W'))
            {
                FA=0.10;LA=69.31;
                FB=0.90;LB=1.733;
                FC=0.50;LC=69.31;
                FD=0.50;LD=3.466;
                FE=0.15;LE=0.01386;
                FF=0.40;LF=0.006931;
                FG=0.40;LG=0.01386;
                FH=0.05;LH=0.01386;
                FI=1.0; LI=0.01459;
                FJ=0.0; LJ=0.0;
            }
            if ((CLASSE == 'y') || (CLASSE =='Y'))
            {
          
                FA=0.01;LA=69.31;
                FB=0.99;LB=1.733;
                FC=0.01;LC=69.31;
                FD=0.99;LD=3.466;
                FE=0.05;LE=0.001386;
                FF=0.40;LF=0.6931;
                FG=0.40;LG=0.001386;
                FH=0.15;LH=0.001386;
                FI=0.90;LI=7.295E-4;
                FJ=0.10;LJ=0.0;
            }
            */
            switch (RN)
            {
                case 1:
                    NO = 6;
                    F1 = 0.05f; TR = 1.64E12f;
                    FO[0] = 0.2f; LO[1] = 0.0346f;
                    FO[1] = 0.12f; LO[2] = 0.11552f;
                    FO[2] = 0.12f; LO[3] = 0.11552f;
                    FO[3] = 0.023f; LO[4] = 1.386E-4f;
                    FO[4] = 0.00052f; LO[5] = 4.62E-4f;
                    FO[5] = 0.00032f; LO[6] = 4.62E-4f;
                    FP = 0.54f; LP = 2.773f;
                    FU = 1.0f;
                    break;
                    /*
                case 2:
                    NO = 3;
                    F1 = 2.0E-4; TR = 1.0E10;
                    FO[0] = 0.7; LO[1] = 8.66E-5;
                    FO[1] = 0.04; LO[2] = 9.90E-4;
                    FO[2] = 0.16; LO[3] = 9.90E-4;
                    FP = 0.10; LP = 1.38629;
                    FU = 1.0;
                    break;

                case 3:
                    NO = 2;
                    F1 = 0.001;
                    FO[0] = 0.45; LO[1] = 3.79E-5;
                    FO[1] = 0.45; LO[2] = 9.49E-5;
                    FP = 0.1; LP = 2.773;
                    FU = 1.0;
                    break;

                case 4:
                    NO = 3;
                    F1 = 0.05;
                    FO[1] = 0.3; LO[1] = 0.1155;
                    FO[2] = 0.1; LO[2] = 0.01155;
                    FO[3] = 0.1; LO[3] = 8.66E-4;
                    FP = 0.5; LP = 1.3863;
                    FU = 0.7;
                    break;
                    */
            }

            LS = F1 * LSI / (1 - F1);

            
            C[1] = DNP*FA;
            C[2] = DNP*FB;
            C[3] = DTB*FC;
            C[4] = DTB*FD;
            C[5] = DP*FE;
            C[6] = DP*FF;
            C[7] = DP*FG;
            C[8] = DP*FH;

            for (i = 1; i < C.Length; i++)
                R[i, i] = C[i];

            
            R[1,11] = LA;
            R[2,12] = LB;
            R[3,11] = LC;
            R[4,12] = LD;
            R[5,11] = LE;
            R[6,4] = LF;
            R[7,4] = LG;
            R[8,9] = FI*LH;
            R[9,11] = LI;
            R[8,10] = FJ*LH; 
            R[11,14] = FO[1]*LP;
            R[11,15] = FO[2]*LP;
            R[11,16] = FO[3]*LP;
            R[11,18] = FO[4]*LP;
            R[11,19] = FO[5]*LP;
            R[11,20] = FO[6]*LP;
            R[11,17] = FP * LP;
            R[14,17] = LO[1];
            R[15,17] = LO[2];
            R[16,17] = LO[3];
            R[18,17] = LO[4];
            R[19,17] = LO[5];
            R[20,17] = LO[6];
            R[12,13] = llst;
            R[13,11] = LS;
            R[13,21] = LSI;
            R[21,22] = LULI;
            R[22,23] = LLLI;

            lam = 0.693f / TR;
            terr = 1E-10f;
        }

        private void Calculo()
        {
            //d = lam * t;
            
            for (i = 1; i < n; i++)
            {
                for (j = 1; j < n; j++)
                {
                    sum[i, j] = 0;
                    a[i, j] = R[j, i];
                }
            }

            for (i = 1; i < n; i++)
            {
                a[i, i] = -lam;
                for (k = 1; k < n; k++)
                    if (k != i)
                        a[i, i] = a[i, i] - R[i, k];
            }

            for (i = 1; i < n; i++)
            {
                xo[i] = R[i, i];
                sum[i, i] = 1;
                term[i, i] = 1;
            }

            MyMatrix(xo, "xo");

            for (i=1; i < n; i++)
                for (j = 1; j < n; j++)
                {
                    q[i, j] = a[i, j];
                    a[i, j] = a[i, j] * t;
                }

            max = 0;

            for (i = 1; i < n; i++)
                if (a[i, i] < max)
                    max = a[i, i];

            for (iz = 0; iz <= 100; iz++)
                if (-max / Math.Exp(Math.Log(2) * iz) < 0.2d)
                    goto Fora;

            Fora:
        
            for (i = 1; i < n; i++)
                for (j = 1; j < n; j++)
                    a[i, j] = (float)a[i, j] / (float)(Math.Exp(Math.Log(2) * iz));

            for (ir = 1; ir <= 10000; ir++)
            {
                for (j = 1; j < n; j++)
                    for (i = 1; i < n; i++)
                    {
                        b[i, j] = 0;
                        for (k = 1; k < n; k++)
                            b[i, j] = b[i, j] + term[i, k] * a[k, j];
                    }

                for (i = 1; i < n; i++)
                    for (j = 1; j < n; j++)
                    {
                        term[i, j] = b[i, j] / ir;
                        sum[i, j] = sum[i, j] + term[i, j];
                    }

                for (i = 1; i < n; i++)
                    for (j = 1; j < n; j++)
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

            for (id = 1; id < iz; id++)
            {
                for (i = 1; i < n; i++)
                    for (j = 1; j < n; j++)
                    {
                        a[i, j] = 0;
                        for (k = 1; k < n; k++)
                            a[i, j] = a[i, j] + sum[i, k] * sum[k, j];
                    }

                for (i = 1; i < n; i++)
                    for (j = 1; j < n; j++)
                        sum[i, j] = a[i, j];
            }

            if (lam != 0)
                Inversao();

            for (i = 1; i < n; i++)
            {
                xt[i] = 0;
                for (k = 1; k < n; k++)
                    xt[i] = xt[i] + sum[i, k] * xo[k];
            }


            MyMatrix(xt, "xt");
            MyMatrix(xo, "xo");
            if (lam != 0)
            {
                for (i = 1; i < n; i++)
                    sum[i, i] = sum[i, i] - 1;

                for (i = 1; i < n; i++)
                    for (j = 1; j < n; j++)
                    {
                        b[i, j] = 0;
                        for (id = 1; id < n; id++)
                            b[i, j] = b[i, j] + qi[i, id] * sum[id, j];
                    }

                for (i = 1; i < n; i++)
                {
                    u[i] = 0;
                    for (j = 1; j < n; j++)
                        u[i] = u[i] + b[i, j] * xo[j];
                    u[i] = u[i] * lam;
                }

            }
        }

        private void Inversao()
        {
            for (i1 = 1; i1 < n; i1++)
            {
                for (j1 = n; j1 < 2*n; j1++)
                    q[i1, j1] = 0;
                q[i1, i1 + (n-1)] = 1;
            }

            for (ip1 = 1; ip1 < n; ip1++)
            {
                for (k1 = 1; k1 < n; k1++)
                    qi[ip1, k1] = q[ip1, k1 + ip1] / (q[ip1, ip1]);

                for (j1 = 1; j1 < n; j1++)
                {
                    if (ip1 == j1)
                        goto desvio;
                    for (k1 = 1; k1 < n; k1++)

                        qi[j1, k1] = q[j1, k1 + ip1] - q[j1, ip1] * qi[ip1, k1];

                desvio:
                   int xxx = 1;
                }
                for (j1 = 1; j1 < n; j1++)
                    for (k1 = 1; k1 < n; k1++)
                        q[j1, k1 + ip1] = qi[j1, k1];
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void MyMatrix(float[,] m, string nome)

        {
           /*
            lvw1.FullRowSelect = true;
            lvw1.GridLines = true;

            lvw1.Columns.Clear();
            lvw1.Items.Clear();
            lvw1.View = View.Details;
            lvw1.Columns.Add("",70);
            */
            Console.WriteLine(nome);

            for (int i = 1; i < m.GetLength(0); i++)
            {
            //    lvw1.Columns.Add(i.ToString(), 70);
                Console.Write(i.ToString() + "\t");
            }
            Console.WriteLine();
            for (int j = 1; j < m.GetLength(1); j++)
            {
            //    ListViewItem item1 = lvw1.Items.Add(j.ToString());
                Console.Write(j.ToString().PadRight(tamanho).Substring(0, tamanho - 1));
                for (int col = 1; col < m.GetLength(0); col++)
                {
             //       item1.SubItems.Add(m[col, j].ToString());
                    Console.Write(m[col, j].ToString().PadRight(tamanho).Substring(0, tamanho - 1));
                }
                Console.WriteLine();
                
            }
            Console.WriteLine();
         
        }

        private void MyMatrix(float[] m, string nome)
        {
           /* lvw1.FullRowSelect = true;
            lvw1.GridLines = true;

            lvw1.Columns.Clear();
            lvw1.Items.Clear();
            lvw1.View = View.Details;
            lvw1.Columns.Add("", 70);
            */
            Console.WriteLine(nome);

            for (int i = 1; i < m.Length; i++)
            {
              //  lvw1.Columns.Add(i.ToString(), 70);
                Console.Write(i.ToString().PadRight(tamanho).Substring(0, tamanho-1));
            }
            Console.WriteLine();
            for (int j = 1; j < m.Length; j++)
            {
            //    ListViewItem item1 = lvw1.Items.Add(j.ToString());
            //    item1.SubItems.Add(m[j].ToString());
                Console.Write(m[j].ToString().PadRight(tamanho).Substring(0, tamanho-1));
                //Application.DoEvents();
            }
            Console.WriteLine();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}