using Bingo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TarjetasController(IEmail _emailService) : ControllerBase
    {
        [HttpGet]
        [Route("generar_tarjetas/{quantity}/{title}/{email}")]
        public async Task<IActionResult> Generar_Tarjetas(int quantity, string title, string email)
        {
            var obj = await _emailService.SendEmail(quantity, title, email);

            if (obj == true)
            {
                return Ok(obj);
            }
            else
            {
                return NotFound(new { messaje = "no se puedo completar la operacion" });
            }
        }
    }
}