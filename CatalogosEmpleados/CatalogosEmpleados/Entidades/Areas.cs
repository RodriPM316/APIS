using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Areas
    {
        [Key]
        public int Id_Area { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string NombreArea { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string DireccionEmail { get; set; }
    }
}
