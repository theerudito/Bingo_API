using Bingo.Service;
using Bingo_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController(IEmail _emailService) : ControllerBase
    {
        [HttpPost]
        [Route("/gmail/enviar")]
        public async Task<IActionResult> Gmail([FromBody] TarjetaRequest request)
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

        [HttpPost]
        [Route("/zoho/enviar")]
        public async Task<IActionResult> Zoho([FromBody] TarjetaRequest request)
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

        [HttpPost]
        [Route("/sendgrid/enviar")]
        public async Task<IActionResult> SendGrid([FromBody] TarjetaRequest request)
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