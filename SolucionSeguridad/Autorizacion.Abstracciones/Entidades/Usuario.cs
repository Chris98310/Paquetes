

namespace Autorizacion.Abstracciones.Entidades
{
    public class Usuario
    {
        public Guid UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public Guid UsuarioCrea { get; set; }
        public Guid UsuarioModifica { get; set; }
    }
}
