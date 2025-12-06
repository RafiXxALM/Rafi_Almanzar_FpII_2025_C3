using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoslerRiskApp.Models;

namespace MoslerRiskApp
{
    // Ventana principal de la aplicación
    public class MainForm : Form
    {
        // Controles principales
        private TabControl tabControl;
        private TabPage tabDefinition;
        private TabPage tabAnalysis;
        private TabPage tabEvaluation;
        private TabPage tabClassification;

        // Controles de la pestaña Definición
        private TextBox txtNumeroFicha;
        private TextBox txtNombreAnalista;
        private DateTimePicker dtpFecha;
        private TextBox txtBienActivo;
        private TextBox txtRiesgo;
        private TextBox txtDano;
        private TextBox txtId; // identificador (será tomado de la BD más adelante)
        private DataGridView dgvDefinition; // grid para mostrar las entradas guardadas

        // Controles de análisis
        private ComboBox cmbFuncion;
        private ComboBox cmbSustitucion;
        private ComboBox cmbProfundidad;
        private ComboBox cmbExtension;
        private ComboBox cmbAgresion;
        private ComboBox cmbVulnerabilidad;

        // Modelo en memoria
        private Riesgo currentRiesgo = new Riesgo();

        // Constructor
        public MainForm()
        {
            InitializeComponents();
        }

        // Inicializa los controles del formulario
        private void InitializeComponents()
        {
            Text = "Mosler RiskMan";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1500, 480);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            tabControl = new TabControl { Dock = DockStyle.Fill, Padding = new Point(12, 8) };

            tabDefinition = new TabPage("Definición");
            tabAnalysis = new TabPage("Análisis");
            tabEvaluation = CreateTab("Evaluación", "Aquí irá la interfaz para evaluar el riesgo.");
            tabClassification = CreateTab("Clasificación", "Aquí irá la interfaz para clasificar el riesgo.");

            tabControl.TabPages.AddRange(new[] { tabDefinition, tabAnalysis, tabEvaluation, tabClassification });

            Controls.Add(tabControl);

            // Pie con autor
            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 160 };

            // Fila superior del panel inferior: contiene la fecha alineada a la derecha
            var bottomTopRow = new Panel { Dock = DockStyle.Top, Height = 22, Padding = new Padding(8, 2, 8, 2) };
            var lblAutor = new Label
            {
                Text = "Autor: Rafi Junior Almanzar",
                AutoSize = false,
                Width = 220,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.DimGray,
                Font = new Font(Font.FontFamily, 8F, FontStyle.Regular),
                Name = "lblAutor"
            };
            bottomTopRow.Controls.Add(lblAutor);

            var fechaPanel = new TableLayoutPanel { AutoSize = true, ColumnCount = 2, RowCount = 1 };
            fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var lblFecha = new Label { Text = "Fecha:", AutoSize = true, Padding = new Padding(0, 4, 6, 0), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            dtpFecha = new DateTimePicker { Width = 120, Format = DateTimePickerFormat.Short, Name = "dtpFecha", Anchor = AnchorStyles.Top | AnchorStyles.Right };
            fechaPanel.Controls.Add(lblFecha, 0, 0);
            fechaPanel.Controls.Add(dtpFecha, 1, 0);

            bottomTopRow.Controls.Add(fechaPanel);
            bottomPanel.Controls.Add(bottomTopRow);

            // Inicializar contenido de pestañas
            InitializeDefinitionTab();
            InitializeAnalysisTab();
        }

