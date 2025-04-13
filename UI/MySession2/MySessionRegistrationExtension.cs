using UI.MySession;

namespace UI.MySession2
{
    public static class MySessionRegistrationExtension
    {
        public static IServiceCollection AddMySession(this IServiceCollection services)
        {
            services.AddSingleton<IMySessionStorageEngine>(services =>
            {
                var path = Path.Combine(services.GetRequiredService<IHostEnvironment>().ContentRootPath, "sessions");
                Directory.CreateDirectory(path);
                return new FileMySessionStorageEngine(path);
            });
            services.AddSingleton<IMySessionStorage, MySessionStorage>();
            services.AddScoped<MySessionScopedContainer>();

            return services;
        }
    }
}
