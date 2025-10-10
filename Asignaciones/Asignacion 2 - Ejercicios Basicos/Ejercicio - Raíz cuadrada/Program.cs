// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        //Escribir un programa que calcule la raiz cuadrada de cualquier numero.
        // Se calcula con Math.Sqrt()

        // Datos de entrada
        // numero, 0, double
        double numero = 0;

        // Dato de trabajo
        // string, valor, ""
        string valor = "";

        // Dato de salida
        // raiz, 0, double
        double raiz = 0;

        // Pedir el numero
        Console.WriteLine("Ingrese un numero para calcular su raiz cuadrada:");
        valor = Console.ReadLine();
        numero = Convert.ToDouble(valor);

        // Calcular la raiz
        raiz = Math.Sqrt(numero);

        // Mostrar la raiz
        Console.WriteLine("La raiz cuadrada de " + numero + " es: " + raiz);
    }
}
