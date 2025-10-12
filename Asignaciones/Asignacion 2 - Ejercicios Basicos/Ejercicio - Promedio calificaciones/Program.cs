// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que determine el total y el promedio de cuatro calificaciones de
        // examen enteras. Dar el promedio como entero

        // Datos de entrada
        // cal1, cal2, cal3, cal4, int, 0
        int cal1 = 0;
        int cal2 = 0;
        int cal3 = 0;
        int cal4 = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";

        //Datos de salida
        // total, int, 0
        // promedio, int, 0
        int total = 0;
        int promedio = 0;

        // Pedimos las calificaciones
        Console.WriteLine("Dame la calificacion 1: ");
        valor = Console.ReadLine();
        cal1 = Convert.ToInt32(valor);

        Console.WriteLine("Dame la calificacion 2: ");
        valor = Console.ReadLine();
        cal2 = Convert.ToInt32(valor);

        Console.WriteLine("Dame la calificacion 3: ");
        valor = Console.ReadLine();
        cal3 = Convert.ToInt32(valor);

        Console.WriteLine("Dame la calificacion 4: ");
        valor = Console.ReadLine();
        cal4 = Convert.ToInt32(valor);

        // Calculamos el total
        total = cal1 + cal2 + cal3 + cal4;

        // Calculamos el promedio
        // es una division entera
        promedio = total / 4;

        // Mostramos los resultados
        Console.WriteLine("El total es: " + total + ", con un promedio de "+ promedio);
    }
}