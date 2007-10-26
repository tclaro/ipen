using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ipen.CompartimentalModel
{
    public class CaixasCollection : List<Caixas>
    {
        #region Construtor
        public CaixasCollection()
        {

        }
        #endregion

        #region Override de métodos na base
        public new void Add(Caixas cx)
        {
            base.Add(cx);
            cx.Click += new EventHandler(cx_Click);
            cx.MouseDown += new MouseEventHandler(cx_MouseDown);
            cx.MouseMove += new MouseEventHandler(cx_MouseMove);
            cx.MouseUp += new MouseEventHandler(cx_MouseUp);
            cx.DoubleClick += new EventHandler(cx_DoubleClick);
            cx.KeyDown += new System.Windows.Forms.KeyEventHandler(cx_KeyDown);
            cx.Deleted += new Caixas.CaixaEventHandler(cx_Deleted);
            cx.PropertyChanged += new Caixas.CaixaEventHandler(cx_PropertyChanged);
            cx.Moved += new Caixas.CaixaEventHandler(cx_Moved);
            cx.Move += new EventHandler(cx_Move);
            Reindex();
        }
        public new void Remove(Caixas cx)
        {
            base.Remove(cx);
            cx.Delete();
            Reindex();
        }
        public new void RemoveAt(int Index)
        {
            Caixas cx = this[Index];
            base.RemoveAt(Index);
            cx.Delete();
            Reindex();
        }
        #endregion

        #region Novos eventos
        public event EventHandler BoxMove;
        public event Caixas.CaixaEventHandler BoxMoved;
        public event Caixas.CaixaEventHandler BoxPropertyChanged;
        public event Caixas.CaixaEventHandler BoxDeleted;
        public event KeyEventHandler BoxKeyDown;
        public event EventHandler BoxDoubleClick;
        public event EventHandler BoxClick;
        public event MouseEventHandler BoxMouseDown;
        public event MouseEventHandler BoxMouseUp;
        public event MouseEventHandler BoxMouseMove;

        protected void OnBoxMove(object sender, EventArgs e)
        {
            if (BoxMove != null) { BoxMove(sender, e); }
        }
        protected void OnBoxMoved(Caixas.BoxEventArgs be)
        {
            if (BoxMoved != null) { BoxMoved(be); }
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
            if (BoxMouseUp!= null) { BoxMouseUp(sender, e); }
        }
        protected void OnBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (BoxMouseMove != null) { BoxMouseMove(sender, e); }
        }
        #endregion

        #region Métodos para eventos
        void cx_Move(object sender, EventArgs e)
        {
            OnBoxMove(sender, e);
        }
        void cx_Moved(Caixas.BoxEventArgs be)
        {
            OnBoxMoved(be);
        }
        void cx_PropertyChanged(Caixas.BoxEventArgs be)
        {
            OnBoxPropertyChanged(be);
        }
        void cx_Deleted(Caixas.BoxEventArgs be)
        {
            OnBoxDeleted(be);
        }
        void cx_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            OnBoxKeyDown(sender, e);
        }
        void cx_DoubleClick(object sender, EventArgs e)
        {
            OnBoxDoubleClick(sender, e);
        }
        void cx_Click(object sender, EventArgs e)
        {
            OnBoxClick(sender, e);
        }
        void cx_MouseUp(object sender, MouseEventArgs e)
        {
            OnBoxMouseUp(sender, e);
        }
        void cx_MouseMove(object sender, MouseEventArgs e)
        {
            OnBoxMouseMove(sender, e);
        }
        void cx_MouseDown(object sender, MouseEventArgs e)
        {
            OnBoxMouseDown(sender, e);
        }
        #endregion

        #region Métodos internos
        private void Reindex()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Numero = i + 1;
            }
        }
        #endregion


    }
}