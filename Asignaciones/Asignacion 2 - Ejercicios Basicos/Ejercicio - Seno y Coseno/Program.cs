// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Escribir un programa que pida un angulo en grados y calcule su seno y coseno.
        // El angulo es necesario en radianes para Math.Sin y Math.Cos

        // Datos de entrada
        // angulo , double, 0
        double angulo = 0;

        // Datos de trabajo
        // valor, string, ""
        // radianes, double, 0
        // pi, double, 3.14159
        string valor = "";
        double radianes = 0;
        double pi = 3.14159;

        // Datos de salida
        // seno, double, 0
        // coseno, double, 0
        double seno = 0;
        double coseno = 0;

        // Pedir el angulo
        Console.WriteLine("Escribe el angulo: ");
        valor = Console.ReadLine();
        angulo = Convert.ToDouble(valor);

        // Convertirlo a radianes
        radianes = angulo * (pi / 180);

        // Calcular Seno
        seno = Math.Sin(radianes);

        // Calcular Coseno
        coseno = Math.Cos(radianes);

        // Mostrar resultados
        Console.WriteLine("El seno de " + angulo + " grados es: " + seno);
        Console.WriteLine("El coseno de " + angulo + " grados es: " + coseno);
    }
}   
