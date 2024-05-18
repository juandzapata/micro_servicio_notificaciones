using FirebaseManager.Dtos;

namespace FirebaseManager.Manager
{
    public interface IFirebaseNotificationRepository
    {
        public Task<bool> SendPushNotification(MessageDto message);
    }
}
