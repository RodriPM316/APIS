using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Empleados.Controllers
{
    [Route("api/configuraciones")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection seccion_01;
        private readonly IConfigurationSection seccion_02;
        private readonly PersonaOpciones _opcionesPersona;
        private readonly PagosProcesamiento pagosProcesamiento;


        public ConfiguracionesController(IConfiguration configuration, 
            IOptionsSnapshot<PersonaOpciones> opcionesPersona, PagosProcesamiento pagosProcesamiento)
        {
            this.configuration = configuration;
            seccion_01 = configuration.GetSection("seccion_1");
            seccion_02 = configuration.GetSection("seccion_2");
            _opcionesPersona = opcionesPersona.Value;
            this.pagosProcesamiento = pagosProcesamiento;
        }

        [HttpGet("options-monitor")]
        public ActionResult GetTarifas()
        {
            return Ok(pagosProcesamiento.ObtenerTarifas());
        }

        [HttpGet("seccion_1_opciones")]
        public ActionResult GetSeccion1Opciones()
        {
            return Ok(_opcionesPersona);
        }

        [HttpGet("proveedores")]
        public IActionResult GetProveedor()
        {
            var valor = configuration.GetValue<string>("quien_soy");
            return Ok(new { valor });
        }

        [HttpGet("obtenertodos")]
        public IActionResult GetObtenerTodos()
        {
            var hijos = seccion_02.GetChildren().Select(x => $"{x.Key}: {x.Value}");
            return Ok(new { hijos });
        }

        [HttpGet("seccion_01")]
        public ActionResult GetSeccion_01()
        {
            var nombre = seccion_01.GetValue<string>("nombre");
            var edad = seccion_01.GetValue<int>("edad");
            return Ok(new { nombre, edad });
        }

        [HttpGet("seccion_02")]
        public ActionResult GetSeccion_02()
        {
            var nombre = seccion_02.GetValue<string>("nombre");
            var edad = seccion_02.GetValue<int>("edad");
            return Ok(new { nombre, edad });
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var opcion1 = configuration["apellido"];

            var opcion2 = configuration.GetValue<string>("apellido")!;

            return opcion2;
        }

        [HttpGet("secciones")]
        public ActionResult<string> GetSeccion()
        {
            var opcion1 = configuration["ConnectionStrings:DefaultConnection"]; 

            var opcion2 = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            var seccion = configuration.GetSection("ConnectionStrings");

            var opcion3 = seccion["DefaultConnection"];

            return opcion3;
        }
    }
}
