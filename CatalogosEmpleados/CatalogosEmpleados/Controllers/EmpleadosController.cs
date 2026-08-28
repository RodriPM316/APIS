using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using CatalogosEmpleados.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IEmpleadoNotificacionService notificador;

        public EmpleadosController(ApplicationDbContext context, IEmpleadoNotificacionService notificador)
        {
            this.context = context;
            this.notificador = notificador;
        }

        [HttpGet]
        public async Task<IEnumerable<Empleados>> Get()
        {
            return await context.Empleados.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Empleados empleados)
        {
            context.Add(empleados);
            await context.SaveChangesAsync();

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

            context.Update(empleados);
            await context.SaveChangesAsync();

            await notificador.NotificarModificacion(empleados);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empleados = await context.Empleados.FindAsync(id);

            var registrosBorrados = await context.Empleados.Where(x => x.Id_Empleado == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            await notificador.NotificarBaja(empleados!);

            return NoContent();
        }
    }
}