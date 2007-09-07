using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Ipen.CompartimentalModel
{
    public class Modelos
    {
        public Sistema Colecao = new Sistema();

        private int _idModelo;
        private string _nmModelo;
        private DateTime _dtCriacao;
        private string _Descricao;

        #region Acesso aos Campos
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
        }

        public void PreencherCaixasLinhas()
        {
            //Nunca poderia ser preenchido linhas antes de caixas
            this.Colecao.Caixas = DataBD.PreencherCaixas(this.idModelo);
            this.Colecao.Linhas = DataBD.PreencherLinhas(this.idModelo, this.Colecao.Caixas);
        }

    }
}
