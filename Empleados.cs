using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDB
{
    public partial class Empleados : Form

    {
        private Ejecutor ejec;

        
        public Empleados(Ejecutor exec)
        {

            InitializeComponent();
            ejec = exec;
            comboBox1.Items.Add("Empleado");
            comboBox1.Items.Add("Administrador");
            comboBox1.SelectedItem = "Empleado";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ejec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                

                if (textBox1.Text == "")
                {
                    textBox1.Text = "NULL";
                }
                if (textBox2.Text == "")
                {
                    textBox2.Text = "NULL";
                }
                if (textBox3.Text == "")
                {
                    textBox3.Text = "NULL";
                }
                if (textBox4.Text == "")
                {
                    textBox4.Text = "";
                }
                if (textBox5.Text == "")
                {
                    textBox5.Text = "AUTOMATICA";
                }

               
                ejec.AltaEmpleado(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.Text );
                this.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
