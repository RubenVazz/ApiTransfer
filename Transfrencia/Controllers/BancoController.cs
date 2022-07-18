using AccesoDatos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Transfrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : Controller
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoController(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBancos()
        {
            return Ok(await _bancoRepository.GetAllBancos());
        }

        [HttpGet("{cod_banco}")]
        public async Task<IActionResult> GetBancoDetalle(string cod_banco)
        {
            return Ok(await _bancoRepository.GetBancoDetalle(cod_banco));
        }

        [HttpPost]
        public async Task<IActionResult> CrearBanco([FromBody] BancoRequest bancoRequest)
        {
            if (bancoRequest == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid guid = Guid.NewGuid();
            var Banco = new Banco() { 
               CodigoBanco = guid.ToString(),
               NombreBanco = bancoRequest.NombreBanco,
               Direccion = bancoRequest.Direccion,
            };

            var created = await _bancoRepository.InsertBanco(Banco);
            return Ok("Banco Registrado");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBanco([FromBody] Banco bancos)
        {
            if (bancos == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _bancoRepository.UpdateBanco(bancos);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBanco(string codigoBanco)
        {
            await _bancoRepository.UpdateBanco(new Banco { CodigoBanco = codigoBanco });

            return NoContent();
        }
    }
}

