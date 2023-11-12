using CRUDAPI_DAPPER_PROCEDURE.DbContext;
using CRUDAPI_DAPPER_PROCEDURE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI_DAPPER_PROCEDURE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadoController : ControllerBase
    {
        private readonly IResultado _Resultado;
        public ResultadoController(IResultado Resultado)
        {
            _Resultado = Resultado;
        }

        [HttpGet("Resultado/{id}")]
        public IActionResult GetProductId(int id)
        {
            if (id != null || id != 0)
            {
                var product = _Resultado.ObtenerResultadoPorId(id);
                return Ok(product);
            }
            return BadRequest();
        }
        [HttpGet("Resultados")]
        public IActionResult GetProducts()
        {
            var products = _Resultado.ObtenerResultados();
            return Ok(products);
        }
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Resultado p)
        {
            _Resultado.CrearResultado(p);

            return Ok("creado correctamente");
        }
        //[HttpPost("EditProduct")]
        //public IActionResult EditProduct(Resultado p)
        //{
        //    if(p.Id!=null || p.Id != 0)
        //    {
        //        _Resultado.EditarResultado(p);
        //        return Ok("actualizado correctamente");
        //    }
        //    return BadRequest();
        //}
        [HttpPost("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id != null || id != 0)
            {
                _Resultado.EliminarResultado(id);
                return Ok("eliminado correctamente");
            }
            else
            {
                return NotFound();
            }
        }

		[HttpGet("ResultadoGanador")]
		public IActionResult ObtenerResultadoGanador()
		{
			var products = _Resultado.ObtenerResultadoGanador();
			return Ok(products);
		}

		[HttpGet("ResultadoGanadorConCriterio")]
		public IActionResult ObtenerResultadoGanadorConCriterio(string miresult)
		{
			var products = _Resultado.ObtenerResultadoGanadorConCriterio(miresult);
			return Ok(products);
		}
	}
}
