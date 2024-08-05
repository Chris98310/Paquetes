using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizacion.Abstracciones.Entidades
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
