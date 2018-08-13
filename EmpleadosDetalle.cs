using Npgsql;
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
    public partial class EmpleadosDetalle : Form
    {

        private Ejecutor ejec;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        private String id, nombre, p_apellido, s_apellido, s_usuario, s_tipo, query;

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = id;
            textBox2.Text = nombre;
            textBox3.Text = p_apellido;
            textBox4.Text = s_apellido;
            textBox5.Text = s_usuario;
            textBox6.Text = s_tipo;
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
        }

        public EmpleadosDetalle(Ejecutor exec, String id, String nombre, String p_apellido, String s_apellido, String s_usuario, String s_tipo)
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;


            ejec = exec;
            sda = new NpgsqlDataAdapter();
            this.id = id;
            this.nombre = nombre;
            this.p_apellido = p_apellido;
            this.s_apellido = s_apellido;
            this.s_usuario = s_usuario;
            this.s_tipo = s_tipo;
            textBox1.Text = id;
            textBox2.Text = nombre;
            textBox3.Text = p_apellido;
            textBox4.Text = s_apellido;
            textBox5.Text = s_usuario;
            textBox6.Text = s_tipo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea modifiar los datos del empleado?", "Actualizar empleado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                nombre = textBox2.Text;
                p_apellido = textBox3.Text;
                s_apellido = textBox4.Text;
                s_usuario = textBox5.Text;
                s_tipo = textBox6.Text;

                ejec.ModificaEmpleado(nombre, p_apellido, s_apellido, s_usuario, s_tipo, id);
            }
            else
            {
            }
           
        }
    }
}
