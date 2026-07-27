using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace Ipen.CompartimentalModel
{
    public class Configuracoes
    {
        public static string Arquivo;
        public static bool ExibirRotulos;
        public static bool ExibirSetas;
        public static bool ExibirTodasLigacoes;

        /// <summary>
        /// Força um provider OLE DB específico. Se ficar nulo (o normal),
        /// Conectar() escolhe sozinho entre os candidatos instalados na máquina.
        /// </summary>
        public static string Provider;

        /// <summary>
        /// O Microsoft.Jet.OLEDB.4.0 nunca teve versão de 64 bits e foi descontinuado.
        /// O ACE o substitui e lê tanto .mdb quanto .accdb, dispensando converter o
        /// banco. O 12.0 acompanha o Access Database Engine redistribuível e o
        /// Office 2010+; o 16.0, o Office 2016+.
        /// </summary>
        private static readonly string[] ProvidersCandidatos =
        {
            "Microsoft.ACE.OLEDB.12.0",
            "Microsoft.ACE.OLEDB.16.0",
        };

        private static string _providerResolvido;

        /// <summary>
        /// Provider efetivamente em uso, ou nulo enquanto nenhuma conexão foi aberta.
        /// </summary>
        public static string ProviderEmUso
        {
            get { return _providerResolvido; }
        }

        public static OleDbConnection Conectar()
        {
            OleDbConnection cn = new OleDbConnection(MontarStringConexao(ResolverProvider()));
            cn.Open();
            return cn;
        }

        public static string MontarStringConexao(string Provider)
        {
            return "Provider=" + Provider + ";" +
                   "Data Source=" + Arquivo + ";" +
                   "Persist Security Info=False;";
        }

        /// <summary>
        /// Decide qual provider usar, uma única vez por execução. A escolha se baseia no
        /// que está registrado na máquina, e não em tentar abrir o arquivo: assim um erro
        /// real do banco (ausente, corrompido, em uso) continua subindo como
        /// OleDbException, em vez de virar um enganoso "nenhum provider encontrado".
        /// </summary>
        private static string ResolverProvider()
        {
            if (_providerResolvido != null)
                return _providerResolvido;

            List<string> candidatos = new List<string>();
            if (!string.IsNullOrEmpty(Provider))
                candidatos.Add(Provider);
            candidatos.AddRange(ProvidersCandidatos);

            List<string> instalados = ListarProvidersInstalados();

            foreach (string p in candidatos)
                if (instalados.Contains(p))
                {
                    _providerResolvido = p;
                    return p;
                }

            string nl = Environment.NewLine;
            throw new InvalidOperationException(
                "Nenhum provider OLE DB compatível está instalado." + nl + nl +
                "Este aplicativo é de 64 bits e precisa do Microsoft Access Database " +
                "Engine de 64 bits (ACE). Instale o redistribuível de 64 bits ou uma " +
                "versão de 64 bits do Office." + nl + nl +
                "Procurados: " + string.Join(", ", candidatos.ToArray()) + nl +
                "Instalados: " +
                (instalados.Count == 0 ? "(nenhum)" : string.Join(", ", instalados.ToArray())));
        }

        /// <summary>
        /// Nomes dos providers OLE DB registrados para a arquitetura deste processo.
        /// </summary>
        private static List<string> ListarProvidersInstalados()
        {
            List<string> nomes = new List<string>();
            try
            {
                using (OleDbDataReader dr = OleDbEnumerator.GetRootEnumerator())
                {
                    int col = dr.GetOrdinal("SOURCES_NAME");
                    while (dr.Read())
                    {
                        object v = dr.GetValue(col);
                        if (v != null && v != DBNull.Value)
                            nomes.Add(v.ToString());
                    }
                }
            }
            catch (Exception)
            {
                //Se a enumeração falhar, a lista fica vazia e ResolverProvider
                //lança a mensagem explicando o que faltou.
            }
            return nomes;
        }
    }
}
