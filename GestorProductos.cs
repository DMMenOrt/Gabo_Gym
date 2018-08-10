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
    public partial class GestorProductos : Form
    {
        private int tipo_producto;
        private Ejecutor ejec;
        private String query,where1,where2,where3;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        public GestorProductos(Ejecutor exec)
        {
            InitializeComponent();
            ejec = exec;
            sda = new NpgsqlDataAdapter();
            comboBox1.Items.Add("Producto");
            comboBox1.Items.Add("Servicio");
            comboBox1.SelectedItem = "Servicio";
            textBox3.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox2.Items.Add("Todo");
            comboBox2.Items.Add("Productos");
            comboBox2.Items.Add("Servicios");
            comboBox2.SelectedItem = "Todo";
            comboBox3.Items.Add("Todos");
            comboBox3.Items.Add("Activos");
            comboBox3.Items.Add("Inactivos");
            comboBox3.SelectedItem = "Todos";
            comboBox3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Producto")
            {
                tipo_producto = 1;
                textBox3.Enabled = false;
                textBox3.Text = "";
            }
            if (comboBox1.SelectedItem.ToString() == "Servicio")
            {
                tipo_producto = 2;
                textBox3.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                query = "select * from gym.productos as prod join gym.precios as pre on pre.clave_producto = prod.clave_producto join gym.tipo_producto as tip on prod.clave_tipo_producto = tip.clave_tipo_producto WHERE 1 = 1 ";
                if (comboBox2.SelectedItem.ToString() == "Todo")
                {
                    where1 = " ";
                }
                if (comboBox2.SelectedItem.ToString() == "Productos")
                {
                    where1 = "AND prod.clave_tipo_producto = 1 ";
                }
                if (comboBox2.SelectedItem.ToString() == "Servicios")
                {
                    where1 = "AND prod.clave_tipo_producto = 2 ";
                }
                if (comboBox3.SelectedItem.ToString() == "Todos")
                {
                    where2 = " ";
                }
                if (comboBox3.SelectedItem.ToString() == "Activos")
                {
                    where2 = "AND CURRENT_DATE <= fecha_expiracion ";
                }
                if (comboBox3.SelectedItem.ToString() == "Inactivos")
                {
                    where2 = "AND CURRENT_DATE > fecha_expiracion ";
                }
                if (textBox5.Text == "")
                {
                    where3 = " ";
                }
                else
                {
                    where3 = "AND nombre_producto ILIKE '%" + textBox5.Text + "%' ";
                }
                query = query + where1 + where2 + where3;
                comando = new NpgsqlCommand(query, ejec.conexion);
                sda.SelectCommand = comando;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                button4.Enabled = true;
                button6.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            button4.Enabled = false;
            button6.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }

        private void Gestor_productos_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    textBox3.Text = "0";
                }
                int duracion = Convert.ToInt32(textBox3.Text);
                double precio = Convert.ToDouble(textBox1.Text);
                int clave = Convert.ToInt32(textBox4.Text);
                ejec.AltaProducto(tipo_producto, textBox2.Text, duracion, precio, clave);
            } catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Todo")
            {
                comboBox3.SelectedItem = "Todos";
                comboBox3.Enabled = false;
            }
            if (comboBox2.SelectedItem.ToString() == "Productos")
            {
                comboBox3.SelectedItem = "Todos";
                comboBox3.Enabled = false;
            }
            if (comboBox2.SelectedItem.ToString() == "Servicios")
            {
                comboBox3.Enabled = true;
            }
        }
    }
}
