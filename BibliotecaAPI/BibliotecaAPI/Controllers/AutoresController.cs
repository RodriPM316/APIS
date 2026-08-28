using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("")] 
    public class AutoresController: ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "autores";
        }
    }
}
