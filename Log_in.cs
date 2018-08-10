using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace ConexionDB
{
    public partial class Log_in : Form
    {

        private Ejecutor ejec;

        public Log_in(Ejecutor exec)
        {
            InitializeComponent();
            ejec = exec;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Introduce un usuario");
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Introduce una contraseña");
                } else
                {
                    ejec.Conector(textBox1.Text, textBox2.Text);
                    if (ejec.conexion.State == System.Data.ConnectionState.Open)
                    {
                        MessageBox.Show("Conexión exitosa");
                        this.Close();
                    } else
                    {
                        MessageBox.Show("Error al intentar iniciar sesion");
                    }                    
                    
                }
                
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
