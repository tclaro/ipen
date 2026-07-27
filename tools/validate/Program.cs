using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;
using CM = Ipen.CompartimentalModel;
using Ipen.SSID.UI;
using DotNumerics.ODE;

namespace Validate
{
    /// <summary>
    /// Harness de validacao numerica. Nao toca o banco de dados nem a UI: constroi
    /// modelos compartimentais em memoria com solucao analitica conhecida, e invoca
    /// os quatro metodos numericos de frmCalculo diretamente (via reflection para os
    /// membros privados) para comparar o resultado contra a formula fechada.
    ///
    /// frmCalculo e instanciado com FormatterServices.GetUninitializedObject, que pula
    /// o construtor (logo InitializeComponent() nunca roda e nenhum Control e criado).
    /// Isso so e seguro porque os metodos exercitados aqui (Calculo, Init, PreencherMatrizR,
    /// MontarEquacao, ResolverPorKutta5/45/AdamsM) tocam apenas os campos numericos
    /// declarados diretamente na classe -- nenhum deles referencia um Control.
    /// </summary>
    class Program
    {
        const BindingFlags NP = BindingFlags.NonPublic | BindingFlags.Instance;

        // Birchall resolve a exponencial de matriz por serie de Taylor ate 1e-10 de
        // tolerancia relativa (terr) -- e essencialmente exato, deve bater a analitica
        // ate erro de arredondamento em double.
        const double TOL_BIRCHALL = 1e-6;

        // RK5/RK45/AdamsMoulton sao integradores numericos reais com passo fixo (Passo
        // de 1-2 dias nestes testes): tem erro de discretizacao genuino, nao sao exatos.
        const double TOL_RK_BRUTO = 3e-3;

        // Incrementos (diferenca entre dois valores ja aproximados) amplificam o erro
        // relativo quando o incremento em si e pequeno -- e o proprio motivo de usar
        // erro relativo aqui superestimar o problema; a tolerancia e mais frouxa de
        // proposito, e o teste imprime o erro absoluto ao lado para quem quiser conferir.
        const double TOL_RK_INCREMENTO = 5e-2;

        static void SetF(object o, string nome, object v) =>
            typeof(frmCalculo).GetField(nome, NP).SetValue(o, v);

        static T GetF<T>(object o, string nome) =>
            (T)typeof(frmCalculo).GetField(nome, NP).GetValue(o);

        static object Invoke(object o, string nome, params object[] args) =>
            typeof(frmCalculo).GetMethod(nome, NP).Invoke(o, args);

        static frmCalculo NovoFrm()
        {
            var frm = (frmCalculo)FormatterServices.GetUninitializedObject(typeof(frmCalculo));
            // Campos com inicializador de instancia nao rodam sem o construtor.
            SetF(frm, "TodosCompartimentos", new CM.CaixasCollection());
            return frm;
        }

        static CM.Caixas NovaCaixa(CM.Sistema sis, string nome, bool incorporacao, double fracao, bool eliminacao)
        {
            var cx = new CM.Caixas(nome, Color.SteelBlue, true /*Acompanhar*/, eliminacao, incorporacao, fracao);
            sis.Caixas.Add(cx);
            return cx;
        }

        static CM.Modelos NovoModelo(double meiaVida)
        {
            var m = new CM.Modelos();
            m.Colecao.Clear(); // Sistema e singleton de processo -- limpa o residuo do teste anterior.
            m.meiaVida = meiaVida;
            m.nmModelo = "Teste";
            return m;
        }

        // ---------- Birchall: chama Calculo() a cada dia inteiro, le xt por reflection ----------
        static double[][] RunBirchall(CM.Modelos modelo, double lam, int final_, int passo)
        {
            var frm = NovoFrm();
            SetF(frm, "ModeloAberto", modelo);
            frm.PreencherMatrizR();               // publico -- indexacao 1-based, index 0 nao usado
            SetF(frm, "lam", lam);
            SetF(frm, "terr", 1e-10);
            SetF(frm, "Tempo", 0);
            Invoke(frm, "Init");

            int n = GetF<int>(frm, "n");
            int passos = final_ / passo;
            var linhas = new System.Collections.Generic.List<double[]>();
            int tempo = 0;
            for (int T = 0; T <= passos; T++)
            {
                SetF(frm, "Tempo", tempo);
                Invoke(frm, "Calculo");
                var xt = GetF<double[]>(frm, "xt");
                var linha = new double[n];
                linha[0] = tempo;
                Array.Copy(xt, 1, linha, 1, n - 1);
                linhas.Add(linha);
                tempo += passo;
            }
            return linhas.ToArray();
        }

