﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Windows.Forms;



namespace ConexionDB{
    public class Ejecutor {

        public NpgsqlConnection conexion = new NpgsqlConnection();
        private String query;
        public Ejecutor() {

        }

        public int Conector(String usr, String pwd)
        {
            if (usr != "gabo_gym" || pwd != "g@bo_adm")
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
                    } else
                    {
                        return 0;
                    }

                } catch (Exception e)
                {
                    MessageBox.Show("No se pudo realizar la conexión" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

            }

        }
        //CURRENT_DATE +  INTERVAL '" + duracion + " months' para calcular la fecha de termino de la suscripcion
        public void AltaSocio(String nombre, String pApellido, String sApellido, int duracion, String clave, String clave_producto, String precio) {

            try
            {
                if (clave == "")
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
                    MessageBox.Show("Error al registrar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Se inserta empleado
        public void AltaEmpleado(String nombre, String pApellido, String sApellido, String sUsuario, String clave, String sTipo)
        {

            try
            {
                if (clave == "AUTOMATICA" && sTipo == "Administrador")
                {
                    query = "INSERT INTO usrs.usuarios (nombre,primer_apellido,segundo_apellido, nombre_usuario, id_tipo_usuario) VALUES ('" + nombre + "', '" + pApellido + "', '" + sApellido + "', '" + sUsuario + "', '" + 1 + "');";
                }
                else
                {
                    if (clave == "AUTOMATICA" && sTipo == "Empleado")
                    {
                        query = "INSERT INTO usrs.usuarios (nombre,primer_apellido,segundo_apellido, nombre_usuario, id_tipo_usuario) VALUES ('" + nombre + "', '" + pApellido + "', '" + sApellido + "', '" + sUsuario + "', '" + 2 + "');";

                    }
                    else
                    {
                        if (sTipo == "Administrador")
                        {
                            query = "INSERT INTO usrs.usuarios (id_usuario,nombre,primer_apellido,segundo_apellido, nombre_usuario, id_tipo_usuario) VALUES (" + clave + ",'" + nombre + "', '" + pApellido + "', '" + sApellido + "', '" + sUsuario + "', '" + 1 + "');";
                        }
                        else
                        {
                            query = "INSERT INTO usrs.usuarios (id_usuario,nombre,primer_apellido,segundo_apellido, nombre_usuario, id_tipo_usuario) VALUES (" + clave + ",'" + nombre + "', '" + pApellido + "', '" + sApellido + "', '" + sUsuario + "', '" + 2 + "');";
                        }


                    }

                    NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();

                    if (comando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Empleado registrado exitosamente", "Registrar empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Eliminar empleado
        public void EliminarEmpleado(String id)
        {
            query = "DELETE FROM usrs.usuarios WHERE id_usuario = " + id;

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
                    MessageBox.Show("El empleado ha sido eliminado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al eliminar empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Modificar empleado
        public void ModificaEmpleado(String nombre, String pApellido, String sApellido, String sUsuario, String sTipo, String id)
        {
            int tipo;
            try
            {
                if (sTipo == "Administrador")
                {
                    tipo = 1;
                }
                else
                {
                    tipo = 2;
                }

                query = "UPDATE usrs.usuarios SET nombre = '" + nombre + "', primer_apellido = '" + pApellido + "', segundo_apellido = '" + sApellido + "', nombre_usuario = '" + sUsuario + "', id_tipo_usuario = '" + tipo + "' WHERE id_usuario = " + id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Se ha actualizado el registro de este empleado", "Actualizar datos", MessageBoxButtons.OK,  MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //SE EJECUTA JUSTO DESPUES DE INSERTAR AL SOCIO
        public void Venta_suscripcion(String clave_producto, String precio, String clave_socio)
        {
            try
            {
                query = "INSERT INTO gym.ventas(clave_socio, fecha_venta) VALUES('" + clave_socio + "', CURRENT_DATE);";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    query = "SELECT clave_venta FROM gym.ventas WHERE 1=1 AND clave_socio = '" + clave_socio + "' AND fecha_venta = CURRENT_DATE";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    int clave_venta = (Int32)comando.ExecuteScalar();
                    query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto,cantidad,clave_tipo_venta) VALUES ('" + clave_producto + "','" + clave_venta + "','" + precio + "',DEFAULT,1);";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Proceso de inscripción completado", "Inscripción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al registrar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public int get_Ultima_Venta()
        {
            int clave_venta;
            try
            {
                query = "SELECT clave_venta FROM gym.ventas ORDER BY clave_venta DESC LIMIT 1";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    clave_venta = (Int32)comando.ExecuteScalar();
                }
                catch
                {
                    clave_venta = 0;
                }
                return clave_venta;
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 0;
            }

        }

        public void Venta_Producto(int[] clave_producto, decimal[] precio, int clave_venta, int[] cantidad, int contador)
        {
            int recorre = 0;
            try
            {
                query = "INSERT INTO gym.ventas(clave_venta, clave_socio, fecha_venta) VALUES(" + clave_venta + ", NULL, CURRENT_DATE);";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion)
                {
                    Connection = conexion,
                    CommandTimeout = 60
                };
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    for (recorre = 0; recorre < contador; recorre++)
                    {
                        if (clave_producto[recorre] == 1)
                        {
                            query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto,cantidad,clave_tipo_venta) VALUES (" + clave_producto[recorre] + "," + clave_venta + "," + precio[recorre] + "," + cantidad[recorre] + ",4);";
                        } else
                        {
                            query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto,cantidad,clave_tipo_venta) VALUES (" + clave_producto[recorre] + "," + clave_venta + "," + precio[recorre] + "," + cantidad[recorre] + ",3);";
                        }

                        comando = new NpgsqlCommand(query, conexion);
                        comando.Connection = conexion;
                        comando.CommandTimeout = 60;
                        comando.Prepare();

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Venta completada", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Producto registrado exitosamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        if (tipo == 2)
                        {
                            MessageBox.Show("Servicio registrado exitosamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Error al registrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ModificaSocio(String nombre, String pApellido, String sApellido, String id)
        {
            try
            {
                query = "UPDATE gym.socios SET nombre = '" + nombre + "', primer_apellido = '" + pApellido + "', segundo_apellido = '" + sApellido + "' WHERE clave_socio = " + id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Se ha actualizado el registro de este socio", "Actualizar datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Se ha eliminado", "Eliminar datos del socio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al eliminar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Producto eliminado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Error al eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Simetrias registradas exitosamente", "Registrar simetrias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al registrar simetria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void PagoSuscripcion(int clave_socio, int clave_producto, decimal precio, int duracion)
        {
            try
            {
                int clave_venta = get_Ultima_Venta();
                query = "INSERT INTO gym.ventas(clave_venta, clave_socio, fecha_venta) VALUES(" + clave_venta + ", " + clave_socio + ", CURRENT_DATE);";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion)
                {
                    Connection = conexion,
                    CommandTimeout = 60
                };
                comando.Prepare();
                if (comando.ExecuteNonQuery() > 0)
                {
                    query = "INSERT INTO gym.venta_producto (clave_producto,clave_venta,precio_producto,cantidad,clave_tipo_venta) VALUES (" + clave_producto + "," + clave_venta + "," + precio + ",1,2);";
                    comando = new NpgsqlCommand(query, conexion);
                    comando.Connection = conexion;
                    comando.CommandTimeout = 60;
                    comando.Prepare();
                    if (comando.ExecuteNonQuery() > 0)
                    {
                        query = "UPDATE gym.socios SET fecha_fin = (fecha_fin +  INTERVAL '" + duracion + " months');";
                        comando = new NpgsqlCommand(query, conexion);
                        comando.Connection = conexion;
                        comando.CommandTimeout = 60;
                        comando.Prepare();
                        if (comando.ExecuteNonQuery() > 0)
                        {
                                MessageBox.Show("Renovación realizada", "Renovación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        else
                        {
                            MessageBox.Show("Error al registrar socio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public String get_Nombre(String id)
        {
            String nombre;
            try
            {
                query = "SELECT nombre FROM gym.socios WHERE clave_socio = "+id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    nombre = (String)comando.ExecuteScalar();
                }
                catch
                {
                    nombre = "";
                }
                return nombre;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public String get_pApellido(String id)
        {
            String apellido;
            try
            {
                query = "SELECT primer_apellido FROM gym.socios WHERE clave_socio = " + id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    apellido = (String)comando.ExecuteScalar();
                }
                catch
                {
                    apellido = "";
                }
                return apellido;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        public String get_sApellido(String id)
        {
            String apellido;
            try
            {
                query = "SELECT segundo_apellido FROM gym.socios WHERE clave_socio = " + id;
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    apellido = (String)comando.ExecuteScalar();
                }
                catch
                {
                    apellido = "";
                }
                return apellido;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public int get_Ultimo_Socio()
        {
            int clave_socio;
            try
            {
                query = "SELECT clave_socio FROM gym.socios ORDER BY clave_socio DESC LIMIT 1";
                NpgsqlCommand comando = new NpgsqlCommand(query, conexion);
                comando.Connection = conexion;
                comando.CommandTimeout = 60;
                comando.Prepare();
                try
                {
                    clave_socio = (Int32)comando.ExecuteScalar();
                    clave_socio++;
                }
                catch
                {
                    clave_socio = 1;
                }
                return clave_socio;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return 1;
            }

        }

        
    }
}
