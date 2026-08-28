namespace Api.Empleados.DTOs
{
    public class LibroConAutoresDTO: LibroDTO
    {
        public List<AutorDTO> Autores { get; set; } = [];
    }
}
