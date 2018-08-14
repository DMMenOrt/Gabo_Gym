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
    public partial class Ver_Socios : Form
    {
        private Ejecutor ejec;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        private String where, where3, where4, where5, query;
        private String id,nombre, p_apellido, s_apellido, f_inicio, f_fin;

        private void button5_Click(object sender, EventArgs e)
        {
            if (id == null || nombre == null || p_apellido == null || s_apellido == null || f_inicio == null || f_fin == null || id == "" || nombre == "" || p_apellido == "" || s_apellido == "" || f_inicio == "" || f_fin == "")
            {
                MessageBox.Show("Selecciona un socio");
            }
            else
            {
                GestionSuscripcion GestionSuscripcion = new GestionSuscripcion(ejec,Convert.ToInt32(id), f_fin, nombre, p_apellido, s_apellido, f_inicio);
                GestionSuscripcion.Show();
            }
        }

        public Ver_Socios(Ejecutor exec)        
        {
            InitializeComponent();
            ejec = exec;
            sda = new NpgsqlDataAdapter();
            button1.Enabled = false;
            button4.Enabled = false;
            comboBox1.Items.Add("Todos");
            comboBox1.Items.Add("Activos");
            comboBox1.Items.Add("Inactivos");
            comboBox1.SelectedItem = "Todos";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Todos")
            {
                where = "";
            }
            if (comboBox1.SelectedItem.ToString() == "Activos")
            {
                where = "AND CURRENT_DATE <= fecha_fin ";
            }
            if (comboBox1.SelectedItem.ToString() == "Inactivos")
            {
                where = "AND CURRENT_DATE > fecha_fin ";
            }
        }

        private void Ver_Socios_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button4.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            button3.Enabled = true;
            button1.Enabled = false;
        }

        private void Visualizador_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            query = "SELECT clave_socio as " + '"' + "Clave" + '"' + ",nombre as " + '"' + "Nombre" + '"' + ",primer_apellido as " + '"' + "Primer Apellido" + '"' + ", segundo_apellido as " + '"' + "Segundo Apellido" + '"' + ", fecha_inicio as " + '"' + "Fecha de inscripción" + '"' + ", fecha_fin as " + '"' + "Feecha de expiracion de la membresia" + '"' + " FROM gym.socios WHERE 1 = 1 ";
            
            if (textBox1.Text == "")
            {
                where3 = "";
            }else
            {
                where3 = "AND nombre ILIKE '%" + textBox1.Text + "%' ";
            }
            if (textBox2.Text == "")
            {
                where4 = "";
            }
            else
            {
                where4 = "AND primer_apellido ILIKE '%" + textBox2.Text + "%' ";
            }
            if (textBox3.Text == "")
            {
                where5 = "";
            }
            else
            {
                where5 = "AND segundo_apellido ILIKE '%" + textBox3.Text + "%' ";
            }
            try
            {
                query = query + where + where3 + where4 + where5;
                comando = new NpgsqlCommand(query, ejec.conexion);
                sda.SelectCommand = comando;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                button1.Enabled = true;
                button4.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == null || nombre == null || p_apellido == null || s_apellido == null || f_inicio == null || f_fin == null || id == "" || nombre == "" || p_apellido == "" || s_apellido == "" || f_inicio == "" || f_fin == "")
            {
                MessageBox.Show("Selecciona un socio");
            }
            else
            {
                Inspector inspeccionar_socio = new Inspector(ejec, id, nombre, p_apellido, s_apellido, f_inicio, f_fin);
                inspeccionar_socio.Show();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            p_apellido = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            s_apellido = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            f_inicio = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            f_fin = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (id == null || nombre == null || p_apellido == null || s_apellido == null || f_inicio == null || f_fin == null || id == "" || nombre == "" || p_apellido == "" || s_apellido == "" || f_inicio == "" || f_fin == "")
            {
                MessageBox.Show("Selecciona un socio");
            } else
            {
                
                try
                {
                    DialogResult result = MessageBox.Show("¿Desea eliminar los datos del socio?", "Actualizar empleado", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                    {
                        ejec.EliminaraSocio(id);
                        button3.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }
    }
}
