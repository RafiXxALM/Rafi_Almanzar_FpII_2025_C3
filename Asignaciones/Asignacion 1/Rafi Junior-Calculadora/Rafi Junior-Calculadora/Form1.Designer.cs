
namespace Rafi_Junior_Calculadora
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            txtResultado = new TextBox();
            btnUno = new Button();
            btnDos = new Button();
            btnTres = new Button();
            btnSeis = new Button();
            btnCinco = new Button();
            btnCuatro = new Button();
            btnNueve = new Button();
            btnOcho = new Button();
            btnSiete = new Button();
            btnQuitar = new Button();
            btnBorrarTodo = new Button();
            btnBorrar = new Button();
            btnCuadrado = new Button();
            btnRaizCuadrada = new Button();
            btnMultiplicar = new Button();
            btnDividir = new Button();
            btnRestar = new Button();
            btnSumar = new Button();
            btnResultado = new Button();
            btnComa = new Button();
            btnCero = new Button();
            btnSigno = new Button();
            SuspendLayout();
            // 
            // txtResultado
            // 
            txtResultado.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtResultado.Location = new Point(12, 12);
            txtResultado.MaxLength = 14;
            txtResultado.Multiline = true;
            txtResultado.Name = "txtResultado";
            txtResultado.ReadOnly = true;
            txtResultado.Size = new Size(301, 65);
            txtResultado.TabIndex = 0;
            txtResultado.Text = "0";
            txtResultado.TextAlign = HorizontalAlignment.Right;
            // 
            // btnUno
            // 
            btnUno.Font = new Font("Segoe UI", 16F);
            btnUno.Location = new Point(12, 269);
            btnUno.Name = "btnUno";
            btnUno.Size = new Size(50, 50);
            btnUno.TabIndex = 1;
            btnUno.Text = "1";
            btnUno.UseVisualStyleBackColor = true;
            btnUno.Click += agregarNumero;
            // 
            // btnDos
            // 
            btnDos.Font = new Font("Segoe UI", 16F);
            btnDos.Location = new Point(68, 269);
            btnDos.Name = "btnDos";
            btnDos.Size = new Size(50, 50);
            btnDos.TabIndex = 2;
            btnDos.Text = "2";
            btnDos.UseVisualStyleBackColor = true;
            btnDos.Click += agregarNumero;
            // 
            // btnTres
            // 
            btnTres.Font = new Font("Segoe UI", 16F);
            btnTres.Location = new Point(124, 269);
            btnTres.Name = "btnTres";
            btnTres.Size = new Size(50, 50);
            btnTres.TabIndex = 3;
            btnTres.Text = "3";
            btnTres.UseVisualStyleBackColor = true;
            btnTres.Click += agregarNumero;
            // 
            // btnSeis
            // 
            btnSeis.Font = new Font("Segoe UI", 16F);
            btnSeis.Location = new Point(124, 213);
            btnSeis.Name = "btnSeis";
            btnSeis.Size = new Size(50, 50);
            btnSeis.TabIndex = 6;
            btnSeis.Text = "6";
            btnSeis.UseVisualStyleBackColor = true;
            btnSeis.Click += agregarNumero;
            // 
            // btnCinco
            // 
            btnCinco.Font = new Font("Segoe UI", 16F);
            btnCinco.Location = new Point(68, 213);
            btnCinco.Name = "btnCinco";
            btnCinco.Size = new Size(50, 50);
            btnCinco.TabIndex = 5;
            btnCinco.Text = "5";
            btnCinco.UseVisualStyleBackColor = true;
            btnCinco.Click += agregarNumero;
            // 
            // btnCuatro
            // 
            btnCuatro.Font = new Font("Segoe UI", 16F);
            btnCuatro.Location = new Point(12, 213);
            btnCuatro.Name = "btnCuatro";
            btnCuatro.Size = new Size(50, 50);
            btnCuatro.TabIndex = 4;
            btnCuatro.Text = "4";
            btnCuatro.UseVisualStyleBackColor = true;
            btnCuatro.Click += agregarNumero;
            // 
            // btnNueve
            // 
            btnNueve.Font = new Font("Segoe UI", 16F);
            btnNueve.Location = new Point(124, 157);
            btnNueve.Name = "btnNueve";
            btnNueve.Size = new Size(50, 50);
            btnNueve.TabIndex = 9;
            btnNueve.Text = "9";
            btnNueve.UseVisualStyleBackColor = true;
            btnNueve.Click += agregarNumero;
            // 
            // btnOcho
            // 
            btnOcho.Font = new Font("Segoe UI", 16F);
            btnOcho.Location = new Point(68, 157);
            btnOcho.Name = "btnOcho";
            btnOcho.Size = new Size(50, 50);
            btnOcho.TabIndex = 8;
            btnOcho.Text = "8";
            btnOcho.UseVisualStyleBackColor = true;
            btnOcho.Click += agregarNumero;
            // 
            // btnSiete
            // 
            btnSiete.Font = new Font("Segoe UI", 16F);
            btnSiete.Location = new Point(12, 157);
            btnSiete.Name = "btnSiete";
            btnSiete.Size = new Size(50, 50);
            btnSiete.TabIndex = 7;
            btnSiete.Text = "7";
            btnSiete.UseVisualStyleBackColor = true;
            btnSiete.Click += agregarNumero;
            // 
            // btnQuitar
            // 
            btnQuitar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnQuitar.Location = new Point(138, 92);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.Size = new Size(50, 50);
            btnQuitar.TabIndex = 12;
            btnQuitar.Text = "<-";
            btnQuitar.TextAlign = ContentAlignment.MiddleLeft;
            btnQuitar.UseVisualStyleBackColor = true;
            btnQuitar.Click += btnQuitar_Click;
            // 
            // btnBorrarTodo
            // 
            btnBorrarTodo.Font = new Font("Segoe UI", 16F);
            btnBorrarTodo.Location = new Point(82, 92);
            btnBorrarTodo.Name = "btnBorrarTodo";
            btnBorrarTodo.Size = new Size(50, 50);
            btnBorrarTodo.TabIndex = 11;
            btnBorrarTodo.Text = "C";
            btnBorrarTodo.UseVisualStyleBackColor = true;
            btnBorrarTodo.Click += btnBorrarTodo_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Font = new Font("Segoe UI", 16F);
            btnBorrar.Location = new Point(12, 92);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(64, 50);
            btnBorrar.TabIndex = 10;
            btnBorrar.Text = "CE";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnCuadrado
            // 
            btnCuadrado.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCuadrado.Location = new Point(262, 92);
            btnCuadrado.Name = "btnCuadrado";
            btnCuadrado.Size = new Size(50, 50);
            btnCuadrado.TabIndex = 14;
            btnCuadrado.Tag = "²";
            btnCuadrado.Text = "x²";
            btnCuadrado.UseVisualStyleBackColor = true;
            btnCuadrado.Click += clickOperador;
            // 
            // btnRaizCuadrada
            // 
            btnRaizCuadrada.Font = new Font("Segoe UI", 16F);
            btnRaizCuadrada.Location = new Point(206, 92);
            btnRaizCuadrada.Name = "btnRaizCuadrada";
            btnRaizCuadrada.Size = new Size(50, 50);
            btnRaizCuadrada.TabIndex = 13;
            btnRaizCuadrada.Tag = "√";
            btnRaizCuadrada.Text = "√";
            btnRaizCuadrada.UseVisualStyleBackColor = true;
            btnRaizCuadrada.Click += clickOperador;
            // 
            // btnMultiplicar
            // 
            btnMultiplicar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMultiplicar.Location = new Point(262, 148);
            btnMultiplicar.Name = "btnMultiplicar";
            btnMultiplicar.Size = new Size(50, 50);
            btnMultiplicar.TabIndex = 16;
            btnMultiplicar.Tag = "X";
            btnMultiplicar.Text = "X";
            btnMultiplicar.UseVisualStyleBackColor = true;
            btnMultiplicar.Click += clickOperador;
            // 
            // btnDividir
            // 
            btnDividir.Font = new Font("Segoe UI", 16F);
            btnDividir.Location = new Point(206, 148);
            btnDividir.Name = "btnDividir";
            btnDividir.Size = new Size(50, 50);
            btnDividir.TabIndex = 15;
            btnDividir.Tag = "⁄";
            btnDividir.Text = "⁄";
            btnDividir.UseVisualStyleBackColor = true;
            btnDividir.Click += clickOperador;
            // 
            // btnRestar
            // 
            btnRestar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRestar.Location = new Point(262, 204);
            btnRestar.Name = "btnRestar";
            btnRestar.Size = new Size(50, 50);
            btnRestar.TabIndex = 18;
            btnRestar.Tag = "‒";
            btnRestar.Text = "‒";
            btnRestar.UseVisualStyleBackColor = true;
            btnRestar.Click += clickOperador;
            // 
            // btnSumar
            // 
            btnSumar.Font = new Font("Segoe UI", 16F);
            btnSumar.Location = new Point(206, 204);
            btnSumar.Name = "btnSumar";
            btnSumar.Size = new Size(50, 50);
            btnSumar.TabIndex = 17;
            btnSumar.Tag = "+";
            btnSumar.Text = "+";
            btnSumar.UseVisualStyleBackColor = true;
            btnSumar.Click += clickOperador;
            // 
            // btnResultado
            // 
            btnResultado.Font = new Font("Segoe UI", 16F);
            btnResultado.Location = new Point(206, 260);
            btnResultado.Name = "btnResultado";
            btnResultado.Size = new Size(106, 50);
            btnResultado.TabIndex = 19;
            btnResultado.Tag = "=";
            btnResultado.Text = "=";
            btnResultado.UseVisualStyleBackColor = true;
            btnResultado.Click += btnResultado_Click;
            // 
            // btnComa
            // 
            btnComa.Font = new Font("Segoe UI", 16F);
            btnComa.Location = new Point(124, 334);
            btnComa.Name = "btnComa";
            btnComa.Size = new Size(50, 50);
            btnComa.TabIndex = 22;
            btnComa.Text = ",";
            btnComa.UseVisualStyleBackColor = true;
            btnComa.Click += btnComa_Click;
            // 
            // btnCero
            // 
            btnCero.Font = new Font("Segoe UI", 16F);
            btnCero.Location = new Point(68, 334);
            btnCero.Name = "btnCero";
            btnCero.Size = new Size(50, 50);
            btnCero.TabIndex = 21;
            btnCero.Text = "0";
            btnCero.UseVisualStyleBackColor = true;
            btnCero.Click += agregarNumero;
            // 
            // btnSigno
            // 
            btnSigno.Font = new Font("Segoe UI", 10F);
            btnSigno.Location = new Point(12, 334);
            btnSigno.Name = "btnSigno";
            btnSigno.Size = new Size(50, 50);
            btnSigno.TabIndex = 20;
            btnSigno.Text = "+/-";
            btnSigno.UseVisualStyleBackColor = true;
            btnSigno.Click += btnSigno_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(325, 419);
            Controls.Add(btnComa);
            Controls.Add(btnCero);
            Controls.Add(btnSigno);
            Controls.Add(btnResultado);
            Controls.Add(btnRestar);
            Controls.Add(btnSumar);
            Controls.Add(btnMultiplicar);
            Controls.Add(btnDividir);
            Controls.Add(btnCuadrado);
            Controls.Add(btnRaizCuadrada);
            Controls.Add(btnQuitar);
            Controls.Add(btnBorrarTodo);
            Controls.Add(btnBorrar);
            Controls.Add(btnNueve);
            Controls.Add(btnOcho);
            Controls.Add(btnSiete);
            Controls.Add(btnSeis);
            Controls.Add(btnCinco);
            Controls.Add(btnCuatro);
            Controls.Add(btnTres);
            Controls.Add(btnDos);
            Controls.Add(btnUno);
            Controls.Add(txtResultado);
            MaximizeBox = false;
            Name = "Form1";
            RightToLeft = RightToLeft.No;
            Text = "Calculadora By Rafi Junior Almanzar";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private TextBox txtResultado;
        private Button btnUno;
        private Button btnDos;
        private Button btnTres;
        private Button btnSeis;
        private Button btnCinco;
        private Button btnCuatro;
        private Button btnNueve;
        private Button btnOcho;
        private Button btnSiete;
        private Button btnQuitar;
        private Button btnBorrarTodo;
        private Button btnBorrar;
        private Button btnCuadrado;
        private Button btnRaizCuadrada;
        private Button btnMultiplicar;
        private Button btnDividir;
        private Button btnRestar;
        private Button btnSumar;
        private Button btnResultado;
        private Button btnComa;
        private Button btnCero;
        private Button btnSigno;
    }
}
