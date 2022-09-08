using System;
using System.Collections.Generic;

namespace FacturacionAPI.Models
{
    public partial class FacturaDetalle
    {
        public int Id { get; set; }
        public int? FacturaId { get; set; }
        public int? ProductoId { get; set; }
        public int? Cantidad { get; set; }

        public virtual Factura? Factura { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
