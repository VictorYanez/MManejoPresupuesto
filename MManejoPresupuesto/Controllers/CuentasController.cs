using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MManejoPresupuesto.Models;
using MManejoPresupuesto.Servicios;

namespace MManejoPresupuesto.Controllers
{
    public class CuentasController : Controller
    {
        private readonly IRepositorioTiposCuentas repositoriotiposCuentas;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public CuentasController(IRepositorioTiposCuentas repositoriotiposCuentas, IServiciosUsuarios serviciosUsuarios)
        {
            this.repositoriotiposCuentas = repositoriotiposCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }   
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tiposCuentas = await repositoriotiposCuentas.Obtener(usarioId);
            var modelo = new CuentaCreacionViewModel();
            modelo.TiposCuentas = tiposCuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));

            return View(modelo);    
                       
        }
    }
}
