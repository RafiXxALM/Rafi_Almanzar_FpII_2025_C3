// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // El error relativo en una medicion m es la razon de la diferenccia absoluta entre la
        // medicion y el valor verdadero v al valor, Hacer un programa que calcule el
        // error realtivo. Por ejemplo m = 51.3, v = 51.3 entonces el error relativo es 5.8479e-3

        // errorR = |medicion-verdadero| \ verdadero

        // Datos de entrada
        // medicion, double, 0
        // verdadero, double, 0
        double medicion = 0;
        double verdadero = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";

        // Datos de salida
        double errorR = 0;

        // Pedimos la medicion
        Console.WriteLine("Ingrese la medicion: ");
        valor = Console.ReadLine();
        medicion = Convert.ToDouble(valor);

        // pedimos el valor verdadero
        Console.WriteLine("Ingrese el valor verdadero: ");
        valor = Console.ReadLine();
        verdadero = Convert.ToDouble(valor);

        // Calculamos el error relativo
        errorR = Math.Abs(medicion - verdadero) / verdadero;

        // Mostramos el error
        Console.WriteLine("El error relativo es: " + errorR);
    }
}
