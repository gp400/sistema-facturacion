using System;
using System.Collections.Generic;

namespace FacturacionAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Rnc { get; set; }
        public int? CompaniaId { get; set; }

        public virtual Empresa? Compania { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
