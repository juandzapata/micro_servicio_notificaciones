using FirebaseManager.Dtos;
using FirebaseManager.Manager;
using Microsoft.AspNetCore.Mvc;

namespace diabecaremsnotifications.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IFirebaseNotificationRepository _notificationRepository;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NotificationController(IFirebaseNotificationRepository notificationRepository, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _notificationRepository = notificationRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("sendNotification")]
        public IActionResult Send(MessageDto message)
        {
            try
            {
                bool result = _notificationRepository.SendPushNotification(message);
                Console.WriteLine(result);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message , Ex = ex});
            }
            
        }

        [HttpGet("sendToken/{token}")]
        public IActionResult SendToken(string token)
        {
            return Ok(new CustomResponse<bool>(true, $" {token}" +
                "Token registrado"));
        }

        [HttpGet("getAmbiente")]
        public IActionResult GetAmbiente()
        {
            var menssage = _configuration.GetValue<String>("EnvironmentVariable");
            var environment = _webHostEnvironment.EnvironmentName;
            return Ok(new {Message = menssage, CurrentEnvironment = environment});
        }

    }
}
