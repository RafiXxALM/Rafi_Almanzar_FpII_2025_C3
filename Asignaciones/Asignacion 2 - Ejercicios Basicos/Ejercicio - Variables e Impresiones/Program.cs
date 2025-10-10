// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // que un numero de tres dgitos tiene la forma abc. Por ejemplo el numero 731,
        // a es siete, b es tres y c es uno. Hacer un programa que produzca numeros ca y accb.
        // 137 7113

        // Datos de entrada
        // a 7, b 3, c 1, int
        int a = 7;
        int b = 3;
        int c = 1;

        // Datos de salida
        // num1, num2
        int num1 =0;
        int num2 =0;

        // Datos de trabajo

        // Pasos del programa

        // Imprimir cba
        //Console.WriteLine("{0}{1}{2}", c, b, a);


        // Imprimir accb
        //Console.WriteLine("{0}{1}{2}{3}", a, c, c, b);

        ///////////////////////////////////

        // Calcular num1 cba
        num1 = (c * 100) + (b * 10) + a;

        // Calcular num2 accb
        num2 = (a * 1000) + (c * 100) + (c * 10) + b;

        // Mostrar num1
        Console.WriteLine(num1);

        // Mostrar num2
        Console.WriteLine(num2);
    }
}