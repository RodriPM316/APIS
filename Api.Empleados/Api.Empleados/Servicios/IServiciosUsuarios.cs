using Api.Empleados.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Api.Empleados.Servicios
{
    public interface IServiciosUsuarios
    {
        Task<Usuario?> ObtenerUsuario();
    }
}