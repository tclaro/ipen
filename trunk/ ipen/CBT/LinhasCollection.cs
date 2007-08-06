using System;
using System.Collections.Generic;
using System.Text;

namespace CBT
{
    public class LinhasCollection : List<Linhas>
    {
        public LinhasCollection()
        {
        }
        public void Remove(Caixas cx)
        {
            for(int i = this.Count-1 ; i>=0 ; i--)

            {
                if (this[i].CaixaFim == cx || this[i].CaixaInicio == cx)
                    this.RemoveAt(i);
            }
        }



    }
}
