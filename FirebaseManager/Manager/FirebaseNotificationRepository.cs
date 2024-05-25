﻿using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FirebaseManager.Dtos;


namespace FirebaseManager.Manager
{
    public class FirebaseNotificationRepository : IFirebaseNotificationRepository
    {
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
            Console.WriteLine(Environment.GetEnvironmentVariable("TEST_VARIABLE"));
            string keyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json");
            return GoogleCredential.FromFile(keyFilePath);
        }
    }
}
