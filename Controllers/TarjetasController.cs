using Bingo.Service;
using Bingo_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TarjetasController(IEmail _emailService) : ControllerBase
    {
        [HttpGet]
        [Route("mensaje")]
        public IActionResult Mensaje()
        {
            return Ok(new { mesaaje = "Hola Mundo..." });
        }
        [HttpPost]
        [Route("enviar_tarjetas")]
        public async Task<IActionResult> Generar_Tarjetas([FromBody] TarjetaRequest request)
        {
            var obj = await _emailService.SendEmail(request.quantity, request.title, request.email);

            if (obj.Codigo == 200)
            {
                return Ok(new { messaje = obj.Messages });
            }
            else
            {
                return NotFound(new { messaje = obj!.Messages });
            }
        }
    }
}