using Bingo.Models;
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
        [Route("/gmail/enviar_api")]
        public async Task<IActionResult> SendCard([FromBody] TarjetaRequest request)
        {
            var obj = await _emailService.SendCard(request.quantity, request.title, request.email);

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
        [Route("/gmail/enviar_movil")]
        public async Task<IActionResult> SendCards([FromBody] List<CardDto> request)
        {
            var obj = await _emailService.SendCards(request);

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
            var obj = await _emailService.SendCard(request.quantity, request.title, request.email);

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
            var obj = await _emailService.SendCard(request.quantity, request.title, request.email);

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