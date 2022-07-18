using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Repositorios;
using Model;
using System.Threading.Tasks;
namespace Transfrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : Controller
    {
        private readonly ICuentasRepository _cuentaRepository;

        public CuentaController(ICuentasRepository cuentaRepository)
        {
            this._cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCuentas()
        {
            return Ok(await _cuentaRepository.GetAllCuentas());
        }

        [HttpGet("{id_Cta}")]
        public async Task<IActionResult> GetCuentaDetalle(string idCuenta )
        {
            return Ok(await _cuentaRepository.GetCuentaDetalle(idCuenta));
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuenta(CuentaRequest cuentaRequest)
        {

            if (cuentaRequest == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid guid = Guid.NewGuid();
            var Cuenta = new Cuentas()
            {
                IdCuenta = guid.ToString(),
                NumeroCuenta = cuentaRequest.NumeroCuenta,
                Moneda = cuentaRequest.Moneda,
                Cedula = cuentaRequest.Cedula,
                Saldo = cuentaRequest.Saldo,
                CodigoBanco = cuentaRequest.CodigoBanco,
            };
            var created = await _cuentaRepository.InsertCuenta(Cuenta);
            return Created("created", created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCuentas([FromBody] Cuentas cuenta)
        {
            if (cuenta == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cuentaRepository.UpdateCuenta(cuenta);

            return NoContent();
        }

        [HttpDelete("Guid id_cta")]
        public async Task<IActionResult> DeleteCuenta(string idCuenta)
        {
            await _cuentaRepository.UpdateCuenta(new Cuentas { IdCuenta = idCuenta });

            return NoContent();
        }


        
    }
}
