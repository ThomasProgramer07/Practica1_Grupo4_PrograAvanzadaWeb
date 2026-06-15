using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Cliente
{
    public interface IClienteRepositorio
    {
        Task<List<Entidades.Cliente>> GetClientes();
        Task<Entidades.Cliente?> GetClienteById(int id);
        Task<bool> UpdateCliente(Entidades.Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}