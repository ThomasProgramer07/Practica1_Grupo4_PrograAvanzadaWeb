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

        public async Task<IActionResult> UpdateCliente(ClienteDto cliente)
        {
            var respuesta = await _clienteServicio.UpdateCliente(cliente);
            return Json(respuesta);
        }

        public IActionResult Detalle(int id)
        {
            return View();
        }
    }
}