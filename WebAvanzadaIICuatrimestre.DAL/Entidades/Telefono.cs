using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public int Fkcliente { get; set; }

    public virtual Cliente? FkclienteNavigation { get; set; }
}
