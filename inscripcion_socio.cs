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
    public partial class inscripcion_socio : Form
    {
        private Ejecutor ejec;

        private String clave_producto, nombre_producto,precio;
        private int duracion;
        public inscripcion_socio(Ejecutor exec)
        {
            ejec = exec;
            InitializeComponent();
            Ver_Productos();
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
                MessageBox.Show("Necesitas iniciar sesion para continuar", "Error al iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Necesitas ingresar todos los datos para regisrar un socio", "Registrar socio", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                } else
                {
                    ejec.AltaSocio(textBox1.Text, textBox2.Text, textBox3.Text, duracion, textBox4.Text, clave_producto, precio);
                    this.Close();
                }
               
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            MessageBox.Show("Operación cancelada", "Cancelar", MessageBoxButtons.OK, MessageBoxIcon.None);
            this.Close();
        }

        private void textBox4_Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                clave_producto = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                nombre_producto = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String dur = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                duracion = Convert.ToInt32(dur);
                precio = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void Ver_Productos()
        {
            try
            {
                String query = "select prod.clave_producto as " + '"' + "Clave del producto" + '"' + ", prod.nombre_producto as " + '"' + "Nombre del producto" + '"' + ",duracion as " + '"' + "Meses" + '"' + ",precio from gym.productos as prod join gym.precios as pre on pre.clave_producto = prod.clave_producto WHERE 1 = 1 AND clave_tipo_producto = 2 AND (pre.fecha_expiracion IS NULL OR CURRENT_DATE <= pre.fecha_expiracion)";
                NpgsqlCommand comando = new NpgsqlCommand(query, ejec.conexion);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter
                {
                    SelectCommand = comando
                };
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                dataGridView1.Refresh();
            }
        }
    }
}
