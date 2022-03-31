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
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            Form2 fm = new Form2();
            fm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> simbolosEntrada = textBox1.Text.Split(',').ToList();
            List<string> simbolosEnPila = textBox2.Text.Split(',').ToList();
            List<string> confInicial = textBox5.Text.Split(',').ToList();
            automataPila AP = new automataPila(simbolosEntrada, simbolosEnPila, confInicial);
            Form2 fm = new Form2();
            fm.ShowDialog();
        }



    }
}
