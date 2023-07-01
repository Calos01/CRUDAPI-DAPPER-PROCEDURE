using CRUDAPI_DAPPER_PROCEDURE.DbContext;
using CRUDAPI_DAPPER_PROCEDURE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI_DAPPER_PROCEDURE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _producto;
        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }

        [HttpGet("Producto/{id}")]
        public IActionResult GetProductId(int id)
        {
            if (id != null || id != 0)
            {
                var product = _producto.ObtenerProductoPorId(id);
                return Ok(product);
            }
            return BadRequest();
        }
        [HttpGet("Productos")]
        public IActionResult GetProducts()
        {
            var products = _producto.ObtenerProductos();
            return Ok(products);
        }
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Producto p)
        {
            _producto.CrearProducto(p);

            return Ok("creado correctamente");
        }
        [HttpPost("EditProduct")]
        public IActionResult EditProduct(Producto p)
        {
            if(p.Id!=null || p.Id != 0)
            {
                _producto.EditarProducto(p);
                return Ok("actualizado correctamente");
            }
            return BadRequest();
        }
        [HttpPost("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id != null || id != 0)
            {
                _producto.EliminarProducto(id);
                return Ok("eliminado correctamente");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
