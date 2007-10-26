using System;
using System.Collections.Generic;
using System.Text;

namespace Ipen.CompartimentalModel
{
    public class TipoModelos
    {
        private int _idTipoModelo;
        private string _nmTipoModelo;

        #region Acesso aos Campos
        public int idTipoModelo
        {
            get
            {
                return this._idTipoModelo;
            }
            set
            {
                this._idTipoModelo = value;
            }
        }
        public string nmTipoModelo
        {
            get
            {
                return this._nmTipoModelo;
            }
            set
            {
                this._nmTipoModelo = value;
            }
        }
        #endregion

    }
}
