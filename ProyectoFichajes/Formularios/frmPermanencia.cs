using AP17;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFichajes
{
    public partial class frmPermanencia : Form
    {
        string nifEmpleado;
        public frmPermanencia(string nif)
        {
            InitializeComponent();
            nifEmpleado = nif;
        }

        private void frmPemanencia_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 246, 203);
            this.dgvPermanencia.BackgroundColor = Color.FromArgb(250, 246, 203);

            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    if (ConBD.Conexion != null)
                    {
                        ConBD.AbrirConexion();
                        Fichaje.CalcularDuracionFichaje(nifEmpleado); // Calcula la duración antes de la consulta
                        CargaListaEmpleados(nifEmpleado);
                        txtHoras.Text = Fichaje.CalcularDuracionTotal(nifEmpleado, dtpInicio.Value.ToString("yyyy/MM/dd"), dtpFin.Value.ToString("yyyy/MM/dd")).ToString();
                        ConBD.CerrarConexion();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        /// <summary>
        /// Validaciones de datos
        /// </summary>
        /// <returns> True si las validaciones son correctas, false si las validaciones son incorrectas</returns>
        private bool ValidarDatos()
        {
            bool ok = true;
            errorProvider1.Clear();

            if (dtpFin.Value < dtpInicio.Value)
            {
                ok = false;
                errorProvider1.SetError(dtpInicio, "La fecha de inicio debe ser menor que la fecha final!");
            }
            return ok;
        }
        /// <summary>
        /// Carga una lista de empleados mediante el nif del usuario
        /// </summary>
        /// <param name="nif">Obtener los datos de la consulta en base al nif introducido.</param>
        private void CargaListaEmpleados(string nif)
        {
            string seleccion = String.Format("SELECT dia, horaEntrada, horaSalida, duracionFichaje FROM fichajes " +
                "WHERE nifEmpleado LIKE '{0}' AND dia BETWEEN '{1}' AND '{2}' AND fichajeCompletado=1", nif, dtpInicio.Value.ToString("yyyy/MM/dd"), dtpFin.Value.ToString("yyyy/MM/dd"));
            dgvPermanencia.DataSource = Fichaje.RellenarDatos(seleccion);
            if(dgvPermanencia.Rows.Count == 0)
            {
                MessageBox.Show("No hay fichajes dentro del rango de días introducidos!","Avíso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
