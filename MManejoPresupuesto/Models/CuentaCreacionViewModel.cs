using Microsoft.AspNetCore.Mvc.Rendering;

namespace MManejoPresupuesto.Models
{
    public class CuentaCreacionViewModel : Cuenta
    {
        public IEnumerable<SelectListItem> TiposCuentas { get; set; }
    }
}
