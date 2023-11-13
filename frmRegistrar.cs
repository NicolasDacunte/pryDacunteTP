using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDacunteTP
{
    public partial class frmRegistrar : Form
    {
        clsInicio objInicio;
        public frmRegistrar()
        {
            InitializeComponent();
            objInicio = new clsInicio();
            objInicio.ConectarBD();
        }
        private void frmRegistrar_Load(object sender, EventArgs e)
        {
            
            this.TableAdapter.Fill(this.empleadosDataSet.Usuarios);

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "")
            {
                
                
                objInicio.CrearUsuario(txtNombre.Text, txtApellido.Text, txtDireccion.Text, txtCiudad.Text,txtTelefono.Text, dtNac.Value);
                txtNombre.Focus();
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDireccion.Text = "";
                txtCiudad.Text = "";
                txtTelefono.Text = "";
                
                
            }
            else
            {
                MessageBox.Show("NECESITA COMPLETAR LOS CAMPOS ´USUARIO´ Y ´CONTRASEÑA´");
            }

        }

        
    }
}
