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
    public partial class DetallesProdServ : Form
    {
        Ejecutor ejec;
        private String clave, tipo_prod, nombre, dur, prec, f_expiracion, f_alta;


        //Activar modificación
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = true;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }

        }

        //CONFIRMAR MODIFICACIÓN
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (textBox5.Text == "")
                {
                    textBox5.Text = prec;
                }
                if (textBox2.Text == "")
                {
                    textBox2.Text = nombre;
                }

                String query = "UPDATE gym.precios SET fecha_expiracion = CURRENT_DATE WHERE clave_producto = " + clave + " AND fecha_expiracion IS NULL";
                NpgsqlCommand comando = new NpgsqlCommand(query, ejec.conexion)
                {
                    CommandTimeout = 60
                };
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                     query = "INSERT INTO gym.precios (clave_producto,fecha_alta,precio,fecha_expiracion) VALUES ('" + textBox1.Text + "',CURRENT_DATE,'" + textBox5.Text + "',NULL)";
                     comando = new NpgsqlCommand(query, ejec.conexion)
                    {
                        CommandTimeout = 60
                    };
                    comando.Prepare();
                    
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        query = "UPDATE gym.productos SET nombre_producto = '"+textBox2.Text+"'  WHERE clave_producto = " + clave +"";
                        comando = new NpgsqlCommand(query, ejec.conexion)
                        {
                            CommandTimeout = 60
                        };
                        comando.Prepare();
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Detalles del producto actualizados");
                        }
                    }
                        
                }
                else
                {
                    textBox1.Text = clave;
                    textBox2.Text = nombre;
                    textBox3.Text = tipo_prod;
                    textBox4.Text = dur;
                    textBox5.Text = prec;
                    textBox6.Text = f_alta;
                    textBox7.Text = f_expiracion;
                }
                Cargar_historico();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Cargar_historico();
            }
        }

        public DetallesProdServ(Ejecutor exec, String clave_producto, String clave_tipo_prod, String nombre_producto, String duracion, String precio, String fecha_expiracion, String fecha_alta)
        {
            InitializeComponent();
            ejec = exec;
            clave = clave_producto;
            tipo_prod = clave_tipo_prod;
            nombre = nombre_producto;
            dur = duracion;
            prec = precio;
            f_alta = fecha_alta.Substring(0, 10);
            f_expiracion = fecha_expiracion;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            checkBox1.Checked = false;
            textBox1.Text = clave;
            textBox3.Text = tipo_prod;
            textBox2.Text = nombre;
            textBox4.Text = dur;
            textBox5.Text = prec;
            textBox6.Text = f_alta;
            textBox7.Text = f_expiracion;
            Cargar_historico();
        }

        private void DetallesProdServ_cerrado(Object sender, FormClosingEventHandler e)
        {
            this.Close();
        }
        private void Cargar_historico()
        {
            try
            {
                String query = "select fecha_alta, fecha_expiracion, precio from gym.precios where clave_producto = '" + clave + "'";
                NpgsqlCommand comando = new NpgsqlCommand(query, ejec.conexion);
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter();
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
                dataGridView1.Refresh();
            }
        }
    }
}
