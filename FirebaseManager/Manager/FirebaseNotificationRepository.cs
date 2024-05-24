using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FirebaseManager.Dtos;

namespace FirebaseManager.Manager
{
    public class FirebaseNotificationRepository : IFirebaseNotificationRepository
    {
        public async Task<bool> SendPushNotification(MessageDto message)
        {
            try
            {
                FirebaseApp defaultApp = FirebaseApp.DefaultInstance;
                if (defaultApp == null)
                {
                    string keyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json");
                    defaultApp = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(keyFilePath)
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
    }
}
