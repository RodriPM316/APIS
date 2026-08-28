using CatalogosEmpleados.Datos;
using CatalogosEmpleados.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/departamentos")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartamentosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Departamentos>> Get() 
        {
            return await context.Departamentos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Departamentos departamentos)
        {
            context.Add(departamentos);
            await context.SaveChangesAsync();
            return Ok();    
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Departamentos departamentos) 
        {
            if (id != departamentos.Id_Departamento)
            {
                return BadRequest("Los ids deben de coincidir");
            }

            context.Update(departamentos);
            await context.SaveChangesAsync(); 
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        { 
            var registrosBorrados = await context.Departamentos.Where(x => x.Id_Departamento == id).ExecuteDeleteAsync();

            if(registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
