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
    public class CuentaRepository : ICuentasRepository
    {
        private ConfiguracionBD _connectionString;
        public CuentaRepository(ConfiguracionBD connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            DELELETE
                            FROM public.""CUENTAS"" 
                            WHERE ID_Cta=@ID_CUENTA";

            var result = await db.ExecuteAsync(sql, new {cuentas = cuentas.IdCuenta  });
            return result > 0;
        }

        public async Task<IEnumerable<Cuentas>> GetAllCuentas()
        {
            var db = dbConnection();

            var sql = @"
                            SELECT ID_CUENTA NUM_CTA MONEDA CEDULA SALDO COD_BANCO
                            FROM public.""CUENTAS"" ";

            return await db.QueryAsync<Cuentas>(sql, new { });
        }

        public async Task<Cuentas> GetCuentaDetalle(string IdCuenta)
        {
            var db = dbConnection();

            var sql = @"
                            SELECT ID_CTA NUM_CTA MONEDA CEDULA SALDO COD_BANCO
                            FROM public.""CUENTAS"" 
                            WHERE ID_Cta=@ID_CUENTA";

            return await db.QueryFirstOrDefaultAsync<Cuentas>(sql, new { });
        }

        public async Task<bool> InsertCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            INSERT INTO public.""CUENTAS""(ID_CTA NUM_CTA MONEDA CEDULA SALDO COD_BANCO)
                            VALUES(@ID_CUENTA,@NUM_CTA,@MONEDA,@CEDULA,@SALDO,@COD_BANCO) ";

            var result = await db.ExecuteAsync(sql, new {cuentas.IdCuenta,cuentas.NumeroCuenta,cuentas.Moneda,cuentas.Cedula,
                                                         cuentas.Saldo,cuentas.CodigoBanco });

            return result > 0;
        }

        public async Task<bool> UpdateCuenta(Cuentas cuentas)
        {
            var db = dbConnection();

            var sql = @"
                            UPDATE public.""CUENTAS""
                            SET ID_CTA = @ID_CTA,
                                NUM_CTA = @NUM_CTA,
                                Moneda = @MONEDA,   
                                Cedula = @CEDULA
                                Saldo = @SALDO
                                Cod_Banco =@COD_BANCO)       
                            WHERE Id_Cta=@ID_CUENTA";

            var result = await db.ExecuteAsync(sql, new {cuentas.IdCuenta,cuentas.NumeroCuenta,cuentas.Moneda,cuentas.Cedula,
                                                         cuentas.Saldo,cuentas.CodigoBanco});

            return result > 0;
        }
    }
}
