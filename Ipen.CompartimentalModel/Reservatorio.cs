using System;
using System.Collections.Generic;
using System.Text;

namespace Ipen.CompartimentalModel
{
    class Reservatorio : System.Data.DataSet
    {
        #region Construtor
        public Reservatorio()
        {
            this.EnforceConstraints = false;
            this.Tables.Add("Modelo");
            this.Tables["Modelo"].Columns.Add("nmModelo", typeof(string));
            this.Tables["Modelo"].Columns.Add("dtCriacao", typeof(DateTime));
            this.Tables["Modelo"].Columns.Add("Descricao", typeof(string));
            this.Tables["Modelo"].Columns.Add("TipoModelo", typeof(string));
            
            this.Tables.Add("TableCaixas");
            this.Tables["TableCaixas"].Columns.Add("Numero", typeof(int));
            this.Tables["TableCaixas"].Columns.Add("Nome", typeof(string));
            this.Tables["TableCaixas"].Columns.Add("PosLeft", typeof(int));
            this.Tables["TableCaixas"].Columns.Add("PosTop", typeof(int));
            this.Tables["TableCaixas"].Columns.Add("PosWidth", typeof(int));
            this.Tables["TableCaixas"].Columns.Add("PosHeight", typeof(int));
            this.Tables["TableCaixas"].Columns.Add("CorR", typeof(byte));
            this.Tables["TableCaixas"].Columns.Add("CorG", typeof(byte));
            this.Tables["TableCaixas"].Columns.Add("CorB", typeof(byte));
            this.Tables["TableCaixas"].Columns.Add("Acompanhar", typeof(bool));
            this.Tables["TableCaixas"].Columns.Add("Eliminacao", typeof(bool));

            this.Tables.Add("TableLinhas");
            this.Tables["TableLinhas"].Columns.Add("CaixaInicio", typeof(int));
            this.Tables["TableLinhas"].Columns.Add("CaixaFim", typeof(int));
            this.Tables["TableLinhas"].Columns.Add("CorR", typeof(byte));
            this.Tables["TableLinhas"].Columns.Add("CorG", typeof(byte));
            this.Tables["TableLinhas"].Columns.Add("CorB", typeof(byte));
            this.Tables["TableLinhas"].Columns.Add("Direcao", typeof(byte));
            this.Tables["TableLinhas"].Columns.Add("ValorAB", typeof(float));
            this.Tables["TableLinhas"].Columns.Add("ValorBA", typeof(float));
        }
        #endregion

        public void ExportarModelo(Modelos Modelo)
        {
            System.Data.DataRow dr = this.Tables["Modelo"].NewRow();
            dr["nmModelo"] = Modelo.nmModelo;
            dr["dtCriacao"] = Modelo.dtCriacao;
            dr["Descricao"] = Modelo.Descricao;
            dr["TipoModelo"] = Modelo.Tipo.nmTipoModelo;
            this.Tables["Modelo"].Rows.Add(dr);

            if (Modelo.Colecao.Caixas != null)
                ExportarCaixas(Modelo.Colecao.Caixas);
            if (Modelo.Colecao.Linhas != null)
                ExportarLinhas(Modelo.Colecao.Linhas);
        }


        private void ExportarCaixas(CaixasCollection ConjuntoDeCaixas)
        {
            foreach (Caixas cx in ConjuntoDeCaixas)
            {
                System.Data.DataRow dr = this.Tables["TableCaixas"].NewRow();
                dr["Numero"] = cx.Numero;
                dr["Nome"] = cx.Nome;
                dr["PosLeft"] = cx.Left;
                dr["PosTop"] = cx.Top;
                dr["PosWidth"] = cx.Width;
                dr["PosHeight"] = cx.Height;
                dr["CorR"] = cx.BackColor.R;
                dr["CorG"] = cx.BackColor.G;
                dr["CorB"] = cx.BackColor.B;
                dr["Acompanhar"] = cx.Acompanhar;
                dr["Eliminacao"] = cx.Eliminacao;
                this.Tables["TableCaixas"].Rows.Add(dr);
            }
        }
        private void ExportarLinhas(LinhasCollection ConjuntoDeLinhas)
        {
            foreach (Linhas ln in ConjuntoDeLinhas)
            {
                System.Data.DataRow dr = this.Tables["TableLinhas"].NewRow();
                dr["CaixaInicio"] = ln.CaixaInicio.Numero;
                dr["CaixaFim"] = ln.CaixaFim.Numero;
                dr["CorR"] = ln.BackColor.R;
                dr["CorG"] = ln.BackColor.G;
                dr["CorB"] = ln.BackColor.B;
                dr["Direcao"] = (byte) ln.DirecaoDaLinha;
                dr["ValorAB"] = ln.ValorAB;
                dr["ValorBA"] = ln.ValorBA;
                this.Tables["TableLinhas"].Rows.Add(dr);
            }
        }
        public void ImportarArquivo(string Arquivo, ref Modelos Modelo)
        {
            Modelo.Colecao.Caixas.Clear();
            Modelo.Colecao.Linhas.Clear();
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(Arquivo, System.Data.XmlReadMode.ReadSchema);

            foreach (System.Data.DataRow dr in ds.Tables["Modelo"].Rows)
            {
                Modelo.nmModelo = dr["nmModelo"].ToString();
                Modelo.dtCriacao = (DateTime)dr["dtCriacao"];
                Modelo.Descricao = dr["Descricao"].ToString();
            }

            foreach (System.Data.DataRow dr in ds.Tables["TableCaixas"].Rows)
            {
                Caixas cx = new Caixas((int)dr["Numero"], (string)dr["Nome"], new System.Drawing.Point((int)dr["PosLeft"], (int)dr["PosTop"]), System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]), (bool)dr["Acompanhar"], (bool)dr["Eliminacao"]);
                Modelo.Colecao.Caixas.Add(cx);
            }

            foreach (System.Data.DataRow dr in ds.Tables["TableLinhas"].Rows)
            {
                Caixas cxInicio = null;
                Caixas cxFim = null;

                foreach (Caixas cx in Modelo.Colecao.Caixas)
                {
                    if (cx.Numero == (int)dr["CaixaInicio"])
                        cxInicio = cx;
                    else if (cx.Numero == (int)dr["CaixaFim"])
                        cxFim = cx;

                    if (cxInicio != null && cxFim != null)
                        break;
                }
                Linhas ln = new Linhas(cxInicio, cxFim, System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]), (Linhas.Direcao)dr["Direcao"], (float)dr["ValorAB"], (float)dr["ValorBA"]);
                Modelo.Colecao.Linhas.Add(ln);
            }
        }
    }
}
