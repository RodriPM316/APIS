using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/puestos")]
    [ApiController]
    public class PuestosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PuestosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Puestos>> Get() 
        {
            return await context.Puestos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Puestos puestos) 
        {
            context.Add(puestos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Puestos puestos) 
        { 
            if (id != puestos.Id_Puesto)
            {
                return BadRequest("Los ids deben coincidir");
            }

            context.Update(puestos);
            await context.SaveChangesAsync(); 
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Puestos.Where(x => x.Id_Puesto == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
