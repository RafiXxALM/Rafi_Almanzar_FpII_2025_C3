// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Un solenoiide es una bobina de alambre enrollado con un determinado radio y longitud.
        // Una de las caracteristicas del solenoide es la inductancia, la cual se calcula con la
        // siguiente formula:
        // L = mu * longitud * N^2 * A
        // L inductancia en henries
        // mu constante de permeabilidad 4PI * 10^ - 7
        // longitud es la longitud de solenoide en metros
        // N es el numero de vueltas por unidad de longitud
        // A es el area de la seccion transversal en metros cuadrados
        // Hacer un programa que calcule la inductancia enn microhenries dados los datos de
        // entrada

        // Datos de entrada
        // longitud, double, 0
        // n, double, 0
        // A, double, 0
        double longitud = 0;
        double n = 0;
        double A = 0;

        // Datos de trabajo
        // mu, double, 4PI * 10^ - 7
        // valor, string, ""
        double mu = 4 * 3.14159 * Math.Pow(10, -7);
        string valor = "";

        // Datos de salida
        // L, double, 0
        double L = 0;

        // Pedimos los datos
        Console.WriteLine("Dame la longitud");
        valor = Console.ReadLine();
        longitud = Convert.ToDouble(valor);

        Console.WriteLine("Dame el numero de vueltas");
        valor = Console.ReadLine();
        n = Convert.ToDouble(valor);

        Console.WriteLine("Dame el area de la seccion transversal");
        valor = Console.ReadLine();
        A = Convert.ToDouble(valor);

        // Calculamos la inductancia
        L = mu * longitud * (n * n) * A;

        // Mostramos el resultado
        Console.WriteLine("La inductancia es: " + L + " microhenries");
    }
}
