using System;
using System.Collections.Generic;
using System.Text;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Cliente
{
    public interface IClienteServicio
    {
        Task<Respuesta<List<ClienteDto>>> GetClientes();
        Task<Respuesta<ClienteDto?>> GetClienteById(int id);
        Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente);
    }
}
