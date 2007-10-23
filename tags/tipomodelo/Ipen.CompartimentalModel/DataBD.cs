using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Ipen.CompartimentalModel
{
    public class DataBD
    {
        
        public static void GravarModelo(Modelos M)
        {
            if (M.idModelo == 0)
                IncluirModelo(M);
            else
                AlterarModelo(M);
        }

        public static void RemoverModelo(int idModelo)
        {
            //vai por cascata
            //ExcluirColecao(idModelo);

            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "DELETE FROM MODELO where idModelo = " + idModelo.ToString();
            cmd.CommandText = textoSql;
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

        private static void IncluirModelo(Modelos M)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "INSERT INTO MODELO (nmModelo, dtCriacao, dtAlteracao, Descricao, idTipoModelo) VALUES (@nmModelo, @dtCriacao, @dtAlteracao, @Descricao, @idTipoModelo);";
                
            cmd.CommandText = textoSql;
            cmd.Parameters.Add("@nmModelo", OleDbType.VarChar, 50).Value = M.nmModelo;
            cmd.Parameters.Add("@dtCriacao", OleDbType.Date).Value = System.DateTime.Now;
            cmd.Parameters.Add("@dtAlteracao", OleDbType.Date).Value = System.DateTime.Now;
            cmd.Parameters.Add("@Descricao", OleDbType.VarChar, 200).Value = M.Descricao;
            cmd.Parameters.Add("@idTipoModelo", OleDbType.Integer).Value = M.Tipo.idTipoModelo;
            cmd.ExecuteNonQuery();

            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT @@Identity";
            M.idModelo = (int)cmd.ExecuteScalar();
            cn.Close();
            cn.Dispose();

            IncluirColecao(M.Colecao, M.idModelo);
        }

        private static void AlterarModelo(Modelos M)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "UPDATE MODELO " +
                            "set nmModelo = @nmModelo, " +
                            "dtAlteracao = @dtAlteracao, " + 
                            "Descricao = @Descricao, " +
                            "idTipoModelo = @idTipoModelo " +
                            "WHERE idModelo = " + M.idModelo;
            cmd.CommandText = textoSql;
            cmd.Parameters.Add("@nmModelo", OleDbType.VarChar, 50).Value = M.nmModelo;
            cmd.Parameters.Add("@dtAlteracao", OleDbType.Date).Value = System.DateTime.Now;
            cmd.Parameters.Add("@Descricao", OleDbType.VarChar, 200).Value = M.Descricao;
            cmd.Parameters.Add("@idTipoModelo", OleDbType.Integer).Value = M.Tipo.idTipoModelo;
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();

            ExcluirColecao(M.idModelo);
            IncluirColecao(M.Colecao, M.idModelo);

        }

        private static void IncluirColecao(Sistema Colecao, int idModelo)
        {
            foreach (Caixas C in Colecao.Caixas)
                IncluirCaixa(C, idModelo);

            foreach (Linhas L in Colecao.Linhas)
                IncluirLinha(L, idModelo);
        }

        private static void ExcluirColecao(int idModelo)
        {
            //ExcluirLinhas(idModelo); vai por cascata
            ExcluirCaixas(idModelo);
        }

        private static void ExcluirLinhas(int idModelo)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "DELETE FROM TableLinhas WHERE idModelo = " + idModelo;
            cmd.CommandText = textoSql;
            int LinhasExcluidas = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

        private static void ExcluirCaixas(int idModelo)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "DELETE FROM TableCaixas WHERE idModelo = " + idModelo;
            cmd.CommandText = textoSql;
            int LinhasExcluidas = cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }


        private static void IncluirCaixa(Caixas C, int idModelo)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "INSERT INTO TableCaixas ( idModelo, Numero, Nome, [Left], [Top], Width, Height, CorR, CorG, CorB, Acompanhar, Eliminacao, Incorporacao, Fracao )" +
                "VALUES (@idModelo, @Numero, @Nome, @Left, @Top, @Width, @Height, @CorR, @CorG, @CorB, @Acompanhar, @Eliminacao, @Incorporacao, @Fracao) ";
            cmd.CommandText = textoSql;
            cmd.Parameters.Add("@idModelo", OleDbType.Integer).Value = idModelo;
            cmd.Parameters.Add("@Numero", OleDbType.Integer).Value = C.Numero;
            cmd.Parameters.Add("@Nome", OleDbType.VarChar, 255).Value = C.Nome;
            cmd.Parameters.Add("@Left", OleDbType.Integer).Value = C.Left;
            cmd.Parameters.Add("@Top", OleDbType.Integer).Value = C.Top;
            cmd.Parameters.Add("@Width", OleDbType.Integer).Value = C.Width;
            cmd.Parameters.Add("@Height", OleDbType.Integer).Value = C.Height;
            cmd.Parameters.Add("@CorR", OleDbType.UnsignedTinyInt).Value = C.BackColor.R;
            cmd.Parameters.Add("@CorG", OleDbType.UnsignedTinyInt).Value = C.BackColor.G;
            cmd.Parameters.Add("@CorB", OleDbType.UnsignedTinyInt).Value = C.BackColor.B;
            cmd.Parameters.Add("@Acompanhar", OleDbType.Boolean).Value = C.Acompanhar;
            cmd.Parameters.Add("Eliminacao", OleDbType.Boolean).Value = C.Eliminacao;
            cmd.Parameters.Add("Incoporacao", OleDbType.Boolean).Value = C.Incorporacao;
            cmd.Parameters.Add("Fracao", OleDbType.Double).Value = C.Fracao;
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

        private static void IncluirLinha(Linhas L, int idModelo)
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            string textoSql = "INSERT INTO TableLinhas ( idModelo, CaixaInicio, CaixaFim, CorR, CorG, CorB, Direcao, ValorAB, ValorBA )" +
                    "VALUES (@idModelo, @CaixaInicio, @CaixaFim, @CorR, @CorG, @CorB, @Direcao, @ValorAB, @ValorBA )";
            cmd.CommandText = textoSql;
            cmd.Parameters.Add("@idModelo", OleDbType.Integer).Value = idModelo;
            cmd.Parameters.Add("@CaixaInicio", OleDbType.Integer).Value = L.CaixaInicio.Numero;
            cmd.Parameters.Add("@CaixaFim", OleDbType.Integer).Value = L.CaixaFim.Numero;
            cmd.Parameters.Add("@CorR", OleDbType.UnsignedTinyInt).Value = L.BackColor.R;
            cmd.Parameters.Add("@CorG", OleDbType.UnsignedTinyInt).Value = L.BackColor.G;
            cmd.Parameters.Add("@CorB", OleDbType.UnsignedTinyInt).Value = L.BackColor.B;
            cmd.Parameters.Add("Direcao", OleDbType.UnsignedTinyInt).Value = L.DirecaoDaLinha;
            cmd.Parameters.Add("ValorAB", OleDbType.Single).Value = L.ValorAB;
            cmd.Parameters.Add("ValorBA", OleDbType.Single).Value = L.ValorBA;
            cmd.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
        }

        public static DataTable SelecionarModelos()
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            string textoSql = "select idModelo, nmModelo, dtCriacao, Descricao, nmTipoModelo " +
                                "from Modelo M inner join TipoModelo TM on (M.idTipoModelo = TM.idTipoModelo) " +
                                "order by idModelo";
            
            OleDbCommand cmd = cn.CreateCommand();
            cmd.CommandText = textoSql;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable resultado = new DataTable();
            da.Fill(resultado);
            cn.Close();
            cn.Dispose();
            return resultado;
        }

        public static DataTable SelecionarTipos()
        {
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            string textoSql = "select idTipoModelo, nmTipoModelo " +
                                "from TipoModelo ";

            OleDbCommand cmd = cn.CreateCommand();
            cmd.CommandText = textoSql;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable resultado = new DataTable();
            da.Fill(resultado);
            cn.Close();
            cn.Dispose();
            return resultado;
        }

        public static Modelos SelecionarModelos(int cod)
        {
            OleDbConnection cn;
            Modelos Modelo = new Modelos();

            cn = Configuracoes.Conectar();
            string textoSql = "select idModelo, nmModelo, dtCriacao, Descricao, idTipoModelo " +
                                "from modelo M " +
                                "where idModelo = " + cod.ToString();
            
            OleDbCommand cmd = cn.CreateCommand();
            cmd.CommandText = textoSql;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable resultado = new DataTable();
            da.Fill(resultado);
            
            DataRow drow = resultado.Rows[0];
            cn.Close();
            cn.Dispose();
            Modelo.idModelo = (int)drow["idModelo"];
            Modelo.nmModelo = drow["nmModelo"].ToString();
            Modelo.dtCriacao = (DateTime)drow["dtCriacao"];
            Modelo.Descricao = drow["Descricao"].ToString();
            Modelo.Tipo.idTipoModelo = (int)drow["idTipoModelo"];
            return Modelo;
        }

        public static LinhasCollection PreencherLinhas(int codModelo, CaixasCollection ColCaixas)
        {
            LinhasCollection LinCol = new LinhasCollection();

            string textoSQL = "SELECT CaixaInicio, CaixaFim, " +
                        "CorR, CorG, CorB, Direcao, ValorAB, ValorBA FROM TableLinhas " +
                        "WHERE idModelo = " + codModelo.ToString();
                        
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            cmd.CommandText = textoSQL;
            OleDbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Linhas L = new Linhas();

                Caixas cxInicio = null;
                Caixas cxFim = null;
                foreach (Caixas cx in ColCaixas)
                {
                    if (cx.Numero == (int)dr["CaixaInicio"])
                        cxInicio = cx;
                    else if (cx.Numero == (int)dr["CaixaFim"])
                        cxFim = cx;

                    if (cxInicio != null && cxFim != null)
                        break;
                }

                L.CaixaInicio = cxInicio;
                L.CaixaFim = cxFim;
                L.BackColor = System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]);
                L.DirecaoDaLinha = (Linhas.Direcao)dr["Direcao"];
                L.ValorAB = (float)dr["ValorAB"];
                L.ValorBA = (float)dr["ValorBA"];

                LinCol.Add(L);
            }
            return LinCol;

        }

        public static CaixasCollection PreencherCaixas(int codModelo)
        {
            CaixasCollection CaixaCol = new CaixasCollection();

            string textoSql = " SELECT Numero, Nome, " +
                "Left, Top, Width, Height, " +
                "CorR, CorG, CorB, Acompanhar, " +
                "Eliminacao, Incorporacao, Fracao FROM TableCaixas WHERE idModelo = " + codModelo.ToString() + 
            " ORDER BY Numero";
            OleDbConnection cn;
            cn = Configuracoes.Conectar();
            OleDbCommand cmd = cn.CreateCommand();
            cmd.CommandText = textoSql;
            OleDbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Caixas C = new Caixas();

                //C.Numero = (dr["Numero"]== DBNull.Value? 0: (int)dr["Numero"]);
                //C.Numero = dr.GetInt32(0);
                C.Numero = (int)dr["Numero"];
                C.Nome = dr["Nome"].ToString();
                C.Acompanhar = (bool)dr["Acompanhar"];
                C.Eliminacao = (bool)dr["Eliminacao"];
                C.Incorporacao = (bool)dr["Incorporacao"];
                C.Fracao = (double)dr["Fracao"];
                C.Left = (int)dr["Left"];
                C.Top = (int)dr["Top"];
                C.Width = (int)dr["Width"];
                C.Height = (int)dr["Height"];
                C.BackColor = System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]);
                CaixaCol.Add(C);
            }
            return CaixaCol;
        }

    }
}
