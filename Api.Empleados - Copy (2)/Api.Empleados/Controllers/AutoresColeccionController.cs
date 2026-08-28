using Api.Empleados.Datos;
using Api.Empleados.DTOs;
using Api.Empleados.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Empleados.Controllers
{
    [Route("api/autores-coleccion")]
    [ApiController]
    public class AutoresColeccionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresColeccionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{ids}", Name = "ObtenerAutoresPorIds")] //api/autores-coleccion/(1,2,3)
        public async Task<ActionResult<List<AutorConLibrosDTO>>> Get(string ids)
        {
            var idsColeccion = new List<int>();

            foreach (var id in ids.Split(','))
            {
                if (int.TryParse(id, out int idInt))
                {
                    idsColeccion.Add(idInt);

                }
            }

            if (!idsColeccion.Any())
            {
                ModelState.AddModelError(nameof(ids), "No se ha recibido ningún id válido.");
                return ValidationProblem();
            }

            var autores = await context.Autores
                .Include(x => x.Libros)
                .ThenInclude(x => x.Libro)
                .Where(x => idsColeccion.Contains(x.Id))
                .ToListAsync();

            if (autores.Count != idsColeccion.Count)
            {
                return NotFound();
            }

            var autoresDTO = mapper.Map<List<AutorConLibrosDTO>>(autores);
            return autoresDTO;
        }


        [HttpPost]
        public async Task<ActionResult> Post(IEnumerable<AutorCreacionDTO> autoresCreacionDTO)
        {
            var autores = mapper.Map<IEnumerable<Autor>>(autoresCreacionDTO);
            context.AddRange(autores);
            await context.SaveChangesAsync();

            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);
            var ids = autores.Select(x => x.Id);
            var idsString = string.Join(",", ids);
            return CreatedAtRoute("ObtenerAutoresPorIds", new { ids = idsString }, autoresDTO);

            return Ok();
        }
    }
}
