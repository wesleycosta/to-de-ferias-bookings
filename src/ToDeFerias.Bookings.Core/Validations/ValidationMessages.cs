namespace ToDeFerias.Bookings.Core.Validations;

public sealed class ValidationMessages
{
    public static string NotInformed(string field) =>
        $"Field {field} is not found";

    public static string IdentifierIsInvalid() =>
        "Field id is invalid";

    public static string GreaterThan(string field, int number = 0) =>
        $"Field {field} should be greater than {number}";

    public static string NotFoundInTheDatabase(string field) =>
        $"Field {field} was not found in the database";
}
