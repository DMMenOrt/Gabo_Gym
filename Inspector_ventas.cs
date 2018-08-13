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
    public partial class Inspector_ventas : Form
    {
        private Ejecutor exec;
        private NpgsqlDataAdapter sda;
        String nombre, pApellido, sApellido,clave_socio;

        public Inspector_ventas(Ejecutor exec, String clave_venta, String clave_socio, String fecha_venta, String importe)
        {
            InitializeComponent();
            this.exec = exec;
            sda = new NpgsqlDataAdapter();
            inicializacion(clave_socio);
            textBox1.Text = clave_venta;
            textBox2.Text = fecha_venta.Substring(0, 10);
            textBox3.Text = this.clave_socio;
            textBox7.Text = importe;
            textBox4.Text = nombre;
            textBox5.Text = pApellido;
            textBox6.Text = sApellido;
            carga_detalle();
        }

        private void Inspector_ventas_Load(object sender, EventArgs e)
        {

        }

        private void inicializacion(String clave_socio)
        {
            if (clave_socio == "")
            {
                this.clave_socio = "0";
                nombre = "Sin nombre";
                pApellido = "Sin primer apellido";
                sApellido = "Sin segundo apellido";


            }else
            {
                this.clave_socio = clave_socio;
                nombre = exec.get_Nombre(clave_socio);
                pApellido = exec.get_pApellido(clave_socio);
                sApellido = exec.get_sApellido(clave_socio);

            }
        }

        public void carga_detalle()
        {

            try
            {
                String query = "select vp.clave_producto,vp.precio_producto as total_pedido,vp.cantidad,tv.tipo_venta,prod.nombre_producto,pre.precio as precio_unitario from gym.venta_producto as vp join gym.tipos_ventas as tv on vp.clave_tipo_venta = tv.clave_tipo_venta join gym.productos as prod on vp.clave_producto = prod.clave_producto join gym.precios as pre on vp.clave_producto = pre.clave_producto where pre.fecha_expiracion is NULL and vp.clave_venta = "+textBox1.Text;
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
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
            /*
             * query = 
             */
        }
    }
}
