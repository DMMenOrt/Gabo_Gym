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
        private String query;
        private String clave_venta, clave_tipo_venta, tipo_venta, clave_socio, fecha_venta;
        private String clave_producto, precio, importe;
        private int[] a_carrito_prod;
        private decimal[] a_carrito_prec;
        private int[] a_cantidad;
        private int contador;

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int cantidad;
            decimal precio;
            try
            {
                if (Convert.ToInt32(textBox3.Text) <= 0)
                {
                    textBox3.Text = "1";
                }
                if (Convert.ToInt32(textBox3.Text) >= 1)
                {
                    cantidad = Convert.ToInt32(textBox3.Text);
                    precio = Convert.ToDecimal(this.precio);
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
            Catalogo_Productos();
            textBox2.Enabled = false;
            textBox3.Text = "1";
            contador = 0;
            a_carrito_prod = new int[40];
            a_carrito_prec = new decimal[40];
            a_cantidad = new int[40];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                a_carrito_prod[contador] = Convert.ToInt32(clave_producto);
                a_carrito_prec[contador] = Convert.ToDecimal(textBox2.Text);
                a_cantidad[contador] = Convert.ToInt32(textBox3.Text);
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
                int clave_venta = exec.get_Ultima_Venta();
                clave_venta++;
                if (clave_venta >= 0)
                {
                    exec.Venta_Producto(a_carrito_prod, a_carrito_prec, clave_venta, a_cantidad,contador);
                }
                contador = 0;
            } catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                contador = 0;
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Gestor_Ventas_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Inspector_ventas inspector_Ventas = new Inspector_ventas(exec, clave_venta, clave_socio, fecha_venta,importe);
            inspector_Ventas.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                query = "select sm.clave_venta,clave_socio,fecha_venta,sum as total from gym.ventas as v join gym.suma_venta as sm on v.clave_venta = sm.clave_venta WHERE 1 = 1 ORDER BY v.fecha_venta DESC";
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
            clave_socio = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            fecha_venta = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            importe = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clave_producto = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            precio = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = "1";
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
