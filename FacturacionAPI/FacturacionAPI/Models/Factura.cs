using System;
using System.Collections.Generic;

namespace FacturacionAPI.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaDetalles = new HashSet<FacturaDetalle>();
        }

        public int Id { get; set; }
        public int? Numero { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? Vence { get; set; }
        public string? ModoPago { get; set; }
        public string? CondicionPago { get; set; }
        public bool? UsaComprobante { get; set; }
        public string? Ncf { get; set; }
        public string? Estado { get; set; }
        public int? ClienteId { get; set; }
        public int? CompaniaId { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa? Compania { get; set; }
        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
