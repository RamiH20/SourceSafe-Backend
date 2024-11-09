using ErrorOr;

namespace SourceSafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class File
    {
        public static Error DuplicateName => Error.Conflict(
            description:"This Name is already taken");
        public static Error NotAvailable => Error.Forbidden(
            description:"Not available at the moment");
        public static Error FileNotFound => Error.NotFound(
            description:"No File");
    }
}
