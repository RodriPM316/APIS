using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using CatalogosEmpleados.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/empleados-stored")]
    [ApiController]
    public class EmpleadosStoredController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEmpleadoNotificacionService notificador;

        public EmpleadosStoredController(ApplicationDbContext context, IEmpleadoNotificacionService notificador)
        {
            this.context = context;
            this.notificador = notificador;
        }

        [HttpGet]
        public async Task<IEnumerable<Empleados>> Get()
        {
            return await context.Empleados
                .FromSqlInterpolated($"EXEC sp_Empleados_Get")
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Empleados empleados)
        {
            // Creamos una consulta pura para obtener el nuevo ID
            var resultado = await context.Empleados
                .FromSqlInterpolated($@"
            EXEC sp_Empleados_Insert 
            {empleados.Nombre}, {empleados.Paterno}, {empleados.Materno}, 
            {empleados.Id_Puesto}, {empleados.Id_Turno}, {empleados.Id_Departamento}, 
            {empleados.Id_Sexo}, {empleados.Id_EdoCivil}")
                .ToListAsync();

            // Actualizamos nuestro objeto con el ID generado por la base de datos
            empleados.Id_Empleado = resultado.First().Id_Empleado;

            await notificador.NotificarAlta(empleados);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Empleados empleados)
        {
            if (id != empleados.Id_Empleado)
            {
                return BadRequest("Los ids deben de coincidir");
            }

            await context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_Empleados_Update 
            {id}, {empleados.Nombre}, {empleados.Paterno}, {empleados.Materno}, 
            {empleados.Id_Puesto}, {empleados.Id_Turno}, {empleados.Id_Departamento}, 
            {empleados.Id_Sexo}, {empleados.Id_EdoCivil}");

            await notificador.NotificarModificacion(empleados);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await context.Empleados
            .FromSqlInterpolated($"EXEC sp_Empleados_Get {id}")
            .ToListAsync();

            var empleado = resultado.FirstOrDefault();

            if (empleado == null)
            {
                return NotFound();
            }

            await context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_Empleados_Delete {id}");
            return NoContent();
        }
    }
}
