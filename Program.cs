using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaPago.Infraestructure;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sistema de Pago: Esperando recetas médicas...");

        var pagoServiceBusReceiver = new PagoServiceBusReceiver("Endpoint=sb://arquitecturaar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=liz2Rdnrrk71JmuXcI3xyWnanI36FTj2h+ASbNqRNXI="); // Reemplazar con tu cadena de conexión
        await pagoServiceBusReceiver.RecibirRecetasAsync();

        Console.WriteLine("Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}