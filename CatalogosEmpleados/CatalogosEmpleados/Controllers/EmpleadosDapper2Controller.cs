using CatalogosEmpleados.Entidades;
using CatalogosEmpleados.Servicios;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/empleados-dapper2")]
    [ApiController]
    public class EmpleadosDapper2Controller : ControllerBase
    {
        private string connectionString;
        private readonly IEmpleadoNotificacionService notificador;

        public EmpleadosDapper2Controller(IConfiguration configuration, IEmpleadoNotificacionService notificador)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
            this.notificador = notificador;
        }

        [HttpGet]
        public async Task<IEnumerable<Empleados>> Get()
        {
            using var connection = new SqlConnection(connectionString);

            // Consulta SQL pura
            string sql = "SELECT * FROM Empleados";

            // Ya no es necesario especificar CommandType.StoredProcedure
            return await connection.QueryAsync<Empleados>(sql);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Empleados empleado)
        {
            using var connection = new SqlConnection(connectionString);

            string sql = @"
        INSERT INTO Empleados (Nombre, Paterno, Materno, Id_Puesto, Id_Turno, Id_Departamento, Id_Sexo, Id_EdoCivil)
        VALUES (@Nombre, @Paterno, @Materno, @Id_Puesto, @Id_Turno, @Id_Departamento, @Id_Sexo, @Id_EdoCivil);
        
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            // Como las propiedades del objeto 'empleado' se llaman igual que los parámetros @, 
            // puedes pasar el objeto completo directamente
            var nuevoId = await connection.QuerySingleAsync<int>(sql, empleado);

            empleado.Id_Empleado = nuevoId;

            await notificador.NotificarAlta(empleado);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Empleados empleado)
        {
            if (id != empleado.Id_Empleado) return BadRequest();

            using var connection = new SqlConnection(connectionString);

            string sql = @"
        UPDATE Empleados 
        SET Nombre = @Nombre, 
            Paterno = @Paterno, 
            Materno = @Materno, 
            Id_Puesto = @Id_Puesto, 
            Id_Turno = @Id_Turno, 
            Id_Departamento = @Id_Departamento, 
            Id_Sexo = @Id_Sexo, 
            Id_EdoCivil = @Id_EdoCivil
        WHERE Id_Empleado = @Id_Empleado";

            await connection.ExecuteAsync(sql, empleado);
            await notificador.NotificarModificacion(empleado);

            return Ok();
        }

        [HttpPut("put/{id:int}")]
        public async Task<ActionResult> Putin(int id, Employees employees)
        {
            if (id != employees.EmployeeId) return BadRequest();

            using var connection = new SqlConnection(connectionString);

            string sql = @"UPDATE Empleados 
                   SET Nombre = @Nombre, 
                       Paterno = @Paterno, 
                       Id_Puesto = @Id_Puesto 
                   WHERE Id_Empleado = @Id_Empleado";

            // Creamos un objeto anónimo donde las propiedades coinciden con los @parametros
            var parametros = new
            {
                Nombre = employees.FirstName,
                Paterno = employees.LastName,
                Id_Puesto = employees.RoleId,
                Id_Empleado = id // Podemos usar el id que viene en la ruta directamente
            };

            await connection.ExecuteAsync(sql, parametros);
            // await notificador.NotificarModificacion(empleado);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);

            // Reutilizamos la lógica pura para buscar al empleado antes de borrarlo
            var empleado = await connection.QueryFirstOrDefaultAsync<Empleados>(
                "SELECT * FROM Empleados WHERE Id_Empleado = @Id_Empleado",
                new { Id_Empleado = id });

            if (empleado == null) return NotFound();

            // Ejecutamos la eliminación
            string sql = "DELETE FROM Empleados WHERE Id_Empleado = @Id_Empleado";
            await connection.ExecuteAsync(sql, new { Id_Empleado = id });

            await notificador.NotificarBaja(empleado);
            return Ok();
        }
    }
}
