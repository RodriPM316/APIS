using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Turnos
    {
        [Key]
        public int Id_Turno { get; set; }
        public required string Tipo_Turno { get; set; }
    }
}
