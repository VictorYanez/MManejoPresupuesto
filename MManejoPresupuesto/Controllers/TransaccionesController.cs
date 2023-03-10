using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MManejoPresupuesto.Models;
using MManejoPresupuesto.Servicios;

namespace MManejoPresupuesto.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IServiciosUsuarios servicioUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;

        public TransaccionesController(IServiciosUsuarios servicioUsuarios, IRepositorioCuentas repositorioCuentas)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioCuentas = repositorioCuentas;
        }

        public async Task<IActionResult> Crear() 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var modelo = new TransaccionCreacionViewModel();
            modelo.Cuentas = await ObtenerCuentas(usuarioId);
            return View(modelo);

        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCuentas(int usuarioId) 
        {
            var cuentas = await repositorioCuentas.Buscar(usuarioId);
            return cuentas.Select(x=> new SelectListItem(x.Nombre, x.Id.ToString()));
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Semanal()
        {
            return View();
        }

        public IActionResult Mensual()
        {
            return View();
        }

        public IActionResult ExcelReporte()
        {
            return View();
        }

        public IActionResult Calendario()
        {
            return View();
        }


        /*
        public async Task<JsonResult> ObtenerTransaccionesCalendario(DateTime start, DateTime end) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var transacciones = await RepositorioTransacciones.ObtenerPorUsuarioId(
                new ParametroObtenerTransaccionesPorUsuario
                {
                    usuarioId = usuarioId,
                    FechaInicio = start,
                    FechaOuticio = end
                });

       
        }
         */
    }
}
