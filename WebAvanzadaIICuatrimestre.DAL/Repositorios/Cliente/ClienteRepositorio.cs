using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.DAL.Data;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Cliente
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entidades.Cliente>> GetClientes()
        {
            return await _context.Clientes.Include(c => c.Telefonos).ToListAsync();
        }

        public async Task<Entidades.Cliente?> GetClienteById(int id)
        {
            return await _context.Clientes.Include(c => c.Telefonos).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}