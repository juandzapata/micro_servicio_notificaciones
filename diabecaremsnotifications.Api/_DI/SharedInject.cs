namespace diabecaremsnotifications.Api._DI
{
    public static class SharedInject
    {
        public static void Inject(this WebApplicationBuilder builder)
        {
            builder.ManagerInject();
        }
    }
}
