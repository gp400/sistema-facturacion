using FacturacionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacturaDetalle : ControllerBase
    {
        private readonly APIContext _APIContext = new APIContext();

        [HttpGet("{facturaId}")]
        public IEnumerable<Models.FacturaDetalle> Get(int facturaId)
        {
            return this._APIContext.FacturaDetalles.Where(c => c.FacturaId == facturaId).ToList();
        }

        [HttpGet("{facturaId}/{id}")]
        public Models.FacturaDetalle GetWhere(int facturaId, int id)
        {
            return this._APIContext.FacturaDetalles.Where(c => c.FacturaId == facturaId && c.Id == id).FirstOrDefault();
        }

        [HttpGet("productos/{productoId}")]
        public List<Models.FacturaDetalle> GetProductosWhere(int productoId)
        {
            return this._APIContext.FacturaDetalles.Where(c => c.ProductoId == productoId).ToList();
        }

        [HttpPost]
        public void Post([FromBody] List<Models.FacturaDetalle> facturaDetalle)
        {
            this._APIContext.FacturaDetalles.AddRange(facturaDetalle);
            this._APIContext.SaveChanges();
        }

        [HttpDelete("{facturaId}")]
        public void Delete(int facturaId)
        {
            IEnumerable<Models.FacturaDetalle> oldFacturaDetalles = this.Get(facturaId);
            foreach (var oldFacturaDetalle in oldFacturaDetalles)
            {
                var pClass = new Producto();
                var producto = pClass.GetWhere(Convert.ToInt32(oldFacturaDetalle.ProductoId));
                producto.Cantidad = producto.Cantidad + oldFacturaDetalle.Cantidad;
                pClass.Put(producto);
                this._APIContext.FacturaDetalles.Remove(oldFacturaDetalle);
            }
            this._APIContext.SaveChanges();
        }
    }
}
