using Microsoft.Extensions.Options;

namespace Api.Empleados
{
    public class PagosProcesamiento
    {
        private TarifaOpciones _tarifaOpciones;

        public PagosProcesamiento(IOptionsMonitor<TarifaOpciones> optionsMonitor)
        {
            _tarifaOpciones = optionsMonitor.CurrentValue;

            optionsMonitor.OnChange(tarifaOpciones =>
            {
                Console.WriteLine("Se han actualizado las opciones de tarifa.");
                _tarifaOpciones = tarifaOpciones;
            });
        }

        public void ProcesarPago()
        {
            // Aqui usamos las tarifas
        }

        public TarifaOpciones ObtenerTarifas()
        {
            return _tarifaOpciones;
        }
    }
}
