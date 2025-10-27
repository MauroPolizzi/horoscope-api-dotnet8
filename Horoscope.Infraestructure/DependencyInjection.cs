using Horoscope.Application.Interfaces;
using Horoscope.Infraestructure.Data;
using Horoscope.Infraestructure.Repositories;
using Horoscope.Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Horoscope.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, string connectionString) 
        { 
            services.AddDbContext<HoroscopeDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<HoroscopeRepository>();
            services.AddScoped<IHoroscopeService, HoroscopeService>();

            var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(retry * 2));

            services.AddHttpClient<IHoroscopeService, HoroscopeService>(client =>
            {
                client.BaseAddress = new Uri("https://newastro.vercel.app");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = true })
                .AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}
