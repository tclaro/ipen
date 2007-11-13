using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ipen.CompartimentalModel
{
    public class Sistema
    {
        #region Campos
        CaixasCollection _caixas;
        LinhasCollection _linhas;
        #endregion

        #region Construtor
        private Sistema()
        {
            _caixas = new CaixasCollection();
            _caixas.BoxClick += new EventHandler(_caixas_BoxClick);
            _caixas.BoxMouseDown += new MouseEventHandler(_caixas_BoxMouseDown);
            _caixas.BoxMouseMove += new MouseEventHandler(_caixas_BoxMouseMove);
            _caixas.BoxMouseUp += new MouseEventHandler(_caixas_BoxMouseUp);
            _caixas.BoxDeleted += new Caixas.CaixaEventHandler(_caixas_BoxDeleted);
            _caixas.BoxDoubleClick += new EventHandler(_caixas_BoxDoubleClick);
            _caixas.BoxKeyDown += new System.Windows.Forms.KeyEventHandler(_caixas_BoxKeyDown);
            _caixas.BoxMoved += new Caixas.CaixaEventHandler(_caixas_BoxMoved);
            _caixas.BoxMove += new EventHandler(_caixas_BoxMove);
            _caixas.BoxPropertyChanged += new Caixas.CaixaEventHandler(_caixas_BoxPropertyChanged);
            _linhas = new LinhasCollection();
        }

        //public static readonly Sistema Instancia = new Sistema();


        private static Sistema referencia;

        public static Sistema getInstance()
        {
            if (referencia == null)
                referencia = new Sistema();

            return referencia;
        }

        #endregion

        #region Novos eventos
        public event Caixas.CaixaEventHandler BoxMoved;
        public event EventHandler BoxMove;
        public event Caixas.CaixaEventHandler BoxPropertyChanged;
        public event Caixas.CaixaEventHandler BoxDeleted;
        public event KeyEventHandler BoxKeyDown;
        public event EventHandler BoxDoubleClick;
        public event EventHandler BoxClick;
        public event MouseEventHandler BoxMouseDown;
        public event MouseEventHandler BoxMouseUp;
        public event MouseEventHandler BoxMouseMove;

        protected void OnBoxMoved(Caixas.BoxEventArgs be)
        {
            if (BoxMoved != null) { BoxMoved(be); }
        }
        protected void OnBoxMove(object sender, EventArgs e)
        {
            if (BoxMove != null) { BoxMove(sender, e); }
        }
        protected void OnBoxPropertyChanged(Caixas.BoxEventArgs be)
        {
            if (BoxPropertyChanged != null) { BoxPropertyChanged(be); }
        }
        protected void OnBoxDeleted(Caixas.BoxEventArgs be)
        {
            if (BoxDeleted != null) { BoxDeleted(be); }
        }
        protected void OnBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (BoxKeyDown != null) { BoxKeyDown(sender, e); }
        }
        protected void OnBoxDoubleClick(object sender, EventArgs e)
        {
            if (BoxDoubleClick != null) { BoxDoubleClick(sender, e); }
        }
        protected void OnBoxClick(object sender, EventArgs e)
        {
            if (BoxClick != null) { BoxClick(sender, e); }
        }
        protected void OnBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (BoxMouseDown != null) { BoxMouseDown(sender, e); }
        }
        protected void OnBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (BoxMouseUp != null) { BoxMouseUp(sender, e); }
        }
        protected void OnBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (BoxMouseMove != null) { BoxMouseMove(sender, e); }
        }
        #endregion

        #region Métodos de eventos
        void _caixas_BoxPropertyChanged(Caixas.BoxEventArgs be)
        {
            OnBoxPropertyChanged(be);
        }
        void _caixas_BoxMoved(Caixas.BoxEventArgs be)
        {
            OnBoxMoved(be);
        }
        void _caixas_BoxMove(object sender, EventArgs e)
        {
            OnBoxMove(sender, e);
        }
        void _caixas_BoxKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            OnBoxKeyDown(sender, e);
        }
        void _caixas_BoxDoubleClick(object sender, EventArgs e)
        {
            OnBoxDoubleClick(sender, e);
        }
        void _caixas_BoxDeleted(Caixas.BoxEventArgs be)
        {
            _linhas.Remove(be.Box);
            OnBoxDeleted(be);
        }
        void _caixas_BoxClick(object sender, EventArgs e)
        {
            OnBoxClick(sender, e);
        }
        void _caixas_BoxMouseUp(object sender, MouseEventArgs e)
        {
            OnBoxMouseUp(sender, e);
        }
        void _caixas_BoxMouseMove(object sender, MouseEventArgs e)
        {
            OnBoxMouseMove(sender, e);
        }
        void _caixas_BoxMouseDown(object sender, MouseEventArgs e)
        {
            OnBoxMouseDown(sender, e);
        }
        #endregion

        #region Propriedades
        public CaixasCollection Caixas
        {
            get { return _caixas; }
            set { _caixas = value; }
        }
        public LinhasCollection Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        #endregion

        #region Métodos

        public Linhas ObterLinhaPorCaixas(Caixas cx1, Caixas cx2)
        {
            foreach (Linhas ln in this.Linhas)
            {
                if (ln.CaixaInicio == cx1 && ln.CaixaFim == cx2 || ln.CaixaFim == cx1 && ln.CaixaInicio == cx2)
                    return ln;
            }
            return null;
        }

        public int ContarLigacoesAssociadas(Caixas cx)
        {
            int contLigacao = 0;
            foreach (Linhas ln in this.Linhas)
                if (ln.CaixaFim == cx || ln.CaixaInicio == cx)
                    contLigacao++;
            return contLigacao;
        }

        public void Clear()
        {
            this.Linhas.Clear();
            this.Caixas.Clear();
        }
                

        #endregion
    }
}
