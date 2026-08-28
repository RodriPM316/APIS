using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/turnos")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TurnosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Turnos>> Get()
        {
            return await context.Turnos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Turnos turnos)
        {
            context.Turnos.Add(turnos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Turnos turnos) 
        {
            if(id != turnos.Id_Turno)
            {
                return BadRequest("Los ids deben coincidir");
            }

            context.Update(turnos);
            await context.SaveChangesAsync(); 
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Turnos.Where(x => x.Id_Turno == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
