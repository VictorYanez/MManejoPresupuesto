using System.ComponentModel.DataAnnotations;

namespace MManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Display(Name ="Fecha Transacción")]
        [DataType(DataType.Date)]
        public DateTime FechaTransaccion { get; set; }  = DateTime.Today; // Valor predeterminado es la fecha actual
        public decimal Monto { get; set; }
        [Range(1, maximum:int.MaxValue, ErrorMessage = "Seleccione una categoría válida.")]
        public int CategoriaId { get; set; }
        public string Nota  { get; set; }
        [Range(1, maximum:int.MaxValue, ErrorMessage = "Seleccione una cuenta válida.")]
        public int CuentaId  { get; set; }

    }

}
