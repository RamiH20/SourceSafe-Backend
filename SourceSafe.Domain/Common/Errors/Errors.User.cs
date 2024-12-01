using ErrorOr;

namespace SourceSafe.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict();
        public static Error InvalidCredentials => Error.Validation(
            description: "Invalid Credentials.");
        public static Error NoUser => Error.NotFound(
            description: "No User Found");
        public static Error InactiveToken => Error.Forbidden(
            description: "Inactive Token");
    }
}
