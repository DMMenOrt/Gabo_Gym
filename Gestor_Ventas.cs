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
    public partial class Gestor_Ventas : Form
    {
        private Ejecutor exec;
        private NpgsqlDataAdapter sda;
        private NpgsqlCommand comando;
        private String query, where, where1;
        private String clave_venta, clave_tipo_venta, tipo_venta, clave_socio, fecha_venta;
        private String clave_producto, precio;
        private int[] a_carrito_prod;
        private decimal[] a_carrito_prec;
        private int contador;

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int cantidad;
            double precio;
            try
            {
                if (Convert.ToInt32(textBox3.Text) <= 0)
                {
                    textBox3.Text = "0";
                }
                if (Convert.ToInt32(textBox3.Text) >= 1)
                {
                    cantidad = Convert.ToInt32(textBox3.Text);
                    precio = Convert.ToDouble(this.precio);
                    precio = precio * cantidad;
                    textBox2.Text = precio.ToString();
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        public Gestor_Ventas(Ejecutor ejec)
        {
            InitializeComponent();
            exec = ejec;
            sda = new NpgsqlDataAdapter();
            comboBox1.Items.Add("Todas");
            comboBox1.Items.Add("Suscripcion a servicio");
            comboBox1.Items.Add("Pago de servicio");
            comboBox1.Items.Add("Compra de producto");
            comboBox1.Items.Add("Visita");
            comboBox1.SelectedItem = "Todas";
            Catalogo_Productos();
            textBox2.Enabled = false;
            textBox3.Text = "0";
            contador = 0;
            a_carrito_prod = new int[40];
            a_carrito_prec = new decimal[40];
    }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                a_carrito_prod[contador] = Convert.ToInt32(clave_producto);
                a_carrito_prec[contador] = Convert.ToDecimal(textBox2.Text);
                contador++;
                MessageBox.Show("Producto agregado al carrito");
            } catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int contador = 0;
                int clave_venta = exec.get_Ultima_Venta();
                if (clave_venta > 0)
                {
                    for (contador = 0; contador <= this.contador; contador++)
                    {
                        exec.Venta_Producto(a_carrito_prod[contador], a_carrito_prec[contador], clave_venta);
                    }
                }
                this.contador = 0;
            } catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                this.contador = 0;
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Todas")
            {
                where = "";
            }
            if (comboBox1.SelectedItem.ToString() == "Suscripcion a servicio")
            {
                where = "AND v.clave_tipo_venta = 1 ";
            }
            if (comboBox1.SelectedItem.ToString() == "Pago de servicio")
            {
                where = "AND v.clave_tipo_venta = 2 ";
            }
            if (comboBox1.SelectedItem.ToString() == "Compra de producto")
            {
                where = "AND v.clave_tipo_venta = 3 ";
            }
            if (comboBox1.SelectedItem.ToString() == "Visita")
            {
                where = "AND v.clave_tipo_venta = 4 ";
            }
        }

        private void Gestor_Ventas_Load(object sender, EventArgs e)
        {

        }
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                where1 = "";
            }
            else
            {
                where1 = "AND s.nombre ilike '%" + textBox1.Text + "%' ";
            }
            try
            {
                query = "select v.clave_venta,tv.clave_tipo_venta,tv.tipo_venta,s.clave_socio,s.nombre,s.primer_apellido,s.segundo_apellido,fecha_venta from gym.ventas as v join gym.socios as s on v.clave_socio = s.clave_socio join gym.tipos_ventas as tv on v.clave_tipo_venta = tv.clave_tipo_venta WHERE 1 = 1 "+where+where1+ "";
                comando = new NpgsqlCommand(query, exec.conexion);
                sda.SelectCommand = comando;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clave_venta = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            clave_tipo_venta = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tipo_venta = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            clave_socio = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            fecha_venta = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clave_producto = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            precio = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = "0";
        }

        private void Catalogo_Productos()
        {
            try
            {
                String query = "select prod.clave_producto as " + '"' + "Clave del producto" + '"' + ", prod.nombre_producto as " + '"' + "Nombre del producto" + '"' + ",duracion as " + '"' + "Meses" + '"' + ",precio from gym.productos as prod join gym.precios as pre on pre.clave_producto = prod.clave_producto WHERE 1 = 1 AND clave_tipo_producto = 1 AND (pre.fecha_expiracion IS NULL OR CURRENT_DATE <= pre.fecha_expiracion)";
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter
                {
                    SelectCommand = comando
                };
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView2.DataSource = dt;
                dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                dataGridView2.Refresh();
            }

        }
    }
}
