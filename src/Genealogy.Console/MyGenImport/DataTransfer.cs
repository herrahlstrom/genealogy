using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.Shared;
using Microsoft.Extensions.Logging;
using MyGen.Data;
using MyGenEntities = MyGen.Data.Entities;

namespace Genealogy.Console.MyGenImport;

internal class DataTransfer
{
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IEntityRepository _entityRepository;
    private readonly ILogger<DataTransfer> _logger;

    public DataTransfer(IEntityRepository entityRepository, IUnitOfWorkFactory uowFactory, ILogger<DataTransfer> logger)
    {
        _logger = logger;
        _uowFactory = uowFactory;
        _entityRepository = entityRepository;
    }

    public async Task TransferAsync()
    {
        using var uow = _uowFactory.CreateDbContext();

        _entityRepository.Load();

        _logger.LogDebug("Start importing media");
        foreach (var m in _entityRepository.GetEntities<MyGenEntities.Media>())
        {
            uow.MediaRepository.Add(Map(m));
        }
        await uow.CommitAsync();


        _logger.LogDebug("Start importing persons");
        foreach (var p in _entityRepository.GetEntities<MyGenEntities.Person>())
        {
            uow.PersonRepository.Add(Map(p));
            if (p.MediaIds is { Count: > 0 } mediaIds)
            {
                uow.MediaReferencesRepository
                         .AddRange(mediaIds.Select(mediaId => new MediaReference()
                         {
                             EntityId = p.Id,
                             MediaId = mediaId
                         }));
            }
        }
        await uow.CommitAsync();


        _logger.LogDebug("Start importing sources");
        foreach (var s in _entityRepository.GetEntities<MyGenEntities.Source>())
        {
            uow.SourceRepository.Add(Map(s));
            if (s.MediaIds is { Count: > 0 } mediaIds)
            {
                uow.MediaReferencesRepository
                         .AddRange(mediaIds.Select(mediaId => new MediaReference()
                         {
                             EntityId = s.Id,
                             MediaId = mediaId
                         }));
            }
        }
        await uow.CommitAsync();


        _logger.LogDebug("Start importing events");
        foreach (var ls in _entityRepository.GetEntities<MyGenEntities.LifeStory>())
        {
            uow.EventRepository.Add(Map(ls));
            if (ls.SourceIds != null)
            {
                uow.EventSourcesRepository.AddRange(ls.SourceIds.Select(sourceId => Map(ls, sourceId)));
            }
        }
        await uow.CommitAsync();


        _logger.LogDebug("Start importing families");
        foreach (var f in _entityRepository.GetEntities<MyGenEntities.Family>())
        {
            uow.FamilyRepository.Add(Map(f));
            if (f.Members != null)
            {
                uow.FamilyMemberRepository.AddRange(f.Members.Select(member => Map(f, member)));
            }
            if (f.LifeStories != null)
            {
                uow.EventMemberRepository.AddRange(f.LifeStories.Select(ls => Map(f, ls)));
            }
        }
        await uow.CommitAsync();
    }

    private static SourceEntity Map(MyGenEntities.Source s)
    {
        return new SourceEntity()
        {
            Id = s.Id,
            Type = (SourceType)s.Type,
            Name = s.Name ?? "",
            Repository = s.Repository,
            Volume = s.Volume,
            Page = s.Page,
            ReferenceId = s.ReferenceId,
            Url = s.Url,
            Notes = s.Notes ?? ""
        };
    }

    private static MediaEntity Map(MyGenEntities.Media m)
    {
        IEnumerable<MediaMeta> metaCollection = Enumerable.Empty<MediaMeta>();
        if(m.Meta is { Count: > 0 } meta)
        {
            metaCollection = meta.Select(x => new MediaMeta() { MediaId = m.Id, Key = x.Key, Value = x.Value });
        }

        return new MediaEntity()
        {
            Id = m.Id,
            Type = (MediaType)m.Type,
            Title = m.Title,
            Path = m.Path,
            Size = m.Size,
            FileCrc = m.FileCrc,
            Notes = m.Notes ?? "",
            Meta = metaCollection.ToArray()
        };
    }

    private static PersonEntity Map(MyGenEntities.Person p)
    {
        return new PersonEntity()
        {
            Id = p.Id,
            Name = $"{p.Firstname} /{p.Lastname}/",
            Sex = p.Sex,
            Profession = p.Profession ?? "",
            Notes = p.Notes ?? ""
        };
    }

    private static EventEntity Map(MyGenEntities.LifeStory ls)
    {
        return new EventEntity()
        {
            Id = ls.Id,
            Type = (EventType)ls.Type,
            Date = ls.Date,
            EndDate = ls.EndDate,
            Location = ls.Location
        };
    }
    private static FamilyEntity Map(MyGenEntities.Family p)
    {
        return new FamilyEntity()
        {
            Id = p.Id,
            Notes = p.Notes
        };
    }

    private static EventSources Map(MyGenEntities.LifeStory ls, Guid sourceId)
    {
        return new EventSources() { EventId = ls.Id, SourceId = sourceId };
    }

    private static FamilyMember Map(MyGenEntities.Family f, MyGenEntities.FamilyMember member)
    {
        return new FamilyMember()
        {
            FamilyId = f.Id,
            PersonId = member.PersonId,
            MemberType = (FamilyMemberType)member.MemberType
        };
    }

    private static EventMember Map(MyGenEntities.Family f, MyGenEntities.FamilyLifeStory ls)
    {
        return new EventMember()
        {
            EventId = ls.LifeStoryId,
            EntityId = f.Id,
            Type = (EventType)ls.Type
        };
    }
}
