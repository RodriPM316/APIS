using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogosEmpleados.Entidades
{
    public class Empleados
    {
        [Key]
        public int Id_Empleado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string? Paterno { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener entre {1} caracteres o menos")]
        public required string? Materno { get; set; }
        public int Id_Puesto { get; set; }
        public int Id_Turno { get; set; }
        public int Id_Departamento { get; set; }
        public int Id_Sexo { get; set; }
        public int Id_EdoCivil { get; set; }
    }
}
