﻿using Autorizacion.Abstracciones.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Autorizacion.DA
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configutarion;
        private SqlConnection _connection;

        public RepositorioDapper(IConfiguration configutarion)
        {
            _configutarion = configutarion;
            _connection = new SqlConnection(_configutarion.GetConnectionString("BDSeguridad"));
        }

        public SqlConnection ObtenerRepositorioDapper()
        {
            return _connection;
        }
    }
}
