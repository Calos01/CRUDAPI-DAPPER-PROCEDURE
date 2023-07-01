using CRUDAPI_DAPPER_PROCEDURE.Models;
using Dapper;
using System.Data;

namespace CRUDAPI_DAPPER_PROCEDURE.DbContext
{
    public class ProductoRepository : IProducto
    {
        private readonly ConexionBD _conexionBD;
        public ProductoRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public void CrearProducto(Producto product)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Nombre", product.Nombre,DbType.String);
                parameters.Add("Precio", product.Precio,DbType.Decimal);
                con.Execute("InsertarProducto",parameters,commandType:CommandType.StoredProcedure);
            }
        }

        public void EditarProducto(Producto product)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", product.Id, DbType.Int32);
                parameters.Add("Nombre", product.Nombre, DbType.String);
                parameters.Add("Precio", product.Precio, DbType.Decimal);
                con.Execute("ActualizarProducto", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void EliminarProducto(int id)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);
                
                con.Execute("EliminarProducto", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Producto ObtenerProductoPorId(int id)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);

                return con.QueryFirstOrDefault<Producto>("ObtenerProductoPorId", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Producto> ObtenerProductos()
        {
            using (var con= _conexionBD.ObtenerConexion())
            {
                return  con.Query<Producto>("ObtenerProductos", commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
