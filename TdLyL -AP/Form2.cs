using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TdLyL__AP
{
    public partial class Form2 : Form
    {
        automataPila AP;
        public Form2()
        {
            List<string> simbolosEntrada = new List<string>();
            List<string> simbolosEnPila = new List<string>();
            this.AP = new automataPila(simbolosEntrada, simbolosEnPila, "▼"); //creación del autómata
            InitializeComponent();
        }

        //cambio dinámico de la tabla
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            setDataGridViewColumns();
            setDataGridViewRows();
        }

        //añadir columnas a la tabla
        private void setDataGridViewColumns()
        {
            AP.simbolosEntrada = textBox1.Text.Split(',').ToList();
            int col = AP.simbolosEntrada.Count;
            dataGridView1.Columns.Clear();
            for (int i = 0; i < col; i++)
            {
                dataGridView1.Columns.Add("Column" + i, AP.simbolosEntrada[i]);
            }
        }


        //añadir filas a la tabla
        private void setDataGridViewRows()
        {
            AP.simbolosEnPila = textBox2.Text.Split(',').ToList();
            int row = AP.simbolosEnPila.Count;
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(row);
            for (int j = 0; j < row; j++)
            {
                dataGridView1.Rows[j].HeaderCell.Value = String.Join("", AP.simbolosEnPila[j]);

            }
        }

        //añadir dinamicamente a la tabla
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            setDataGridViewRows();
        }

        //botón revisar
        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals("") && !textBox2.Text.Equals("") && !textBox3.Text.Equals("")) //comprobación campos vacíos
            {
            AP.simbolosEntrada = textBox1.Text.Split(',').ToList();
            AP.simbolosEnPila = textBox2.Text.Split(',').ToList();
            AP.setPila(textBox3.Text); 
            AP.transiciones = datagridviewToMatrix(this.dataGridView1,AP.simbolosEnPila.Count,AP.simbolosEntrada.Count); 
            try
            {
            label8.Text =  "Proceso:\n"+AP.validarHilera(textBox4.Text); //Aquí se lee el proceso y si es válida
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Error al analizar, ¿ha olvidado llenar alguna transición?");
                Console.WriteLine(ex.ToString());
            }

            }
            else
            {
                MessageBox.Show("¿Ha rellenado todos los campos?");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            AP.setPila(textBox3.Text);
        }

        //pasar datos de la tabla a matriz
        private List<List<string>> datagridviewToMatrix(DataGridView datagridview, int row, int col)
        {
            List<List<string>> matrix = new List<List<string>>();
            for (int i = 0; i < row; i++)
            {
                List<string> stateRow = new List<string>();

                for (int j = 0; j < col; j++)
                {
                    try
                    {
                        if (datagridview.Rows[i].Cells[j].Value.ToString().Equals(""))
                        {
                            stateRow.Add("E");
                        }
                        else
                        {
                            stateRow.Add(datagridview.Rows[i].Cells[j].Value.ToString().Trim(' '));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetBaseException().ToString());
                        stateRow.Add(("E"));
                    }
                }
                matrix.Add(stateRow);
            }
            return matrix;
        }
    }
}
