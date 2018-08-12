using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDB
{
    public partial class Main : Form
    {
        private Ejecutor exec = new Ejecutor();

        public Main()
        {
            InitializeComponent();
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_cerrado(Object sender, FormClosingEventHandler e)
        {
            exec.conexion.Close();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Ya hay una sesion iniciada");
                
            } else
            {
                Log_in log_in = new Log_in(exec);
                log_in.MdiParent = this;
                log_in.Show();
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exec.conexion.Close();
        }

        
        private void agregarSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                inscripcion_socio inscripcion = new inscripcion_socio(exec);
                inscripcion.MdiParent = this;
                inscripcion.Show();
            }
        }

        private void GestionSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                Ver_Socios ver_socios = new Ver_Socios(exec);
                ver_socios.MdiParent = this;
                ver_socios.Show();
            }
        }

        private void verProductosServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                GestorProductos gestion_productos = new GestorProductos(exec);
                gestion_productos.MdiParent = this;
                gestion_productos.Show();
            }
            
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                Gestor_Ventas gestion_ventas = new Gestor_Ventas(exec);
                gestion_ventas.MdiParent = this;
                gestion_ventas.Show();
            }
        }
        
        private void agregarEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                Empleados empleados = new Empleados(exec);
                empleados.MdiParent = this;
                empleados.Show();
            }
        }
        
         private void verEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exec.conexion.State == System.Data.ConnectionState.Closed)
            {
                MessageBox.Show("Necesitas iniciar sesion para continuar");
            }
            else
            {
                Ver_empleados ver_empleados = new Ver_empleados(exec);
                ver_empleados.MdiParent = this;
                ver_empleados.Show();
            }
        }
    }
}
