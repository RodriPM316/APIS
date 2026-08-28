using System.ComponentModel.DataAnnotations;

namespace Api.Empleados.DTOs
{
    public class LibroCreacionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string Titulo { get; set; }
        public List<int> AutoresIds { get; set; } = [];
    }
}
