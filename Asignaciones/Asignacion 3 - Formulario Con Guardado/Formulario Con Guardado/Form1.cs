namespace Formulario_Con_Guardado
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private readonly List<Product> productos = new();

        public Form1()
        {
            InitializeComponent();
        }

        private record Product(string Nombre, string Tipo, decimal Precio);

        private void btnSave_Click(object? sender, EventArgs e)
        {
            var nombre = txtName.Text.Trim();
            var tipo = cmbType.SelectedItem as string;
            var precio = numPrice.Value;

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Introduce el nombre del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Selecciona el tipo de producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.DroppedDown = true;
                return;
            }

            if (precio <= 0m)
            {
                MessageBox.Show("El precio debe ser mayor que 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return;
            }

            var p = new Product(nombre, tipo, precio);
            productos.Add(p);
            UpdateList();

            // Limpiar inputs
            txtName.Clear();
            cmbType.SelectedIndex = -1;
            numPrice.Value = 0m;
            txtName.Focus();
        }

        private void UpdateList()
        {
            lstProducts.BeginUpdate();
            lstProducts.Items.Clear();
            foreach (var p in productos)
            {
                lstProducts.Items.Add($"{p.Nombre} — {p.Tipo} — {p.Precio:C2}");
            }
            lstProducts.EndUpdate();
        }

        private void lstProducts_DoubleClick(object? sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex < 0) return;

            var idx = lstProducts.SelectedIndex;
            var item = productos[idx];
            var confirm = MessageBox.Show($"Eliminar '{item.Nombre}' de la lista?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                productos.RemoveAt(idx);
                UpdateList();
            }
        }

        private void btnExport_Click(object? sender, EventArgs e)
        {
            if (productos.Count == 0)
            {
                MessageBox.Show("No hay productos para exportar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(productos, options);

                // Ruta dinámica al Escritorio del usuario + carpetas solicitadas
                var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var targetDir = Path.Combine(
                    desktop,
                    "Espacio De Trabajo Fund. De Programacion II",
                    "Asignaciones",
                    "Asignacion 3 - Formulario Con Guardado",
                    "Formulario Con Guardado",
                    "Base de Datos"
                );

                // Asegurar que la carpeta exista
                Directory.CreateDirectory(targetDir);

                var filePath = Path.Combine(targetDir, "productos.json");
                File.WriteAllText(filePath, json);

                MessageBox.Show($"Exportado a:\n{filePath}", "Exportación completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
