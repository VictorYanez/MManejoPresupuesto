namespace MManejoPresupuesto.Servicios
{
    internal class sqlConnection
    {
        private string conectionString;

        public sqlConnection(string conectionString)
        {
            this.conectionString = conectionString;
        }
    }
}