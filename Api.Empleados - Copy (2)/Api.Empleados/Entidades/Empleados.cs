using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Empleados.Entidades
{
    public class Empleados
    {
        public int Id_Empleado { get; set; }
        public string? Nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public int Id_Puesto { get; set; }
        public int Id_Turno { get; set; }
        public int Id_Departamento { get; set; }
        public int Id_Sexo { get; set; }
        public int Id_EdoCivil { get; set; }
        [NotMapped]
        public string? Rodri { get; set; }
    }
}
