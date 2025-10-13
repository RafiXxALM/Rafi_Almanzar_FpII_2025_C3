// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que lea dos enteros que repesenten el peso de un objeto en libras y
        // onzas, en seguida el programa mostrara el peso del objeto en kilogramos.
        // 1 libra 0.454 kg
        // 1 onza 0.0283 kg

        // Datos de entrada
        // libras, double, 0
        // onzas, double, 0
        double libras = 0;
        double onzas = 0;


        // Datos de trabajo
        // valor, string, ""
        // libKG, double, 0
        // onzKG, double, 0
        string valor = "";
        double libKG = 0;
        double onzKG = 0;

        // Datos de salida
        // total, double, 0
        double total = 0;

        // Pedimos las libras
        Console.WriteLine("Ingrese el peso en libras: ");
        valor = Console.ReadLine();
        libras = Convert.ToDouble(valor);

        // Pedimos las onzas
        Console.WriteLine("Ingrese el peso en onzas: ");
        valor = Console.ReadLine();
        onzas = Convert.ToDouble(valor);

        // Convertimos las libras
        libKG = libras * 0.454;

        // Convertimos las onzas
        onzKG = onzas * 0.0283;

        // Encontramos el total
        total = libKG + onzKG;

        // Mostramos el resultado
        Console.WriteLine("El peso en kilogramos es: " + total);

    }
}
