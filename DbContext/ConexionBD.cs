using System.Data.SqlClient;

namespace CRUDAPI_DAPPER_PROCEDURE.DbContext
{
    public class ConexionBD
    {
        private readonly string _conexion;
        public ConexionBD(string conexion)
        {
            _conexion = conexion;
        }
        public SqlConnection ObtenerConexion()
        {
            var connection = new SqlConnection(_conexion);
            connection.Open();
            return connection;
        }
        
    }
}
