using Dapper;
using Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class BancoRepository : IBancoRepository
    {
        private ConfiguracionBD _connectionString;
        public BancoRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteBanco(Banco bancos)
        {
            var db = dbConnection();

            var sql = @"
                            DELETE
                            FROM public.""banco"" 
                            WHERE cod_banco=@CodigoBanco";

            var result = await db.ExecuteAsync(sql, new { cod_banco = bancos.CodigoBanco});
            return result > 0;
        }

        public async Task<IEnumerable<Banco>> GetAllBancos()
        {
            var db = dbConnection();

            var sql = @"
                            SELECT (cod_banco,nombre_banco,direccion)
                            FROM public.""banco"" ";

            return await db.QueryAsync<Banco>(sql, new { });
        }

        public async Task<Banco> GetBancoDetalle(string cod_banco)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT (cod_banco,nombre_banco,direccion)
                            FROM public.""banco"" 
                            WHERE cod_banco=@CodigoBanco";

            return await db.QueryFirstOrDefaultAsync<Banco>(sql, new { COD_BANCO = cod_banco});
        }

        public async Task<bool> InsertBanco(Banco banco)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.""banco""(cod_banco,nombre_banco,direccion)
                            VALUES(@CodigoBanco, @NombreBanco ,@Direccion)";
            


            var result = await db.ExecuteAsync(sql, new {banco.CodigoBanco,banco.NombreBanco,banco.Direccion });

            return result > 0;
        }

        public async Task<bool> UpdateBanco(Banco bancos)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.""banco""
                            SET cod_banco = @CodigoBanco,
                                nombre_banco = @NombreBanco,
                                 direccion= @Direccion
                            WHERE cod_banco=@CodigoBanco";

            var result = await db.ExecuteAsync(sql, new { bancos.CodigoBanco, bancos.NombreBanco, bancos.Direccion });

            return result > 0;
        }
    }
}
