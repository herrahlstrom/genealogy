using Genealogy.Console;
using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyGen.Data;

var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();

using var services = new ServiceCollection().AddLogging(x => x.AddConfiguration(configuration.GetRequiredSection("Logging"))
                                                              .AddDebug()
                                                              .AddConsole())
                                            .AddInfrastructure(configuration)
                                            .BuildServiceProvider();

var uofFactory = services.GetRequiredService<IUnitOfWorkFactory>();


//var importer = new MyGenImporter(uofFactory);
//await importer.ImportPersonEventReferencesAsync();
//await importer.ImportFamilyEventReferencesAsync();




Guid myId = Guid.Parse("e202a6e0-f9b3-4934-ac34-bb0b7e805045");

using (var uof = uofFactory.CreateUnitOfWork())
{
    var me = await uof.PersonRepository.GetByIdAsync(myId);
    await uof.PersonRepository.LoadEvents(me);

    //var changes = await uof.CommitAsync();
}


using (var uof = uofFactory.CreateUnitOfWork())
{
    var me = await uof.PersonRepository.GetByIdAsync(myId);




    //var e = await uof.EventRepository.GetAllAsync();
    //var e2 = e.Where(x => x.Sources.Count > 0).ToList();

    //var person = await uof.PersonRepository.GetByIdAsync(personId);
}

