using System;
using System.Linq;

namespace Genealogy.Application.Exceptions;

public abstract class NotFoundException(string message) : Exception(message)
{
}
