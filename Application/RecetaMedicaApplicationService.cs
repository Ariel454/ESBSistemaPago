using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaPago.Domain;

namespace SistemaPago.Application
{
    public class RecetaMedicaApplicationService
    {
        public void ProcesarRecetaEnPago(RecetaMedica receta)
        {

            Console.WriteLine($"Procesando receta en Pago para el paciente: {receta.Paciente}");
        }
    }
}
