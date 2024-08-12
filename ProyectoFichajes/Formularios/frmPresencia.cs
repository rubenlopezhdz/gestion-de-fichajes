using AP17;
using ProyectoFichajes.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProyectoFichajes
{
    public partial class frmPresencia : Form
    {
        public frmPresencia()
        {
            InitializeComponent();
        }

        private void frmPresencia_Load(object sender, EventArgs e)
        {
            CargaListaFichajes();

            this.BackColor = Color.FromArgb(250, 246, 203);
            this.dgvPresencia.BackgroundColor = Color.FromArgb(250, 246, 203);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// Carga un datatable en memoria con la consulta que utilizamoss
        /// </summary>
        private void CargaListaFichajes()
        {
            string seleccion = "SELECT nombre,apellidos,horaEntrada FROM empleados INNER JOIN fichajes ON nif=nifEmpleado WHERE fichajeEntrada=1 AND fichajeSalida=0";
            if (ConBD.Conexion != null)
            {
                ConBD.AbrirConexion();
                dgvPresencia.DataSource = Fichaje.RellenarDatos(seleccion);
                ConBD.CerrarConexion();
                if (dgvPresencia.Rows.Count == 0)
                {
                    MessageBox.Show("No hay empleados presentes!", "Avíso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
