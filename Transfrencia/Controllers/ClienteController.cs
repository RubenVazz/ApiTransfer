using AccesoDatos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;

namespace Transfrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            return Ok(await _clienteRepository.GetAllClientes()); 
        }
       
        [HttpGet("{CEDULA}")]
        public async Task<IActionResult> GetClientesDetalle(string cedula)
        {
            return Ok(await _clienteRepository.GetClienteDetalle(cedula));
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {
           
            if (cliente == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _clienteRepository.InsertCliente(cliente);
            return Created("created", created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _clienteRepository.UpdateCliente(cliente);
            
            return NoContent();
        }

        [HttpDelete("string cedula")]
        public async Task<IActionResult> DeleteCliente(string cedula)
        {
            await _clienteRepository.UpdateCliente(new Cliente { Cedula = cedula});

            return NoContent();
        }
    }
}
