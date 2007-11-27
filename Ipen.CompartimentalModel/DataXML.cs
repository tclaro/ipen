using System;
using System.Collections.Generic;
using System.Text;

namespace Ipen.CompartimentalModel
{
    public class DataXML
    {
        #region Campos
        string _arquivo;
        Modelos _Modelo;
        #endregion

        #region Construtor
        public DataXML(string Arquivo)
        {
            ConstrutorAuxiliar(Arquivo, null);
        }
        private void ConstrutorAuxiliar(string Arquivo, Modelos Modelo)
        {
            _arquivo = Arquivo;
        }
        #endregion

        #region Propriedades
        public string Arquivo
        {
            get { return _arquivo; }
            set { _arquivo = value; }
        }
        public Modelos Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        #endregion

        #region Métodos
        public void ExportarXML()
        {
            if (this.Modelo.Colecao.Caixas.Count == 0)
                throw new Exception("Nada a exportar");
            
            Reservatorio res = new Reservatorio();

            res.ExportarModelo(this.Modelo);
            res.WriteXml(_arquivo, System.Data.XmlWriteMode.WriteSchema);
            
            res.Dispose();
            res = null;
        }
        
        public void ImportarXML()
        {
            Reservatorio res = new Reservatorio();
            Modelos M = new Modelos();

            res.ImportarArquivo(_arquivo, ref M);
            this.Modelo = M;

            res.Dispose();
            res = null;
        }
        #endregion
    }
}