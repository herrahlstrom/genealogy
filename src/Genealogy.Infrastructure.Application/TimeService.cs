using Genealogy.Application;

namespace Genealogy.Infrastructure.Application;

internal class TimeService : ITimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
