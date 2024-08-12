namespace ProyectoFichajes
{
    partial class frmAccesoAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccesoAdmin));
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnAcceso = new System.Windows.Forms.Button();
            this.lblNif = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.pibAdmin = new System.Windows.Forms.PictureBox();
            this.mtxtNif = new System.Windows.Forms.MaskedTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pibAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtClave
            // 
            this.txtClave.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClave.Location = new System.Drawing.Point(44, 164);
            this.txtClave.Margin = new System.Windows.Forms.Padding(4);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(339, 39);
            this.txtClave.TabIndex = 1;
            this.txtClave.Enter += new System.EventHandler(this.txtClave_Enter);
            this.txtClave.Leave += new System.EventHandler(this.txtClave_Leave);
            // 
            // btnAcceso
            // 
            this.btnAcceso.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcceso.Location = new System.Drawing.Point(172, 229);
            this.btnAcceso.Margin = new System.Windows.Forms.Padding(4);
            this.btnAcceso.Name = "btnAcceso";
            this.btnAcceso.Size = new System.Drawing.Size(211, 47);
            this.btnAcceso.TabIndex = 2;
            this.btnAcceso.Text = "Acceder";
            this.btnAcceso.UseVisualStyleBackColor = true;
            this.btnAcceso.Click += new System.EventHandler(this.btnAcceso_Click);
            // 
            // lblNif
            // 
            this.lblNif.AutoSize = true;
            this.lblNif.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNif.Location = new System.Drawing.Point(37, 25);
            this.lblNif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNif.Name = "lblNif";
            this.lblNif.Size = new System.Drawing.Size(221, 33);
            this.lblNif.TabIndex = 3;
            this.lblNif.Text = "Introduce el nif:";
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Font = new System.Drawing.Font("Century", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.Location = new System.Drawing.Point(37, 127);
            this.lblClave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(327, 33);
            this.lblClave.TabIndex = 4;
            this.lblClave.Text = "Introduce la contraseña:";
            // 
            // pibAdmin
            // 
            this.pibAdmin.Image = global::ProyectoFichajes.Properties.Resources.admin;
            this.pibAdmin.Location = new System.Drawing.Point(443, 54);
            this.pibAdmin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pibAdmin.Name = "pibAdmin";
            this.pibAdmin.Size = new System.Drawing.Size(139, 149);
            this.pibAdmin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibAdmin.TabIndex = 5;
            this.pibAdmin.TabStop = false;
            this.pibAdmin.Paint += new System.Windows.Forms.PaintEventHandler(this.pibAdmin_Paint);
            // 
            // mtxtNif
            // 
            this.mtxtNif.Font = new System.Drawing.Font("Century", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtNif.Location = new System.Drawing.Point(44, 73);
            this.mtxtNif.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxtNif.Name = "mtxtNif";
            this.mtxtNif.Size = new System.Drawing.Size(339, 40);
            this.mtxtNif.TabIndex = 0;
            this.mtxtNif.Enter += new System.EventHandler(this.mtxtNif_Enter);
            this.mtxtNif.Leave += new System.EventHandler(this.mtxtNif_Leave);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAccesoAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(621, 289);
            this.Controls.Add(this.mtxtNif);
            this.Controls.Add(this.pibAdmin);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.lblNif);
            this.Controls.Add(this.btnAcceso);
            this.Controls.Add(this.txtClave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAccesoAdmin";
            this.Text = "Acceso Admin";
            this.Load += new System.EventHandler(this.frmAccesoAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnAcceso;
        private System.Windows.Forms.Label lblNif;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.PictureBox pibAdmin;
        private System.Windows.Forms.MaskedTextBox mtxtNif;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}