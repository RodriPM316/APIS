using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Puestos
    {
        [Key]
        public int Id_Puesto { get; set; }
        public required string Nom_Puesto { get; set; }
        public required decimal Salario { get; set; }
    }
}
