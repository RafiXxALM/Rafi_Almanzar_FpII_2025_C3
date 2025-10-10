// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Escribir un programa que convierta angulos de grados a radianes y viceversa
        // r= g*pi/180
        // g= r*180/pi

        // Datos de entrada
        // Angulo a convertir, double, 0
        // seleccion, string, ""
        double angulo = 0;
        string seleccion = "";

        // Datos de trabajo
        // valor, string, ""
        // pi, double, 3.14159
        string valor = "";
        double pi = 3.14159;

        // Datos de salida
        // radianes, double, 0
        // grados, double, 0
        double radianes = 0;
        double grados = 0;

        // Pedir si es grados a radianes o radianes a grados
        Console.WriteLine("1- radianes a grados, 2- grados a radianes ");
        seleccion = Console.ReadLine();


        // pedir angulo
        Console.WriteLine("Ingrese el angulo a convertir: ");
        valor = Console.ReadLine();
        angulo = Convert.ToDouble(valor);

        // Convertir radianes a grados
        if (seleccion == "1")
        {
            grados = angulo * 180 / pi;
            // Mostrar resultado
            Console.WriteLine("{0} radianes son {1} grados", angulo, grados);
        }


        // Convertir grados a radianes
        if (seleccion == "2")
        {
            radianes = angulo * pi / 180;
            Console.WriteLine("{0} grados son {1} radianes", grados, angulo);
        }
    }
}
