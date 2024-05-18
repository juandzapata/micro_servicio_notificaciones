using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FirebaseManager.Dtos;
using Google.Apis.Auth.OAuth2;

namespace FirebaseManager.Manager
{
    public class FirebaseNotificationRepository : IFirebaseNotificationRepository
    {
        public async Task<bool> SendPushNotification(MessageDto message)
        {
            try
            {
                message.RegistrationIds = getDevices();
                FirebaseApp defaultApp = FirebaseApp.DefaultInstance;
                if (defaultApp == null)
                {
                    defaultApp = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile("C:\\Users\\Asus\\Desktop\\TOMANSY\\PROYECTOS\\diabecaremsnotifications.Api\\FirebaseManager\\key.json")
                    });
                }
                Console.WriteLine(defaultApp.Name);

                FirebaseMessaging firebaseMessaging = FirebaseMessaging.GetMessaging(defaultApp);
                foreach (var device in message.RegistrationIds)
                {
                    var result = await firebaseMessaging.SendAsync(new Message()
                    {
                        Data = new Dictionary<string, string>()
                        {
                            ["FirstName"] = message.Name,
                            ["LastName"] = message.FullName
                        },
                        Notification = new Notification()
                        {
                            Title = message.Notification.Title,
                            Body = message.Notification.Body,
                        },
                        Token = device
                    });

                    Console.WriteLine(result); //projects/projectId/messages/0:messageId
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        string[] getDevices()
        {
            return new string[] { "dsnvsSHOSf-xQeeUJCI0qR:APA91bGLzPwx43LAqUQXI2r8ft1XqyDFkKl6OhSlZL6lhOsTPp12yMdtlVePWFExKqbFzvYCFy5VXiQZETjTPEfrLKLp3ZuQwejkyLuBhsim1jr4JIlEnF1TZcVDvXJV4ik98L99uauq" };
        }
    }
}
