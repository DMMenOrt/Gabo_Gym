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
    public partial class CapturadorSimetrias : Form
    {
        private Ejecutor ejec;
        private String id, query;
        private NpgsqlCommand comando;

        public CapturadorSimetrias(String id, Ejecutor exec)
        {
            InitializeComponent();
            ejec = exec;
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Capturador_Simetrias_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            query = "INSERT INTO gym.simetrias VALUES ("+id + ",CURRENT_DATE," + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text + "," + textBox7.Text + "," + textBox8.Text + "," + textBox9.Text+ "," + textBox10.Text + "," + textBox11.Text + "," + textBox12.Text + "," + textBox13.Text + "," + textBox14.Text + "," + textBox15.Text + "," + textBox16.Text + "," + textBox17.Text
                + "," + textBox18.Text + "," + textBox19.Text + "," + textBox20.Text + "," + textBox21.Text + "," + textBox22.Text + "," + textBox23.Text + ");";
            try
            {
                comando = new NpgsqlCommand(query, ejec.conexion);
                comando.Connection = ejec.conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Simetria registrada exitosamente");
                }
                else
                {
                    MessageBox.Show("Error al registrar simetria");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
