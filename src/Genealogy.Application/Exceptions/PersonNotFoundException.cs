using System;
using System.Linq;

namespace Genealogy.Application.Exceptions;

public class PersonNotFoundException(string message) : Exception(message)
{
}
