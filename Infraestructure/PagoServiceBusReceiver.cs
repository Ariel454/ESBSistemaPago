using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SistemaPago.Application;
using SistemaPago.Domain;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPago.Infraestructure
{
    public class PagoServiceBusReceiver
    {
        private readonly string _serviceBusConnectionString;
        private const string QueueNamePago = "pago";

        public PagoServiceBusReceiver(string serviceBusConnectionString)
        {
            _serviceBusConnectionString = serviceBusConnectionString;
        }

        public async Task RecibirRecetasAsync()
        {
            var client = new QueueClient(_serviceBusConnectionString, QueueNamePago);

            RegisterOnMessageHandlerAndReceiveMessages(client);

            Console.ReadKey();

            await client.CloseAsync();
        }

        private void RegisterOnMessageHandlerAndReceiveMessages(QueueClient queueClient)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(async (message, token) =>
            {
                var recetaMedica = JsonConvert.DeserializeObject<RecetaMedica>(Encoding.UTF8.GetString(message.Body));

                // Procesar la receta médica en el sistema de pago
                var recetaApplicationService = new RecetaMedicaApplicationService();
                recetaApplicationService.ProcesarRecetaEnPago(recetaMedica);

                Console.WriteLine($"Receta procesada en Pago: {recetaMedica}");

                await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }, messageHandlerOptions);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Mensaje de error: {exceptionReceivedEventArgs.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
