namespace MManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaTransaccion { get; set; }  = DateTime.Today;
        public decimal Monto { get; set; }
        public int CategoriaId { get; set; }
        public string Nota  { get; set; }
        public int CuentaId  { get; set; }

    }

}
