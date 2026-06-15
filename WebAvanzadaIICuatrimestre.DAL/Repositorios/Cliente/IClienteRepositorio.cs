using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Cliente
{
    public interface IClienteRepositorio
    {
        Task<List<Entidades.Cliente>> GetClientes();
        Task<Entidades.Cliente?> GetClienteById(int id);
        Task<bool> UpdateCliente(Entidades.Cliente cliente);
    }
}