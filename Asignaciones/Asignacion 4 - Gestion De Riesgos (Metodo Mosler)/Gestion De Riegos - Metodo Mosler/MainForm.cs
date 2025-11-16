using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoslerRiskApp.Models;

namespace MoslerRiskApp
{
    public class MainForm : Form
    {
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

        // Controles de análisis
        private ComboBox cmbFuncion;
        private ComboBox cmbSustitucion;
        private ComboBox cmbProfundidad;
        private ComboBox cmbExtension;
        private ComboBox cmbAgresion;
        private ComboBox cmbVulnerabilidad;

        // Modelo actual
        private Riesgo currentRiesgo = new Riesgo();

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form: inicio más ancho
            Text = "Mosler RiskMan";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1150, 450); // ancho aumentado
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(12, 8)
            };

            // Pestañas
            tabDefinition = new TabPage("Definición");
            InitializeDefinitionTab();

            tabAnalysis = new TabPage("Análisis");
            InitializeAnalysisTab();

            tabEvaluation = CreateTab("Evaluación", "Aquí irá la interfaz para evaluar el riesgo.");
            tabClassification = CreateTab("Clasificación", "Aquí irá la interfaz para clasificar el riesgo.");

            tabControl.TabPages.AddRange(new[] { tabDefinition, tabAnalysis, tabEvaluation, tabClassification });

            Controls.Add(tabControl);

            // Panel inferior con autor
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 22,
                Padding = new Padding(8, 2, 8, 2)
            };

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

            bottomPanel.Controls.Add(lblAutor);
            Controls.Add(bottomPanel);
        }

        private void InitializeDefinitionTab()
        {
            var container = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(80, 12, 80, 12)
            };

            var titleLabel = new Label
            {
                Text = "Ficha Tecnica Analisis De Riesgos",
                Font = new Font(Font.FontFamily, 14F, FontStyle.Bold),
                AutoSize = false,
                Height = 36,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var topFields = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 12)
            };

            var lblNumero = new Label { Text = "Número de ficha:", AutoSize = true, Padding = new Padding(0, 6, 6, 0) };
            txtNumeroFicha = new TextBox { Width = 120, Name = "txtNumeroFicha" };

            var lblAnalista = new Label { Text = "Nombre del analista:", AutoSize = true, Padding = new Padding(12, 6, 6, 0) };
            txtNombreAnalista = new TextBox { Width = 250, Name = "txtNombreAnalista" };

            var lblFecha = new Label { Text = "Fecha:", AutoSize = true, Padding = new Padding(12, 6, 6, 0) };
            dtpFecha = new DateTimePicker { Width = 140, Format = DateTimePickerFormat.Short, Name = "dtpFecha" };

            topFields.Controls.AddRange(new Control[] { lblNumero, txtNumeroFicha, lblAnalista, txtNombreAnalista, lblFecha, dtpFecha });

            var mainFields = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 8, 0, 0)
            };

            mainFields.RowCount = 6;
            mainFields.RowStyles.Clear();
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lblBien = new Label
            {
                Text = "Bien / Activo",
                Font = new Font(Font.FontFamily, 9F, FontStyle.Bold),
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 10)
            };
            txtBienActivo = new TextBox
            {
                Name = "txtBienActivo",
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.None,
                MinimumSize = new Size(0, GetSingleLineHeight()),
                Height = GetSingleLineHeight(),
                MaxLength = 200,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 12)
            };

            var lblRiesgo = new Label
            {
                Text = "Riesgo",
                Font = new Font(Font.FontFamily, 9F, FontStyle.Bold),
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 10)
            };
            txtRiesgo = new TextBox
            {
                Name = "txtRiesgo",
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.None,
                MinimumSize = new Size(0, GetSingleLineHeight()),
                Height = GetSingleLineHeight(),
                MaxLength = 200,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 12)
            };

            var lblDano = new Label
            {
                Text = "Daño",
                Font = new Font(Font.FontFamily, 9F, FontStyle.Bold),
                AutoSize = false,
                Height = 20,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 10)
            };
            txtDano = new TextBox
            {
                Name = "txtDano",
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.None,
                MinimumSize = new Size(0, GetSingleLineHeight()),
                Height = GetSingleLineHeight(),
                MaxLength = 400,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 12)
            };

            txtBienActivo.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 3);
            txtRiesgo.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 3);
            txtDano.TextChanged += (s, e) => AdjustHeight((TextBox)s, maxLines: 6);

            mainFields.Controls.Add(lblBien, 0, 0);
            mainFields.Controls.Add(txtBienActivo, 0, 1);
            mainFields.Controls.Add(lblRiesgo, 0, 2);
            mainFields.Controls.Add(txtRiesgo, 0, 3);
            mainFields.Controls.Add(lblDano, 0, 4);
            mainFields.Controls.Add(txtDano, 0, 5);

            container.Controls.Add(mainFields);
            container.Controls.Add(topFields);
            container.Controls.Add(titleLabel);

            tabDefinition.Controls.Add(container);
        }

        private void InitializeAnalysisTab()
        {
            var container = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(140, 20, 140, 20)
            };

            var title = new Label
            {
                Text = "Análisis — Seis Criterios",
                Font = new Font(Font.FontFamily, 13F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 34,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                RowCount = 6,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                Padding = new Padding(0, 12, 0, 0)
            };

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));

            void AddCriterionRow(int rowIndex, string labelText, ComboBox comboBox, EventHandler onInfoClick, string[] items)
            {
                var lbl = new Label
                {
                    Text = labelText,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(Font.FontFamily, 9F, FontStyle.Bold),
                    Margin = new Padding(0, 6, 0, 6)
                };

                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Dock = DockStyle.Fill;
                comboBox.Margin = new Padding(6);
                comboBox.Items.AddRange(items);
                if (comboBox.Items.Count > 0) comboBox.SelectedIndex = comboBox.Items.Count - 1;

                var btnInfo = new Button
                {
                    Text = "i",
                    Width = 28,
                    Height = 24,
                    Dock = DockStyle.Right,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(6)
                };
                btnInfo.Click += onInfoClick;

                table.Controls.Add(lbl, 0, rowIndex);
                table.Controls.Add(comboBox, 1, rowIndex);
                table.Controls.Add(btnInfo, 2, rowIndex);
            }

            var funcionItems = new[]
            {
                "5 - Muy Grave",
                "4 - Grave",
                "3 - Medianamente Grave",
                "2 - Levemente Grave",
                "1 - Muy Levemente Grave"
            };

            var sustitucionItems = new[]
            {
                "5 - Muy Dificilmente",
                "4 - Dificilmente",
                "3 - Sin Mucha Dificultad",
                "2 - Facilmente",
                "1 - Muy Facilmente"
            };

            var profundidadItems = new[]
            {
                "5 - Perturbaciones Muy Graves",
                "4 - Perturbaciones Graves",
                "3 - Perturbaciones Limitadas",
                "2 - Perturbaciones Leves",
                "1 - Perturbaciones Muy Leves"
            };

            var extensionItems = new[]
            {
                "5 - De Carácter Internacional",
                "4 - De Carácter Nacional",
                "3 - De Carácter Regional",
                "2 - De Carácter Local",
                "1 - De Carácter Individual"
            };

            var agresionItems = new[]
            {
                "5 - Muy Alta",
                "4 - Alta",
                "3 - Normal",
                "2 - Baja",
                "1 - Muy Baja"
            };

            var vulnerabilidadItems = agresionItems.ToArray();

            cmbFuncion = new ComboBox();
            cmbSustitucion = new ComboBox();
            cmbProfundidad = new ComboBox();
            cmbExtension = new ComboBox();
            cmbAgresion = new ComboBox();
            cmbVulnerabilidad = new ComboBox();

            AddCriterionRow(0, "F - Función (consecuencias)", cmbFuncion, (s, e) =>
            {
                MessageBox.Show(
                    "Damos un valor a la concecuencias si este riego se materializara.",
                    "F - Criterio de función",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, funcionItems);

            AddCriterionRow(1, "S - Sustitución", cmbSustitucion, (s, e) =>
            {
                MessageBox.Show(
                    "Valoramos si los bienes pueden ser sustituidos o remplazados y la dificultad que supone su reemplazo, sustitucion o recambio.",
                    "S - Sustitución",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, sustitucionItems);

            AddCriterionRow(2, "P - Profundidad", cmbProfundidad, (s, e) =>
            {
                MessageBox.Show(
                    "Valoramos la perturbacion y efectos Psicologicos que producira. estimamos el nivel de aceptacion psicologia negativa y la imagen corporativa como marca.",
                    "P - Profundidad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, profundidadItems);

            AddCriterionRow(3, "E - Extensión", cmbExtension, (s, e) =>
            {
                MessageBox.Show(
                    "Aqui valoramos el Alcance de los daños, donde podrian llegar los daños.",
                    "E - Extensión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, extensionItems);

            AddCriterionRow(4, "A - Agresión (probabilidad)", cmbAgresion, (s, e) =>
            {
                MessageBox.Show(
                    "Aqui pondremos la probabilidad de que el riego se materialice, la probabilidad de manifestacion.",
                    "A - Agresión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, agresionItems);

            AddCriterionRow(5, "V - Vulnerabilidad", cmbVulnerabilidad, (s, e) =>
            {
                MessageBox.Show(
                    "Aqui pondremos la probabilidad de que se produzcan perdidas o Daños.",
                    "V - Vulnerabilidad",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }, vulnerabilidadItems);

            cmbFuncion.SelectedIndexChanged += (s, e) => currentRiesgo.Funcion = ParseSelectedValue(cmbFuncion);
            cmbSustitucion.SelectedIndexChanged += (s, e) => currentRiesgo.Sustitucion = ParseSelectedValue(cmbSustitucion);
            cmbProfundidad.SelectedIndexChanged += (s, e) => currentRiesgo.Profundidad = ParseSelectedValue(cmbProfundidad);
            cmbExtension.SelectedIndexChanged += (s, e) => currentRiesgo.Extension = ParseSelectedValue(cmbExtension);
            cmbAgresion.SelectedIndexChanged += (s, e) => currentRiesgo.Agresion = ParseSelectedValue(cmbAgresion);
            cmbVulnerabilidad.SelectedIndexChanged += (s, e) => currentRiesgo.Vulnerabilidad = ParseSelectedValue(cmbVulnerabilidad);

            var btnCalcular = new Button
            {
                Text = "Calcular evaluación",
                AutoSize = true,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 12, 0, 0)
            };
            btnCalcular.Click += (s, e) =>
            {
                currentRiesgo.CalcularEvaluacion();
                MessageBox.Show($"Carácter: {currentRiesgo.Carácter:F2}\nProbabilidad: {currentRiesgo.ProbabilidadEvaluada:F2}\nCuantificación: {currentRiesgo.Cuantificacion:F2}", "Resultado (preliminar)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            container.Controls.Add(title);
            container.Controls.Add(table);
            container.Controls.Add(btnCalcular);

            tabAnalysis.Controls.Add(container);

            // Inicializar valores del modelo con selecciones por defecto
            currentRiesgo.Funcion = ParseSelectedValue(cmbFuncion);
            currentRiesgo.Sustitucion = ParseSelectedValue(cmbSustitucion);
            currentRiesgo.Profundidad = ParseSelectedValue(cmbProfundidad);
            currentRiesgo.Extension = ParseSelectedValue(cmbExtension);
            currentRiesgo.Agresion = ParseSelectedValue(cmbAgresion);
            currentRiesgo.Vulnerabilidad = ParseSelectedValue(cmbVulnerabilidad);
        }

        private static int ParseSelectedValue(ComboBox cmb)
        {
            if (cmb?.SelectedItem is string s)
            {
                var part = s.Split('-', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (int.TryParse(part?.Trim(), out int v)) return v;
            }
            return 1;
        }

        private TabPage CreateTab(string title, string placeholderText)
        {
            var tab = new TabPage(title);

            var header = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(Font.FontFamily, 12F, FontStyle.Bold),
                Padding = new Padding(8, 6, 0, 0)
            };

            var placeholder = new Label
            {
                Text = placeholderText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(12),
                ForeColor = Color.DimGray
            };

            tab.Controls.Add(placeholder);
            tab.Controls.Add(header);

            return tab;
        }

        private static int GetSingleLineHeight()
        {
            return TextRenderer.MeasureText("A", SystemFonts.MessageBoxFont).Height + 6;
        }

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