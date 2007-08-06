using System;
using System.Collections.Generic;
using System.Text;

namespace SSID
{
    class Compartimento
    {
        #region Campos
		private string _Nome;
        private int _Numero;
        private int _Id;
        private bool _Acompanhar;
        private System.Drawing.Color _Cor;
		#endregion

		#region Construtor

        public Compartimento(int Numero, int Id, string Texto, System.Drawing.Color Cor, bool Acompanhar)
		{
            _Numero = Numero;
            _Id = Id;
            _Nome = Texto;
            _Cor = Cor;
            _Acompanhar = Acompanhar;
        }

		#endregion

		#region Acesso aos Campos
        public string Nome
		{
			get
			{
				return _Nome;
			}
			set
			{
				_Nome = value;
			}
		}
        public bool Acompanhar
        {
            get
            {
                return _Acompanhar;
            }
            set
            {
                _Acompanhar = value;
            }
        }
		public System.Drawing.Color Cor
		{
			get
			{
				return _Cor;
			}
			set
			{
				_Cor = value;
			}
		}
        public int Numero
        {
            get
            {
                return _Numero;
            }
            set
            {
                _Numero = value;
            }
        }
        public int Id
        {
            get
            {
                return _Id;
            }
        }
		#endregion

    }
}
