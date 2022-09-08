using FacturacionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Empresa : ControllerBase
    {
        private readonly APIContext _APIContext = new APIContext();

        [HttpGet]
        public IEnumerable<Models.Empresa> Get()
        {
            return this._APIContext.Empresas.ToList();
        }

        [HttpGet("id/{id}")]
        public Models.Empresa GetWhereId(int id)
        {
            return this._APIContext.Empresas.Where(e => e.Id == id).FirstOrDefault();
        }

        [HttpGet("{codigo}")]
        public Models.Empresa GetWhereCodigo(string codigo)
        {
            return this._APIContext.Empresas.Where(e => e.Codigo == codigo).FirstOrDefault();
        }

        [HttpPost]
        public Dictionary<string, bool> Post([FromBody] Models.Empresa empresa)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            if (this.GetWhereCodigo(empresa.Codigo) != null)
            {
                this._APIContext.Empresas.Add(empresa);
                this._APIContext.SaveChanges();
                dic.Add("ok", true);
                return dic;
            }
            dic.Add("ok", false);
            return dic;
        }

        [HttpPut]
        public void Put([FromBody] Models.Empresa empresa)
        {
            Models.Empresa newEmpresa = this.GetWhereId(empresa.Id);
            newEmpresa.Codigo = empresa.Codigo;
            newEmpresa.Direccion = empresa.Direccion;
            newEmpresa.Itbis = empresa.Itbis;
            newEmpresa.Ncf = empresa.Ncf;
            newEmpresa.Nombre = empresa.Nombre;
            newEmpresa.Rnc = empresa.Rnc;
            newEmpresa.Telefono = empresa.Telefono;
            this._APIContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public Dictionary<string, bool> Delete(int id)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            Models.Empresa oldEmpresa = this.GetWhereId(id);
            List<Cliente> clientes = (List<Cliente>)new Clientes().Get(id);
            List<Models.Factura> facturas = (List<Models.Factura>)new Factura().Get(id);
            List<Models.Producto> productos = (List<Models.Producto>)new Producto().Get(id);
            if (clientes.Count != 0 && facturas.Count != 0 && productos.Count != 0)
            {
                dic.Add("ok", false);
                return dic;
            }
            this._APIContext.Empresas.Remove(oldEmpresa);
            this._APIContext.SaveChanges();
            dic.Add("ok", true);
            return dic;
        }
    }
}
