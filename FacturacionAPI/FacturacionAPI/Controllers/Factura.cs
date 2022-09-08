using FacturacionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Factura : ControllerBase
    {
        private readonly APIContext _APIContext = new APIContext();

        [HttpGet("{companiaId}")]
        public IEnumerable<Models.Factura> Get(int companiaId)
        {
            return this._APIContext.Facturas.Where(c => c.CompaniaId == companiaId).ToList();
        }

        [HttpGet("{companiaId}/{id}")]
        public Models.Factura GetWhere(int companiaId, int id)
        {
            var factura = this._APIContext.Facturas.Where(c => c.CompaniaId == companiaId && c.Id == id).FirstOrDefault();
            if (factura != null)
                factura.Cliente = new Clientes().GetWhere(companiaId, Convert.ToInt32(factura.ClienteId));
            return factura;
        }

        [HttpGet("clientes/{companiaId}/{clienteId}")]
        public List<Models.Factura> GetClientesWhere(int companiaId, int clienteId)
        {
            return this._APIContext.Facturas.Where(c => c.CompaniaId == companiaId && c.ClienteId == clienteId).ToList();
        }

        [HttpPost]
        public List<string> Post([FromBody] Models.Factura factura)
        {
            List<string> errores = new List<string>();
            var facturas = this.Get(Convert.ToInt32(factura.CompaniaId)).Where(f=>f.Numero == factura.Numero).ToList();
            if (facturas.Count == 0)
            {
                foreach(var detalle in factura.FacturaDetalles)
                {
                    var producto = new Producto().GetWhere(Convert.ToInt32(factura.CompaniaId), Convert.ToInt32(detalle.ProductoId));
                    if (producto.Cantidad < detalle.Cantidad)
                    {
                        detalle.ProductoId = producto.Id;
                        detalle.Producto = producto;
                        errores.Add($"La cantidad en stock de {producto.Nombre} es menor a la cantidad seleccionada");
                    }
                }
                if (errores.Count == 0)
                {
                    foreach (var detalle in factura.FacturaDetalles)
                    {
                        var producto = new Producto().GetWhere(Convert.ToInt32(factura.CompaniaId), Convert.ToInt32(detalle.ProductoId));
                        producto.Cantidad = producto.Cantidad - detalle.Cantidad;
                        new Producto().Put(producto);
                    }
                    this._APIContext.Facturas.Add(factura);
                    this._APIContext.SaveChanges();
                }
                return errores;
            }
            errores.Add("-1");
            return errores;
        }

        [HttpPut]
        public List<string> Put([FromBody] Models.Factura factura)
        {
            List<string> errores = new List<string>();
            var facturas = this.Get(Convert.ToInt32(factura.CompaniaId)).Where(f => f.Numero == factura.Numero && f.Id != factura.Id).ToList();
            if (facturas.Count == 0)
            {
                foreach (var detalle in factura.FacturaDetalles)
                {
                    var producto = new Producto().GetWhere(Convert.ToInt32(factura.CompaniaId), Convert.ToInt32(detalle.ProductoId));
                    if (producto.Cantidad < detalle.Cantidad)
                    {
                        detalle.ProductoId = producto.Id;
                        detalle.Producto = producto;
                        errores.Add($"La cantidad en stock de {producto.Nombre} es menor a la cantidad seleccionada");
                    }
                }
                if (errores.Count == 0)
                {
                    foreach (var detalle in factura.FacturaDetalles)
                    {
                        var producto = new Producto().GetWhere(Convert.ToInt32(factura.CompaniaId), Convert.ToInt32(detalle.ProductoId));
                        producto.Cantidad = producto.Cantidad - detalle.Cantidad;
                        detalle.FacturaId = factura.Id;
                        detalle.Id = 0;
                        new Producto().Put(producto);
                    }
                    new FacturaDetalle().Delete(factura.Id);
                    new FacturaDetalle().Post(factura.FacturaDetalles.ToList());
                    Models.Factura newFactura = this.GetWhere(Convert.ToInt32(factura.CompaniaId), Convert.ToInt32(factura.Id));
                    newFactura.ClienteId = factura.ClienteId;
                    newFactura.CompaniaId = factura.CompaniaId;
                    newFactura.CondicionPago = factura.CondicionPago;
                    newFactura.Fecha = factura.Fecha;
                    newFactura.ModoPago = factura.ModoPago;
                    newFactura.Ncf = factura.Ncf;
                    newFactura.Estado = factura.Estado;
                    newFactura.Numero = factura.Numero;
                    newFactura.UsaComprobante = factura.UsaComprobante;
                    newFactura.Vence = factura.Vence;
                    this._APIContext.SaveChanges();
                }
                return errores;
            }
            errores.Add("-1");
            return errores;
        }

        [HttpDelete("{companiaId}/{id}")]
        public void Delete(int companiaId, int id)
        {
            Models.Factura oldFactura = this.GetWhere(companiaId, id);
            if (oldFactura != null)
            {
                try
                {
                    new FacturaDetalle().Delete(oldFactura.Id);
                    this._APIContext.Facturas.Remove(oldFactura);
                    this._APIContext.SaveChanges();
                } catch (Exception ex)
                {

                }
            }
        }
    }
}
