using FirebaseManager.Dtos;

namespace FirebaseManager.Manager
{
    public interface IFirebaseNotificationRepository
    {
        public bool SendPushNotification(MessageDto message);
    }
}
