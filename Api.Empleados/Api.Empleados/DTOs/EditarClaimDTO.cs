using System.ComponentModel.DataAnnotations;

namespace Api.Empleados.DTOs
{
    public class EditarClaimDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
    }
}
