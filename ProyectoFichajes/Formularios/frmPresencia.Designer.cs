namespace ProyectoFichajes
{
    partial class frmPresencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPresencia));
            this.dgvPresencia = new System.Windows.Forms.DataGridView();
            this.lblPresencia = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresencia)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPresencia
            // 
            this.dgvPresencia.AllowUserToAddRows = false;
            this.dgvPresencia.AllowUserToDeleteRows = false;
            this.dgvPresencia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPresencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresencia.Location = new System.Drawing.Point(99, 80);
            this.dgvPresencia.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPresencia.Name = "dgvPresencia";
            this.dgvPresencia.ReadOnly = true;
            this.dgvPresencia.RowHeadersWidth = 51;
            this.dgvPresencia.Size = new System.Drawing.Size(856, 414);
            this.dgvPresencia.TabIndex = 0;
            // 
            // lblPresencia
            // 
            this.lblPresencia.AutoSize = true;
            this.lblPresencia.Font = new System.Drawing.Font("Century", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresencia.Location = new System.Drawing.Point(305, 17);
            this.lblPresencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPresencia.Name = "lblPresencia";
            this.lblPresencia.Size = new System.Drawing.Size(419, 47);
            this.lblPresencia.TabIndex = 1;
            this.lblPresencia.Text = "Empleados Presentes";
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Century", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(931, 532);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(120, 43);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // frmPresencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1067, 590);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblPresencia);
            this.Controls.Add(this.dgvPresencia);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPresencia";
            this.Text = "Presencia";
            this.Load += new System.EventHandler(this.frmPresencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresencia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPresencia;
        private System.Windows.Forms.Label lblPresencia;
        private System.Windows.Forms.Button btnVolver;
    }
}