using Microsoft.AspNetCore.Identity;

namespace Api.Empleados.Entidades
{
    public class Usuario: IdentityUser
    {
        public DateTime FechaNacimiento { get; set; }
    }
}
