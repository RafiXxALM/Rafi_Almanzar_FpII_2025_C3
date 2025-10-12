// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que en los datos de entrada se proporciones dos tiemposo como
        // enteros de la forma hh mm donde hh representa las horas(menios de 24) y mm los
        // minutos(menos de 60). Determinese la suma de estos os tiempos y exhibase en la
        // forma d hh mm, donde d es dias

        // Datos de entrada
        // horas1 y horas2, enteros, 0
        // minutos1 y minutos2, enteros, 0
        int horas1 = 0;
        int horas2 = 0;
        int minutos1 = 0;
        int minutos2 = 0;

        // Datos de trabajo
        // valor, string, ""
        // residuo, int, 0
        string valor = "";
        int residuo = 0;

        // Datos de salida
        // rDia, int, 0
        // rMin, int, 0
        // rHoras, int, 0
        int rDia = 0;
        int rMin = 0;
        int rHoras = 0;

        // Pedimos las horas y los minutos para el primer tiempo
        // Solo tomares enn el rango de valores de 0 a 23 para horas
        // 0 a 59 para minutos

        do
        {
            Console.WriteLine("Ingrese las horas del primer tiempo: ");
            valor = Console.ReadLine();
            horas1 = Convert.ToInt32(valor);
        } while (horas1 < 0 || horas1 > 23);

        do {
            Console.WriteLine("Ingrese los minutos del primer tiempo: ");
            valor = Console.ReadLine();
            minutos1 = Convert.ToInt32(valor);
        } while (minutos1 < 0 || minutos1 > 59);

        // Pedimos para el segundo tiempo

        do
        {
            Console.WriteLine("Ingrese las horas del segundo tiempo: ");
            valor = Console.ReadLine();
            horas2 = Convert.ToInt32(valor);
        } while (horas2 < 0 || horas2 > 23);

        do {
            Console.WriteLine("Ingrese los minutos del segundo tiempo: ");
            valor = Console.ReadLine();
            minutos2 = Convert.ToInt32(valor);
        } while (minutos2 < 0 || minutos2 > 59);

        // Sumamos los minutos
        rMin = minutos1 + minutos2;

        // Verificamos si hemos pasado de la hora
        if (rMin > 59)
        {
            rMin = rMin - 60;
            residuo = 1;
        }

        // Sumamos las horas
        rHoras = horas1 + horas2 + residuo;
        residuo = 0;

        // Verificamos si hemos pasado de los dias
        if (rHoras > 23)
        {
            rHoras = rHoras - 24;
            residuo = 1;
        }

        // Obtenemos el dia
        rDia = residuo;
        residuo = 0;

        // Mostramos el resultado
        Console.WriteLine("El resultado es: " + rDia + " " + rHoras + " " + rMin);



    }
}
