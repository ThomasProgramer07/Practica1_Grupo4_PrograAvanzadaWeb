using System.Collections.Generic;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Cliente
{
    public interface IClienteServicio
    {
        Task<Respuesta<List<ClienteDto>>> GetClientes();
        Task<Respuesta<ClienteDto?>> GetClienteById(int id);
        Task<Respuesta<ClienteDto>> CrearCliente(ClienteDto clienteDto);
        Task<Respuesta<ClienteDto>> GetClienteById(int id);
        Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente);
        Task<Respuesta<bool>> DeleteCliente(int id);
    }
}
