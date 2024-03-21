using Microsoft.AspNetCore.Mvc;
using RusClimate.EmailVerificationService.BLL.Interface;
using RusClimate.EmailVerificationService.PL.WebAPI.Models;

namespace RusClimate.EmailVerificationService.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public VerifyController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> SendAsync(SendInputModel model)
        {
            var response = await _emailService.SendVerificationEmailAsync(model.Email, model.Text);

            return StatusCode(response.ErrorCode);
        }

        [HttpGet("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckAsync(string token)
        {
            var response = await _emailService.VerifyAsync(token);

            return StatusCode(response.ErrorCode);
        }
    }
}
