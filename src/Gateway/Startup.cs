using GrpcServiceTests.Gateway.Services;

namespace GrpcServiceTests.Gateway;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IExtraService, RealExtraService>();

        services.AddGrpc(o => o.EnableDetailedErrors = true);
        services.AddGrpcReflection();

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<GreeterService>();
            endpoints.MapGet("/",
                () =>
                    "Communication with gRPC endpoints must be made through a gRPC client. " +
                    "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            endpoints.MapGrpcReflectionService();
        });
    }
}
