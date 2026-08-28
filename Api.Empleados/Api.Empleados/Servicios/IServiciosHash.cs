using Api.Empleados.DTOs;

namespace Api.Empleados.Servicios
{
    public interface IServiciosHash
    {
        ResultadoHashDTO Hash(string input);
        ResultadoHashDTO Hash(string input, byte[] sal);
    }
}