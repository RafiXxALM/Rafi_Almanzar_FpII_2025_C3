namespace Formulario_Con_Guardado
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstProducts;
        private System.Windows.Forms.Button btnExport;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SuspendLayout();
            // 
            // Form1
            // 
            BackColor = System.Drawing.Color.WhiteSmoke;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(820, 320);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "Form1";
            Text = "Guardado De Producto";
            // 
            // lblName
            // 
            lblName = new System.Windows.Forms.Label();
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(20, 18);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(64, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Nombre:";
            // 
            // txtName
            // 
            txtName = new System.Windows.Forms.TextBox();
            txtName.Location = new System.Drawing.Point(20, 42);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(340, 25);
            txtName.TabIndex = 1;
            txtName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // lblType
            // 
            lblType = new System.Windows.Forms.Label();
            lblType.AutoSize = true;
            lblType.Location = new System.Drawing.Point(20, 78);
            lblType.Name = "lblType";
            lblType.Size = new System.Drawing.Size(39, 20);
            lblType.TabIndex = 2;
            lblType.Text = "Tipo:";
            // 
            // cmbType
            // 
            cmbType = new System.Windows.Forms.ComboBox();
            cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] {
            "Alimentos",
            "Bebidas",
            "Limpieza",
            "Higiene",
            "Otros"});
            cmbType.Location = new System.Drawing.Point(20, 101);
            cmbType.Name = "cmbType";
            cmbType.Size = new System.Drawing.Size(340, 25);
            cmbType.TabIndex = 2;
            cmbType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // lblPrice
            // 
            lblPrice = new System.Windows.Forms.Label();
            lblPrice.AutoSize = true;
            lblPrice.Location = new System.Drawing.Point(20, 138);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new System.Drawing.Size(52, 20);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Precio:";
            // 
            // numPrice
            // 
            numPrice = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            numPrice.DecimalPlaces = 2;
            numPrice.Location = new System.Drawing.Point(20, 161);
            numPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            numPrice.Name = "numPrice";
            numPrice.Size = new System.Drawing.Size(340, 25);
            numPrice.TabIndex = 3;
            numPrice.ThousandsSeparator = true;
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            // 
            // btnSave
            // 
            btnSave = new System.Windows.Forms.Button();
            btnSave.Location = new System.Drawing.Point(20, 205);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(160, 38);
            btnSave.TabIndex = 4;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            btnExport = new System.Windows.Forms.Button();
            btnExport.Location = new System.Drawing.Point(200, 205);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(160, 38);
            btnExport.TabIndex = 5;
            btnExport.Text = "Exportar JSON";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnExport.ForeColor = System.Drawing.Color.White;
            btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lstProducts
            // 
            lstProducts = new System.Windows.Forms.ListBox();
            lstProducts.FormattingEnabled = true;
            lstProducts.HorizontalScrollbar = true;
            lstProducts.ItemHeight = 20;
            lstProducts.Location = new System.Drawing.Point(380, 18);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new System.Drawing.Size(420, 264);
            lstProducts.TabIndex = 6;
            lstProducts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left;
            lstProducts.DoubleClick += new System.EventHandler(this.lstProducts_DoubleClick);
            lstProducts.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // Add controls to form
            // 
            Controls.Add(lstProducts);
            Controls.Add(btnExport);
            Controls.Add(btnSave);
            Controls.Add(numPrice);
            Controls.Add(lblPrice);
            Controls.Add(cmbType);
            Controls.Add(lblType);
            Controls.Add(txtName);
            Controls.Add(lblName);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
