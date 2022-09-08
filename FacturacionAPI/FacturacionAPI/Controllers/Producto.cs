using FacturacionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Producto : ControllerBase
    {
        private readonly APIContext _APIContext = new APIContext();

        [HttpGet("{companiaId}")]
        public IEnumerable<Models.Producto> Get(int companiaId)
        {
            return this._APIContext.Productos.Where(c => c.CompaniaId == companiaId).ToList();
        }

        [HttpGet("{companiaId}/{id}")]
        public Models.Producto GetWhere(int companiaId, int id)
        {
            return this._APIContext.Productos.Where(c => c.CompaniaId == companiaId && c.Id == id).FirstOrDefault();
        }
        
        [HttpGet("id/{id}")]
        public Models.Producto GetWhere(int id)
        {
            return this._APIContext.Productos.Where(c => c.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public Dictionary<string, bool> Post([FromBody] Models.Producto producto)
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            var productos = this.Get(Convert.ToInt32(producto.CompaniaId)).Where(p => p.Codigo == producto.Codigo).ToList();
            if (productos.Count == 0)
            {
                this._APIContext.Productos.Add(producto);
                this._APIContext.SaveChanges();
                data.Add("ok", true);
                return data;
            }
            data.Add("ok", false);
            return data;
        }

        [HttpPut]
        public Dictionary<string, bool> Put([FromBody] Models.Producto producto)
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            var productos = this.Get(Convert.ToInt32(producto.CompaniaId)).Where(p => p.Codigo == producto.Codigo && p.Id != producto.Id).ToList();
            if (productos.Count == 0)
            {
                Models.Producto newProducto = this.GetWhere(Convert.ToInt32(producto.CompaniaId), Convert.ToInt32(producto.Id));
                newProducto.Cantidad = producto.Cantidad;
                newProducto.Codigo = producto.Codigo;
                newProducto.Nombre = producto.Nombre;
                newProducto.Precio = producto.Precio;
                this._APIContext.SaveChanges();
                data.Add("ok", true);
                return data;
            }
            data.Add("ok", false);
            return data;
        }

        [HttpDelete("{companiaId}/{id}")]
        public void Delete(int companiaId, int id)
        {
            Models.Producto oldProducto = this.GetWhere(companiaId, id);
            this._APIContext.Productos.Remove(oldProducto);
            this._APIContext.SaveChanges();
        }
    }
}
