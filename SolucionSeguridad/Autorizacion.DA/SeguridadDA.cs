using Autorizacion.Abstracciones.DA;
using Autorizacion.Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using Helpers;
using Dapper;

namespace Autorizacion.DA
{
    public class SeguridadDA : ISeguridadDA
    {
        IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SeguridadDA(IRepositorioDapper repositorioDapper, SqlConnection sqlConnection)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<Roles>> ObtenerPerfilesxUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerRolesxUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Roles>(sql, new { Email = usuario.Email, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.ConvertirLista<Abstracciones.Entidades.Roles, Abstracciones.Modelos.Roles>(consulta);
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidades.Usuario>(sql, new { Email = usuario.Email, NombreUsuario = usuario.NombreUsuario });
            return Convertidor.Convertir<Abstracciones.Entidades.Usuario, Abstracciones.Modelos.Usuario>(consulta.FirstOrDefault());
        }
    }
}
