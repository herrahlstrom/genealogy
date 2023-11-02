using System;
using System.Linq;
using System.Reflection;
using Genealogy.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Genealogy.PowerShell;

internal class ServiceLocator : IServiceProvider
{
    private static ServiceLocator? _instance;
    private readonly ServiceProvider _services;

    public ServiceLocator()
    {
        var configuration = BuildConfiguration();
        _services = CreateServices(configuration).BuildServiceProvider();
    }

    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    public object? GetService(Type serviceType)
    {
        return _services.GetService(serviceType);
    }

    private static IConfiguration BuildConfiguration()
    {
        var appsettignsResourceName = "Genealogy.PowerShell.appsettings.json";
        using var resourceStream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream(appsettignsResourceName)
            ?? throw new Exception($"Embedded resource not found: {appsettignsResourceName}");

        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonStream(resourceStream)
                                         .Build();
    }

    private static ServiceCollection CreateServices(IConfiguration configuration)
    {
        ServiceCollection services = new();

        services.AddLogging(builder => builder.AddConfiguration(configuration.GetSection("Logging"))
                                              .AddConsole()
                                              .AddDebug());

        services.AddInfrastructure(configuration);

        return services;
    }
}
