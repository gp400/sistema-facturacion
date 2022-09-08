using System;
using System.Collections.Generic;

namespace FacturacionAPI.Models
{
    public partial class Producto
    {
        public Producto()
        {
            FacturaDetalles = new HashSet<FacturaDetalle>();
        }

        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public int? CompaniaId { get; set; }

        public virtual Empresa? Compania { get; set; }
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
