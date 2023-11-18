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
    public partial class frmEmpleados : Form
    {
        clsInicio objBD;
        public frmEmpleados()
        {
            InitializeComponent();

            lstBuscar.SelectedIndex = 0;
            objBD = new clsInicio();
            objBD.TraerDatos(dgv);
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (lstBuscar.SelectedIndex == 0)
            {
                objBD.BuscarPorApellido(txtBuscar.Text, dgv);

            }
            else
            {
                objBD.BuscarPorCiudad(txtBuscar.Text, dgv);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            objBD.TraerDatos(dgv);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {

            frmMenu menu = new frmMenu();
            this.Hide();
            menu.Show();
        }
    }
}
