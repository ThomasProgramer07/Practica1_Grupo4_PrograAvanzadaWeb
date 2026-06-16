using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La identificación es requerida")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido 1 es requerido")]
        public string Apellido1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El apellido 2 es requerido")]
        public string? Apellido2 { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        public string? Correo { get; set; }

        public List<TelefonoDto> Telefonos { get; set; } = new List<TelefonoDto>();
    }
}