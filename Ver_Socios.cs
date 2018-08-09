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
        private String where1, where2, where3, where4, where5, query;
        private String id,nombre, p_apellido, s_apellido, f_inicio, f_fin;

        public Ver_Socios(Ejecutor exec)        
        {
            InitializeComponent();
            ejec = exec;
            sda = new NpgsqlDataAdapter();
            button1.Enabled = false;
            button4.Enabled = false;

        }

        private void Ver_Socios_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox3.Checked = false;
            checkBox2.Checked = false;
            checkBox1.Checked = false;
            button4.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            button3.Enabled = true;
            button1.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked == true )
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            } else
            {
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void Visualizador_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            query = "SELECT * FROM gym.socios WHERE 1 = 1 ";
            if (checkBox3.Checked == true || checkBox2.Checked == true || checkBox1.Checked == true)
            {
                if (checkBox3.Checked == true)
                {
                    where1 = "";
                    where2 = "";
                }
                if (checkBox2.Checked == true)
                {
                    where2 = "AND fecha_inicio > fecha_fin ";
                    where1 = "";
                }
                if (checkBox1.Checked == true)
                {
                    where1 = "AND fecha_inicio <= fecha_fin ";
                    where2 = "";
                }
                if (textBox1.Text == "")
                {
                    where3 = "";
                }else
                {
                    where3 = "AND nombre LIKE '%" + textBox1.Text + "%' ";
                }
                if (textBox2.Text == "")
                {
                    where4 = "";
                }
                else
                {
                    where4 = "AND primer_apellido LIKE '%" + textBox2.Text + "%' ";
                }
                if (textBox3.Text == "")
                {
                    where5 = "";
                }
                else
                {
                    where5 = "AND segundo_apellido LIKE '%" + textBox3.Text + "%' ";
                }
                try
                {
                    query = query + where1 + where2 + where3 + where4 + where5;
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
            } else
            {
                MessageBox.Show("Selecciona Activos, Inactivos, o Todos");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
            } else
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox1.Enabled = true;
                checkBox3.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            else
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
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
                query = "DELETE FROM gym.socios WHERE 1 = 1 AND clave_socio = " + id;

                try
                {
                    comando = new NpgsqlCommand(query, ejec.conexion);
                    sda.SelectCommand = comando;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    button1.Enabled = true;
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
        }
    }
}
