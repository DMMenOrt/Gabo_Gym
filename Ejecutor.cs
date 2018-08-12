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
        private String query;
        public Ejecutor(){
            
        }

        public int Conector(String usr, String pwd)
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
        //CURRENT_DATE +  INTERVAL '" + duracion + " months' para calcular la fecha de termino de la suscripcion
        public void AltaSocio(String nombre,String pApellido, String sApellido,int duracion,String clave,String clave_producto,String precio){

            try
            {
                if (clave == "AUTOMATICA")
                {
                    query = "INSERT INTO gym.socios (nombre,primer_apellido,segundo_apellido, fecha_inicio, fecha_fin) VALUES ('" + nombre + "', '" + pApellido + "', '" + sApellido + "', CURRENT_DATE, CURRENT_DATE +  INTERVAL '" + duracion + " months');";
                }
                else
                {
                    query = "INSERT INTO gym.socios (clave_socio,nombre,primer_apellido,segundo_apellido, fecha_inicio, fecha_fin) VALUES (" + clave + ",'" + nombre + "', '" + pApellido + "', '" + sApellido + "', CURRENT_DATE, CURRENT_DATE +  INTERVAL '" + duracion + " months');";
                }
                
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    Venta_suscripcion(clave_producto, precio, clave);
                } else
                {
                    MessageBox.Show("Error al registrar socio");
                }
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //SE EJECUTA JUSTO DESPUES DE INSERTAR AL SOCIO
        public void Venta_suscripcion(String clave_producto,String precio,String clave_socio)
        {
            try
            {
                query = "INSERT INTO gym.ventas(clave_tipo_venta, clave_socio, fecha_venta) VALUES(1, '" + clave_socio + "', CURRENT_DATE);";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    query = "SELECT clave_venta FROM gym.ventas WHERE 1=1 AND clave_tipo_venta = 1 AND clave_socio = '" + clave_socio + "' AND fecha_venta = CURRENT_DATE";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    int clave_venta = (Int32)comando.ExecuteScalar();
                    query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto) VALUES ('" + clave_producto + "','" + clave_venta + "','" + precio + "');";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Proceso de inscripcion completado");
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar socio");
                    }
                }
                else
                {
                    MessageBox.Show("Error al registrar socio");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
           
        }
        public int get_Ultima_Venta()
        {
            try
            {
                query = "SELECT clave_venta FROM gym.ventas ORDER BY clave_venta DESC LIMIT 1";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                int clave_venta = (Int32)comando.ExecuteScalar();
                clave_venta = clave_venta + 1;
                if (clave_venta > 0)
                {
                    query = "INSERT INTO gym.ventas(clave_venta,clave_tipo_venta, clave_socio, fecha_venta) VALUES(" + clave_venta + ",3, NULL, CURRENT_DATE);";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        return clave_venta;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
                    
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 0;
            }
            
        }

        public void Venta_Producto(int clave_producto, decimal precio, int clave_venta)
        {
            try
            {
                
                
                query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto) VALUES (" + clave_producto + "," + clave_venta + "," + precio + ");";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Venta completada");
                }
                else
                {
                    MessageBox.Show("Error al registrar venta");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public void AltaProducto(int tipo, String nombre, int duracion, decimal precio, int clave)
        {
            
            try
            {
                query = "INSERT INTO gym.productos (clave_tipo_producto,nombre_producto,duracion,clave_producto) VALUES ('" + tipo + "', '" + nombre + "', '" + duracion + "','" + clave + "');";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    if (duracion == 0)
                    {
                        query = "INSERT INTO gym.precios(clave_producto,fecha_alta,precio,fecha_expiracion) VALUES ('" + clave + "',CURRENT_DATE, '" + precio + "',NULL);";
                    } else
                    {
                        query = "INSERT INTO gym.precios(clave_producto,fecha_alta,precio,fecha_expiracion) VALUES ('" + clave + "',CURRENT_DATE, '" + precio + "',NULL);";
                    }
                    
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        if (tipo == 1)
                        {
                            MessageBox.Show("Producto registrado exitosamente");
                        }
                        if (tipo == 2)
                        {
                            MessageBox.Show("Servicio registrado exitosamente");
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Error al registrar");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public void ModificaSocio(String nombre, String pApellido, String sApellido, String id)
        {
            try
            {
                query = "UPDATE gym.socios SET nombre = '"+nombre+"', primer_apellido = '"+pApellido+"', segundo_apellido = '"+sApellido+"' WHERE clave_socio = "+id;
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

        public void EliminaraSocio(String id)
        {
            query = "DELETE FROM gym.socios WHERE 1 = 1 AND clave_socio = " + id;

            try
            {
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion)
                {
                    Connection = conexion,
                    CommandTimeout = 60
                };
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Se ha eliminado");
                }
                else
                {
                    MessageBox.Show("Error al eliminar socio");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void EliminarProducto(String id)
        {
            query = "DELETE FROM gym.precios WHERE 1 = 1 AND clave_producto = " + id;
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    query = "DELETE FROM gym.productos WHERE 1 = 1 AND clave_producto = " + id;
                    comando = new NpgsqlCommand(query, conexion)
                    {
                        Connection = conexion,
                        CommandTimeout = 60
                    };
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Registro eliminado");
                    }
                        
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        

        public void InsertaSimetrias(String id, String val1, String val2, String val3, String val4, String val5, String val6, String val7, String val8, String val9,
            String val10, String val11, String val12, String val13, String val14, String val15, String val16, String val17, String val18, String val19, String val20,
            String val21, String val22, String val23)
        {
            
            try
            {
                if (val1 == "") { val1 = "NULL"; }
                if (val2 == "") { val2 = "NULL"; }
                if (val3 == "") { val3 = "NULL"; }
                if (val4 == "") { val4 = "NULL"; }
                if (val5 == "") { val5 = "NULL"; }
                if (val6 == "") { val6 = "NULL"; }
                if (val7 == "") { val7 = "NULL"; }
                if (val8 == "") { val8 = "NULL"; }
                if (val9 == "") { val9 = "NULL"; }
                if (val10 == "") { val10 = "NULL"; }
                if (val11 == "") { val11 = "NULL"; }
                if (val12 == "") { val12 = "NULL"; }
                if (val13 == "") { val13 = "NULL"; }
                if (val14 == "") { val14 = "NULL"; }
                if (val15 == "") { val15 = "NULL"; }
                if (val16 == "") { val16 = "NULL"; }
                if (val17 == "") { val17 = "NULL"; }
                if (val18 == "") { val18 = "NULL"; }
                if (val19 == "") { val19 = "NULL"; }
                if (val20 == "") { val20 = "NULL"; }
                if (val21 == "") { val21 = "NULL"; }
                if (val22 == "") { val22 = "NULL"; }
                if (val23 == "") { val23 = "NULL"; }
                query = "INSERT INTO gym.simetrias VALUES (" + id + ",CURRENT_DATE," + val1 + "," + val2 + "," + val3 + "," + val4 + "," + val5 + "," + val6 + "," + val7
                + "," + val8 + "," + val9 + "," + val10 + "," + val11 + "," + val12 + "," + val13 + "," + val14 + "," + val15 + "," + val16 + "," + val17 + "," + val18 + ","
                + val19 + "," + val20 + "," + val21 + "," + val22 + "," + val23 + ");";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Simetria registrada exitosamente");
                }
                else
                {
                    MessageBox.Show("Error al registrar simetria");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
