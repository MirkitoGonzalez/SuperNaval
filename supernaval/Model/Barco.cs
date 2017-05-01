using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNaval.Model
{

    public abstract class Barco : Casilla
    {
        protected int size;
        protected int damage;
        protected bool hiited;
        protected static string TOCADO = "Tocado";
        protected static string HUNDIDO  = "Tocado y hundido";

        public override void Mostrar(DataGridViewCell casilla)
        {
            casilla.Value = Properties.Resources.destruccion;
            damage++;
            hiited = true;
        }
        public int getSize()
        {
            return size;
        }
        public int getDamage()
        {
            return damage;
        }
        public bool isHitted()
        {
            return hiited;
        }

    }
    public class Portaviones : Barco
    {
        public Portaviones()
        {         
            size = 4;
            damage = 0;
        }
        public override string getMessage()
        {
            if (size > damage)
            {
                return TOCADO;
            }
            else
                return HUNDIDO;
        }
    }
    public class Crucero : Barco
    { // 2 de estos
        public Crucero()
        {
            size = 3;
            damage = 0;
        }
        public override string getMessage()
        {
            if (size > damage)
            {
                return TOCADO;
            }
            else
                return HUNDIDO;
        }
    }
    public class Destroyer : Barco
    { // 3 de estos 
        public Destroyer()
        {
            size = 2;
            damage = 0;
        }
        public override string getMessage()
        {
            if (size > damage)
            {
                return TOCADO;
            }
            else
                return HUNDIDO;
        }
    }
    public class Submarine : Barco
    { // 4 submarinitos
        public Submarine()
        {
            size = 1;
            damage = 0;
        }
        public override string getMessage()
        {
            return HUNDIDO;
        }


    }
}
