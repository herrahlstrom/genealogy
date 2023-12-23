using System;
using System.Linq;

namespace Genealogy.Domain.Data.Entities;
public interface IEntity<TKey>
{
    public TKey Id { get; }
}
