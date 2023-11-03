﻿using Genealogy.Domain.Data;
using Genealogy.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();

using var services = new ServiceCollection().AddLogging(x => x.AddConfiguration(configuration.GetRequiredSection("Logging"))
                                                              .AddDebug()
                                                              .AddConsole())
                                            .AddInfrastructure(configuration)
                                            .BuildServiceProvider();

var dbContextFactory = services.GetRequiredService<IUnitOfWorkFactory>();

using (var dbContext = dbContextFactory.CreateDbContext())
{
    var items = await dbContext.EventRepository.GetAllAsync();
}