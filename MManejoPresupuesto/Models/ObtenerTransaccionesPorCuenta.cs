namespace MManejoPresupuesto.Models
{
    public class ObtenerTransaccionesPorCuenta
    {
        public int UsuarioId { get; set; }
        public int CuantaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
