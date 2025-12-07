using System;
using LiteDB;

namespace MoslerRiskApp.Models
{
    public class Riesgo
    {
        // Cambiamos string a int. LiteDB autoincrementará esto (1, 2, 3...)
        [BsonId]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Activo { get; set; }
        public string Dano { get; set; }
        public DateTime Fecha { get; set; }

        // Valores numéricos
        public int F { get; set; }
        public int S { get; set; }
        public int P { get; set; }
        public int E { get; set; }
        public int A { get; set; }
        public int V { get; set; }

        [BsonIgnore]
        public int ER => (F * S + P * E) * (A * V);
    }
}