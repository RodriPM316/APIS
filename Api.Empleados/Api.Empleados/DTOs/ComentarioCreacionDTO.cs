using System.ComponentModel.DataAnnotations;

namespace Api.Empleados.DTOs
{
    public class ComentarioCreacionDTO
    {
        [Required]
        public required string Cuerpo { get; set; }
    }
}
