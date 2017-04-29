using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNaval.Model
{

    public class Barco : Casilla
    {
        protected int size;

        public override void Mostrar(DataGridViewCell casilla)
        {
            casilla.Value = Properties.Resources.Subma30x30;
        }

        public int getSize()
        {
            return this.size;
        }
    }
    public class Portaviones : Barco
    {
        public Portaviones(int size)
        {
            // 1 de este
            size = 4;
        }
    }
    public class Crucero : Barco
    { // 2 de estos
        public Crucero()
        {
            size = 3;
        }
    }
    public class Destroyer : Barco
    { // 3 de estos 
        public Destroyer()
        {
            size = 2;
        }
    }
    public class Submarine : Barco
    { // 4 submarinitos
        public Submarine()
        {
            size = 1;
        }


    }
}
