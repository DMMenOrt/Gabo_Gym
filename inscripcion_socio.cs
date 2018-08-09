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
    public partial class inscripcion_socio : Form
    {
        private Ejecutor ejec;
        public inscripcion_socio(Ejecutor exec)
        {
            ejec = exec;
            InitializeComponent();
            
        }

        private void inscripcion_socio_Load(object sender, EventArgs e)
        {

        }

        private void inscriptor_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ejec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                ejec.altaSocio(textBox1.Text, textBox2.Text, textBox3.Text);
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            MessageBox.Show("Operación cancelada");
            this.Close();
        }
    }
}
