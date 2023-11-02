using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MyGen.Data;

namespace Genealogy.Console.MyGenImport;

internal static class MyGenExtensions
{
    public static IServiceCollection AddMyGenImport(this IServiceCollection services)
    {
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IEntityRepository, EntityRepository>();
        services.AddTransient<DataTransfer>();

        return services;

    }
}
