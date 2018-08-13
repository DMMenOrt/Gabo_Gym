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
    public partial class GestionSuscripcion : Form
    {
        Ejecutor exec;
        String nombre, pApellido, sApelldido,nombreServicio,F_fin,f_ini;
        int clave_socio,clave_producto, duracion;
        decimal costo;
        public GestionSuscripcion(Ejecutor ejec, int id,String f_fin,String nombre,String pApellido,String sApelldido,String f_ini)
        {
            InitializeComponent();
            exec = ejec;
            f_ini = f_ini.Substring(0, 10);
            this.f_ini = f_ini;
            f_fin = f_fin.Substring(0, 10);
            this.F_fin = f_fin;
            clave_socio = id;
            this.nombre = nombre;
            this.pApellido = pApellido;
            this.sApelldido = sApelldido;
            clave_producto = get_id_servicio();
            costo = get_precio_producto();
            duracion = get_duracion();
            nombreServicio = get_Nombre_Servicio();
            Completar_campos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exec.PagoSuscripcion(clave_socio, clave_producto, costo,duracion);
        }

        public void Completar_campos()
        {
            textBox1.Text = clave_socio.ToString();
            textBox2.Text = nombre;
            textBox3.Text = pApellido;
            textBox4.Text = sApelldido;
            textBox5.Text = clave_producto.ToString();
            textBox6.Text = nombreServicio;
            textBox7.Text = costo.ToString();
            textBox8.Text = F_fin;
        }


        public int get_id_servicio()
        {
            int clave_producto;
            try
            {
                String query = "select clave_producto from gym.venta_producto as vp join gym.ventas as v on vp.clave_venta = v.clave_venta join gym.socios as s on v.clave_socio = s.clave_socio WHERE clave_tipo_venta = 1 AND s.clave_socio = "+ clave_socio+" AND fecha_venta = fecha_inicio";
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
                comando.Connection = exec.conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    clave_producto = (Int32)comando.ExecuteScalar();
                }
                catch
                {
                    clave_producto = 0;
                }
                return clave_producto;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 0;
            }

        }

        public decimal get_precio_producto()
        {
            decimal precio_producto;
            try
            {
                String query = "select precio from gym.precios as pre WHERE pre.clave_producto = "+this.clave_producto+" AND fecha_expiracion IS NULL";
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
                comando.Connection = exec.conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    precio_producto = (decimal)comando.ExecuteScalar();
                }
                catch
                {
                    precio_producto = 0;
                }
                return precio_producto;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 0;
            }

        }

        public int get_duracion()
        {
            int duracion;
            try
            {
                String query = "select duracion from gym.productos as pro WHERE pro.clave_producto = " + this.clave_producto + "";
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
                comando.Connection = exec.conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    duracion = (int)comando.ExecuteScalar();
                }
                catch
                {
                    duracion = 0;
                }
                return duracion;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 0;
            }

        }

        public String get_Nombre_Servicio()
        {
            String nombre_servicio;
            try
            {
                String query = "select nombre_producto from gym.productos where clave_producto = "+this.clave_producto;
                NpgsqlCommand comando = new NpgsqlCommand(query, exec.conexion);
                comando.Connection = exec.conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    nombre_servicio = (String)comando.ExecuteScalar();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: " + e.Message);
                    return "";
                }
                return nombre_servicio;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return "";
            }
        }
    }
}
