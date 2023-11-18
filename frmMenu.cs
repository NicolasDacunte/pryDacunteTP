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
    public partial class frmMenu : Form
    {
        clsInicio objInicio;

        public frmMenu()
        {
            InitializeComponent();
            objInicio = new clsInicio();
            objInicio.ConectarBD();
            if (objInicio.EstadoConexion == "Conectado")
            {
                lblConectar.Text = Convert.ToString(DateTime.Now);
                lblConectar.BackColor = Color.LightGreen;
            }
            else
            {
                lblConectar.Text = objInicio.EstadoConexion;
                lblConectar.BackColor = Color.Red;
            }

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblConectar_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Actualiza el contenido del Label con la hora actual.
            lblConectar.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void registroDeEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistrar frmRegistrar = new frmRegistrar();
            this.Hide();
            frmRegistrar.Show();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleados frmEmpleados = new frmEmpleados();
            this.Hide();
            frmEmpleados.Show();
        }
    }
}
