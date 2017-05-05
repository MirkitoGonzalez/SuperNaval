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
        private static int SUBMARINO = 1;
        private static int DESTRUCTOR = 2;
        private static int CRUCERO = 3;
        private static int PORTAVIONES = 4;


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

            this.dataGridView.DoubleBuffered(true);
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
            var sortedArray = Enumerable.Range(0, 100).ToArray();
            // Este es el indice que usaremos para recorrer el array ya una vez desordenado
            shuffledArray = Shuffle(sortedArray);

            createBoat(4, 1);
            createBoat(3, 2);
            createBoat(2, 3);
            createBoat(1, 4);

            rellenarSobrantesAgua();

            dataGridView.Enabled = true;


            ////solo para comprobar como se situan los barcos
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        DataGridViewCell cell;
            //        if (matrix[i, j] != null)
            //        {
            //            cell = dataGridView.Rows[i].Cells[j];
            //            matrix[i, j].Mostrar(cell);
            //        }
            //        else
            //        {
            //            cell = dataGridView.Rows[i].Cells[j];
            //            Casilla casilla = matrix[i, j] = new Agua();
            //            casilla.Mostrar(cell);
            //        }
            //    }
            //}
        }


        private void rellenarSobrantesAgua()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (matrix[i, j] == null)
                    {
                        Casilla casilla = matrix[i, j] = new Agua();
                    }
                }
            }
        }

        private void createBoat(int size, int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                ShipLocation shipLocation = getEmptyPoint(size);
                Barco barco;

                if (size == SUBMARINO)
                    barco = new Submarine();
                else if (size == DESTRUCTOR)
                    barco = new Destroyer();
                else if (size == CRUCERO)
                    barco = new Crucero();
                else
                    barco = new Portaviones();


                for (int j = 0; j < size; j++)
                {
                    if (shipLocation.Orientation == UP)
                    {
                        matrix[shipLocation.Row - j, shipLocation.Col] = barco;
                    }
                    else if (shipLocation.Orientation == DOWN)
                    {
                        matrix[shipLocation.Row + j, shipLocation.Col] = barco;
                    }
                    else if (shipLocation.Orientation == LEFT)
                    {
                        matrix[shipLocation.Row, shipLocation.Col - j] = barco;
                    }
                    else if (shipLocation.Orientation == RIGHT)
                    {
                        matrix[shipLocation.Row, shipLocation.Col + j] = barco;
                    }
                }
                rodearConAgua(shipLocation);
            }
        }

        private void rodearConAgua(ShipLocation shipLocation)
        {
            for (int i = 0; i < shipLocation.Size; i++)
            {
                if (shipLocation.Orientation == UP)
                {
                    rodeaCasilladeAgua(shipLocation.Row - i, shipLocation.Col);
                }
                else if (shipLocation.Orientation == DOWN)
                {
                    rodeaCasilladeAgua(shipLocation.Row + i, shipLocation.Col);
                }
                else if (shipLocation.Orientation == LEFT)
                {
                    rodeaCasilladeAgua(shipLocation.Row, shipLocation.Col - i);
                }
                else if (shipLocation.Orientation == RIGHT)
                {
                    rodeaCasilladeAgua(shipLocation.Row, shipLocation.Col + i);
                }
            }
        }

        private void rodeaCasilladeAgua(int row, int col)
        {
            // Esquina superior izquierda
            if (row - 1 >= 0 && col - 1 >= 0 && matrix[row - 1, col - 1] == null)
            {
                matrix[row - 1, col - 1] = new Agua();
            }
            // Esquina superior derecha
            if (row - 1 >= 0 && col + 1 <= 9 && matrix[row - 1, col + 1] == null)
            {
                matrix[row - 1, col + 1] = new Agua();
            }
            // Esquina inferior izquierda
            if (row + 1 <= 9 && col - 1 >= 0 && matrix[row + 1, col - 1] == null)
            {
                matrix[row + 1, col - 1] = new Agua();
            }
            // Esquina inferior derecha
            if (row + 1 <= 9 && col + 1 <= 9 && matrix[row + 1, col + 1] == null)
            {
                matrix[row + 1, col + 1] = new Agua();
            }
            // Izquierda
            if (col - 1 >= 0 && matrix[row, col - 1] == null)
            {
                matrix[row, col - 1] = new Agua();
            }
            // Derecha
            if (row + 1 <= 9 && matrix[row + 1, col] == null)
            {
                matrix[row + 1, col] = new Agua();
            }
            // Abajo
            if (col + 1 <= 9 && matrix[row, col + 1] == null)
            {
                matrix[row, col + 1] = new Agua();
            }
            // Arriba
            if (row - 1 >= 0 && matrix[row - 1, col] == null)
            {
                matrix[row - 1, col] = new Agua();
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
                if (col - size - 1 >= 0)
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
                if (col + size - 1 <= NUM_COLS - 1)
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
                if (row - size - 1 >= 0)
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
                if (row + size - 1 <= NUM_ROWS - 1)
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
            DataGridViewCell cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Casilla casilla = matrix[e.RowIndex, e.ColumnIndex];
            casilla.Mostrar(cell);


            string position = e.RowIndex + "," + e.ColumnIndex;
            string message = casilla.getMessage();

            // label1.Text = position + " - " + message;
            label1.Text = position;
            label2.Text = message;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Initialize();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            createBoat(4, 1);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DataGridViewCell cell;
                    if (matrix[i, j] != null)
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        matrix[i, j].Mostrar(cell);
                    }
                    //else{
                    //    cell = dataGridView.Rows[i].Cells[j];
                    //    Casilla casilla = matrix[i, j] = new Agua();
                    //    casilla.Mostrar(cell);
                    //}
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            createBoat(3, 2);


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DataGridViewCell cell;
                    if (matrix[i, j] != null)
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        matrix[i, j].Mostrar(cell);
                    }
                    //else{
                    //    cell = dataGridView.Rows[i].Cells[j];
                    //    Casilla casilla = matrix[i, j] = new Agua();
                    //    casilla.Mostrar(cell);
                    //}
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            createBoat(2, 3);


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DataGridViewCell cell;
                    if (matrix[i, j] != null)
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        matrix[i, j].Mostrar(cell);
                    }
                    //else{
                    //    cell = dataGridView.Rows[i].Cells[j];
                    //    Casilla casilla = matrix[i, j] = new Agua();
                    //    casilla.Mostrar(cell);
                    //}
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            createBoat(1, 4);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DataGridViewCell cell;
                    if (matrix[i, j] != null)
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        matrix[i, j].Mostrar(cell);
                    }
                    //else{
                    //    cell = dataGridView.Rows[i].Cells[j];
                    //    Casilla casilla = matrix[i, j] = new Agua();
                    //    casilla.Mostrar(cell);
                    //}
                }
            }
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

        private void button8_Click(object sender, EventArgs e)
        {

            //solo para comprobar como se situan los barcos
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DataGridViewCell cell;
                    if (matrix[i, j] != null)
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        matrix[i, j].Mostrar(cell);
                    }
                    else
                    {
                        cell = dataGridView.Rows[i].Cells[j];
                        Casilla casilla = matrix[i, j] = new Agua();
                        casilla.Mostrar(cell);
                    }
                }
            }
        }
    }
}