        // ---------- RK5 / RK45 / Adams-Moulton: via PreencherMatrizR(true) + MontarEquacao ----------
        static double[,] RunRK(string metodoResolver, CM.Modelos modelo, double lam, double final_, double passo)
        {
            var frm = NovoFrm();
            SetF(frm, "ModeloAberto", modelo);
            frm.PreencherMatrizR(true);           // publico -- indexacao 0-based

            int count = modelo.Colecao.Caixas.Count;
            var y0 = new double[count];
            foreach (CM.Caixas cx in modelo.Colecao.Caixas)
                y0[(int)cx.Tag] = cx.Incorporacao ? cx.Fracao : 0d;

            // MontarEquacao usa Math.Log(2)/meiaVida internamente a partir de ModeloAberto.meiaVida,
            // entao lam aqui e so para os calculos analiticos de comparacao, nao precisa ser setado no frm.

            var meMethod = typeof(frmCalculo).GetMethod("MontarEquacao", NP);
            var ydot = (OdeFunction)Delegate.CreateDelegate(typeof(OdeFunction), frm, meMethod);

            var resM = typeof(frmCalculo).GetMethod(metodoResolver, NP);
            return (double[,])resM.Invoke(frm, new object[] { ydot, count, y0, 0.0, final_, passo });
        }

        static double RelErr(double calc, double analit)
        {
            double esc = Math.Max(Math.Abs(analit), 1e-12);
            return Math.Abs(calc - analit) / esc;
        }

        static string Fmt(double v) => v.ToString("0.000000e+00");

        static bool _falhou = false;

        static void Checar(string rotulo, double calc, double analit, double tolerancia)
        {
            double erro = RelErr(calc, analit);
            bool ok = erro <= tolerancia;
            if (!ok) _falhou = true;
            Console.WriteLine("  {0,-46} calc={1}  analitico={2}  erro_rel={3}  {4}",
                rotulo, Fmt(calc), Fmt(analit), Fmt(erro), ok ? "OK" : "*** FALHOU ***");
        }

