using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Cliente;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteServicio(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<Respuesta<List<ClienteDto>>> GetClientes()
        {
            var respuesta = new Respuesta<List<ClienteDto>>();
            var list = await _clienteRepositorio.GetClientes();
            respuesta.Dato = _mapper.Map<List<ClienteDto>>(list);
            respuesta.esCorrecto = true;
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto?>> GetClienteById(int id)
        {
            var respuesta = new Respuesta<ClienteDto?>();
            var entity = await _clienteRepositorio.GetClienteById(id);

            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Cliente no encontrado";
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(entity);
            respuesta.esCorrecto = true;
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> CrearCliente(ClienteDto clienteDto)
        {
            var respuesta = new Respuesta<ClienteDto>();
            try
            {
                var entidad = _mapper.Map<DAL.Entidades.Cliente>(clienteDto);
                var resultado = await _clienteRepositorio.CrearCliente(entidad);
                respuesta.Dato = _mapper.Map<ClienteDto>(resultado);
                respuesta.esCorrecto = true;
                respuesta.mensaje = "Cliente creado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = $"Error al crear cliente: {ex.Message}";
            }
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente)
        {
            var respuesta = new Respuesta<ClienteDto>();
            try
            {
                var entity = _mapper.Map<DAL.Entidades.Cliente>(cliente);
                var exito = await _clienteRepositorio.UpdateCliente(entity);

                respuesta.esCorrecto = exito;
                respuesta.Dato = cliente;
                respuesta.mensaje = exito
                    ? "Cliente actualizado correctamente"
                    : "No se pudo actualizar el cliente";
            }
            catch (Exception ex)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = $"Error al actualizar cliente: {ex.Message}";
            }
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> DeleteCliente(int id)
        {
            var respuesta = new Respuesta<ClienteDto>();
            try
            {
                var exito = await _clienteRepositorio.DeleteCliente(id);
                respuesta.esCorrecto = exito;
                respuesta.mensaje = exito
                    ? "Cliente eliminado correctamente"
                    : "No se encontró el cliente";
            }
            catch (Exception ex)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = $"Error al eliminar cliente: {ex.Message}";
            }
            return respuesta;
        }
    }
}