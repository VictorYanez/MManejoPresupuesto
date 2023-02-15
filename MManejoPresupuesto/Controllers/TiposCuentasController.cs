using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;
using MManejoPresupuesto.Servicios;

namespace MManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }

        public async Task<IActionResult> Index() 
        {
            var usuarioId = 1;
            var tiposCuentas = await this.repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);

        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Crear(TipoCuenta tipoCuenta)
        {

            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }

            tipoCuenta.UsuarioId = 1;
            await repositorioTiposCuentas.Crear(tipoCuenta);

            return RedirectToAction("Index");

        }
    }
}
