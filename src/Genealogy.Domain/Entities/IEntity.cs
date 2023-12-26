using System;
using System.Linq;

namespace Genealogy.Domain.Entities;
public interface IEntity<TKey>
{
    public TKey Id { get; }
}
