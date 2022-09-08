using System;
using System.Collections.Generic;

namespace ProgWeb.Models
{
    public partial class Integrante
    {
        public int Id { get; set; }
        public string? Documento { get; set; }
        public string? Foto { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Sexo { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? EstadoCivil { get; set; }
        public string? TipoSangre { get; set; }
        public string? Alergias { get; set; }
        public string? Comentario { get; set; }
        public int? Estado { get; set; }
    }
}
