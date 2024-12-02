using ErrorOr;

namespace SourceSafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class Group
    {
        public static Error NoGroup => Error.NotFound(
            description: "This Group no longer exists");
        public static Error UserWithNoGroup => Error.NotFound(
            description: "You are not included in any group yet");
    }
}
