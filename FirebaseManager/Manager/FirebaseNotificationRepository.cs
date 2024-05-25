using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FirebaseManager.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace FirebaseManager.Manager
{
    public class FirebaseNotificationRepository : IFirebaseNotificationRepository
    {
        private IConfiguration _configuration;

        public FirebaseNotificationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendPushNotification(MessageDto message)
        {
            FirebaseApp defaultApp = FirebaseApp.DefaultInstance;
            defaultApp = CreateApp(defaultApp);
            return SendMessages(defaultApp, message);
        }

        private bool SendMessages(FirebaseApp defaultApp, MessageDto message)
        {
            try
            {
                FirebaseMessaging firebaseMessaging = FirebaseMessaging.GetMessaging(defaultApp);
                message.RegistrationIds.ToList().ForEach(async device => await firebaseMessaging.SendAsync(BuildMessageForDevice(message, device)));
                return true;
            }
            catch { return false; }
        }

        private FirebaseApp CreateApp(FirebaseApp defaultApp) =>
            defaultApp ?? FirebaseApp.Create(new AppOptions() { Credential = GetGoogleCredentials() });

        private Message BuildMessageForDevice(MessageDto message, string device)
        {
            return new Message()
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
            };
        }

        private GoogleCredential GetGoogleCredentials()
        {
            var firebaseJsonKey = GetFirebaseJsonKey();
            string keyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json");
            return GoogleCredential.FromJson(firebaseJsonKey);
        }

        private string GetFirebaseJsonKey()
        {
            var diccionary = GetDictioanry();
            return  JsonConvert.SerializeObject(diccionary, Formatting.Indented);
        }

        private Dictionary<string, string> GetDictioanry()
        {
            return new Dictionary<string, string>()
            {
                { "type", _configuration.GetSection("type").Value },
                { "project_id",  _configuration.GetSection("project_id").Value },
                { "private_key_id",  _configuration.GetSection("private_key_id").Value },
                { "private_key", _configuration.GetSection("private_key").Value },
                { "client_email",  _configuration.GetSection("client_email").Value },
                { "client_id", _configuration.GetSection("client_id").Value },
                { "auth_uri", _configuration.GetSection("auth_uri").Value },
                { "token_uri", _configuration.GetSection("token_uri").Value },
                { "auth_provider_x509_cert_url", _configuration.GetSection("auth_provider_x509_cert_url").Value },
                { "client_x509_cert_url",  _configuration.GetSection("client_x509_cert_url").Value },
                { "universe_domain", _configuration.GetSection("universe_domain").Value }
            };
        }
    }
}
