<<<<<<< Updated upstream
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Stashed changes
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

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var respuesta = await _clienteServicio.GetClientes();
            return Json(respuesta);
        }

        [HttpGet]
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
<<<<<<< Updated upstream
=======

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ClienteDto clienteDto)
        {
            clienteDto.Telefonos = (clienteDto.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            if (!ModelState.IsValid)
            {
                return Json(new { esCorrecto = false, mensaje = "Datos inválidos" });
            }

            var respuesta = await _clienteServicio.CrearCliente(clienteDto);
            return Json(respuesta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ClienteDto clienteDto)
        {
            clienteDto.Telefonos = (clienteDto.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            if (!ModelState.IsValid)
            {
                return Json(new { esCorrecto = false, mensaje = "Datos inválidos" });
            }

            var respuesta = await _clienteServicio.UpdateCliente(clienteDto);
            return Json(respuesta);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _clienteServicio.DeleteCliente(id);
            return Json(respuesta);
        }
>>>>>>> Stashed changes
    }
}