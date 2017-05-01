using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNaval.Model
{
    /// <summary>
    /// The tile are the individual pieces of the map.
    /// </summary>
    public class Agua : Casilla
    {
        public Agua()

        {

        }
        public Agua(int row, int col)

        {
        }

        public override string getMessage()
        {
            return "Agua";
        }

        public override void Mostrar(DataGridViewCell casilla)
        {
            casilla.Style.BackColor = System.Drawing.Color.Blue;
        }

    }
}



