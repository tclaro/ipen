using System;
using System.Collections.Generic;
using System.Text;

namespace CBT
{
    class Reservatorio : System.Data.DataSet
    {
        #region Construtor
        public Reservatorio()
        {
            this.EnforceConstraints = false;
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

        #region Métodos Públicos
        public void ImportarCaixas(CaixasCollection ConjuntoDeCaixas)
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
        public void ImportarLinhas(LinhasCollection ConjuntoDeLinhas)
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
        public void ImportarArquivo(string Arquivo, ref CaixasCollection refCaixas, ref LinhasCollection refLinhas)
        {
            refCaixas.Clear();
            refLinhas.Clear();
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(Arquivo, System.Data.XmlReadMode.ReadSchema);
            foreach (System.Data.DataRow dr in ds.Tables["TableCaixas"].Rows)
            {
                Caixas cx = new Caixas((int)dr["Numero"], (string)dr["Nome"], new System.Drawing.Point((int)dr["PosLeft"], (int)dr["PosTop"]), System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]), (bool)dr["Acompanhar"], (bool)dr["Eliminacao"]);
                refCaixas.Add(cx);
            }
            foreach (System.Data.DataRow dr in ds.Tables["TableLinhas"].Rows)
            {
                Caixas cxInicio = null;
                Caixas cxFim = null;

                foreach (Caixas cx in refCaixas)
                {
                    if (cx.Numero == (int)dr["CaixaInicio"])
                        cxInicio = cx;
                    else if (cx.Numero == (int)dr["CaixaFim"])
                        cxFim = cx;

                    if (cxInicio != null && cxFim != null)
                        break;
                }
                Linhas ln = new Linhas(cxInicio, cxFim, System.Drawing.Color.FromArgb((byte)dr["CorR"], (byte)dr["CorG"], (byte)dr["CorB"]), (Linhas.Direcao)dr["Direcao"], (float)dr["ValorAB"], (float)dr["ValorBA"]);
                refLinhas.Add(ln);
            }
        }
        #endregion

    }
}
