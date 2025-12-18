using System.ComponentModel.DataAnnotations;

namespace MManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [Display(Name = "Fecha Transacción")]
        [DataType(DataType.Date)]
        // Valor predeterminado es la fecha actual  / Con Hora DateTime.Parse(DateTime.Now.ToString("g")); 
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }
        [Range(1, maximum:int.MaxValue, ErrorMessage = "Seleccione una categoría válida.")]

        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        public string Nota  { get; set; }
        [Range(1, maximum:int.MaxValue, ErrorMessage = "Seleccione una cuenta válida.")]

        [Display(Name = "Cuenta")]
        public int CuentaId  { get; set; }

    }

}
