using CatalogosEmpleados.Entidades;

namespace CatalogosEmpleados.Servicios
{
    public interface IEmpleadoNotificacionService
    {
        Task NotificarAlta(Empleados empleado);
        Task NotificarModificacion(Empleados empleado);
        Task NotificarBaja(Empleados empleado);
    }
}
