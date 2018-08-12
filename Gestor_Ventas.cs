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
        String clave_venta, clave_tipo_venta, tipo_venta, clave_socio, fecha_venta;
        public Gestor_Ventas(Ejecutor ejec)
        {
            InitializeComponent();
            exec = ejec;
            sda = new NpgsqlDataAdapter();
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
            try
            {
                query = "select v.clave_venta,tv.clave_tipo_venta,tv.tipo_venta,s.clave_socio,s.nombre,s.primer_apellido,s.segundo_apellido,fecha_venta from gym.ventas as v join gym.socios as s on v.clave_socio = s.clave_socio join gym.tipos_ventas as tv on v.clave_tipo_venta = tv.clave_tipo_venta;";
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
    }
}
