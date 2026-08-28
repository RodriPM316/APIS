namespace Api.Empleados.DTOs
{
    public class AutorConLibrosDTO: AutorDTO
    {
        public List<LibroDTO> Libros { get; set; } = [];
    }
}
