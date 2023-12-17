//using Genealogy.Domain.Data.Entities;
//using Genealogy.Domain.Data;
//using Microsoft.Extensions.Logging.Abstractions;
//using MyGen.Data.Entities;
//using MyGen.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Genealogy.Console;

//internal class DefaultFileSystem(string basePath) : IFileSystem
//{
//    public void DeleteFile(string filename) => throw new NotImplementedException();

//    public ICollection<string> GetFiles() => Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);

//    public ICollection<string> GetFiles(string extension) => Directory.GetFiles(basePath, $"*{extension}", SearchOption.AllDirectories);

//    public Stream OpenForRead(string filename) => File.OpenRead(Path.Combine(basePath, filename));

//    public Stream OpenForWrite(string filename) => throw new NotImplementedException();
//}

//internal class MyGenImporter
//{
//    private readonly IEntityRepository _legacyRepo;
//    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

//    public MyGenImporter(IUnitOfWorkFactory unitOfWorkFactory)
//    {
//        _unitOfWorkFactory = unitOfWorkFactory;

//        IFileSystem fileSystem = new DefaultFileSystem("C:\\Users\\marti\\source\\repos\\my-gen-data");

//        throw new InvalidOperationException();
//        _legacyRepo = new EntityRepository(fileSystem, NullLogger<EntityRepository>.Instance);
//        _legacyRepo.Load();
//    }

//    public async Task ImportFamilyEventReferencesAsync()
//    {
//        using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
//        {
//            foreach (var family in _legacyRepo.GetEntities<Family>())
//            {
//                if (family.LifeStories is { Count: > 0 } lss)
//                {
//                    FamilyEntity f = await unitOfWork.FamilyRepository.GetByIdAsync(family.Id);
//                    await unitOfWork.FamilyRepository.LoadEvents(f);
//                    foreach (var ls in lss)
//                    {
//                        if (f.Events.Any(x => x.EventId == ls.LifeStoryId))
//                        {
//                            continue;
//                        }

//                        f.Events
//                         .Add(new FamilyEventMember()
//                         {
//                             EntityId = family.Id,
//                             EventId = ls.LifeStoryId,
//                             EventType = (EventType)(int)ls.Type,
//                             Date = null,
//                             EndDate = null
//                         });
//                    }
//                    await unitOfWork.CommitAsync();
//                }
//            }
//        }
//    }
//    public async Task ImportPersonEventReferencesAsync()
//    {
//        using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
//        {
//            foreach (var person in _legacyRepo.GetEntities<Person>())
//            {
//                if (person.LifeStories is { Count: > 0 } lss)
//                {
//                    PersonEntity p = await unitOfWork.PersonRepository.GetByIdAsync(person.Id);
//                    await unitOfWork.PersonRepository.LoadEvents(p);
//                    foreach (var ls in lss)
//                    {
//                        if (p.Events.Any(x => x.EventId == ls.LifeStoryId))
//                        {
//                            continue;
//                        }

//                        p.Events
//                         .Add(new PersonEventMember()
//                         {
//                             EntityId = person.Id,
//                             EventId = ls.LifeStoryId,
//                             EventType = (EventType)(int)ls.Type,
//                             Date = ls.Date,
//                             EndDate = ls.EndDate
//                         });
//                    }
//                    await unitOfWork.CommitAsync();
//                }
//            }

//        }
//    }
//}
