using Microsoft.EntityFrameworkCore;

namespace Api.Empleados.Entidades
{
    [PrimaryKey(nameof(AutorID), nameof(LibroID))]
    public class AutorLibro
    {
        public int AutorID { get; set; }
        public int LibroID { get; set; }
        public int Orden { get; set; }
        public Autor? Autor { get; set; }
        public Libro? Libro { get; set; }


    }
}
