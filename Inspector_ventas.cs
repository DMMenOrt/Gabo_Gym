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
                String query = "SELECT * FROM gym.vista_detalle_venta where fecha_expiracion is NULL and clave_venta = " + textBox1.Text;
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
