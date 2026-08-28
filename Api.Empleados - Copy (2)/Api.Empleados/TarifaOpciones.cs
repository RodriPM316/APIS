using System.ComponentModel.DataAnnotations;

namespace Api.Empleados
{
    public class TarifaOpciones
    {
        public const string Seccion = "tarifas";
        [Required]
        public decimal dia { get; set; }
        [Required]
        public decimal noche { get; set; }
    }
}
