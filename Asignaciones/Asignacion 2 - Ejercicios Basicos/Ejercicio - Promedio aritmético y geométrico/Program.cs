// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que pida cuatro numeros y calcule el promedio aritmetico y
        //geometrico de los mismos, La formula del promedio geometrico es (v1* v1*....*vn)^(1/n)
        // Math.Pow(base, potencia)

        // Datos de entrada
        // n1, n2, n3, n4, double, 0
        double n1 = 0;
        double n2 = 0;
        double n3 = 0;
        double n4 = 0;

        // Datos de trabajo
        // valor, string. ""
        // baseP, double, 0
        // potencia, double, 0
        string valor = "";
        double baseP = 0;
        double potencia = 0;

        // Datos de salida
        // pArit, double, 0
        // pGeom, double, 0
        double pArit = 0;
        double pGeom = 0;

        // Pedir los numeros
        Console.WriteLine("Ingrese el primer numero:");
        valor = Console.ReadLine();
        n1 = Convert.ToDouble(valor);

        Console.WriteLine("Ingrese el segundo numero:");
        valor = Console.ReadLine();
        n2 = Convert.ToDouble(valor);

        Console.WriteLine("Ingrese el tercer numero:");
        valor = Console.ReadLine();
        n3 = Convert.ToDouble(valor);

        Console.WriteLine("Ingrese el cuarto numero:");
        valor = Console.ReadLine();
        n4 = Convert.ToDouble(valor);

        // Calcular el promedio aritmetico
        pArit = (n1 + n2 + n3 + n4) / 4;

        // Calcular el promedio geometrico
        // Calcular la base
        // Calcular la potencia
        baseP = n1 * n2 * n3 * n4;
        potencia = 1.0 / 4.0;
        pGeom = Math.Pow(baseP, potencia);

        // Mostrar los resultados
        Console.WriteLine("El promedio aritmetico es: " + pArit);
        Console.WriteLine("El promedio geometrico es: " + pGeom);
    }
}   
