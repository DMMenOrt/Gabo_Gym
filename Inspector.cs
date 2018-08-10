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
    public partial class Inspector : Form
    {
        private Ejecutor ejec;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        private String id, nombre, p_apellido, s_apellido, f_inicio, f_fin, query;
        private String clave_socio, fecha, estatura, peso,pecho, espalda, biceps, cintura, cadera, cuadriceps, pantorrilla, agua_espalda_sup, grasa_espalda_sup;
        private String agua_espalda_inf, grasa_espalda_inf, agua_abdomen_sup, grasa_abdomen_sup, agua_abdomen_inf, grasa_abdomen_inf, agua_triceps, grasa_triceps;
        private String agua_abductores, grasa_abductores, agua_chaparreras, grasa_chaparreras;

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (id == null || fecha == null)
            {
                MessageBox.Show("Selecciona una simetria");
            }
            else
            {
                try
                {
                    string fecha_sub = fecha.Substring(0, 10);
                    query = "DELETE FROM gym.simetrias WHERE 1 = 1 AND clave_socio = " + id + "AND fecha = '" + fecha_sub + "'";
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

        

        private void button3_Click(object sender, EventArgs e)
        {
            CapturadorSimetrias captura = new CapturadorSimetrias(id,ejec);
            captura.Show();            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clave_socio = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            fecha = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            estatura = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            peso = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            pecho = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            espalda = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            biceps = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            cintura = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            cadera = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            cuadriceps = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            pantorrilla = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            agua_espalda_sup = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            grasa_espalda_sup = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            agua_espalda_inf = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            grasa_espalda_inf = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
            agua_abdomen_sup = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
            grasa_abdomen_sup = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
            agua_abdomen_inf = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
            grasa_abdomen_inf = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
            agua_triceps = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
            grasa_triceps = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
            agua_abductores = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
            grasa_abductores = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
            agua_chaparreras = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
            grasa_chaparreras = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                try
                {
                    query = "SELECT * FROM gym.simetrias WHERE 1 = 1 AND clave_socio = " + id + " ORDER BY fecha DESC";
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
            nombre = textBox2.Text;
            p_apellido = textBox3.Text;
            s_apellido = textBox4.Text;
            ejec.ModificaSocio(nombre,p_apellido,s_apellido,id);            
        }

        public Inspector(Ejecutor exec, String id, String nombre, String p_apellido, String s_apellido, String f_inicio,String f_fin)
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
            this.f_inicio = f_inicio.Substring(0,10);
            this.f_fin = f_fin.Substring(0, 10);
            textBox1.Text = id;
            textBox2.Text = nombre;
            textBox3.Text = p_apellido;
            textBox4.Text = s_apellido;
            textBox5.Text = this.f_inicio;
            textBox6.Text = this.f_fin;
        }
        private void inspector(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = id;
            textBox2.Text = nombre;
            textBox3.Text = p_apellido;
            textBox4.Text = s_apellido;
            textBox5.Text = f_inicio;
            textBox6.Text = f_fin;
            checkBox1.Checked = false;
        }
    }
}
