using System.Text.RegularExpressions;
using MassTransit;
using MassTransit.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayamEvents;



public static class MassTransitConfig
{
    public static void ConfigureMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
                cfg.Message<PayamEvent>(e =>
                {
                    e.SetEntityName("PayamEvents:OtpMessage");
                });

                cfg.ConfigureEndpoints(ctx);

            });

        });
  
    }
}