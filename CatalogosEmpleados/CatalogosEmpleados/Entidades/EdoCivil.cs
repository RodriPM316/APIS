using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class EdoCivil
    {
        [Key]
        public int Id_EdoCivil { get; set; }
        public required string Tipo_EdoCivil { get; set; }
    }
}
