using System;
using System.Collections.Generic;
using System.Text;

namespace CompartimentalModel
{
    public class DataXML
    {
        #region Campos
        string _arquivo;
        LinhasCollection _linhas;
        CaixasCollection _caixas;
        #endregion

        #region Construtor
        public DataXML(string Arquivo)
        {
            ConstrutorAuxiliar(Arquivo, null, null);
        }
        public DataXML(string Arquivo, LinhasCollection ColecaoDeLinhas, CaixasCollection ColecaoDeCaixas)
        {
            ConstrutorAuxiliar(Arquivo, ColecaoDeLinhas, ColecaoDeCaixas);
        }
        private void ConstrutorAuxiliar(string Arquivo, LinhasCollection ColecaoDeLinhas, CaixasCollection ColecaoDeCaixas)
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
        public LinhasCollection Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        public CaixasCollection Caixas
        {
            get { return _caixas; }
            set { _caixas = value; }
        }
        #endregion

        #region Métodos
        public void ExportarXML()
        {
            if (this._caixas == null && this._linhas == null)
                throw new Exception("Nada a exportar");
            
            Reservatorio res = new Reservatorio();
            if (this._caixas != null)
                res.ImportarCaixas(_caixas);
            if (this._linhas != null)
                res.ImportarLinhas(_linhas);
            res.WriteXml(_arquivo, System.Data.XmlWriteMode.WriteSchema);
            res.Dispose();
            res = null;
        }
        public void ImportarXML()
        {
            Reservatorio res = new Reservatorio();
            if (_caixas == null)
                _caixas = new CaixasCollection();
            if (_linhas == null)
                _linhas = new LinhasCollection();

            _caixas.Clear();
            _linhas.Clear();
            res.ImportarArquivo(_arquivo, ref _caixas, ref _linhas);

            res.Dispose();
            res = null;
        }
        #endregion
    }
}