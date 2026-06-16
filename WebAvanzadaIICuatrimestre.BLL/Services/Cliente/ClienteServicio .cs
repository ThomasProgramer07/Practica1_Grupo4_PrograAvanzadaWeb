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
            respuesta.esCorrecto = true;
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto?>> GetClienteById(int id)
        {
            var respuesta = new Respuesta<ClienteDto?>();
            var entity = await _clienteRepositorio.GetClienteById(id);
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Cliente no encontrado";
<<<<<<< Updated upstream
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(entity);
=======
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<ClienteDto>(entity);
            respuesta.esCorrecto = true;
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> CrearCliente(ClienteDto clienteDto)
        {
            var respuesta = new Respuesta<ClienteDto>();
            var entidad = _mapper.Map<DAL.Entidades.Cliente>(clienteDto);
            var resultado = await _clienteRepositorio.CrearCliente(entidad);
            respuesta.Dato = _mapper.Map<ClienteDto>(resultado);
            respuesta.esCorrecto = true;
            respuesta.mensaje = "Cliente creado correctamente";
>>>>>>> Stashed changes
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente)
        {
            var respuesta = new Respuesta<ClienteDto>();
<<<<<<< Updated upstream

            if (cliente == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Cliente inválido";
                respuesta.codigo = 400;
                return respuesta;
=======
            var entity = _mapper.Map<DAL.Entidades.Cliente>(cliente);
            var exito = await _clienteRepositorio.UpdateCliente(entity);

            respuesta.esCorrecto = exito;
            respuesta.Dato = cliente;
            respuesta.mensaje = exito
                ? "Cliente actualizado correctamente"
                : "No se pudo actualizar el cliente";

            return respuesta;
        }

        public async Task<Respuesta<bool>> DeleteCliente(int id)
        {
            var resultado = new Respuesta<bool>();

            try
            {
                var exito = await _clienteRepositorio.DeleteCliente(id);
                resultado.Dato = exito;
                resultado.esCorrecto = exito;
                resultado.mensaje = exito
                    ? "Cliente eliminado correctamente."
                    : "No se encontró el cliente.";
>>>>>>> Stashed changes
            }

            var entity = _mapper.Map<DAL.Entidades.Cliente>(cliente);
            if (!await _clienteRepositorio.UpdateCliente(entity))
            {
<<<<<<< Updated upstream
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el cliente";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = cliente;
            return respuesta;
=======
                resultado.esCorrecto = false;
                resultado.mensaje = ex.Message;
            }

            return resultado;
>>>>>>> Stashed changes
        }
    }
}