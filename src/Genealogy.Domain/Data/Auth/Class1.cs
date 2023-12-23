namespace Genealogy.Domain.Data.Auth;

public class RsaKey
{
    private DateTime m_activeFrom = default!;
    private DateTime m_activeTo = default!;
    private DateTime m_validFrom = default!;
    private DateTime m_validTo = default!;

    public required DateTime ActiveFrom { get; init; }

    public required DateTime ActiveTo { get; init; }

    public required DateTime Created { get; init; }

    public required string Id { get; init; }

    public required string PemKey { get; init; }

    public required DateTime ValidFrom { get; init; }

    public required DateTime ValidTo { get; init; }

    public bool IsActive(DateTime now)
    {
        return IsValid(now) &&
               ActiveFrom <= now &&
               ActiveTo >= now;
    }

    public bool IsValid(DateTime now)
    {
        return ValidFrom <= now &&
               ValidTo >= now;
    }

    public void Revoke(DateTime now)
    {
        var newTargetDate = now - TimeSpan.FromTicks(1);
        EnsureNotAfter(ref m_validFrom, newTargetDate);
        EnsureNotAfter(ref m_validTo, newTargetDate);
        EnsureNotAfter(ref m_activeFrom, newTargetDate);
        EnsureNotAfter(ref m_activeTo, newTargetDate);

        static void EnsureNotAfter(ref DateTime date, DateTime limit)
        {
            if (date > limit)
            {
                date = limit;
            }
        }
    }
}
