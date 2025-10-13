// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Las variables niqueles, dieces, y cuartos representan el numero de monedas de 5, 10 y
        // 25 centavos de dolar. Hacer un programa que pegunte la cantidad de monedas que se
        // tienen de cada tipo y muestre la cantidad total en dolares.

        // Datos de entrada
        // niqueles, int, 0
        // dieces, int, 0
        // cuartos, int, 0
        int niqueles = 0;
        int dieces = 0;
        int cuartos = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";

        // Datos de salida
        // total, double, 0
        double total = 0;

        // Pedimos la cantidad de niqueles
        Console.WriteLine("Ingrese la cantidad de niqueles: ");
        valor = Console.ReadLine();
        niqueles = Convert.ToInt32(valor);

        // Pedimos la cantidad de dieces
        Console.WriteLine("Ingrese la cantidad de dieces: ");
        valor = Console.ReadLine();
        dieces = Convert.ToInt32(valor);

        // Pedimos la cantidad de cuartos
        Console.WriteLine("Ingrese la cantidad de cuartos: ");
        valor = Console.ReadLine();
        cuartos = Convert.ToInt32(valor);

        // Calculamos el total
        total = (niqueles * 0.05) + (dieces * 0.10) + (cuartos * 0.25);

        // Mostramos el total
        Console.WriteLine("El total en dolares es: " + total);
    }
}
