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

        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido1 { get; set; } = string.Empty;

        public string? Apellido2 { get; set; }

        public string? Correo { get; set; }

        public List<TelefonoDto> Telefonos { get; set; } = new List<TelefonoDto>();
    }
}