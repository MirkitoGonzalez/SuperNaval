using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using SuperNaval.Model;
using System.Linq;

namespace SuperNaval
{
    public partial class Form1 : Form
    {
        IList<int> shuffledArray;
        int shuffledIndex;

        /// <summary>
        /// constructor de la clase
        /// </summary>
        public Form1()
        {
            InitializeComponent();

        }
        private Image agua = Properties.Resources.Agua;
        public bool OCUP;      // used to indicate if unit is occupied
        public bool ATACA;      // used to indicate if unit is attacked 

       

        private void Pos_Barco(object sender, EventArgs e)
        {
            Casilla casilla1 = new Agua();
            Casilla casilla2 = new Barco();
          

            Casilla[,] matriz = new Casilla[10, 10];
            matriz[0, 0] = new Barco();
          
        }
        private void Posicion_Click(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition posicion = tableLayoutPanel2.GetCellPosition((Control)sender);
            label1.Text = string.Concat(posicion.Row, "," ,posicion.Column);

            Casilla[,] matriz = new Casilla[10, 10];
            matriz[0, 0] = new Agua();
            matriz[0, 1] = new Barco();
            Casilla casillaPulsada = matriz[posicion.Row, posicion.Column];
           

            PictureBox pic = getPictureBox(posicion.Row, posicion.Column);
            casillaPulsada.Mostrar(a1);


            // label1.Text = new TableLayoutPanelCellPosition().ToString();
            // label1.Text = (posicion.Row + posicion.Column).ToString();
        }

        private PictureBox getPictureBox(int row, int column)
        {
            return new PictureBox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // Me dice la posicion actual pero no se pararlo
        /* label1.Text = new TableLayoutPanelCellPosition(i, j).ToString(); */
        private void getcontrolFromPosBtn_Click(object sender, EventArgs e)
        {
           
            int i = 0;
            int j = 0;
            Trace.WriteLine(this.tableLayoutPanel2.ColumnCount);
            Trace.WriteLine(this.tableLayoutPanel2.RowCount);
            

            for (i = 0; i <= this.tableLayoutPanel2.ColumnCount; i++)
            {
                for (j = 0; j <= tableLayoutPanel2.RowCount; j++)
                {
                    Control c = tableLayoutPanel2.GetControlFromPosition(i, j);

                    if (c != null)
                    {
                        Trace.WriteLine(c.ToString());
                    }
                }
            }
        }



        private void panel1_MouseEnter(object sender, System.EventArgs e)
        {
            // Update the mouse event label to indicate the MouseEnter event occurred.
         
        }


        private void OnPictureBoxMouseEnter(object sender, EventArgs e)
        {
           /* BackColor = Color.Red; */
        }

        private void OnPictureBoxMouseLeave(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rendirse Rndirse = new Rendirse();
            Rndirse.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Wiin Win = new Wiin();
            Win.Show();
        }

        private void button1_Click(object sender, TableLayoutCellPaintEventArgs e)
        {
      //  label1.Text = new TableLayoutPanelCellPosition().ToString();
        {
            for (int j = 0; j < 10; j++)
            {
                if (j % 3 == 0)
                {
                    if (e.Column == j && e.Row == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                    }
                }

                else
                {
                    if (e.Column == j && e.Row == 1)
                    {
                        e.Graphics.FillRectangle(Brushes.Brown, e.CellBounds);
                    }
                }
            }
        }
    }

        

    }
}

