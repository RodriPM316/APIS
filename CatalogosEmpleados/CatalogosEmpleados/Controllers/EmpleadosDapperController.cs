using CatalogosEmpleados.Entidades;
using CatalogosEmpleados.Servicios;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CatalogosEmpleados.Controllers
{
    [Route("api/empleados-dapper")]
    [ApiController]
    public class EmpleadosDapperController : ControllerBase
    {
        private readonly string connectionString;
        private readonly IEmpleadoNotificacionService notificador;

        public EmpleadosDapperController(IConfiguration configuration, IEmpleadoNotificacionService notificador)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
            this.notificador = notificador;
        }

        [HttpGet]
        public async Task<IEnumerable<Empleados>> Get()
        {
            using var connection = new SqlConnection(connectionString);

            // QueryAsync devuelve una lista automáticamente
            var empleados = await connection.QueryAsync<Empleados>(
                "sp_Empleados_Get",
                commandType: CommandType.StoredProcedure);

            return empleados;
        }


        [HttpPost]
        public async Task<ActionResult> Post(Empleados empleado)
        {
            using var connection = new SqlConnection(connectionString);

            // QuerySingleAsync<int> porque nuestro SP hace un SELECT SCOPE_IDENTITY() devolviendo un entero
            var nuevoId = await connection.QuerySingleAsync<int>(
                "sp_Empleados_Insert",
                new
                {
                    empleado.Nombre,
                    empleado.Paterno,
                    empleado.Materno,
                    empleado.Id_Puesto,
                    empleado.Id_Turno,
                    empleado.Id_Departamento,
                    empleado.Id_Sexo,
                    empleado.Id_EdoCivil
                },
                commandType: CommandType.StoredProcedure);

            empleado.Id_Empleado = nuevoId;

            // Mantenemos tu lógica de notificaciones intacta
            await notificador.NotificarAlta(empleado);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Empleados empleado)
        {
            if (id != empleado.Id_Empleado) return BadRequest();

            using var connection = new SqlConnection(connectionString);

            // ExecuteAsync se usa cuando la base de datos no devuelve registros (filas)
            await connection.ExecuteAsync(
                "sp_Empleados_Update",
                new
                {
                    Id_Empleado = id, // Obligatorio para el WHERE del SP
                    empleado.Nombre,
                    empleado.Paterno,
                    empleado.Materno,
                    empleado.Id_Puesto,
                    empleado.Id_Turno,
                    empleado.Id_Departamento,
                    empleado.Id_Sexo,
                    empleado.Id_EdoCivil
                },
                commandType: CommandType.StoredProcedure);

            await notificador.NotificarModificacion(empleado);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);

            // Primero buscamos al empleado para el correo
            var empleado = await connection.QueryFirstOrDefaultAsync<Empleados>(
                "sp_Empleados_Get",
                new { Id_Empleado = id },
                commandType: CommandType.StoredProcedure);

            if (empleado == null) return NotFound();

            // Ejecutamos el borrado
            await connection.ExecuteAsync( 
                "sp_Empleados_Delete",
                new { Id_Empleado = id },
                commandType: CommandType.StoredProcedure);

            await notificador.NotificarBaja(empleado);

            return Ok();
        }
    }
}
