using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

    public partial class Cliente
    {
        public int Id { get; set; }

        public string Identificacion { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido1 { get; set; } = null!;

        public string? Apellido2 { get; set; }

        public string? Correo { get; set; }

        public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
    }

