using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using MoslerRiskApp.Models;
using LiteDB; // Base de datos NoSQL ligera (sin servidor)

namespace MoslerRiskApp
{
    /// <summary>
    /// Formulario Principal de la aplicación Mosler RiskMan.
    /// -------------------------------------------------------------------------
    /// Esta clase gestiona la interfaz de usuario (UI) generada programáticamente
    /// y la lógica de negocio del Método Mosler (Definición, Análisis, Evaluación, Clasificación).
    /// </summary>
    public class MainForm : Form
    {
        #region 1. Configuración y Estado

        // Ruta del archivo de base de datos (se crea automáticamente en la carpeta del ejecutable)
        private const string DbPath = "Filename=MoslerData.db;Connection=Shared";

        // ID del riesgo que se está editando actualmente.
        // null = El usuario está creando un riesgo nuevo (Modo Creación).
        // int  = El usuario está modificando un riesgo existente (Modo Edición).
        private int? _currentRiskId = null;

        #endregion

        #region 2. Controles de Interfaz (UI Variables)

        // Contenedor principal de pestañas
        private TabControl tabControl;
        private TabPage tabDefinition, tabAnalysis, tabEvaluation, tabClassification;

        // Paneles y Layouts (Referencias para el redimensionado responsivo)
        private Panel containerDefinition;
        private Panel containerAnalysis;
        private TableLayoutPanel defContentLayout;
        private TableLayoutPanel analCriteriaLayout;

        // --- FASE 1: Definición (Inputs) ---
        private TextBox txtBienActivo;  // El activo a proteger
        private TextBox txtRiesgo;      // La amenaza
        private TextBox txtDano;        // Descripción del daño potencial
        private Label lblEstadoId;      // Indicador visual (NUEVO vs EDITANDO)
        private Button btnEliminar;     // Botón de borrado
        private TextBox txtSearch;      // Buscador en tiempo real
        private DateTimePicker dtpFecha;
        private DataGridView dgvDefinition;

        // --- FASE 2: Análisis (Los 6 Criterios de Mosler) ---
        private ComboBox cmbFuncion, cmbSustitucion, cmbProfundidad, cmbExtension, cmbAgresion, cmbVulnerabilidad;
        private DataGridView dgvAnalysis;

        // --- FASE 3 & 4: Resultados (Tablas de Cálculo) ---
        private DataGridView dgvEvaluation;     // Muestra cálculos intermedios (I, D, C, PB, ER)
        private DataGridView dgvClassification; // Muestra la clase de riesgo y el semáforo de colores

        #endregion

        /// <summary>
        /// Constructor: Punto de entrada de la ventana.
        /// </summary>
        public MainForm()
        {
            InitializeComponents();
        }

        /// <summary>
        /// Configuración inicial de la ventana, pestañas y eventos globales.
        /// </summary>
        private void InitializeComponents()
        {
            // Configuración base de la ventana
            Text = "Mosler RiskMan - Gestión Profesional de Riesgos";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1500, 700);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // SuspendLayout: Optimización para evitar parpadeos mientras se añaden controles
            this.SuspendLayout();

            // Sistema de Pestañas
            tabControl = new TabControl { Dock = DockStyle.Fill, Padding = new Point(12, 8) };

            tabDefinition = new TabPage("Definición");
            tabAnalysis = new TabPage("Análisis");
            tabEvaluation = new TabPage("Evaluación");
            tabClassification = new TabPage("Clasificación");

            tabControl.TabPages.AddRange(new[] { tabDefinition, tabAnalysis, tabEvaluation, tabClassification });
            Controls.Add(tabControl);

            // --- EVENTO CRÍTICO: Cambio de Pestaña ---
            // Recalcula los datos matemáticos solo cuando el usuario entra a las pestañas de resultados.
            // Esto ahorra procesamiento innecesario.
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                var sel = tabControl.SelectedTab;
                if (sel == tabEvaluation) UpdateEvaluationGrid();
                else if (sel == tabClassification) UpdateClassificationGrid();

                AdjustWindowToContent(); // Ajuste responsivo de altura
            };

            // Inicialización modular (Separation of Concerns)
            InitializeDefinitionTab();
            InitializeAnalysisTab();
            InitializeEvaluationTab();
            InitializeClassificationTab();

            // Pie de página (Créditos)
            var footerPanel = new Panel { Dock = DockStyle.Bottom, Height = 26, Padding = new Padding(8, 2, 8, 2), BackColor = Color.WhiteSmoke };
            var lblAutorFooter = new Label
            {
                Text = "Autor: Rafi Junior Almanzar",
                AutoSize = false,
                Width = 220,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.DimGray,
                Font = new Font(Font.FontFamily, 8F, FontStyle.Regular)
            };
            footerPanel.Controls.Add(lblAutorFooter);
            Controls.Add(footerPanel);

