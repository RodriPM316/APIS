namespace Api.Empleados.DTOs
{
    public class UsuarioDTO
    {
        public required string Email { get; set; }
        public DateTime FechaNAcimiento { get; set; }
    }
}
