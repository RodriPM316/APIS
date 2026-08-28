using Api.Empleados.Entidades;

namespace Api.Empleados
{
    public class RepositorioValores: IRepositorioValores
    {
        public void InsertarValor(Valor valor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Valor> ObtenerValores()
        {
            return new List<Valor>
            {
                new Valor { Id = 1, Nombre = "Valor 1" },
                new Valor { Id = 2, Nombre = "Valor 2" },
            };
        }
    }
}
