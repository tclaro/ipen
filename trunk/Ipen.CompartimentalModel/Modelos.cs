using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Ipen.CompartimentalModel
{
    public class Modelos
    {
        public TipoModelos Tipo;

        private int _idModelo;
        private string _nmModelo;
        private DateTime _dtCriacao;
        private string _Descricao;
        private Sistema _Colecao;
        
        
        #region Acesso aos Campos
        public Sistema Colecao
        {
            get { return _Colecao; }
            set { _Colecao = value; }
        }

        public int idModelo
        {
            get
            {
                return this._idModelo;
            }
            set
            {
                this._idModelo = value;
            }
        }
        public string nmModelo
        {
            get
            {
                return this._nmModelo;
            }
            set
            {
                this._nmModelo = value;
            }
        }
        public DateTime dtCriacao
        {
            get
            {
                return this._dtCriacao;
            }
            set
            {
                this._dtCriacao = value;
            }
        }
        public string Descricao
        {
            get
            {
                return this._Descricao;
            }
            set
            {
                this._Descricao = value;
            }
        }

        #endregion

        public Modelos()
        {
            this.idModelo = 0;
            this.dtCriacao = System.DateTime.Now;
            this.Tipo = new TipoModelos();
            this._Colecao = Sistema.getInstance();
        }

        public void PreencherCaixasLinhas()
        {
            this.Colecao.Clear();

            //Nunca poderia ser preenchido linhas antes de caixas
            CaixasCollection TodasCaixas = DataBD.PreencherCaixas(this.idModelo);

            foreach (Caixas cx in TodasCaixas)
                this.Colecao.Caixas.Add(cx);

            LinhasCollection TodasLinhas = DataBD.PreencherLinhas(this.idModelo, this.Colecao.Caixas);

            foreach (Linhas ln in TodasLinhas)
                this.Colecao.Linhas.Add(ln);

                
        }

    }
}
