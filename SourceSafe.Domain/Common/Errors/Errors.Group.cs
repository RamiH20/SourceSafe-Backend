using ErrorOr;

namespace SourceSafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class Group
    {
        public static Error NoGroup => Error.NotFound(
            description: "This Group no longer exists");
    }
}
