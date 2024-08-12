using AP17;
using ProyectoFichajes.Clases;
using ProyectoFichajes.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFichajes
{
    public partial class frmMantenimiento : Form
    {
        public frmMantenimiento()
        {
            InitializeComponent();
        }
        private void frmMantenimiento_Load(object sender, EventArgs e)
        {
            lblClave.Enabled = false;
            txtClave.Enabled = false;

            CargaListaEmpleados();
            CargaListaFichajes();

            mtxtNif.Mask = "00000000-L";

            this.BackColor = Color.FromArgb(250, 246, 203);
            this.dgvEmpleados.BackgroundColor = Color.FromArgb(250, 246, 203);
            this.dgvFichajes.BackgroundColor = Color.FromArgb(250, 246, 203);
            mnuSalir.BackColor = Color.FromArgb(250, 246, 203);
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void LimpiarCampos()
        {
            mtxtNif.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtClave.Clear();
            chkAdministrador.Checked = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosAgregar())
            {
                int resultado = 0;
                try
                {
                    string nif = Utiles.EliminarGuion(mtxtNif.Text);
                    Empleado empleado = new Empleado();
                    empleado.Nif = nif;
                    empleado.Nombre = txtNombre.Text;
                    empleado.Apellidos = txtApellidos.Text;
                    empleado.Administrador = chkAdministrador.Checked;
                    empleado.Clave = txtClave.Text;

                    if (ConBD.Conexion != null)
                    {
                        ConBD.AbrirConexion();
                        if (Utiles.ValidarLetraNIF(nif))
                        {
                            if (!Empleado.ExisteEmpleado(nif))
                            {
                                resultado = empleado.AgregarEmpleado(empleado);
                                ConBD.CerrarConexion();
                                CargaListaEmpleados();
                            }
                            else
                            {
                                MessageBox.Show("El empleado ya existe!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("NIF incorrecto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        if (resultado > 0)
                        {
                            LimpiarCampos();
                        }
                        ConBD.CerrarConexion();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea eliminar el usuario?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
            {
                if (ValidarDatosEliminar())
                {
                    int resultado = 0;
                    try
                    {
                        string nif = Utiles.EliminarGuion(mtxtNif.Text);
                        if (ConBD.Conexion != null)
                        {
                            ConBD.AbrirConexion();
                            if (Empleado.ExisteEmpleado(nif))
                            {
                                resultado = Empleado.EliminaUsuario(nif);
                                resultado = Fichaje.ActualizarSalida(nif);
                            }
                            else
                            {
                                MessageBox.Show("No existe el empleado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            if (resultado > 0)
                            {
                                LimpiarCampos();
                            }
                            ConBD.CerrarConexion();
                        }
                        CargaListaEmpleados();
                        CargaListaFichajes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                    }
                }
            }
        }
        private void chkAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdministrador.Checked)
            {
                lblClave.Enabled = true;
                txtClave.Enabled = true;
            }
            else
            {
                txtClave.Clear();
                lblClave.Enabled = false;
                txtClave.Enabled = false;
            }
        }
        #region Validaciones

        private bool ValidarDatosAgregar()
        {
            bool ok = true;
            errorProvider1.Clear();

            string nif = mtxtNif.Text;
            char letra = nif[nif.Length - 1];
            if (nif == "        -")
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "Introduce el nif!");
            }
            else if (nif[7] == ' ')
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "Introduce los digitos del nif!");
            }
            else if (!char.IsUpper(letra))
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "El último carácter debe ser una letra mayúscula!");
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                ok = false;
                errorProvider1.SetError(txtNombre, "Introduce el nombre!");
            }
            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                ok = false;
                errorProvider1.SetError(txtApellidos, "Introduce los apellidos!");
            }
            if (chkAdministrador.Checked)
            {
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    ok = false;
                    errorProvider1.SetError(txtClave, "Introduce la contraseña!");
                }
            }

            return ok;
        }
        private bool ValidarDatosEliminar()
        {
            bool ok = true;
            errorProvider1.Clear();

            string nif = mtxtNif.Text;
            char letra = nif[nif.Length - 1];
            if (nif == "        -")
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "Introduce el nif!");
            }
            else if (nif[7] == ' ')
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "Introduce los digitos del nif!");
            }
            else if (!char.IsUpper(letra))
            {
                ok = false;
                errorProvider1.SetError(mtxtNif, "El último carácter debe ser una letra mayúscula!");
            }
            return ok;
        }
        #endregion

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Carga la lista de empleados en el datagridview de empleados
        /// </summary>
        private void CargaListaEmpleados()
        {
            string seleccion = "SELECT * FROM empleados";
            if (ConBD.Conexion != null)
            {
                ConBD.AbrirConexion();
                dgvEmpleados.DataSource = Empleado.BuscarEmpleados(seleccion);
                ConBD.CerrarConexion();
            }
        }

        /// <summary>
        /// Carga un datatable mediante la consulta de fichajes en el datagridview de fichajes
        /// </summary>
        private void CargaListaFichajes()
        {
            string seleccion = "SELECT numFichaje,nifEmpleado,dia,horaEntrada,horaSalida,fichajeEntrada,fichajeSalida FROM fichajes";
            if (ConBD.Conexion != null)
            {
                ConBD.AbrirConexion();
                dgvFichajes.DataSource = Fichaje.RellenarDatos(seleccion);
                ConBD.CerrarConexion();
            }
        }
    }
}
