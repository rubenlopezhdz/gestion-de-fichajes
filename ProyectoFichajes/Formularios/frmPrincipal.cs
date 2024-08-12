using AP17;
using ProyectoFichajes.Clases;
using ProyectoFichajes.Class;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFichajes
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            mtxtNif.Focus();
            mtxtNif.Mask = "00000000-L";

            timerReloj.Enabled = true;
            timerReloj.Interval = 1000;
            timerReloj.Start();

            lblReloj.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            this.BackColor = Color.FromArgb(250, 246, 203);
            rtbMensaje.Visible = false;
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            frmAccesoAdmin frmAdmin = new frmAccesoAdmin();
            frmAdmin.ShowDialog();
            mtxtNif.Focus();
        }

        private async void btnEntrada_Click(object sender, EventArgs e)
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
                            int resultado = 0;

                            Fichaje fichaje = new Fichaje();
                            fichaje.NifEmpleado = nif;
                            fichaje.Dia = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                            fichaje.HoraEntrada = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                            fichaje.FichajeEntrada = true;
                            fichaje.FichajeSalida = false;

                            if (!Fichaje.ExisteFichaje(fichaje))
                            {
                                resultado = fichaje.RegistrarFichaje(fichaje);
                                ConBD.CerrarConexion();
                                mtxtNif.Clear();

                                rtbMensaje.Visible = true;
                                rtbMensaje.Text = $"Entrada realizada \n\nNIF: {fichaje.NifEmpleado}\nHora de entrada: {fichaje.HoraEntrada}";
                                await Task.Delay(3000);
                                rtbMensaje.Visible = false;

                                mtxtNif.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Usuario no dado de salida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El empleado no existe!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NIF incorrecto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                ConBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSalida_Click(object sender, EventArgs e)
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
                            int resultado = 0;

                            Fichaje fichaje = new Fichaje();
                            fichaje.NifEmpleado = nif;
                            fichaje.Dia = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                            fichaje.HoraSalida = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                            fichaje.FichajeSalida = true;
                            fichaje.FichajeCompletado = true;
                            if (Fichaje.ExisteFichaje(fichaje))
                            {
                                resultado = fichaje.RegistrarSalida(fichaje);
                                ConBD.CerrarConexion();
                                mtxtNif.Clear();

                                rtbMensaje.Visible = true;
                                rtbMensaje.Text = $"Salida realizada \n\nNIF: {fichaje.NifEmpleado}\nHora de salida: {fichaje.HoraSalida}";
                                await Task.Delay(3000);
                                rtbMensaje.Visible = false;

                                mtxtNif.Focus();

                            }
                            else
                            {
                                MessageBox.Show("Usuario no dado de entrada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El empleado no existe!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NIF incorrecto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                ConBD.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPermanencia_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() && ConBD.Conexion != null)
            {
                string nif = Utiles.EliminarGuion(mtxtNif.Text);
                ConBD.AbrirConexion();
                if (Utiles.ValidarLetraNIF(nif))
                {
                    if (Empleado.ExisteEmpleado(nif))
                    {
                        ConBD.CerrarConexion();
                        frmPermanencia frm = new frmPermanencia(Utiles.EliminarGuion(mtxtNif.Text));

                        frm.ShowDialog();
                        mtxtNif.Focus();
                        mtxtNif.Clear();
                    }
                    else
                    {
                        MessageBox.Show("El empleado no existe!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ConBD.CerrarConexion();
                    }
                }
                else
                {
                    MessageBox.Show("NIF incorrecto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    ConBD.CerrarConexion();
                }
            }
        }

        private void btnPresencia_Click(object sender, EventArgs e)
        {
            frmPresencia frmPresencia = new frmPresencia();
            frmPresencia.ShowDialog();
            mtxtNif.Focus();
        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        #region Diseño interface
        private void pbFecha_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbFecha.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
        private void pbImagen_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pbImagen.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
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

        /// <summary>
        /// Validación de datos
        /// </summary>
        /// <returns>Validaciones realizadas para la interface</returns>
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

            return ok;
        }
        #endregion
    }
}