using CRUDAPI_DAPPER_PROCEDURE.Models;

namespace CRUDAPI_DAPPER_PROCEDURE.DbContext
{
    public interface IProducto
    {
        IEnumerable<Producto> ObtenerProductos();
        Producto ObtenerProductoPorId(int id);
        void EditarProducto(Producto product);
        void CrearProducto(Producto product);
        void EliminarProducto(int id);
    }
}
