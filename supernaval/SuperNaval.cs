using SuperNaval.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNaval
{
    public partial class SuperNaval : Form
    {
        private static int INVALID = -1;
        private static int UP = 1;
        private static int DOWN = 2;
        private static int LEFT = 3;
        private static int RIGHT = 4;
        private static int NUM_ROWS = 10;
        private static int NUM_COLS = 10;
        private static int WIDTH = 50;
        private static int HEIGHT = 50;


        IList<int> shuffledArray;
        int shuffledIndex = 0;

        IList<int> orientationArray;
        int orientationIndex;

        Casilla[,] matrix;

        public SuperNaval()
        {
            InitializeComponent();
            // eventos de grid            

            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);

            // tamaño del grid
            dataGridView.Width = WIDTH * NUM_ROWS;
            dataGridView.Height = HEIGHT * NUM_COLS;

            // quitar la cabeceras
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.RowHeadersVisible = false;

            dataGridView.ScrollBars = ScrollBars.None;

            // establecer el grid como solo lectura
            dataGridView.ReadOnly = true;
            // no permitir mover lineas
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            // no permitir añadir columnas (esto quita una fila)
            dataGridView.AllowUserToAddRows = false;


            for (int a = 0; a < NUM_COLS; a++)
            {
                // crear una columna de tipo imagen
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();

                imgColumn.Name = "columna" + a.ToString();
                dataGridView.Columns.Insert(a, imgColumn);

                // establecer el ancho
                dataGridView.Columns[a].Width = WIDTH;
            }

            // crear filas
            for (int a = 0; a < NUM_ROWS; a++)
            {
                dataGridView.Rows.Add();

                // establecer el alto de la fila                
                dataGridView.Rows[a].Height = HEIGHT;
            }
        }

        private void Initialize()
        {
            //matriz que contiene los 100 objetos casilla
            matrix = new Casilla[10, 10];

            //Desordenamos el array que usaremos para obtener numeros aleatorios sin que se repitan
            var sortedArray = Enumerable.Range(1, 100).ToArray();
            // Este es el indice que usaremos para recorrer el array ya una vez desordenado
            shuffledArray = Shuffle(sortedArray);

            createBoat(4, 1);
            createBoat(3, 2);
            createBoat(2, 3);
            createBoat(1, 4);


            //solo para comprobar como se situan los barcos
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //       Object casilla = dataGridView.Rows[i].Cells[j].Value = Properties.Resources.Subma30x30;
            //        if (matrix[i, j]!=null)
            //            matrix[i, j].Mostrar(casilla);
            //    }
            //}
        }




        private void createBoat(int size, int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                ShipLocation shipLocation = getEmptyPoint(size - 1);

                for (int j = 0; j < size; j++)
                {                  
                    if (shipLocation.Orientation==UP)
                    {
                        matrix[shipLocation.Row - j, shipLocation.Col] = new Barco();
                        Object casilla = dataGridView.Rows[shipLocation.Row -j].Cells[shipLocation.Col].Value = Properties.Resources.Subma30x30;
                            matrix[shipLocation.Row -j, shipLocation.Col].Mostrar(casilla);
                    }
                    else if (shipLocation.Orientation == DOWN)
                    {
                        matrix[shipLocation.Row + j, shipLocation.Col] = new Barco();
                        Object casilla = dataGridView.Rows[shipLocation.Row +j].Cells[shipLocation.Col].Value = Properties.Resources.Subma30x30;
                        matrix[shipLocation.Row +j, shipLocation.Col].Mostrar(casilla);
                    }
                    else if (shipLocation.Orientation == LEFT)
                    {
                        matrix[shipLocation.Row, shipLocation.Col - j] = new Barco();
                        Object casilla = dataGridView.Rows[shipLocation.Row].Cells[shipLocation.Col-j].Value = Properties.Resources.Subma30x30;
                        matrix[shipLocation.Row, shipLocation.Col -j].Mostrar(casilla);
                    }
                    else if (shipLocation.Orientation == RIGHT)
                    {
                        matrix[shipLocation.Row, shipLocation.Col + j] = new Barco();
                        Object casilla = dataGridView.Rows[shipLocation.Row].Cells[shipLocation.Col + j].Value = Properties.Resources.Subma30x30;
                        matrix[shipLocation.Row, shipLocation.Col + j].Mostrar(casilla);
                    }
                }

            }
        }

        private ShipLocation getEmptyPoint(int size)
        {
            // "x" es la parte entera // "y" es el resto
            int x = shuffledArray.ElementAt(shuffledIndex) / 10;
            int y = shuffledArray.ElementAt(shuffledIndex) % 10;

            shuffledIndex++;
            // Usamos recursividad para obtener un punto vacio
            if (matrix[x, y] != null)
                return getEmptyPoint(size);
            else
            {
                // Una vez tenemos una posicion valida buscamos si tiene una orientacion valida a partid de la cual colocar el barco
                var sortedArray = Enumerable.Range(1, 4).ToArray();
                orientationArray = Shuffle(sortedArray);
                orientationIndex = 0;

                // llamamos al metodo recursivo que nos devuelve una orientacion aleatoria
                // y en caso de que ninguna de las orientaciones posibles sea valida nos devuelve INVALID
                int orientation = getOrientation(size, x, y);
                if (orientation == INVALID)
                {
                    return getEmptyPoint(size);
                }
                //                               
                return new ShipLocation(x, y, size, orientation);
            }

        }


        private int getOrientation(int size, int row, int col)
        {
            if (orientationIndex == 4)
            {
                return INVALID;
            }
            int orientation = orientationArray[orientationIndex];
            orientationIndex++;

            if (orientation == LEFT)
            {
                if (col + size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (matrix[row, col - i] != null)
                        {
                            return getOrientation(size, row, col);
                        }
                    }
                }
                else
                {
                    return getOrientation(size, row, col);
                }
            }
            else if (orientation == RIGHT)
            {
                if (col + size <= NUM_COLS - 1)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (matrix[row, col + i] != null)
                        {
                            return getOrientation(size, row, col);
                        }
                    }
                }
                else
                {
                    return getOrientation(size, row, col);
                }
            }
            else if (orientation == UP)
            {
                if (row + size >= 0)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (matrix[row - i, col] != null)
                        {
                            return getOrientation(size, row, col);
                        }
                    }
                }
                else
                {
                    return getOrientation(size, row, col);
                }
            }
            else if (orientation == DOWN)
            {
                if (row + size <= NUM_ROWS - 1)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (matrix[row + i, col] != null)
                        {
                            return getOrientation(size, row, col);
                        }
                    }
                }
                else
                {
                    return getOrientation(size, row, col);
                }
            }

            return orientation;

        }

        /// <summary>
        /// Algoritmo Fisher-Yates para ordenar aleatoriamente un array
        /// </summary>
        /// <param name="array"> Array que va a desordenar</param>
        /// <returns> array desordenado</returns>
        static IList<int> Shuffle(int[] array)
        {
            var rand = new Random();
            var count = array.Length;
            for (int i = 0; i < count; i++)
            {
                int r = i + (int)(rand.NextDouble() * (count - i));
                int number = array[r];
                array[r] = array[i];
                array[i] = number;
            }
            return array;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            label1.Text = e.RowIndex + "," + e.ColumnIndex;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Initialize();
        }
    }
}
