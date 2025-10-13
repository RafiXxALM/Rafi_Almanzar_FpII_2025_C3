// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa en el cual los datos de etrada incluyen las coordeadas en el plano
        // (coodenadas cartesianas) de los tres vertices de un trangulo como numeros reales.
        // Calculese y muestrese el area del triangulo.

        // La formula a usar es
        // a= (1/2) * abs (x1(y2-y3) + x2(y3-y1) + x3(y1-y2))

        // Datos de entrada
        // x1,y1, double, 0
        // x2,y2, double, 0
        // x3,y3, double, 0
        double x1 = 0;
        double y1 = 0;
        double x2 = 0;
        double y2 = 0;
        double x3 = 0;
        double y3 = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";


        // Datos de salida
        // area, double, 0
        double area = 0;

        // Pedimos los datos
        Console.WriteLine("Ingrese x1:");
        valor = Console.ReadLine();
        x1 = Convert.ToDouble(valor);
        Console.WriteLine("Ingrese y1:");
        valor = Console.ReadLine();
        y1 = Convert.ToDouble(valor);

        Console.WriteLine("Ingrese x2:");
        valor = Console.ReadLine();
        x2 = Convert.ToDouble(valor);
        Console.WriteLine("Ingrese y2:");
        valor = Console.ReadLine();
        y2 = Convert.ToDouble(valor);

        Console.WriteLine("Ingrese x3:");
        valor = Console.ReadLine();
        x3 = Convert.ToDouble(valor);
        Console.WriteLine("Ingrese y3:");
        valor = Console.ReadLine();
        y3 = Convert.ToDouble(valor);


        // Calcula el area
        area = 0.5 * Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));

        // Mostramos el area
        Console.WriteLine("El area del triangulo es: " + area);
    }
}
