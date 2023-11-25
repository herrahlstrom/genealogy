using System;
using System.Linq;
using System.Management.Automation;
using Genealogy.Domain.Data;
using Genealogy.Domain.Data.Entities;
using Genealogy.PowerShell.Models;
using Genealogy.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Genealogy.PowerShell.Cmdlets;

[OutputType(typeof(PSPerson))]
[Cmdlet(VerbsCommon.Get, "Person")]
public class GetPersonCommand : CmdletBase
{
    private IUnitOfWorkFactory _unitOfWorkFactory;

    public GetPersonCommand()
    {
        _unitOfWorkFactory = Services.GetRequiredService<IUnitOfWorkFactory>();
    }

    [Parameter(ParameterSetName = "ById", Mandatory = true)]
    public Guid Id { get; set; }

    [Parameter(ParameterSetName = "ByName", Mandatory = true)]
    public string Name { get; set; } = default!;

    protected override async Task ProcessRecordAsync()
    {
        using var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork();

        IEnumerable<PersonEntity> persons;
        if(IsParameterDefined(nameof(Id)))
        {
            persons = [await unitOfWork.PersonRepository.GetByIdAsync(Id)];
        }
        else if(IsParameterDefined(nameof(Name)))
        {
            persons = await unitOfWork.PersonRepository.FindByName(Name, CancellationToken.None);
        }
        else
        {
            persons = Enumerable.Empty<PersonEntity>();
        }

        foreach(var person in persons)
        {
            await unitOfWork.PersonRepository.LoadEvents(person);
            WriteObject(Map(person));
        }
    }

    private static PSPerson Map(PersonEntity entity)
    {
        var name = new PersonName(entity.Name);

        DateModel birthDate = (from e in entity.Events
                               where e.EventType == EventType.Födelse
                               select e.Date ?? e.Event.Date).FirstOrDefault();

        return new PSPerson
        {
            Id = entity.Id,
            FirstName = name.FirstName,
            LastName = name.LastName,
            BirthYear = birthDate.Year
        };
    }
}
