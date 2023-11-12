using CRUDAPI_DAPPER_PROCEDURE.Models;

namespace CRUDAPI_DAPPER_PROCEDURE.DbContext
{
    public interface IResultado
    {
        IEnumerable<Resultado> ObtenerResultados();
        Resultado ObtenerResultadoPorId(int id);
        void EditarResultado(Resultado product);
        void CrearResultado(Resultado product);
        void EliminarResultado(int id);
        string ObtenerResultadoGanador();
        string ObtenerResultadoGanadorConCriterio(string misresults);

	}
}
