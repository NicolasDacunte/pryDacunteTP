using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
namespace pryDacunteTP
{
    internal class clsInicio
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;
        

        public string EstadoConexion = "";


        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source =C:\\Users\\Alumno\\source\\repos\\pryDacunteTP\bin\\Debug\\EMPLEADO.accdb");
                conexionBD.Open();
                EstadoConexion = "Conectado";
            }
            catch (Exception ex)
            {
                EstadoConexion = "Error" + ex.Message;
            }
        }

        public void CrearUsuario(string Nombre, string Apellido, string Direccion, string Ciudad, string Telefono, DateTime Nacimiento)
        {
            OleDbCommand comandoBD = new OleDbCommand();
            OleDbDataAdapter adaptador;
            DataSet objds = new DataSet(); // objeto DataSet a usar  

            try
            {
                // establecer las propiedades al objeto comando
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                // Que tabla traigo
                comandoBD.CommandText = "Usuarios";

                // abrir la tabla y mostrar por renglón
                lectorBD = comandoBD.ExecuteReader();

                // Verificar si TIENE FILAS
                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read()) // mientras pueda leer, mostrar (leer)
                    {
                        if (lectorBD[1].ToString() == Nombre)
                        {
                            MessageBox.Show("Ya existe este Usuario");
                            return; // Salir del método si ya existe el usuario
                        }
                    }
                }

                // Cerrar el DataReader después de usarlo para evitar conflictos
                lectorBD.Close();

                // Crear el objeto DataAdapter pasando como parámetro el objeto comando que queremos vincular
                adaptador = new OleDbDataAdapter(comandoBD);

                // Ejecutar la lectura de la tabla y almacenar su contenido en el dataAdapter
                adaptador.Fill(objds, "Usuarios");

                // Obtener una referencia a la tabla de Usuarios
                DataTable tabla = objds.Tables["Usuarios"];

                // Crear el nuevo DataRow con la estructura de campos de la tabla Usuarios
                DataRow nuevoRegistro = tabla.NewRow();

                // Asignar los valores a todos los campos del DataRow
                nuevoRegistro["NOMBRE"] = Nombre;
                nuevoRegistro["APELLIDO"] = Apellido;
                nuevoRegistro["DIRECCION"] = Direccion;
                nuevoRegistro["CIUDAD"] = Ciudad;
                nuevoRegistro["TELEFONO"] = Telefono;
                nuevoRegistro["FECHA_NAC"] = Nacimiento;

                // Agregar el DataRow a la tabla Usuarios
                tabla.Rows.Add(nuevoRegistro);

                // Crear el objeto OleDbCommandBuilder pasando como parámetro el DataAdapter
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);

                // Actualizar la base con los cambios realizados
                adaptador.Update(objds, "NOMBRE");

                MessageBox.Show("Usuario creado con éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar el DataReader en el bloque finally
                if (lectorBD != null && !lectorBD.IsClosed)
                {
                    lectorBD.Close();
                }
            }
        }


    }
}
