using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNaval.Model
{
    class ShipLocation
    {
        int row;
        int col;
        int size;
        int orientation;

        public ShipLocation()
        { }

        public ShipLocation(int row, int col, int size, int orientation)
        {
            this.row = row;
            this.col = col;
            this.size = size;
            this.orientation = orientation;
        }

        public int Size { get => size; set => size = value; }      
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public int Orientation { get => orientation; set => orientation = value; }
    }
}
