using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Servicios
{
    public class EmpleadoNotificacionService : IEmpleadoNotificacionService
    {
        private readonly IEmailService emailService;
        private readonly ApplicationDbContext context;

        public EmpleadoNotificacionService(IEmailService emailService, ApplicationDbContext context)
        {
            this.emailService = emailService;
            this.context = context;
        }

       public async Task NotificarAlta(Empleados empleados)
        {
            // 1. Buscamos los correos de Altas (Tipo 1)
            string destinatarios = await ObtenerDestinatariosPorTipoAsync(1);

            // Si no hay áreas configuradas para altas, detenemos el envío
            if (string.IsNullOrEmpty(destinatarios)) return;

            string asunto = "Notificación: Nuevo Empleado Registrado";
            string mensaje = $@"
            <h2>Alta exitosa</h2>
            <p>Se ha registrado un nuevo empleado:</p>
            <ul>
                <li><strong>ID:</strong> {empleados.Id_Empleado}</li>
                <li><strong>Nombre:</strong> {empleados.Nombre} {empleados.Paterno} {empleados.Materno}</li>
                <li><strong>ID Departamento:</strong> {empleados.Id_Departamento}</li>

            </ul>";

            await emailService.EnviarCorreoAsync(destinatarios, asunto, mensaje);
        }

        public async Task NotificarModificacion(Empleados empleados)
        {
            // 1. Buscamos los correos de Ascensos/Modificaciones (Tipo 2)
            string destinatarios = await ObtenerDestinatariosPorTipoAsync(2);

            if (string.IsNullOrEmpty(destinatarios)) return;

            string asunto = "Aviso: Datos de Empleado Actualizados";
            string mensaje = $@"
        <h2>Actualización exitosa</h2>
        <p>Se han modificado los datos del siguiente empleado en el sistema:</p>
        <ul>
            <li><strong>ID:</strong> {empleados.Id_Empleado}</li>
            <li><strong>Nombre:</strong> {empleados.Nombre} {empleados.Paterno} {empleados.Materno}</li>
            <li><strong>ID Departamento:</strong> {empleados.Id_Departamento}</li>
        </ul>
        <p>Por favor, ingrese al sistema si necesita auditar el resto de los cambios.</p>";

            await emailService.EnviarCorreoAsync(destinatarios, asunto, mensaje);
        }

        public async Task NotificarBaja(Empleados empleados)
        {
            string destinatarios = await ObtenerDestinatariosPorTipoAsync(3);

            if (string.IsNullOrEmpty(destinatarios)) return;

            string asunto = "Aviso: Baja de empleado";
            string mensaje = $@"
        <h2>Baja definitiva del sistema</h2>
        <p>Se han eliminado los datos del siguiente empleado en el sistema:</p>
        <ul>
            <li><strong>ID:</strong> {empleados!.Id_Empleado}</li>
            <li><strong>Nombre:</strong> {empleados.Nombre} {empleados.Paterno} {empleados.Materno}</li>
            <li><strong>ID Departamento:</strong> {empleados.Id_Departamento}</li>
        </ul>";

            await emailService.EnviarCorreoAsync(destinatarios, asunto, mensaje);
        }

        private async Task<string> ObtenerDestinatariosPorTipoAsync(int idTipoCorreo)
        {
            var correosInvolucrados = await (from a in context.Areas
                                             join c in context.Correos on a.Id_Area equals c.Id_Area
                                             where c.Id_TipoCorreo == idTipoCorreo
                                             select a.DireccionEmail).ToListAsync();

            // Devuelve el texto unido por comas, o un texto vacío si no encontró nada
            return string.Join(",", correosInvolucrados);
        }
    }
}
