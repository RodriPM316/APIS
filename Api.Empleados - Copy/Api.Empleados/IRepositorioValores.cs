using Api.Empleados.Entidades;

namespace Api.Empleados
{
    public interface IRepositorioValores
    {
        void InsertarValor(Valor valor);
        IEnumerable<Valor> ObtenerValores();
    }
}
