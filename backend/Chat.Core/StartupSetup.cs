using AutoMapper;
using Chat.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Chat.Infrastructure;
using Chat.Core.Interfaces.Services;
using Chat.Core.Services;
using Microsoft.Extensions.Configuration;

namespace Chat.Core
{
    public static class StartupSetup
    {
        public static void AddCustomServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddRepositories();
            services.AddDbContext(connectionString);
            services.AddIdentityDbContext();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigJwtOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtOptions>(config);
        }
    }
}
