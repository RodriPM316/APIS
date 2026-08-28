using Api.Empleados.Datos;
using Api.Empleados.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")] // Define la ruta del controlador
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        //[HttpGet("/listado-de-autores")]
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            logger.LogTrace("Se ha ejecutado el método Get de AutoresController");
            /*logger.LogDebug("Se ha ejecutado el método Get de AutoresController");
            logger.LogInformation("Se ha ejecutado el método Get de AutoresController");
            logger.LogWarning("Se ha ejecutado el método Get de AutoresController");
            logger.LogError("Se ha ejecutado el método Get de AutoresController");
            logger.LogCritical("Se ha ejecutado el método Get de AutoresController");*/
            return await context.Autores.ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<Autor> GetPrimerAutor()
        {
            return await context.Autores.FirstAsync();
        }

        [HttpGet("{id:int}")] // api/autores/id?incluirLibros=true/false
        public async Task<ActionResult<Autor>> Get([FromRoute] int id, [FromHeader] bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet ("{nombre:alpha}")]
        public async Task<IEnumerable<Autor>> Get(string nombre)
        {
            return await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        }

        /*[HttpGet("{parametro1}/{parametro2?}")]
        public ActionResult Get(string parametro1, string parametro2 = "Valor por defecto")
        {
            return Ok(new { parametro1, parametro2 });
        }*/

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            // Aquí iría la lógica para guardar el autor en la base de datos
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("Los IDs deben coincidir");
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
