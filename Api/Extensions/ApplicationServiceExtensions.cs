namespace Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        // services.AddControllers(opt =>
        //     {
        //         var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        //         opt.Filters.Add(new AuthorizeFilter(policy));
        //     });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();

        // services.AddScoped<IGalleryCRUD, GalleryCRUD>();
        // services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));



        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            policy.AllowAnyMethod().
            AllowCredentials().
            AllowAnyHeader().WithOrigins("http://localhost:3000", "https://mamibet.vercel.app", "http://localhost:5001"));
        });

        return services;
    }
}
