using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Departamentos
    {
        [Key]
        public int Id_Departamento { get; set; }

        public required string Nom_Departamento { get; set; }
    }
}
