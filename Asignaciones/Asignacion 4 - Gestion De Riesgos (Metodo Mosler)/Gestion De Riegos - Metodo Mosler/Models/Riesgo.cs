using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MoslerRiskApp.Models
{
    /// <summary>
    /// Representa la ficha de riesgo (fase Definición) y los seis criterios
    /// de análisis (F, S, P, E, A, V) además de resultados de evaluación.
    /// </summary>
    public class Riesgo
    {
        // Fase: Definición
        public string NumeroFicha { get; set; } = string.Empty;
        public string NombreAnalista { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Campos principales (multilínea en UI)
        public string BienActivo { get; set; } = string.Empty;
        public string RiesgoDescripcion { get; set; } = string.Empty;
        public string Dano { get; set; } = string.Empty;

        // Fase: Análisis — seis criterios (1..5)
        // #1 F - Criterio de Función (consecuencias)
        public int Funcion { get; set; } = 1;

        // #2 S - Sustitución (dificultad de reemplazo)
        public int Sustitucion { get; set; } = 1;

        // #3 P - Profundidad (perturbación / efectos psicológicos / imagen)
        public int Profundidad { get; set; } = 1;

        // #4 E - Extensión (alcance geográfico / nivel)
        public int Extension { get; set; } = 1;

        // #5 A - Agresión (probabilidad de materialización)
        public int Agresion { get; set; } = 1;

        // #6 V - Vulnerabilidad (probabilidad de pérdidas / daños)
        public int Vulnerabilidad { get; set; } = 1;

        // Fase: Evaluación — resultados calculados
        public double Carácter { get; private set; }
        public double ProbabilidadEvaluada { get; private set; }
        public double Cuantificacion { get; private set; }

        public Riesgo() { }

        public Riesgo(string numeroFicha, string nombreAnalista, DateTime fecha)
        {
            NumeroFicha = numeroFicha;
            NombreAnalista = nombreAnalista;
            Fecha = fecha;
        }

        /// <summary>
        /// Valida campos obligatorios y rangos (1..5) para los seis criterios.
        /// </summary>
        public IEnumerable<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(NumeroFicha))
                errors.Add("Número de ficha es obligatorio.");

            if (string.IsNullOrWhiteSpace(NombreAnalista))
                errors.Add("Nombre del analista es obligatorio.");

            if (string.IsNullOrWhiteSpace(BienActivo))
                errors.Add("Bien / Activo no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(RiesgoDescripcion))
                errors.Add("Riesgo no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(Dano))
                errors.Add("Daño no puede estar vacío.");

            void CheckRange(int value, string name)
            {
                if (value < 1 || value > 5)
                    errors.Add($"{name} debe estar entre 1 y 5.");
            }

            CheckRange(Funcion, "F - Función");
            CheckRange(Sustitucion, "S - Sustitución");
            CheckRange(Profundidad, "P - Profundidad");
            CheckRange(Extension, "E - Extensión");
            CheckRange(Agresion, "A - Agresión");
            CheckRange(Vulnerabilidad, "V - Vulnerabilidad");

            return errors;
        }

        /// <summary>
        /// Calcula evaluación básica:
        /// - Carácter: promedio de F, S, P, E
        /// - ProbabilidadEvaluada: promedio de A y V
        /// - Cuantificación: Carácter * ProbabilidadEvaluada
        /// </summary>
        public void CalcularEvaluacion()
        {
            Carácter = (Funcion + Sustitucion + Profundidad + Extension) / 4.0;
            ProbabilidadEvaluada = (Agresion + Vulnerabilidad) / 2.0;
            Cuantificacion = Math.Round(Carácter * ProbabilidadEvaluada, 2);
        }

        // Métodos auxiliares para obtener la descripción textual de cada valor (1..5)
        public static string GetFuncionDescripcion(int value) => value switch
        {
            5 => "Muy Grave",
            4 => "Grave",
            3 => "Medianamente Grave",
            2 => "Levemente Grave",
            1 => "Muy Levemente Grave",
            _ => "Valor fuera de rango"
        };

        public static string GetSustitucionDescripcion(int value) => value switch
        {
            5 => "Muy Dificilmente",
            4 => "Dificilmente",
            3 => "Sin Mucha Dificultad",
            2 => "Fácilmente",
            1 => "Muy Fácilmente",
            _ => "Valor fuera de rango"
        };

        public static string GetProfundidadDescripcion(int value) => value switch
        {
            5 => "Perturbaciones Muy Graves",
            4 => "Perturbaciones Graves",
            3 => "Perturbaciones Limitadas",
            2 => "Perturbaciones Leves",
            1 => "Perturbaciones Muy Leves",
            _ => "Valor fuera de rango"
        };

        public static string GetExtensionDescripcion(int value) => value switch
        {
            5 => "De Carácter Internacional",
            4 => "De Carácter Nacional",
            3 => "De Carácter Regional",
            2 => "De Carácter Local",
            1 => "De Carácter Individual",
            _ => "Valor fuera de rango"
        };

        public static string GetAgresionDescripcion(int value) => value switch
        {
            5 => "Muy Alta",
            4 => "Alta",
            3 => "Normal",
            2 => "Baja",
            1 => "Muy Baja",
            _ => "Valor fuera de rango"
        };

        public static string GetVulnerabilidadDescripcion(int value) => value switch
        {
            5 => "Muy Alta",
            4 => "Alta",
            3 => "Normal",
            2 => "Baja",
            1 => "Muy Baja",
            _ => "Valor fuera de rango"
        };

        public override string ToString()
        {
            return $"Ficha {NumeroFicha} - {BienActivo} - {RiesgoDescripcion}";
        }

        // Serialización rápida a JSON (útil para persistencia simple)
        public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

        public static Riesgo FromJson(string json) => JsonSerializer.Deserialize<Riesgo>(json) ?? new Riesgo();
    }
}