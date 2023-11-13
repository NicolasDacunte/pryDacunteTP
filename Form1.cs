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
    public partial class Form1 : Form
    {
        clsInicio objInicio;

        public Form1()
        {
            InitializeComponent();
            objInicio = new clsInicio();
            objInicio.ConectarBD();
            lblConectar.Text = Convert.ToString(DateTime.Now);
            lblConectar.BackColor = Color.LightGreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblConectar_Click(object sender, EventArgs e)
        {

        }
    }
}
