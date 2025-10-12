// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // La compañio]a de herramienta esta realizando una conversion del sistema ingles de
        // medidas al metrico. Hacer un programa que dada una longitud expresada en pies y
        // pulgadas, determine su equivalente metrico en centimetros.
        // pies a centrimetros cm=ft*30.48
        // pulgadas a centimetros cm=in*2.54

        // Datos de entrada
        // pies , double, 0
        // pulgadas, double, 0
        double pies = 0;
        double pulgadas = 0;


        // Datos de trabajo
        // valor, string, ""
        // cmPies, double, 0
        // cmPulg, double, 0
        string valor = "";
        double cmPies = 0;
        double cmPulg = 0;

        // Datos de salida
        // centimetros, double, 0
        double centimetros = 0;


        // Pedir los pies
        Console.WriteLine("Ingrese Cantidad de pies: ");
        valor = Console.ReadLine();
        pies = Convert.ToDouble(valor);

        // Pedir las pulgadas
        Console.WriteLine("Ingrese Cantidad de pulgadas: ");
        valor = Console.ReadLine();
        pulgadas = Convert.ToDouble(valor);

        // Convertimos los pies
        cmPies = pies * 30.48;

        // Convertimos las pulgadas
        cmPulg = pulgadas * 2.54;

        // Obtenemos el total de los centimetros
        centimetros = cmPies + cmPulg;

        // Mostramos el total de los centimetros
        Console.WriteLine("El total de centimetros es: " + centimetros);
    }
}
