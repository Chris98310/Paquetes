

namespace Autorizacion.Abstracciones.Modelos
{ 
    public class Roles
    {

        public Guid RolID { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid UsuarioCrea { get; set; }
        public Guid UsuarioModifica { get; set; }
    }
}
