using SourceSafe.Application.Common.Interfaces.Services;

namespace SourceSafe.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
