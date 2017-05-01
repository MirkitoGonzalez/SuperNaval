using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNaval.Model
{
    public abstract class  Casilla
    {
        public int row;
        public int col;
        public abstract void Mostrar(DataGridViewCell casilla);
        public abstract string getMessage();
    }
}