            this.ResumeLayout(false);

            // Carga inicial de datos al abrir la app
            this.Load += (s, e) =>
            {
                LoadDataFromDb();        // Carga desde LiteDB
                ClearForm();             // Prepara formulario limpio
                AdjustWindowToContent(); // Ajusta tamaño inicial
            };
        }

        #region 3. Lógica de Base de Datos (LiteDB)

        /// <summary>
        /// Conecta a LiteDB, recupera los riesgos y refresca las tablas visuales.
        /// </summary>
        /// <param name="searchTerm">Texto opcional para filtrar por Nombre o ID.</param>
        private void LoadDataFromDb(string searchTerm = "")
        {
            try
            {
                // 'using' asegura que el archivo .db se cierre y libere memoria inmediatamente
                using (var db = new LiteDatabase(DbPath))
                {
                    var col = db.GetCollection<Riesgo>("riesgos");
                    List<Riesgo> lista;

                    // Lógica de filtrado (Buscador)
                    if (string.IsNullOrWhiteSpace(searchTerm))
                    {
                        lista = col.FindAll().OrderBy(x => x.Id).ToList();
                    }
                    else
                    {
                        searchTerm = searchTerm.ToLower();
                        lista = col.Find(x =>
                            x.Nombre.ToLower().Contains(searchTerm) ||
                            x.Id.ToString().Contains(searchTerm)
                        ).OrderBy(x => x.Id).ToList();
                    }

                    // Limpiar tablas antes de repoblar
                    dgvDefinition.Rows.Clear();
                    dgvAnalysis.Rows.Clear();

                    foreach (var r in lista)
                    {
                        // Llenamos Grid de Definición
                        dgvDefinition.Rows.Add(r.Id, r.Nombre, r.Activo, r.Dano);
                        // Llenamos Grid de Análisis (Valores numéricos guardados)
                        dgvAnalysis.Rows.Add(r.Id, r.Nombre, r.F, r.S, r.P, r.E, r.A, r.V);
                    }
                }

                // Actualizar vistas derivadas si son visibles (Optimización UI)
                if (tabControl.SelectedTab == tabEvaluation) UpdateEvaluationGrid();
                if (tabControl.SelectedTab == tabClassification) UpdateClassificationGrid();

                // Desactivar botón eliminar al recargar lista completa
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = false;
                    btnEliminar.BackColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la base de datos:\n" + ex.Message, "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Guarda un nuevo riesgo o actualiza uno existente (Fase 1).
        /// </summary>
        private void SaveDefinitionToDb()
        {
            try
            {
                using (var db = new LiteDatabase(DbPath))
                {
                    var col = db.GetCollection<Riesgo>("riesgos");

                    if (_currentRiskId == null)
                    {
                        // --- MODO CREACIÓN ---
                        var nuevo = new Riesgo
                        {
                            Nombre = txtRiesgo.Text,
                            Activo = txtBienActivo.Text,
                            Dano = txtDano.Text,
                            Fecha = dtpFecha.Value,
                            // Valores iniciales seguros (1 = Mínimo impacto)
                            F = 1,
                            S = 1,
                            P = 1,
                            E = 1,
                            A = 1,
                            V = 1
                        };
                        col.Insert(nuevo); // LiteDB genera el ID
                        _currentRiskId = nuevo.Id;
                        MessageBox.Show($"¡Riesgo guardado exitosamente con ID: {nuevo.Id}!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // --- MODO EDICIÓN ---
                        var existente = col.FindById(_currentRiskId.Value);
                        if (existente != null)
                        {
                            if (MessageBox.Show($"¿Desea actualizar los datos del ID {_currentRiskId}?", "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                existente.Nombre = txtRiesgo.Text;
                                existente.Activo = txtBienActivo.Text;
                                existente.Dano = txtDano.Text;
                                existente.Fecha = dtpFecha.Value;
                                col.Update(existente);
                                MessageBox.Show("Registro actualizado correctamente.", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                // Tareas post-guardado
                LoadDataFromDb();
                txtSearch.Clear();
                UpdateStatusLabel();
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar: " + ex.Message); }
        }

        /// <summary>
        /// Guarda los valores numéricos del análisis Mosler (Fase 2).
        /// </summary>
        private void SaveAnalysisToDb()
        {
            if (_currentRiskId == null)
            {
                MessageBox.Show("Selecciona un riesgo o crea uno nuevo en 'Definición' primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new LiteDatabase(DbPath))
                {
                    var col = db.GetCollection<Riesgo>("riesgos");
                    var riesgo = col.FindById(_currentRiskId.Value);

                    if (riesgo != null)
                    {
                        if (MessageBox.Show($"¿Confirmar valores de análisis para el ID {_currentRiskId}?", "Confirmar Análisis", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Parseamos los ComboBoxes (ej: "5 - Grave" -> 5)
                            riesgo.F = GetComboVal(cmbFuncion);
                            riesgo.S = GetComboVal(cmbSustitucion);
                            riesgo.P = GetComboVal(cmbProfundidad);
                            riesgo.E = GetComboVal(cmbExtension);
                            riesgo.A = GetComboVal(cmbAgresion);
                            riesgo.V = GetComboVal(cmbVulnerabilidad);

                            col.Update(riesgo);
                        }
                        else return; // Cancelado
                    }
                }
                LoadDataFromDb();
                MessageBox.Show("Análisis actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar análisis: " + ex.Message); }
        }

        /// <summary>
        /// Elimina el riesgo actualmente seleccionado de forma permanente.
        /// </summary>
        private void DeleteEntry()
        {
            if (_currentRiskId == null) return;

            if (MessageBox.Show($"¿Estás seguro de ELIMINAR el ID: {_currentRiskId}?\nEsta acción es irreversible.",
                "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = new LiteDatabase(DbPath))
                    {
                        var col = db.GetCollection<Riesgo>("riesgos");
                        col.Delete(_currentRiskId.Value);
                    }
                    ClearForm(); // Resetear formulario
                    txtSearch.Clear();
                    LoadDataFromDb();
                    MessageBox.Show("Entrada eliminada correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message); }
            }
        }

        #endregion

        #region 4. Helpers y Métodos Auxiliares

        // Extrae el número entero de un item de ComboBox (ej: "5 - Muy Grave" devuelve 5)
        private int GetComboVal(ComboBox c)
        {
            if (c.SelectedItem is string s)
            {
                var p = s.Split('-')[0].Trim();
                return int.TryParse(p, out int v) ? v : 1;
            }
            return 1;
        }

        // Selecciona el item del ComboBox que corresponde al valor numérico guardado
        private void SetComboVal(ComboBox c, int val)
        {
            foreach (var item in c.Items)
            {
                if (item.ToString().StartsWith(val.ToString()))
                {
                    c.SelectedItem = item;
                    break;
                }
            }
        }

        // Limpia todos los campos para permitir una nueva entrada
        private void ClearForm()
        {
            _currentRiskId = null; // null = Modo Nuevo
            txtRiesgo.Clear();
            txtBienActivo.Clear();
            txtDano.Clear();
            dtpFecha.Value = DateTime.Now;

            // Resetear combos al valor predeterminado (el último item, que suele ser 1)
            cmbFuncion.SelectedIndex = cmbFuncion.Items.Count - 1;
            cmbSustitucion.SelectedIndex = cmbSustitucion.Items.Count - 1;
            cmbProfundidad.SelectedIndex = cmbProfundidad.Items.Count - 1;
            cmbExtension.SelectedIndex = cmbExtension.Items.Count - 1;
            cmbAgresion.SelectedIndex = cmbAgresion.Items.Count - 1;
            cmbVulnerabilidad.SelectedIndex = cmbVulnerabilidad.Items.Count - 1;

            dgvDefinition.ClearSelection();
            UpdateStatusLabel();
        }

        // Carga los datos de un riesgo seleccionado en la tabla hacia los controles de edición
        private void LoadFromGrid(string idStr)
        {
            if (!int.TryParse(idStr, out int id)) return;

            try
            {
                using (var db = new LiteDatabase(DbPath))
                {
                    var col = db.GetCollection<Riesgo>("riesgos");
                    var r = col.FindById(id);
                    if (r != null)
                    {
                        _currentRiskId = r.Id; // Cambiamos a modo "Edición"

                        // Datos Texto
                        txtRiesgo.Text = r.Nombre;
                        txtBienActivo.Text = r.Activo;
                        txtDano.Text = r.Dano;
                        dtpFecha.Value = r.Fecha;

                        // Datos Numéricos (Combos)
                        SetComboVal(cmbFuncion, r.F);
                        SetComboVal(cmbSustitucion, r.S);
                        SetComboVal(cmbProfundidad, r.P);
                        SetComboVal(cmbExtension, r.E);
                        SetComboVal(cmbAgresion, r.A);
                        SetComboVal(cmbVulnerabilidad, r.V);

                        UpdateStatusLabel();
                    }
                }
            }
            catch { }
        }

        // Actualiza el indicador visual de estado (NUEVO / EDITANDO)
        private void UpdateStatusLabel()
        {
            if (_currentRiskId == null)
            {
                lblEstadoId.Text = "MODO: NUEVO";
                lblEstadoId.ForeColor = Color.Green;
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = false;
                    btnEliminar.BackColor = Color.Gray;
                }
            }
            else
            {
                lblEstadoId.Text = $"EDITANDO ID: {_currentRiskId}";
                lblEstadoId.ForeColor = Color.DarkOrange;
                if (btnEliminar != null)
                {
                    btnEliminar.Enabled = true;
                    btnEliminar.BackColor = Color.FromArgb(231, 76, 60); // Rojo activo
                }
            }
        }

        // Ajusta la altura de la ventana según el contenido de la pestaña activa
        private void AdjustWindowToContent()
        {
            int targetWidth = 1500;
            int targetHeight = 700; // Altura estándar para Evaluación/Clasificación
            int chromeHeight = 70;  // Bordes del sistema operativo

            if (tabControl.SelectedTab == tabDefinition)
            {
                // La pestaña Definición es más compacta
                if (defContentLayout != null) targetHeight = 35 + defContentLayout.Height + 160 + 60 + chromeHeight;
            }
            else if (tabControl.SelectedTab == tabAnalysis)
            {
                // La pestaña Análisis es mediana
                if (analCriteriaLayout != null) targetHeight = 40 + analCriteriaLayout.Height + 42 + 180 + 60 + chromeHeight;
            }

            if (this.ClientSize.Height != targetHeight)
            {
                this.ClientSize = new Size(targetWidth, targetHeight);
                this.CenterToScreen();
            }
        }

        // Auto-altura para TextBoxes multilínea
        private void AdjustHeight(TextBox tb, int maxLines = 5)
        {
            if (tb == null) return;
            var sz = TextRenderer.MeasureText(tb.Text + " ", tb.Font, new Size(Math.Max(1, tb.ClientSize.Width - 4), int.MaxValue), TextFormatFlags.WordBreak);
            int h = Math.Max(30, sz.Height + 6);
            int max = TextRenderer.MeasureText("A", SystemFonts.MessageBoxFont).Height * maxLines + 20;
            tb.Height = h >= max ? max : h;
            tb.ScrollBars = h >= max ? ScrollBars.Vertical : ScrollBars.None;
        }

        // Estilizado moderno para las tablas (DataGridView)
        private void StyleDataGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            // Cabecera Azul Oscuro
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);
            dgv.ColumnHeadersHeight = 40;
            // Celdas
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(214, 234, 248); // Azul claro selección
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Padding = new Padding(4);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249); // Zebra
            dgv.RowTemplate.Height = 30;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }

        #endregion

        #region 5. Inicialización de Componentes (Fases)

        // ---------------------------------------------------------
        // FASE 1: DEFINICIÓN (Layout y Inputs)
        // ---------------------------------------------------------
        private void InitializeDefinitionTab()
        {
            containerDefinition = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12, 8, 12, 8) };

            var titleLabel = new Label { Text = "Ficha Técnica - Análisis De Riesgos", Font = new Font("Segoe UI", 12F, FontStyle.Bold), AutoSize = false, Height = 35, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            containerDefinition.Controls.Add(titleLabel);

            // Selector Fecha
            var fechaPanel = new TableLayoutPanel { AutoSize = true, ColumnCount = 2, RowCount = 1 };
            fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); fechaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            var lblFecha = new Label { Text = "Fecha:", AutoSize = true, Padding = new Padding(0, 4, 6, 0), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            dtpFecha = new DateTimePicker { Width = 120, Format = DateTimePickerFormat.Short, Name = "dtpFecha", Anchor = AnchorStyles.Top | AnchorStyles.Right };
            fechaPanel.Controls.Add(lblFecha, 0, 0); fechaPanel.Controls.Add(dtpFecha, 1, 0);

            // Layout Principal (Izquierda inputs, Derecha botones)
            defContentLayout = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 2, RowCount = 1, AutoSize = true, Padding = new Padding(4, 8, 4, 8) };
            defContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); defContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            // Columna Izquierda: Activo y Riesgo
            var leftStack = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, AutoSize = true };
            var lblBien = new Label { Text = "Bien / Activo", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtBienActivo = new TextBox { Name = "txtBienActivo", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = 30, MaxLength = 200, Dock = DockStyle.Top, Margin = new Padding(0, 0, 8, 8) };
            var lblRiesgo = new Label { Text = "Riesgo", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtRiesgo = new TextBox { Name = "txtRiesgo", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = 30, MaxLength = 200, Dock = DockStyle.Top, Margin = new Padding(0, 0, 8, 8) };
            leftStack.Controls.Add(lblBien); leftStack.Controls.Add(txtBienActivo); leftStack.Controls.Add(lblRiesgo); leftStack.Controls.Add(txtRiesgo);

            // Columna Derecha: Daño y Botonera
            var rightPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, AutoSize = true };
            var damageLayout = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 2, RowCount = 1, AutoSize = true };
            damageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); damageLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            var damagePanel = new Panel { Dock = DockStyle.Fill };
            var lblDano = new Label { Text = "Daño", Font = new Font(Font.FontFamily, 9F, FontStyle.Bold), AutoSize = false, Height = 18, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter };
            txtDano = new TextBox { Name = "txtDano", Multiline = true, ScrollBars = ScrollBars.Vertical, Height = 60, MaxLength = 400, Dock = DockStyle.Fill };
            damagePanel.Controls.Add(txtDano); damagePanel.Controls.Add(lblDano);

            var sideStack = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, Dock = DockStyle.Fill, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = false, Padding = new Padding(6), MinimumSize = new Size(140, 0) };
            lblEstadoId = new Label { Text = "MODO: NUEVO", AutoSize = false, Width = 130, Height = 30, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.Green, BorderStyle = BorderStyle.FixedSingle };
            var btnGuardar = new Button { Text = "Guardar", AutoSize = false, Width = 130, Height = 35, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 10, 0, 5), Cursor = Cursors.Hand };
            btnGuardar.FlatAppearance.BorderSize = 0;
            var btnNuevo = new Button { Text = "Nuevo / Limpiar", AutoSize = false, Width = 130, Height = 35, BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 5, 0, 0), Cursor = Cursors.Hand };
            btnNuevo.FlatAppearance.BorderSize = 0;
            sideStack.Controls.Add(lblEstadoId); sideStack.Controls.Add(btnGuardar); sideStack.Controls.Add(btnNuevo);

            damageLayout.Controls.Add(damagePanel, 0, 0); damageLayout.Controls.Add(sideStack, 1, 0);
            rightPanel.Controls.Add(damageLayout); defContentLayout.Controls.Add(leftStack, 0, 0); defContentLayout.Controls.Add(rightPanel, 1, 0);

            // Tabla (Grid)
            dgvDefinition = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, ReadOnly = true };
            StyleDataGrid(dgvDefinition);
            dgvDefinition.Columns.Add("IdCol", "ID"); dgvDefinition.Columns.Add("RiesgoCol", "Riesgo"); dgvDefinition.Columns.Add("ActivoCol", "Activo"); dgvDefinition.Columns.Add("DanoCol", "Daño");
            dgvDefinition.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Panel Inferior: Buscador y Eliminar
            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 160 };
            var bottomTopRow = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(2) };

            btnEliminar = new Button { Text = "Eliminar Entrada", AutoSize = true, Height = 30, Dock = DockStyle.Left, BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false, Cursor = Cursors.Default, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Click += (s, e) => DeleteEntry();

            var searchPanel = new Panel { Dock = DockStyle.Left, Width = 250, Padding = new Padding(10, 2, 0, 0) };
            txtSearch = new TextBox { Width = 200, PlaceholderText = "🔍 Buscar por Nombre o ID...", Font = new Font("Segoe UI", 10F) };
            txtSearch.TextChanged += (s, e) => LoadDataFromDb(txtSearch.Text); // Evento Buscador
            searchPanel.Controls.Add(txtSearch);

            fechaPanel.Dock = DockStyle.Right;
            bottomTopRow.Controls.Add(searchPanel);
            bottomTopRow.Controls.Add(btnEliminar);
            bottomTopRow.Controls.Add(fechaPanel);
            dgvDefinition.Dock = DockStyle.Fill;
            bottomPanel.Controls.Add(dgvDefinition);
            bottomPanel.Controls.Add(bottomTopRow);
            containerDefinition.Controls.Add(defContentLayout); containerDefinition.Controls.Add(bottomPanel);

            // Eventos inputs
            txtBienActivo.TextChanged += (s, e) => { AdjustHeight((TextBox)s, 3); AdjustWindowToContent(); };
            txtRiesgo.TextChanged += (s, e) => { AdjustHeight((TextBox)s, 3); AdjustWindowToContent(); };
            txtDano.TextChanged += (s, e) => { AdjustHeight((TextBox)s, 6); AdjustWindowToContent(); };
            btnGuardar.Click += (s, e) => SaveDefinitionToDb();
            btnNuevo.Click += (s, e) => ClearForm();

            // Selección en tabla
            dgvDefinition.SelectionChanged += (s, e) => {
                if (dgvDefinition.SelectedRows.Count > 0)
                {
                    btnEliminar.Enabled = true; btnEliminar.BackColor = Color.FromArgb(231, 76, 60); btnEliminar.Cursor = Cursors.Hand;
                    var row = dgvDefinition.SelectedRows[0];
                    if (row.Cells[0].Value != null) LoadFromGrid(row.Cells[0].Value.ToString());
                }
                else
                {
                    btnEliminar.Enabled = false; btnEliminar.BackColor = Color.Gray; btnEliminar.Cursor = Cursors.Default;
                    ClearForm();
                }
            };

            tabDefinition.Controls.Add(containerDefinition);
        }

        // ---------------------------------------------------------
        // FASE 2: ANÁLISIS (Criterios Mosler)
        // ---------------------------------------------------------
        private void InitializeAnalysisTab()
        {
            containerAnalysis = new Panel { Dock = DockStyle.Fill, Padding = new Padding(40, 12, 40, 12) };
            var title = new Label { Text = "Análisis — Seis Criterios", Font = new Font("Segoe UI", 13F, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };

            analCriteriaLayout = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 3, RowCount = 2, AutoSize = true, Padding = new Padding(6) };
            analCriteriaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            analCriteriaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            analCriteriaLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            // Listas de Opciones
            var funcionItems = new[] { "5 - Muy Grave", "4 - Grave", "3 - Medianamente Grave", "2 - Levemente Grave", "1 - Muy Levemente Grave" };
            var sustitucionItems = new[] { "5 - Muy Dificilmente", "4 - Dificilmente", "3 - Sin Mucha Dificultad", "2 - Facilmente", "1 - Muy Facilmente" };
            var profundidadItems = new[] { "5 - Perturbaciones Muy Graves", "4 - Perturbaciones Graves", "3 - Perturbaciones Limitadas", "2 - Perturbaciones Leves", "1 - Perturbaciones Muy Leves" };
            var extensionItems = new[] { "5 - De Carácter Internacional", "4 - De Carácter Nacional", "3 - De Carácter Regional", "2 - De Carácter Local", "1 - De Carácter Individual" };
            var agresionItems = new[] { "5 - Muy Alta", "4 - Alta", "3 - Normal", "2 - Baja", "1 - Muy Baja" };
            var vulnerabilidadItems = agresionItems.ToArray();

            // Factory de ComboBoxes
            ComboBox CreateCriterion(string titleText, string[] items, EventHandler onInfo, out Panel panel)
            {
                panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
                var lbl = new Label { Text = titleText, Dock = DockStyle.Top, Height = 22, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
                var cmb = new ComboBox { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList, Height = 28 };
                cmb.Items.AddRange(items); if (cmb.Items.Count > 0) cmb.SelectedIndex = cmb.Items.Count - 1;
                var infoBtn = new Button { Text = "?", Width = 28, Height = 28, Dock = DockStyle.Top, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 6, 0, 0), TextAlign = ContentAlignment.MiddleCenter, UseCompatibleTextRendering = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
                infoBtn.Click += onInfo;
                panel.Controls.Add(infoBtn); panel.Controls.Add(cmb); panel.Controls.Add(lbl); lbl.BringToFront(); cmb.BringToFront(); infoBtn.BringToFront();
                return cmb;
            }

            Panel p1, p2, p3, p4, p5, p6;
            cmbFuncion = CreateCriterion("F - Función (consecuencias)", funcionItems, (s, e) => MessageBox.Show("Consecuencias si el riesgo se materializa.", "F", MessageBoxButtons.OK, MessageBoxIcon.Information), out p1);
            cmbSustitucion = CreateCriterion("S - Sustitución", sustitucionItems, (s, e) => MessageBox.Show("Facilidad de sustituir los bienes.", "S", MessageBoxButtons.OK, MessageBoxIcon.Information), out p2);
            cmbProfundidad = CreateCriterion("P - Profundidad", profundidadItems, (s, e) => MessageBox.Show("Perturbación y efectos psicológicos.", "P", MessageBoxButtons.OK, MessageBoxIcon.Information), out p3);
            cmbExtension = CreateCriterion("E - Extensión", extensionItems, (s, e) => MessageBox.Show("Alcance geográfico.", "E", MessageBoxButtons.OK, MessageBoxIcon.Information), out p4);
            cmbAgresion = CreateCriterion("A - Agresión (probabilidad)", agresionItems, (s, e) => MessageBox.Show("Probabilidad de materialización.", "A", MessageBoxButtons.OK, MessageBoxIcon.Information), out p5);
            cmbVulnerabilidad = CreateCriterion("V - Vulnerabilidad", vulnerabilidadItems, (s, e) => MessageBox.Show("Probabilidad de daños.", "V", MessageBoxButtons.OK, MessageBoxIcon.Information), out p6);

            analCriteriaLayout.Controls.Add(p1, 0, 0); analCriteriaLayout.Controls.Add(p2, 1, 0); analCriteriaLayout.Controls.Add(p3, 2, 0);
            analCriteriaLayout.Controls.Add(p4, 0, 1); analCriteriaLayout.Controls.Add(p5, 1, 1); analCriteriaLayout.Controls.Add(p6, 2, 1);

            // Tabla inferior
            dgvAnalysis = new DataGridView { Dock = DockStyle.Bottom, Height = 180, AllowUserToAddRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, ReadOnly = true };
            StyleDataGrid(dgvAnalysis);
            dgvAnalysis.Columns.Add("IdCol", "ID"); dgvAnalysis.Columns.Add("RiesgoCol", "Riesgo");
            dgvAnalysis.Columns.Add("FCol", "F"); dgvAnalysis.Columns.Add("SCol", "S"); dgvAnalysis.Columns.Add("PCol", "P");
            dgvAnalysis.Columns.Add("ECol", "E"); dgvAnalysis.Columns.Add("ACol", "A"); dgvAnalysis.Columns.Add("VCol", "V");
            dgvAnalysis.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 2; i < 8; i++) dgvAnalysis.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var btnGuardarAnalisis = new Button { Text = "Guardar Análisis", AutoSize = true, Dock = DockStyle.Top, Margin = new Padding(6), Height = 30, BackColor = Color.LightGray };
            btnGuardarAnalisis.Click += (s, e) => SaveAnalysisToDb();

            dgvAnalysis.CellClick += (s, e) => {
                if (e.RowIndex >= 0) LoadFromGrid(dgvAnalysis.Rows[e.RowIndex].Cells[0].Value?.ToString());
            };

            containerAnalysis.Controls.Add(title); containerAnalysis.Controls.Add(analCriteriaLayout); containerAnalysis.Controls.Add(btnGuardarAnalisis); containerAnalysis.Controls.Add(dgvAnalysis);
            tabAnalysis.Controls.Add(containerAnalysis);
        }

        // ---------------------------------------------------------
        // FASE 3: EVALUACIÓN (Cálculos de Fórmulas)
        // ---------------------------------------------------------
        private void InitializeEvaluationTab()
        {
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            var title = new Label { Text = "EVALUACIÓN DEL RIESGO", Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };

            // Encabezado de Ayuda (Fórmulas)
            float wId = 10f, wRiesgo = 30f, wMetrics = 12f;
            var headerPanel = new TableLayoutPanel { Dock = DockStyle.Top, Height = 50, ColumnCount = 7, RowCount = 1, Padding = new Padding(0, 0, 0, 5) };
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, wId)); headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, wRiesgo));
            for (int k = 0; k < 5; k++) headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, wMetrics));

            void AddHeaderButton(int colIndex, string text, string tooltipTitle, string tooltipDesc)
            {
                var btn = new Button { Text = text, Width = 32, Height = 32, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand, Anchor = AnchorStyles.None, UseCompatibleTextRendering = true };
                btn.FlatAppearance.BorderSize = 0; GraphicsPath gp = new GraphicsPath(); gp.AddEllipse(0, 0, 32, 32); btn.Region = new Region(gp);
                btn.Click += (s, e) => MessageBox.Show(tooltipDesc, tooltipTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                headerPanel.Controls.Add(btn, colIndex, 0);
            }
            headerPanel.Controls.Add(new Label { Text = "Fórmula \u2794", AutoSize = true, ForeColor = Color.Gray, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 9F, FontStyle.Italic) }, 1, 0);
            AddHeaderButton(2, "I", "Importancia", "I = F x S");
            AddHeaderButton(3, "D", "Daños", "D = P x E");
            AddHeaderButton(4, "C", "Carácter", "C = I + D");
            AddHeaderButton(5, "PB", "Probabilidad", "PB = A x V");
            AddHeaderButton(6, "ER", "Riesgo Estimado", "ER = C x PB");

            dgvEvaluation = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            StyleDataGrid(dgvEvaluation);
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCol", HeaderText = "ID", FillWeight = wId });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "RiesgoCol", HeaderText = "Riesgo", FillWeight = wRiesgo });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "ICol", HeaderText = "I", FillWeight = wMetrics });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "DCol", HeaderText = "D", FillWeight = wMetrics });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "CCol", HeaderText = "C", FillWeight = wMetrics });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "PBCol", HeaderText = "PB", FillWeight = wMetrics });
            dgvEvaluation.Columns.Add(new DataGridViewTextBoxColumn { Name = "ERCol", HeaderText = "ER", FillWeight = wMetrics });

            dgvEvaluation.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 2; i < 7; i++) dgvEvaluation.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            container.Controls.Add(dgvEvaluation); container.Controls.Add(headerPanel); container.Controls.Add(title); tabEvaluation.Controls.Add(container);
        }

        // ---------------------------------------------------------
        // FASE 4: CLASIFICACIÓN (Tabla )
        // ---------------------------------------------------------
        private void InitializeClassificationTab()
        {
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            var title = new Label { Text = "CÁLCULO Y CLASIFICACIÓN DEL RIESGO", Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.FromArgb(64, 64, 64), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };

            var topPanel = new Panel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(0, 0, 0, 10) };
            var btnInfo = new Button { Text = "Ver Tabla de Clasificación de Riesgos", AutoSize = true, BackColor = Color.FromArgb(52, 73, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Dock = DockStyle.Left, Cursor = Cursors.Hand };
            btnInfo.FlatAppearance.BorderSize = 0;
            btnInfo.Click += (s, e) => { string msg = "VALOR ER DEL RIESGO  |  CLASE DE RIESGO\n----------------------------------------------------\n2 - 250\t\t|  Muy Pequeño\n251 - 500\t\t|  Pequeño\n501 - 750\t\t|  Normal\n751 - 1.000\t|  Grande\n1.001 - 1.250\t|  Elevado"; MessageBox.Show(msg, "Referencia", MessageBoxButtons.OK, MessageBoxIcon.Information); };
            topPanel.Controls.Add(btnInfo);

            // Tabla de Clasificación (Ocupa todo el espacio restante)
            dgvClassification = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            StyleDataGrid(dgvClassification);
            dgvClassification.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", FillWeight = 10 });
            dgvClassification.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Riesgo", FillWeight = 30 });
            dgvClassification.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Valor ER", FillWeight = 15 });
            dgvClassification.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Clase de Riesgo", FillWeight = 25 });

            dgvClassification.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassification.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassification.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassification.Columns[3].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Añadir en orden: Título -> PanelSuperior -> Tabla
            container.Controls.Add(dgvClassification);
            container.Controls.Add(topPanel);
            container.Controls.Add(title);
            dgvClassification.BringToFront(); // Asegurar que la tabla esté al frente bajo el panel

            tabClassification.Controls.Add(container);
        }

        #endregion

        #region 6. Lógica de Cálculo (Método Mosler)

        // Calcula: I, D, C, PB, ER y llena la tabla de Evaluación
        private void UpdateEvaluationGrid()
        {
            if (dgvEvaluation == null) return;
            dgvEvaluation.Rows.Clear();
            foreach (DataGridViewRow row in dgvAnalysis.Rows)
            {
                if (row.IsNewRow) continue;
                var id = row.Cells[0].Value?.ToString();
                var nombre = row.Cells[1].Value?.ToString();

                int Parse(int idx) => int.TryParse(row.Cells[idx].Value?.ToString(), out int v) ? v : 0;
                int F = Parse(2), S = Parse(3), P = Parse(4), E = Parse(5), A = Parse(6), V = Parse(7);

                // Fórmulas
                int I = F * S;  // Importancia
                int D = P * E;  // Daños
                int C = I + D;  // Carácter
                int PB = A * V; // Probabilidad
                int ER = C * PB; // Riesgo Estimado

                dgvEvaluation.Rows.Add(id, nombre, I, D, C, PB, ER);
            }
        }

        // Calcula la clase del riesgo
        private void UpdateClassificationGrid()
        {
            if (dgvClassification == null) return;
            dgvClassification.Rows.Clear();

            foreach (DataGridViewRow row in dgvAnalysis.Rows)
            {
                if (row.IsNewRow) continue;
                var id = row.Cells[0].Value?.ToString();
                var nombre = row.Cells[1].Value?.ToString();
                int Parse(int idx) => int.TryParse(row.Cells[idx].Value?.ToString(), out int v) ? v : 0;

                // Recalcular ER
                int F = Parse(2), S = Parse(3), P = Parse(4), E = Parse(5), A = Parse(6), V = Parse(7);
                int ER = ((F * S) + (P * E)) * (A * V);

                // Determinar Clase y Color
                string clase = ""; Color back = Color.White; Color fore = Color.Black;

                if (ER <= 250) { clase = "Muy Pequeño"; back = Color.FromArgb(214, 234, 248); }
                else if (ER <= 500) { clase = "Pequeño"; back = Color.FromArgb(169, 223, 191); }
                else if (ER <= 750) { clase = "Normal"; back = Color.FromArgb(249, 231, 159); }
                else if (ER <= 1000) { clase = "Grande"; back = Color.FromArgb(245, 176, 65); }
                else { clase = "Elevado"; back = Color.FromArgb(192, 57, 43); fore = Color.White; }

                int idx = dgvClassification.Rows.Add(id, nombre, ER, clase);
                var cell = dgvClassification.Rows[idx].Cells[3];
                cell.Style.BackColor = back; cell.Style.ForeColor = fore; cell.Style.SelectionBackColor = back; cell.Style.SelectionForeColor = fore;
            }
        }

        #endregion
    }
}