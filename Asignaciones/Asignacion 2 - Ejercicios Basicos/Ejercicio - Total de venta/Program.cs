// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Hacer un programa que pida el costo de un producto y la cantidad de productos a
        // comprar, Debe de mostrar el total a pagar con el IVA desglosado

        // Datos de entrada
        // costo, double, 0
        // cantidad, int, 0
        double costo = 0;
        int cantidad = 0;

        // Datos de trabajo
        // valor, string, ""
        string valor = "";

        // Datos de salida
        // total, double, 0
        // iva, double, 0
        // granTotal, double, 0
        double total = 0;
        double iva = 0;
        double granTotal = 0;

        // Pedimos el costo
        Console.WriteLine("Ingrese el costo del producto:");
        valor = Console.ReadLine();
        costo = Convert.ToDouble(valor);

        // Pedimos la cantidad
        Console.WriteLine("Ingrese la cantidad de productos a comprar:");
        valor = Console.ReadLine();
        cantidad = Convert.ToInt32(valor);

        // Calculamos el total
        total = costo * cantidad;

        // Calculamos el IVA
        iva = total * 0.16;

        // Calculamos el granTotal
        granTotal = total + iva;

        // Mostramos los resultados
        Console.WriteLine("Total: " + total);
        Console.WriteLine("IVA: " + iva);
        Console.WriteLine("------------------");
        Console.WriteLine("Gran Total: " + granTotal);

    }
}
