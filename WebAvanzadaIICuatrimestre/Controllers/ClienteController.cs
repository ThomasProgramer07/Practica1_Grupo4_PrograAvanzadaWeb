using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Cliente;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetClientes()
        {
            var respuesta = await _clienteServicio.GetClientes();
            return Json(respuesta);
        }

        public async Task<IActionResult> GetClienteById(int id)
        {
            var respuesta = await _clienteServicio.GetClienteById(id);
            return Json(respuesta);
        }

        public IActionResult Detalle(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteDto clienteDto)
        {
            clienteDto.Telefonos = (clienteDto.Telefonos ?? new List<TelefonoDto>()).Where(t => !string.IsNullOrWhiteSpace(t.Numero)).ToList();
            if (!ModelState.IsValid)
                return Json(new { esCorrecto = false, mensaje = "Datos inválidos" });

            var respuesta = await _clienteServicio.CrearCliente(clienteDto);
            return Json(new
            {
                esCorrecto = respuesta.esCorrecto,
                mensaje = respuesta.mensaje
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _clienteServicio.DeleteCliente(id);
            return Json(respuesta);
        }
    }
}