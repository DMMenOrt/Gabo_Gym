using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Windows.Forms;



namespace ConexionDB{
    public class Ejecutor  {
        
        public NpgsqlConnection conexion = new NpgsqlConnection();

        public Ejecutor(){
            
        }

        public int conector(String usr, String pwd)
        {
            if (usr != "mogodb" || pwd!= "mogodb")
            {
                return 0;
            } else
            {
                try
                {
                    conexion.ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    conexion.Open();
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        return 1;
                    }else
                    {
                        return 0;
                    }
                
                } catch (Exception e)
                {
                    MessageBox.Show("No se pudo realizar la conexión" + e.Message);
                    return 0;
                }
                
            }
            
        }

        public void altaSocio(String nombre,String pApellido, String sApellido){

            try
            {
                String query = "INSERT INTO gym.socios (nombre,primer_apellido,segundo_apellido, fecha_inicio, fecha_fin) VALUES ('" + nombre + "', '" + pApellido + "', '" + sApellido + "', CURRENT_DATE, CURRENT_DATE + 1);";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Socio registrado exitosamente");
                } else
                {
                    MessageBox.Show("Error al registrar socio");
                }
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public void modificaSocio(String nombre, String pApellido, String sApellido, String id)
        {
            try
            {
                String query = "UPDATE gym.socios SET nombre = '"+nombre+"', primer_apellido = '"+pApellido+"', segundo_apellido = '"+sApellido+"' WHERE clave_socio = "+id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Se ha actualizado el registro de este socio");
                }
                else
                {
                    MessageBox.Show("Error al actualizar socio");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
    }
}
