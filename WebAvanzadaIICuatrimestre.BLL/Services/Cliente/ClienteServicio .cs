using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente)
        {
            var respuesta = new Respuesta<ClienteDto>();

            if (cliente == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Cliente inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            var entity = _mapper.Map<DAL.Entidades.Cliente>(cliente);
            if (!await _clienteRepositorio.UpdateCliente(entity))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el cliente";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = cliente;
            return respuesta;
        }
    }
}