using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;


namespace CatalogosEmpleados.Servicios
{
    public class EmpleadoNotiFicacion2Service : IEmpleadoNotificacionService
    {
        private readonly IEmailService emailService;

        public EmpleadoNotiFicacion2Service(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task NotificarAlta(Empleados empleados)
        {
            string destinatario = "r@gmail.com";
            string asunto = "Notificación: Nuevo Empleado Registrado";
            string mensaje = $@"
            <h2>Alta exitosa</h2>
            <p>Se ha registrado un nuevo empleado:</p>
            <ul>
                <li><strong>ID:</strong> {empleados.Id_Empleado}</li>
                <li><strong>Nombre:</strong> {empleados.Nombre} {empleados.Paterno} {empleados.Materno}</li>
                <li><strong>ID Departamento:</strong> {empleados.Id_Departamento}</li>

            </ul>";

            await emailService.EnviarCorreoAsync(destinatario, asunto, mensaje);
        }

        public async Task NotificarModificacion(Empleados empleados)
        {
            string destinatario = "j@gmail.com";
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

            await emailService.EnviarCorreoAsync(destinatario, asunto, mensaje);
        }

        public async Task NotificarBaja(Empleados empleados)
        {
            string destinatario = "r@gmail.com, in@gmail.com";
            string asunto = "Aviso: Baja de empleado";
            string mensaje = $@"
        <h2>Baja definitiva del sistema</h2>
        <p>Se han eliminado los datos del siguiente empleado en el sistema:</p>
        <ul>
            <li><strong>ID:</strong> {empleados!.Id_Empleado}</li>
            <li><strong>Nombre:</strong> {empleados.Nombre} {empleados.Paterno} {empleados.Materno}</li>
            <li><strong>ID Departamento:</strong> {empleados.Id_Departamento}</li>
        </ul>";

            await emailService.EnviarCorreoAsync(destinatario, asunto, mensaje);
        }
    }
}
