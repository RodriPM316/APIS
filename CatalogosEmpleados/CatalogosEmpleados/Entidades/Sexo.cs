using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Sexo
    {
        [Key]
        public int Id_Sexo { get; set; }
        public required string Genero { get; set; }
    }
}
