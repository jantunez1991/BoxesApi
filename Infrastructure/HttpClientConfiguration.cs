using System.Net.Http.Headers;
using BoxesApi.Services;

namespace BoxesApi.Infrastructure;

public static class HttpClientConfiguration
{
    public static IServiceCollection AddWorkshopHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IWorkshopService, WorkshopService>(client =>
        {
            var authToken = Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authToken);
        });

        return services;
    }
}