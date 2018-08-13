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
    public partial class Ver_empleados : Form
    {
        private Ejecutor ejec;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        private String where3, where4, where5, query;
        private String id, nombre, p_apellido, s_apellido, s_usuario, s_tipo;

        private void button4_Click(object sender, EventArgs e)
        {
            if (id == null || nombre == null || p_apellido == null || s_apellido == null || s_usuario == null || s_tipo == null || id == "" || nombre == "" || p_apellido == "" || s_apellido == "" || s_usuario == "" || s_tipo == "")
            {
                MessageBox.Show("Selecciona un empleado");
            }
            else
            {
                EmpleadosDetalle inspeccionar_empleado = new EmpleadosDetalle(ejec, id, nombre, p_apellido, s_apellido, s_usuario, s_tipo);
                inspeccionar_empleado.Show();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            s_usuario = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            nombre = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            p_apellido = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            s_apellido = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            s_tipo = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();


        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (id == null || nombre == null || p_apellido == null || s_apellido == null || s_usuario == null || s_tipo == null || id == "" || nombre == "" || p_apellido == "" || s_apellido == "" || s_usuario == "" || s_tipo == "")
            {
                MessageBox.Show("Selecciona un empleado");
            }
            else
            {

                try
                {
                    DialogResult result= MessageBox.Show("¿Realmente desea borrar este empleado?", "Borrar", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                      if (result.Equals(DialogResult.OK))
                      {
                        ejec.EliminarEmpleado(id);
                        button1.PerformClick();
                       }
                     else
                     {
                      }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";            
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();          
            
        }

        public Ver_empleados(Ejecutor exec)
        {
            InitializeComponent();
            ejec = exec;
            sda = new NpgsqlDataAdapter();
        }

        private void button1_Click(object sender, EventArgs e)
        {                   
            query = "SELECT usuarios.id_usuario as" + '"' +"Clave usuario" + '"' + ",usuarios.nombre_usuario as" + '"' + "Nombre usuario" + '"' + ", usuarios.nombre as" + '"' + "Nombre" + '"' + ",usuarios.primer_apellido as " + '"' + "Primer Apellido" + '"' + ", usuarios.segundo_apellido as " + '"' + "Segundo Apellido" + '"' + ", tipo_usuarios.tipo_usuario " + '"' + "Tipo de usuario" + '"' + " FROM usrs.usuarios INNER JOIN usrs.tipo_usuarios ON usuarios.id_tipo_usuario = tipo_usuarios.id_tipo_usuario ";


            if (textBox1.Text == "")
            {
                where3 = "";
            }
            else
            {
                where3 = "AND usuarios.nombre ILIKE '%" + textBox1.Text + "%' ";
            }
            if (textBox2.Text == "")
            {
                where4 = "";
            }
            else
            {
                where4 = "AND usuarios.primer_apellido ILIKE '%" + textBox2.Text + "%' ";
            }
            if (textBox3.Text == "")
            {
                where5 = "";
            }
            else
            {
                where5 = "AND usuarios.segundo_apellido ILIKE '%" + textBox3.Text + "%' ";
            }
            try
            {
                query = query + where3 + where4 + where5;
                comando = new NpgsqlCommand(query, ejec.conexion);
                sda.SelectCommand = comando;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                button1.Enabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
