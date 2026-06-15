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
            respuesta.Data = _mapper.Map<List<ClienteDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> GetClienteById(int id)
        {
            var respuesta = new Respuesta<ClienteDto>();
            var entity = await _clienteRepositorio.GetClienteById(id);
            respuesta.Data = _mapper.Map<ClienteDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<ClienteDto>> UpdateCliente(ClienteDto cliente)
        {
            var respuesta = new Respuesta<ClienteDto>();
            var entity = _mapper.Map<DAL.Entidades.Cliente>(cliente);
            var exito = await _clienteRepositorio.UpdateCliente(entity);
            if (exito)
            {
                respuesta.Data = cliente;
            }
            return respuesta;
        }

        public async Task<Respuesta<bool>> DeleteCliente(int id)
        {
            var resultado = new Respuesta<bool>();
            try
            {
                var exito = await _clienteRepositorio.DeleteCliente(id);
                resultado.Data = exito;
                resultado.EsExitoso = exito;
                resultado.Mensaje = exito ? "Cliente eliminado correctamente." : "No se encontró el cliente.";
            }
            catch (Exception ex)
            {
                resultado.EsExitoso = false;
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}