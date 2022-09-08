using FacturacionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Clientes : ControllerBase
    {
        private readonly APIContext _APIContext = new APIContext();

        [HttpGet("{companiaId}")]
        public IEnumerable<Cliente> Get(int companiaId)
        {
            return this._APIContext.Clientes.Where(c => c.CompaniaId == companiaId).ToList();
        }

        [HttpGet("{companiaId}/{id}")]
        public Cliente GetWhere(int companiaId, int id)
        {
            return this._APIContext.Clientes.Where(c => c.CompaniaId == companiaId && c.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public Dictionary<string, bool> Post([FromBody] Cliente cliente)
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            var clientes = ((List<Cliente>)this.Get(Convert.ToInt32(cliente.CompaniaId))).Where(c=>c.Cedula == cliente.Cedula).ToList();
            if (clientes.Count == 0)
            {
                this._APIContext.Clientes.Add(cliente);
                this._APIContext.SaveChanges();
                data.Add("ok", true);
                return data;
            }
            data.Add("ok", false);
            return data;

        }
        
        [HttpPut]
        public Dictionary<string, bool> Put([FromBody] Cliente cliente)
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            var clientes = ((List<Cliente>)this.Get(Convert.ToInt32(cliente.CompaniaId))).Where(c => c.Cedula == cliente.Cedula && c.Id != cliente.Id).ToList();
            if (clientes.Count == 0)
            {
                Cliente newCliente = this.GetWhere(Convert.ToInt32(cliente.CompaniaId), Convert.ToInt32(cliente.Id));
                newCliente.Apellido = cliente.Apellido;
                newCliente.Cedula = cliente.Cedula;
                newCliente.Correo = cliente.Correo;
                newCliente.Direccion = cliente.Direccion;
                newCliente.Nombre = cliente.Nombre;
                newCliente.Rnc = cliente.Rnc;
                newCliente.Telefono = cliente.Telefono;
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
            Cliente oldCliente = this.GetWhere(companiaId, id);
            this._APIContext.Clientes.Remove(oldCliente);
            this._APIContext.SaveChanges();
        }
    }
}
