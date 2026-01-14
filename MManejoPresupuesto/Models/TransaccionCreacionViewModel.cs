using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MManejoPresupuesto.Models
{
    public class TransaccionCreacionViewModel
    {
        public int CuentaId { get; set; }
        public int CategoriaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Nota { get; set; }

        [Required]
        [Display(Name = "Tipo de Operación")]
        public int TipoOperacionId { get; set; }
        public IEnumerable<SelectListItem> Cuentas { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
    }

}
