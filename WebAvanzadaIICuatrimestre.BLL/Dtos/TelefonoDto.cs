using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class TelefonoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número es requerido")]
        public string Numero { get; set; } = string.Empty;

        public int Fkcliente { get; set; }
    }
}