using CRUDAPI_DAPPER_PROCEDURE.Models;
using Dapper;
using System.Data;

namespace CRUDAPI_DAPPER_PROCEDURE.DbContext
{
    public class ResultadoRepository : IResultado
    {
        private readonly ConexionBD _conexionBD;
        public ResultadoRepository(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public void CrearResultado(Resultado product)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Nombre", product.Nombre,DbType.String);
                con.Execute("InsertarResultado",parameters,commandType:CommandType.StoredProcedure);
            }
        }

        public void EditarResultado(Resultado product)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                //parameters.Add("Id", product.Id, DbType.Int32);
                parameters.Add("Nombre", product.Nombre, DbType.String);
                con.Execute("ActualizarResultado", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void EliminarResultado(int id)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);
                
                con.Execute("EliminarResultado", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Resultado ObtenerResultadoPorId(int id)
        {
            using (var con = _conexionBD.ObtenerConexion())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int32);

                return con.QueryFirstOrDefault<Resultado>("ObtenerResultadoPorId", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Resultado> ObtenerResultados()
        {
            using (var con= _conexionBD.ObtenerConexion())
            {
                return  con.Query<Resultado>("ObtenerResultados", commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public string ObtenerResultadoGanador()
        {
			List<Resultado> res;
			using (var con = _conexionBD.ObtenerConexion())
			{
				res = con.Query<Resultado>("ObtenerResultados", commandType: CommandType.StoredProcedure).ToList();
				return GenerarCadenaAleatoriaDiferente(res.Select(r => r.Nombre).ToArray());
			}

		   
			static string GenerarCadenaAleatoriaDiferente(string[] cadenasOriginales)
			{
				Random random = new Random();

				while (true)
				{
					// Generar una cadena aleatoria de la misma longitud que las originales
					string cadenaGenerada = new string(Enumerable.Repeat("LVE", cadenasOriginales[0].Length)
						.Select(s => s[random.Next(s.Length)]).ToArray());

                    if (CumpleRequisitosMinimos(cadenaGenerada))
                    {
					    // Verificar si la cadena generada es diferente de todas las originales
					    if (!cadenasOriginales.Any(c => c == cadenaGenerada))
					    {
						    return cadenaGenerada;
					    }
                    }

				}
			}

			static bool CumpleRequisitosMinimos(string cadenaGenerada)
			{
				// Verificar si la cadena contiene al menos dos letras "L", dos letras "E" y dos letras "V"
				int countL = cadenaGenerada.Count(c => c == 'L');
				int countE = cadenaGenerada.Count(c => c == 'E');
				int countV = cadenaGenerada.Count(c => c == 'V');

				return countL >= 2 && countE >= 2 && countV >= 2;
			}
		}

		public string ObtenerResultadoGanadorConCriterio(string misresults)
		{
			List<Resultado> res;
			using (var con = _conexionBD.ObtenerConexion())
			{
				res = con.Query<Resultado>("ObtenerResultados", commandType: CommandType.StoredProcedure).ToList();
				return GenerarCadenaAleatoriaDiferente(res.Select(r => r.Nombre).ToArray(), misresults);
			}


			static string GenerarCadenaAleatoriaDiferente(string[] cadenasOriginales, string misresults)
			{
				Random random = new Random();

				while (true)
				{
					// Generar una cadena aleatoria de la misma longitud que las originales
					string cadenaGenerada = new string(Enumerable.Repeat("LVE", cadenasOriginales[0].Length)
						.Select(s => s[random.Next(s.Length)]).ToArray());

					if (CumpleRequisitosMinimos(cadenaGenerada) && CumpleRequisitosCriterio(cadenaGenerada, misresults))
					{
						// Verificar si la cadena generada es diferente de todas las originales
						if (!cadenasOriginales.Any(c => c == cadenaGenerada))
						{
							return cadenaGenerada;
						}
					}

				}
			}

			static bool CumpleRequisitosMinimos(string cadenaGenerada)
			{
				// Verificar si la cadena contiene al menos dos letras "L", dos letras "E" y dos letras "V"
				int countL = cadenaGenerada.Count(c => c == 'L');
				int countE = cadenaGenerada.Count(c => c == 'E');
				int countV = cadenaGenerada.Count(c => c == 'V');

				return countL >= 2 && countE >= 2 && countV >= 2;
			}

			static bool CumpleRequisitosCriterio(string cadenaGenerada, string misresults)
			{
				// Verificar si hay más de seis letras iguales en la misma posición que las originales
				int maxRepetidas = 10;
	
				int countRepetidas = cadenaGenerada
					.Zip(misresults, (c1, c2) => c1 == c2)
					.Count(isSame => isSame);

				if (countRepetidas <= maxRepetidas)
				{
					return true;
				}

				return false;
			}
		}
	}
}