        // Construye la pestaña "Definición"
        private void InitializeDefinitionTab()
        {
            // Contenedor principal de la pestaña
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 8, 12, 8) };

            // Título principal: situado justo debajo de la barra de pestañas y centrado
            var titleLabel = new Label
            {
                Text = "Ficha Técnica - Análisis De Riesgos",
                Font = new Font(Font.FontFamily, 12F, FontStyle.Bold),
                AutoSize = false,
                Height = 30,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Añadir el título al contenedor para que quede centrado y no se solape
            container.Controls.Add(titleLabel);

            // Panel superior derecho para la fecha (se usará también en el panel inferior para posicionarlo junto al DataGrid)
            var fechaPanel = new TableLayoutPanel { AutoSize = true, ColumnCount = 2, RowCount = 1 };
            fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var lblFecha = new Label { Text = "Fecha:", AutoSize = true, Padding = new Padding(0, 4, 6, 0), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            dtpFecha = new DateTimePicker { Width = 120, Format = DateTimePickerFormat.Short, Name = "dtpFecha", Anchor = AnchorStyles.Top | AnchorStyles.Right };
            fechaPanel.Controls.Add(lblFecha, 0, 0);
            fechaPanel.Controls.Add(dtpFecha, 1, 0);

            // Contenido principal: dos columnas (izquierda: Bien/Riesgo, derecha: Daño + panel lateral)
            var contentLayout = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 2, RowCount = 1, AutoSize = true, Padding = new Padding(4, 8, 4, 8) };
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            // Columna izquierda: Bien / Activo y Riesgo
            var leftStack = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, AutoSize = true };
            var lblBien = new Label { Text = "Bien / Activo", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtBienActivo = new TextBox { Name = "txtBienActivo", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = GetSingleLineHeight() * 2, MaxLength = 200, Dock = DockStyle.Top, Margin = new Padding(0, 0, 8, 8) };
            var lblRiesgo = new Label { Text = "Riesgo", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtRiesgo = new TextBox { Name = "txtRiesgo", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = GetSingleLineHeight() * 2, MaxLength = 200, Dock = DockStyle.Top, Margin = new Padding(0, 0, 8, 8) };
            leftStack.Controls.Add(lblBien);
            leftStack.Controls.Add(txtBienActivo);
            leftStack.Controls.Add(lblRiesgo);
            leftStack.Controls.Add(txtRiesgo);

            // Columna derecha: Daño y panel lateral con ID y Guardar
            var rightPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, AutoSize = true };

            var damageLayout = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 2, RowCount = 1, AutoSize = true };
            damageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            damageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            var damagePanel = new Panel { Dock = DockStyle.Fill };
            var lblDano = new Label { Text = "Daño", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtDano = new TextBox { Name = "txtDano", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = GetSingleLineHeight() * 4, MaxLength = 400, Dock = DockStyle.Fill };
            damagePanel.Controls.Add(txtDano);
            damagePanel.Controls.Add(lblDano);

            var sideStack = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, Dock = DockStyle.Fill, WrapContents = false, Padding = new Padding(6), Width = 140 };
            var lblIdSmall = new Label { Text = "ID (BD):", AutoSize = true, Padding = new Padding(0, 6, 0, 0) };
            txtId = new TextBox { Width = 120, Name = "txtId" };
            txtId.TextChanged += (s, e) => currentRiesgo.Id = txtId.Text;
            txtId.Text = currentRiesgo.Id;

            var btnGuardar = new Button { Text = "Guardar", AutoSize = false, Width = txtId.Width, Height = 26 };
            sideStack.Controls.Add(lblIdSmall);
            sideStack.Controls.Add(txtId);
            sideStack.Controls.Add(btnGuardar);

            damageLayout.Controls.Add(damagePanel, 0, 0);
            damageLayout.Controls.Add(sideStack, 1, 0);
            rightPanel.Controls.Add(damageLayout);

            contentLayout.Controls.Add(leftStack, 0, 0);
            contentLayout.Controls.Add(rightPanel, 1, 0);

            // Cuadrícula para mostrar registros guardados (sin filas iniciales)
            dgvDefinition = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgvDefinition.Columns.Add("IdCol", "ID");
            dgvDefinition.Columns.Add("RiesgoCol", "Riesgo");
            dgvDefinition.Columns.Add("ActivoCol", "Activo");
            dgvDefinition.Columns.Add("DanoCol", "Daño");

            // Panel inferior que contiene la fecha arriba a la derecha y el DataGrid debajo
            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 160 };

            // Fila superior del panel inferior: contiene la fecha alineada a la derecha
            var bottomTopRow = new Panel { Dock = DockStyle.Top, Height = fechaPanel.PreferredSize.Height + 6 };
            fechaPanel.Dock = DockStyle.Right;
            bottomTopRow.Controls.Add(fechaPanel);

            // DataGrid ocupará el resto del espacio debajo de la fila superior
            dgvDefinition.Dock = DockStyle.Fill;
            bottomPanel.Controls.Add(dgvDefinition);
            bottomPanel.Controls.Add(bottomTopRow);

            // Añadir controles al contenedor en el orden solicitado: título, contenido, panel inferior
            // titleLabel ya fue añadido arriba al contenedor
            container.Controls.Add(contentLayout);
            container.Controls.Add(bottomPanel);

            // Eventos de ajuste de altura para cajas multilínea
            txtBienActivo.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 3);
            txtRiesgo.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 3);
            txtDano.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 6);

            // Acción del botón Guardar: añadir fila abajo (primera guardada será primera fila)
            btnGuardar.Click += (s, e) =>
            {
                var id = txtId.Text ?? string.Empty;
                var riesgo = txtRiesgo.Text ?? string.Empty;
                var activo = txtBienActivo.Text ?? string.Empty;
                var dano = txtDano.Text ?? string.Empty;

                dgvDefinition.Rows.Add(id, riesgo, activo, dano);
                if (dgvDefinition.Rows.Count > 0)
                    dgvDefinition.FirstDisplayedScrollingRowIndex = Math.Max(0, dgvDefinition.Rows.Count - 1);
            };

            tabDefinition.Controls.Add(container);
        }

        // Construye la pestaña "Análisis" with los seis criterios
        private void InitializeAnalysisTab()
        {
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(140, 20, 140, 20) };

            var title = new Label { Text = "Análisis — Seis Criterios", Font = new Font(Font.FontFamily, 13F, FontStyle.Bold), Dock = DockStyle.Top, Height = 34, TextAlign = ContentAlignment.MiddleCenter };

            var table = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 3, RowCount = 6, AutoSize = true, CellBorderStyle = TableLayoutPanelCellBorderStyle.None, Padding = new Padding(0, 12, 0, 0) };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));

            void AddCriterionRow(int rowIndex, string labelText, ComboBox comboBox, EventHandler onInfoClick, string[] items)
            {
                var lbl = new Label { Text = labelText, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), Margin = new Padding(0, 6, 0, 6) };

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Dock = DockStyle.Fill;
                comboBox.Margin = new Padding(6);
                comboBox.Items.AddRange(items);
                if (comboBox.Items.Count > 0) comboBox.SelectedIndex = comboBox.Items.Count - 1;

                var btnInfo = new Button { Text = "i", Width = 28, Height = 24, Dock = DockStyle.Right, FlatStyle = FlatStyle.Flat, Margin = new Padding(6) };
                btnInfo.Click += onInfoClick;

                table.Controls.Add(lbl, 0, rowIndex);
                table.Controls.Add(comboBox, 1, rowIndex);
                table.Controls.Add(btnInfo, 2, rowIndex);
            }

            var funcionItems = new[] { "5 - Muy Grave", "4 - Grave", "3 - Medianamente Grave", "2 - Levemente Grave", "1 - Muy Levemente Grave" };
            var sustitucionItems = new[] { "5 - Muy Dificilmente", "4 - Dificilmente", "3 - Sin Mucha Dificultad", "2 - Facilmente", "1 - Muy Facilmente" };
            var profundidadItems = new[] { "5 - Perturbaciones Muy Graves", "4 - Perturbaciones Graves", "3 - Perturbaciones Limitadas", "2 - Perturbaciones Leves", "1 - Perturbaciones Muy Leves" };
            var extensionItems = new[] { "5 - De Carácter Internacional", "4 - De Carácter Nacional", "3 - De Carácter Regional", "2 - De Carácter Local", "1 - De Carácter Individual" };
            var agresionItems = new[] { "5 - Muy Alta", "4 - Alta", "3 - Normal", "2 - Baja", "1 - Muy Baja" };
            var vulnerabilidadItems = agresionItems.ToArray();

            cmbFuncion = new ComboBox();
            cmbSustitucion = new ComboBox();
            cmbProfundidad = new ComboBox();
            cmbExtension = new ComboBox();
            cmbAgresion = new ComboBox();
            cmbVulnerabilidad = new ComboBox();

            AddCriterionRow(0, "F - Función (consecuencias)", cmbFuncion, (s, e) => MessageBox.Show("Damos un valor a las concecuencias si este riego se materializara.", "F - Criterio de función", MessageBoxButtons.OK, MessageBoxIcon.Information), funcionItems);
            AddCriterionRow(1, "S - Sustitución", cmbSustitucion, (s, e) => MessageBox.Show("Valoramos si los bienes pueden ser sustituidos o remplazados y la dificultad que supone su reemplazo.", "S - Sustitución", MessageBoxButtons.OK, MessageBoxIcon.Information), sustitucionItems);
            AddCriterionRow(2, "P - Profundidad", cmbProfundidad, (s, e) => MessageBox.Show("Valoramos la perturbacion y efectos psicologicos que producira.", "P - Profundidad", MessageBoxButtons.OK, MessageBoxIcon.Information), profundidadItems);
            AddCriterionRow(3, "E - Extensión", cmbExtension, (s, e) => MessageBox.Show("Aqui valoramos el Alcance de los daños.", "E - Extensión", MessageBoxButtons.OK, MessageBoxIcon.Information), extensionItems);
            AddCriterionRow(4, "A - Agresión (probabilidad)", cmbAgresion, (s, e) => MessageBox.Show("Aqui pondremos la probabilidad de que el riego se materialice.", "A - Agresión", MessageBoxButtons.OK, MessageBoxIcon.Information), agresionItems);
            AddCriterionRow(5, "V - Vulnerabilidad", cmbVulnerabilidad, (s, e) => MessageBox.Show("Aqui pondremos la probabilidad de que se produzcan perdidas o Daños.", "V - Vulnerabilidad", MessageBoxButtons.OK, MessageBoxIcon.Information), vulnerabilidadItems);

            cmbFuncion.SelectedIndexChanged += (s, e) => currentRiesgo.Funcion = ParseSelectedValue(cmbFuncion);
            cmbSustitucion.SelectedIndexChanged += (s, e) => currentRiesgo.Sustitucion = ParseSelectedValue(cmbSustitucion);
            cmbProfundidad.SelectedIndexChanged += (s, e) => currentRiesgo.Profundidad = ParseSelectedValue(cmbProfundidad);
            cmbExtension.SelectedIndexChanged += (s, e) => currentRiesgo.Extension = ParseSelectedValue(cmbExtension);
            cmbAgresion.SelectedIndexChanged += (s, e) => currentRiesgo.Agresion = ParseSelectedValue(cmbAgresion);
            cmbVulnerabilidad.SelectedIndexChanged += (s, e) => currentRiesgo.Vulnerabilidad = ParseSelectedValue(cmbVulnerabilidad);

            container.Controls.Add(title);
            container.Controls.Add(table);

            tabAnalysis.Controls.Add(container);

            // Inicializar valores por defecto en modelo
            currentRiesgo.Funcion = ParseSelectedValue(cmbFuncion);
            currentRiesgo.Sustitucion = ParseSelectedValue(cmbSustitucion);
            currentRiesgo.Profundidad = ParseSelectedValue(cmbProfundidad);
            currentRiesgo.Extension = ParseSelectedValue(cmbExtension);
            currentRiesgo.Agresion = ParseSelectedValue(cmbAgresion);
            currentRiesgo.Vulnerabilidad = ParseSelectedValue(cmbVulnerabilidad);
        }

        // Crea pestañas simples con texto de marcador
        private TabPage CreateTab(string title, string placeholderText)
        {
            var tab = new TabPage(title);
            var header = new Label { Text = title, Dock = DockStyle.Top, Height = 36, TextAlign = ContentAlignment.MiddleLeft, Font = new Font(Font.FontFamily, 12F, FontStyle.Bold), Padding = new Padding(8, 6, 0, 0) };
            var placeholder = new Label { Text = placeholderText, Dock = DockStyle.Fill, TextAlign = ContentAlignment.TopLeft, Padding = new Padding(12), ForeColor = Color.DimGray };
            tab.Controls.Add(placeholder);
            tab.Controls.Add(header);
            return tab;
        }

        // Extrae el número entero del item seleccionado (p. ej. "5 - Muy Grave" -> 5)
        private static int ParseSelectedValue(ComboBox cmb)
        {
            if (cmb?.SelectedItem is string s)
            {
                var part = s.Split('-', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (int.TryParse(part?.Trim(), out int v)) return v;
            }
            return 1;
        }

        // Calcula la altura de una línea de texto en el font actual
        private static int GetSingleLineHeight()
        {
            return TextRenderer.MeasureText("A", SystemFonts.MessageBoxFont).Height + 6;
        }

        // Ajusta la altura de un TextBox multilínea según su contenido (máximo de líneas)
        private void AdjustHeight(TextBox tb, int maxLines = 5)
        {
            if (tb == null) return;
            var minHeight = GetSingleLineHeight();
            var maxHeight = minHeight * maxLines + 6;
            var measureSize = TextRenderer.MeasureText(tb.Text + " ", tb.Font, new Size(Math.Max(1, tb.ClientSize.Width - 4), int.MaxValue), TextFormatFlags.WordBreak);
            var desiredHeight = Math.Max(minHeight, measureSize.Height + 6);
            if (desiredHeight >= maxHeight)
            {
                tb.Height = maxHeight;
                tb.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                tb.Height = desiredHeight;
                tb.ScrollBars = ScrollBars.None;
            }
        }
    }
}