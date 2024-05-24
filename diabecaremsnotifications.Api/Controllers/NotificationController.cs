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

        public NotificationController(IFirebaseNotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpPost("sendNotification")]
        public IActionResult Send(MessageDto message)
        {
            bool result =  _notificationRepository.SendPushNotification(message);
            return result ? Ok(result) : BadRequest();
        }

        [HttpGet("sendToken/{token}")]
        public IActionResult SendToken(string token)
        {
            return Ok(new CustomResponse<bool>(true, $" {token}" +
                "Token registrado"));
        }
    }
}
