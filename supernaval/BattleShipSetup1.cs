using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SuperNaval
{
    public class BattleshipSetup1
    {

        public class Boat
        {
           protected int size;
        }
        public class Portaviones : Boat
        {
            public Portaviones()
            { // 1 de este
                size = 4;
            }
        }
        public class Crucero : Boat
        { // 2 de estos
            public Crucero()
            {
                size = 3;
            }
        }
        public class Destroyer : Boat
        { // 3 de estos 
            public Destroyer()
            {
                size = 2;
            }
        }
        public class Submarine : Boat
        { // 4 submarinitos
            public Submarine()
            {
                size = 1;
            }
        }

        public class ShipSetup
        {
            /*    int[][] grid = {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                   {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},{0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},{0, 0, 0, 0, 0, 0, 0, 0, 0, 0},{0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
              */
            public void setShips()
            {
                Boat[] ship;
                ship = new Boat[10];
                ship[0] = new Portaviones();
                ship[1] = new Crucero();
                ship[2] = new Crucero();
                ship[3] = new Destroyer();
                ship[4] = new Destroyer();
                ship[5] = new Destroyer();
                ship[6] = new Submarine();
                ship[7] = new Submarine();
                ship[8] = new Submarine();
                ship[9] = new Submarine(); }
                
          /*      for (int i = 0; i < 10; i++)
                {
                    int way = (int)(Math.random() * 2);
                    if (way == 0)
                    {
                        int x = (int)(Math.random() * 7);
                        int y = (int)(Math.random() * (7 - ship[i].size));
                        for (int j = 0; j < ship[i].size; j++)
                        {
                            grid[x][y + j] = i + 1;
                        }
                    }
                    if (way == 1)
                    {
                        int x = (int)(Math.random() * (7 - ship[i].size));
                        int y = (int)(Math.random() * 7);
                        for (int j = 0; j < ship[i].size; j++)
                        {
                            grid[x + j][y] = i + 1;
                        }
                    }
                }
            } */
         //   public int[][] getShips()
         //   {
           //       return [1][1]; 
         //   }
        }
    }
}