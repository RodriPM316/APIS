using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Correos
    {
        [Key]
        public int Id_Correo { get; set; }
        public int Id_TipoCorreo { get; set; }
        public int Id_Area { get; set; }
    }
}
