using FirebaseManager.Manager;

namespace diabecaremsnotifications.Api._DI
{
    public static class ManagerInjections
    {
        public static void ManagerInject(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IFirebaseNotificationRepository, FirebaseNotificationRepository>();
        }
    }
}
