using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Carro
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int? Fkduenno { get; set; }
}
