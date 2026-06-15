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

        public async Task<bool> UpdateCliente(Entidades.Cliente cliente)
        {
            if (cliente == null) return false;

            var existing = await _context.Clientes.Include(c => c.Telefonos).FirstOrDefaultAsync(c => c.Id == cliente.Id);
            if (existing == null) return false;

            existing.Identificacion = cliente.Identificacion ?? existing.Identificacion;
            existing.Nombre = cliente.Nombre ?? existing.Nombre;
            existing.Apellido1 = cliente.Apellido1 ?? existing.Apellido1;
            existing.Apellido2 = cliente.Apellido2 ?? existing.Apellido2;
            existing.Correo = cliente.Correo ?? existing.Correo;

            var telefonosActuales = existing.Telefonos.ToList();
            foreach (var telefono in telefonosActuales)
            {
                _context.Set<Entidades.Telefono>().Remove(telefono);
            }

            foreach (var telefono in cliente.Telefonos)
            {
                telefono.Fkcliente = cliente.Id;
                existing.Telefonos.Add(telefono);
            }

            _context.Clientes.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Entidades.Cliente> CrearCliente(Entidades.Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}