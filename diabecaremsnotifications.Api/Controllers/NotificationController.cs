﻿using FirebaseManager.Dtos;
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
        public async Task<IActionResult> Send(MessageDto message)
        {
            bool result = await _notificationRepository.SendPushNotification(message);
            return result ? Ok(result) : BadRequest();
        }

        [HttpGet("sendToken/{token}")]
        public IActionResult SendToken(string token)
        {
            return token != default && token != string.Empty ? Ok(new CustomResponse<bool>(true, "Token registrado")) : BadRequest();
        }
    }
}