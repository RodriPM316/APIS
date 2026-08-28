using System.ComponentModel.DataAnnotations;

namespace CatalogosEmpleados.Entidades
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int RoleId { get; set; }
    }
}