        static void Main()
        {
            Console.WriteLine("=================================================================");
            Console.WriteLine(" VALIDACAO NUMERICA -- frmCalculo.cs contra solucao analitica");
            Console.WriteLine("=================================================================\n");

            // ---------------------------------------------------------------
            // TESTE A: decaimento radioativo puro, 1 compartimento, sem linhas.
            //   dx/dt = -lam*x ,  x(0)=1  =>  x(t) = e^(-lam t)
            // ---------------------------------------------------------------
            Console.WriteLine("--- Teste A: decaimento radioativo puro (1 compartimento) ---");
            {
                double meiaVida = 10.0;
                double lam = Math.Log(2) / meiaVida;
                int final_ = 40, passo = 2;

                var m = NovoModelo(meiaVida);
                NovaCaixa(m.Colecao, "C1", incorporacao: true, fracao: 1.0, eliminacao: false);

                Console.WriteLine(" Birchall:");
                var bir = RunBirchall(m, lam, final_, passo);
                foreach (var linha in bir)
                {
                    double t = linha[0];
                    double analit = Math.Exp(-lam * t);
                    Checar($"t={t,5}", linha[1], analit, TOL_BIRCHALL);
                }

                foreach (var (nome, tag) in new[] { ("RK5", "ResolverPorKutta5"), ("RK45", "ResolverPorKutta45"), ("AdamsMoulton", "ResolverPorAdamsM") })
                {
                    Console.WriteLine($" {nome}:");
                    var sol = RunRK(tag, m, lam, final_, passo);
                    for (int i = 0; i < sol.GetLength(0); i++)
                    {
                        double t = sol[i, 0];
                        double analit = Math.Exp(-lam * t);
                        Checar($"t={t,5:0.0}", sol[i, 1], analit, TOL_RK_BRUTO);
                    }
                }
            }

            // ---------------------------------------------------------------
            // TESTE B: transferencia entre 2 compartimentos, sem decaimento.
            //   x1' = -k x1 ,  x2' = k x1 ,  x1(0)=1, x2(0)=0
            //   x1(t)=e^(-kt) ;  x2(t)=1-e^(-kt)
            // ---------------------------------------------------------------
            Console.WriteLine("\n--- Teste B: transferencia entre 2 compartimentos (sem decaimento) ---");
            {
                double k = 0.15;
                int final_ = 20, passo = 1;

                var m = NovoModelo(0.0);
                var c1 = NovaCaixa(m.Colecao, "C1", incorporacao: true, fracao: 1.0, eliminacao: false);
                var c2 = NovaCaixa(m.Colecao, "C2", incorporacao: false, fracao: 0.0, eliminacao: true);
                var ln = new CM.Linhas(c1, c2, Color.DarkRed, CM.Linhas.Direcao.InicioParaFim, (float)k, 0f);
                m.Colecao.Linhas.Add(ln);

                Console.WriteLine(" Birchall:");
                var bir = RunBirchall(m, 0.0, final_, passo);
                foreach (var linha in bir)
                {
                    double t = linha[0];
                    Checar($"t={t,5} x1", linha[1], Math.Exp(-k * t), TOL_BIRCHALL);
                    Checar($"t={t,5} x2", linha[2], 1 - Math.Exp(-k * t), TOL_BIRCHALL);
                }

                Console.WriteLine(" AdamsMoulton:");
                var sol = RunRK("ResolverPorAdamsM", m, 0.0, final_, passo);
                for (int i = 0; i < sol.GetLength(0); i++)
                {
                    double t = sol[i, 0];
                    Checar($"t={t,5:0.0} x1", sol[i, 1], Math.Exp(-k * t), TOL_RK_BRUTO);
                    Checar($"t={t,5:0.0} x2", sol[i, 2], 1 - Math.Exp(-k * t), TOL_RK_BRUTO);
                }
            }

            // ---------------------------------------------------------------
            // TESTE C: decaimento + 2 vias de eliminacao independentes.
            //   Este e o cenario exato do defeito C-4: QuantAnt era uma unica
            //   variavel compartilhada entre TODOS os compartimentos de eliminacao.
            //   K = lam + k12 + k13
            //   x1(t) = e^(-K t)
            //   x2(t) = e^(-lam t) * k12/(k12+k13) * (1 - e^(-(k12+k13) t))
            //   x3(t) = e^(-lam t) * k13/(k12+k13) * (1 - e^(-(k12+k13) t))
            // ---------------------------------------------------------------
            Console.WriteLine("\n--- Teste C: decaimento + 2 vias de eliminacao (regressao do C-4) ---");
            {
                double meiaVida = 20.0;
                double lam = Math.Log(2) / meiaVida;
                double k12 = 0.08, k13 = 0.05;
                double K = lam + k12 + k13;
                int final_ = 30, passo = 1;

                var m = NovoModelo(meiaVida);
                var c1 = NovaCaixa(m.Colecao, "C1", incorporacao: true, fracao: 1.0, eliminacao: false);
                var c2 = NovaCaixa(m.Colecao, "C2 (eliminacao)", incorporacao: false, fracao: 0.0, eliminacao: true);
                var c3 = NovaCaixa(m.Colecao, "C3 (eliminacao)", incorporacao: false, fracao: 0.0, eliminacao: true);
                m.Colecao.Linhas.Add(new CM.Linhas(c1, c2, Color.DarkRed, CM.Linhas.Direcao.InicioParaFim, (float)k12, 0f));
                m.Colecao.Linhas.Add(new CM.Linhas(c1, c3, Color.DarkRed, CM.Linhas.Direcao.InicioParaFim, (float)k13, 0f));

                Func<double, double> X1 = t => Math.Exp(-K * t);
                Func<double, double> X2 = t => Math.Exp(-lam * t) * k12 / (k12 + k13) * (1 - Math.Exp(-(k12 + k13) * t));
                Func<double, double> X3 = t => Math.Exp(-lam * t) * k13 / (k12 + k13) * (1 - Math.Exp(-(k12 + k13) * t));

                Console.WriteLine(" Birchall -- valores brutos (acumulados):");
                var bir = RunBirchall(m, lam, final_, passo);
                foreach (var linha in bir)
                {
                    double t = linha[0];
                    Checar($"t={t,5} x1", linha[1], X1(t), TOL_BIRCHALL);
                    Checar($"t={t,5} x2", linha[2], X2(t), TOL_BIRCHALL);
                    Checar($"t={t,5} x3", linha[3], X3(t), TOL_BIRCHALL);
                }

                // Mirror exato da logica de incremento por compartimento de eliminacao
                // que hoje vive inline em btnCalcular_Click (frmCalculo.cs) -- e nao pode
                // ser chamada isolada porque o resto do metodo mexe em Controls que nao
                // existem aqui. QuantAnt[] por indice e literalmente o que o C-4 corrigiu:
                // antes era um unico "double QuantAnt", compartilhado por C2 e C3.
                Console.WriteLine("\n Birchall -- incrementos reportados para os compartimentos de eliminacao");
                Console.WriteLine(" (reproduz a logica de QuantAnt[] de btnCalcular_Click, agora indexada por compartimento):");
                {
                    int n = bir[0].Length;
                    var quantAnt = new double[n];
                    for (int i = 1; i < bir.Length; i++)
                    {
                        double tPrev = bir[i - 1][0], t = bir[i][0];
                        double inc2 = bir[i][2] - quantAnt[2]; quantAnt[2] = bir[i][2];
                        double inc3 = bir[i][3] - quantAnt[3]; quantAnt[3] = bir[i][3];
                        Checar($"t={tPrev,4}->{t,-4} incremento C2", inc2, X2(t) - X2(tPrev), TOL_BIRCHALL);
                        Checar($"t={tPrev,4}->{t,-4} incremento C3", inc3, X3(t) - X3(tPrev), TOL_BIRCHALL);
                    }
                }

                Console.WriteLine("\n AdamsMoulton -- valores brutos:");
                var sol = RunRK("ResolverPorAdamsM", m, lam, final_, passo);
                for (int i = 0; i < sol.GetLength(0); i++)
                {
                    double t = sol[i, 0];
                    Checar($"t={t,5:0.0} x1", sol[i, 1], X1(t), TOL_RK_BRUTO);
                    Checar($"t={t,5:0.0} x2", sol[i, 2], X2(t), TOL_RK_BRUTO);
                    Checar($"t={t,5:0.0} x3", sol[i, 3], X3(t), TOL_RK_BRUTO);
                }

                Console.WriteLine("\n AdamsMoulton -- incrementos reportados (mirror de SolveRungeKutta, QuantAnt[Tag]):");
                {
                    int count = modeloCount(m);
                    var quantAnt = new double[count];
                    for (int i = 1; i < sol.GetLength(0); i++)
                    {
                        double tPrev = sol[i - 1, 0], t = sol[i, 0];
                        double inc2 = sol[i, 2] - quantAnt[1]; quantAnt[1] = sol[i, 2]; // Tag(C2)=1 (0-based)
                        double inc3 = sol[i, 3] - quantAnt[2]; quantAnt[2] = sol[i, 3]; // Tag(C3)=2
                        Checar($"t={tPrev,4:0}->{t,-4:0} incremento C2", inc2, X2(t) - X2(tPrev), TOL_RK_INCREMENTO);
                        Checar($"t={tPrev,4:0}->{t,-4:0} incremento C3", inc3, X3(t) - X3(tPrev), TOL_RK_INCREMENTO);
                    }
                }
            }

            Console.WriteLine("\n=================================================================");
            Console.WriteLine(_falhou ? " RESULTADO: HA DIVERGENCIAS -- ver *** FALHOU *** acima" : " RESULTADO: TODOS OS CASOS DENTRO DA TOLERANCIA");
            Console.WriteLine("=================================================================");
            Environment.Exit(_falhou ? 1 : 0);
        }

        static int modeloCount(CM.Modelos m) => m.Colecao.Caixas.Count;
    }
}
