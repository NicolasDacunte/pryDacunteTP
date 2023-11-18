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
        OleDbDataAdapter adaptador;
        DataSet objDataSet = new DataSet();
        

        public string EstadoConexion = "";


        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = C:\\Users\\sistema\\Source\\Repos\\pryDacunteTP\\resources\\EMPLEADO.accdb");
                conexionBD.Open();
                EstadoConexion = "Conectado";
            }
            catch (Exception ex)
            {
                EstadoConexion = "Error" + ex.Message;
            }
        }

        public void CrearUsuario(int Codigo,string Nombre, string Apellido, string Direccion, string Ciudad, string Telefono, DateTime Nacimiento)
        {
            ConectarBD();
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;


            // Establece el tipo de comando y la tabla
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comandoBD.CommandText = "DATOS PERSONALES";



            // crear el objeto DataAdapter pasando como parámetro el objeto comando que queremos vincular
            adaptador = new OleDbDataAdapter(comandoBD);
            // ejecutar la lectura de la tabla y almacenar su contenido en el dataAdapter
            adaptador.Fill(objDataSet, "DATOS PERSONALES");
            // obtenemos una referencia a la tabla


            DataTable dt = objDataSet.Tables["DATOS PERSONALES"];

            // creamos el nuevo DataRow con la estructura de campos de la tabla
            DataRow nuevoregistro = dt.NewRow();
            // asignamos los valores a todos los campos del DataRow
            nuevoregistro["CODIGO"] = Codigo;
            nuevoregistro["NOMBRE"] = Nombre;
            nuevoregistro["APELLIDO"] = Apellido;
            nuevoregistro["DIRECCIÒN"] = Direccion;
            nuevoregistro["CIUDAD"] = Ciudad;
            nuevoregistro["TELEFONO"] = Telefono;
            nuevoregistro["FECHA_NACIMIENTO"] = Nacimiento;
            // agregamos el DataRow a la tabla

            dt.Rows.Add(nuevoregistro);

            // creamos el objeto OledBCommandBuilder pasando como parámetro el DataAdapter
            OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);

            // actualizamos la base con los cambios realizados
            adaptador.Update(objDataSet, "DATOS PERSONALES");
            conexionBD.Close();

            MessageBox.Show("El Empleado se cargo correctamente");
        }
        public void TraerDatos(DataGridView dgv)
        {
            ConectarBD();
            dgv.Rows.Clear();
            //instancia un objeto en la memoria
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "DATOS PERSONALES";

            lectorBD = comandoBD.ExecuteReader();

            dgv.Columns.Add("Codigo", "Codigo");
            dgv.Columns.Add("Nombre", "Nombre");
            dgv.Columns.Add("Apellido", "Apellido");
            dgv.Columns.Add("Direccion", "Direccion");
            dgv.Columns.Add("Ciudad", "Ciudad");
            dgv.Columns.Add("Telefono", "Telefono");
            dgv.Columns.Add("Fecha de nacimiento", "Fecha de nacimiento");

            //leo como si fuera un archivo
            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {

                    dgv.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[5], lectorBD[6]);

                }

            }
        }

        int encontrado = 0;
        public void BuscarPorApellido(string codigo, DataGridView dgv)
        {
            ConectarBD();
            dgv.Rows.Clear();
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            //q operacion quiero hacer y que me traiga TODA la tabla con el tabledirect
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comandoBD.CommandText = "DATOS PERSONALES";
            //abre la tabla y muestra por renglon
            lectorBD = comandoBD.ExecuteReader();

            //SI TIENE FILAS
            if (lectorBD.HasRows)
            {
                encontrado = 0;
                while (lectorBD.Read()) //mientras pueda leer, mostrar
                {
                    if (lectorBD[4].ToString() == codigo)
                    {
                        dgv.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[5], lectorBD[6]);
                        encontrado = 1;

                    }
                }
                conexionBD.Close();

                if (encontrado == 0)
                {
                    MessageBox.Show("Apellido " + codigo + " no esta cargado en el sistema");
                    TraerDatos(dgv);
                }
            }
        }

        public void BuscarPorCiudad(string codigo, DataGridView dgv)
        {
            ConectarBD();
            dgv.Rows.Clear();
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            //q tipo de operacion quierp hacer y que me traiga TOD la tabla con el tabledirect
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comandoBD.CommandText = "DATOS PERSONALES";
            //abre la tabla y muestra por renglon
            lectorBD = comandoBD.ExecuteReader();


            //SI TIENE FILAS
            if (lectorBD.HasRows)
            {
                encontrado = 0;
                while (lectorBD.Read()) //mientras pueda leer, mostrar (leer)
                {
                    if (lectorBD[2].ToString() == codigo)
                    {
                        dgv.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[5], lectorBD[6]);
                        encontrado = 1;

                    }
                }
                conexionBD.Close();

                if (encontrado == 0)
                {
                    MessageBox.Show("No se encontro ningun empleado cargado en la ciudad " + codigo);
                    TraerDatos(dgv);
                }
            }
        }
    }
}
