using System;
using System.Collections.Generic;

namespace FacturacionAPI.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Clientes = new HashSet<Cliente>();
            Facturas = new HashSet<Factura>();
            Productos = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int? Itbis { get; set; }
        public string? Rnc { get; set; }
        public string? Ncf { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
