// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que pueda sumar, restar, multiplicar y dividir dos números fraccionarios

        // Datos de entrada
        // num1, num2, int, 0
        // den1, den2, int, 0
        // opcion, int, 0
        int num1 = 0;
        int num2 = 0;
        int den1 = 0;
        int den2 = 0;
        int opcion = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";

        // Presentamos menu
        Console.WriteLine("1-Suma, 2-Resta, 3-Multi, 4-Div");

        // Obtenemos opcion
        Console.WriteLine("Que operacion deseas hacer");
        valor = Console.ReadLine();
        opcion = Convert.ToInt32(valor);

        // Pedimos las fracciones
        Console.WriteLine("Primera fraccion");
        Console.WriteLine("Dame el primer Numerador");
        valor = Console.ReadLine();
        num1 = Convert.ToInt32(valor);

        Console.WriteLine("Dame el primer Denominador");
        valor = Console.ReadLine();
        den1 = Convert.ToInt32(valor);

        // Hacemos la suma
        if (opcion == 1)
        {
            suma(num1, den1, num2, den2);
        }

        // Hacemos la resta
        if (opcion == 2)
        {
            resta(num1, den1, num2, den2);
        }

        // Hacemos la multiplicacion
        if (opcion == 3)
        {
            multi(num1, den1, num2, den2);
        }

        // Hacemos la division
        if (opcion == 4)
        {
            div(num1, den1, num2, den2);

        }
    }

    // Funcion para la suma
    public static void suma(int num1, int den1, int num2, int den2)
        {
            // Datos de trabajo
            // temp1 , int, 0
            // temp2 , int, 0
            int temp1 = 0;
            int temp2 = 0;

            // Datos de salida
            // numR, int, 0 
            // denCom, int, 0
            int numR = 0;
            int denCom = 0;

            // Calculamos el denominador comun
            denCom = den1 * den2;

        // Calculamos el primer temporal
        temp1 = (denCom / den1) * num1;

        // Calculamos el segundo temporal
        temp2 = (denCom / den2) * num2;

        //Calculamos el numerador del resultado
        numR = temp1 + temp2;

        // Mostramos el resultado
        Console.WriteLine(num1 + "/" + den1 + " + " + num2 + "/" + den2 + " = " + numR + "/" + denCom);
        }

    // Funcion para la resta
    public static void resta(int num1, int den1, int num2, int den2)
    {
        // Datos de trabajo
        // temp1 , int, 0
        // temp2 , int, 0
        int temp1 = 0;
        int temp2 = 0;

        // Datos de salida
        // numR, int, 0 
        // denCom, int, 0
        int numR = 0;
        int denCom = 0;

        // Calculamos el denominador comun
        denCom = den1 * den2;

        // Calculamos el primer temporal
        temp1 = (denCom / den1) * num1;

        // Calculamos el segundo temporal
        temp2 = (denCom / den2) * num2;

        //Calculamos el numerador del resultado
        numR = temp1 - temp2;

        // Mostramos el resultado
        Console.WriteLine(num1 + "/" + den1 + " - " + num2 + "/" + den2 + " = " + numR + "/" + denCom);
    }

    // Funcion para la multiplicacion
    public static void multi(int num1, int den1, int num2, int den2)
    {
        // Datos de salida
        // numR, int, 0 
        // denR, int, 0
        int numR = 0;
        int denR = 0;
        // Calculamos el numerador del resultado
        numR = num1 * num2;
        // Calculamos el denominador del resultado
        denR = den1 * den2;
        // Mostramos el resultado
        Console.WriteLine(num1 + "/" + den1 + " * " + num2 + "/" + den2 + " = " + numR + "/" + denR);
    }

    // Funcion para la division
    public static void div(int num1, int den1, int num2, int den2)
    {
        // Datos de salida
        // numR, int, 0 
        // denR, int, 0
        int numR = 0;
        int denR = 0;
        // Calculamos el numerador del resultado
        numR = num1 * den2;
        // Calculamos el denominador del resultado
        denR = den1 * num2;
        // Mostramos el resultado
        Console.WriteLine(num1 + "/" + den1 + " / " + num2 + "/" + den2 + " = " + numR + "/" + denR);
    }

}

