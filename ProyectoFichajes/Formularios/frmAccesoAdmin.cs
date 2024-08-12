using AP17;
using ProyectoFichajes.Clases;
using ProyectoFichajes.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFichajes
{
    public partial class frmAccesoAdmin : Form
    {
        public frmAccesoAdmin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Muestra un messagebox con el error correspondiente.
        /// </summary>
        /// <param name="mensaje"> Un aviso con el mensaje de error. </param>
        private void ErrorDeValidacion(string mensaje)
        {
            ConBD.CerrarConexion();
            MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void btnAcceso_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConBD.Conexion != null && ValidarDatos())
                {
                    string nif = Utiles.EliminarGuion(mtxtNif.Text);
                    ConBD.AbrirConexion();
                    if (Utiles.ValidarLetraNIF(nif))
                    {
                        if (Empleado.ExisteEmpleado(nif))
                        {
                            if (Empleado.EsAdministrador(nif))
                            {
                                if (Empleado.ComprobarClave(nif, txtClave.Text))
                                {
                                    this.Close();
                                    this.Dispose();
                                    ConBD.CerrarConexion();
                                    frmMantenimiento mantenimiento = new frmMantenimiento();
                                    mantenimiento.ShowDialog();
                                }
                                else
                                {
                                    ErrorDeValidacion("Contraseña incorrecta!");
                                    txtClave.Clear();
                                }
                            }
                            else
                            {
                                ErrorDeValidacion("No tienes permisos de administrador!");
                                this.Close();
                                this.Dispose();
                            }
                        }
                        else
                        {
                            ErrorDeValidacion("El empleado no existe!");
                            mtxtNif.Clear();
                            txtClave.Clear();
                        }
                    }
                    else
                    {
                        ErrorDeValidacion("NIF incorrecto!");
                        mtxtNif.Clear();
                        txtClave.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region Diseño de interface
        private void frmAccesoAdmin_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 246, 203);
            mtxtNif.Focus();
            mtxtNif.Mask = "00000000-L";
        }

        private void txtClave_Enter(object sender, EventArgs e)
        {
            txtClave.BackColor = Color.SkyBlue;
        }

        private void txtClave_Leave(object sender, EventArgs e)
        {
            txtClave.BackColor = Color.White;
        }

        private void pibAdmin_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pibAdmin.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void mtxtNif_Enter(object sender, EventArgs e)
        {
            mtxtNif.BackColor = Color.SkyBlue;
        }

        private void mtxtNif_Leave(object sender, EventArgs e)
        {
            mtxtNif.BackColor = Color.White;
        }
        #endregion

        #region Validaciones

        private bool ValidarDatos()
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

            if (string.IsNullOrEmpty(txtClave.Text))
            {
                ok = false;
                errorProvider1.SetError(txtClave, "Introduce la contraseña!");
            }
            return ok;
        }

        #endregion
    }
}