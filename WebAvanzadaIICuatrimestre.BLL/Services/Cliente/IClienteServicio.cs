using System;
using System.Collections.Generic;
using System.Text;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Cliente
{
    public interface IClienteServicio
    {
        Task<Respuesta<List<ClienteDto>>> GetClientes();
<<<<<<< Updated upstream
        Task<Respuesta<ClienteDto?>> GetClienteById(int id);
=======
        Task<Respuesta<ClienteDto>> GetClienteById(int id);
        Task<Respuesta<ClienteDto>> CrearCliente(ClienteDto clienteDto);
>>>>>>> Stashed changes
        Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente);
    }
}
