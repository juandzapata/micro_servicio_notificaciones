namespace FirebaseManager.Dtos
{
    public class MessageDto
    {
        public string[] RegistrationIds { get; set; }
        public string Name { get; set; }
        public NotificationDto Notification { get; set; }
        public string FullName { get; set; }
    }
}
