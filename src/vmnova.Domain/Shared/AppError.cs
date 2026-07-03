namespace vmnova.Domain.Shared;


// simple error type (inspired from ErrorOr library)

public enum ErrorType
{
    NotFound,
    Conflict,
    Validation,
    Unauthorized,
    Forbidden,
    Unexpected
}

public sealed record AppError
{
    public ErrorType Type { get; init; }
    public string Code { get; init; }
    public string Description { get; init; }
    public Dictionary<string, object>? Metadata { get; init; }

    private AppError(ErrorType type, string code, string description, Dictionary<string, object>? metadata = null)
    {
        Type = type;
        Code = code;
        Description = description;
        Metadata = metadata;
    }

    public static AppError NotFound(string code = "General.NotFound", string description = "Resource not found", Dictionary<string, object>? metadata = null)
        => new(ErrorType.NotFound, code, description, metadata);

    public static AppError Conflict(string code = "General.Conflict", string description = "Resource conflict", Dictionary<string, object>? metadata = null)
        => new(ErrorType.Conflict, code, description, metadata);

    public static AppError Validation(string code = "General.Validation", string description = "Validation error", Dictionary<string, object>? metadata = null)
        => new(ErrorType.Validation, code, description, metadata);

    public static AppError Unauthorized(string code = "General.Unauthorized", string description = "Unauthorized access", Dictionary<string, object>? metadata = null)
        => new(ErrorType.Unauthorized, code, description, metadata);

    public static AppError Forbidden(string code = "General.Forbidden", string description = "Forbidden access", Dictionary<string, object>? metadata = null)
        => new(ErrorType.Forbidden, code, description, metadata);

    public static AppError Unexpected(string code = "General.Unexpected", string description = "Unexpected error", Dictionary<string, object>? metadata = null)
        => new(ErrorType.Unexpected, code, description, metadata);


    public AppError WithMetadata(string key, object value)
    {
        var newMetadata = Metadata == null
            ? []
            : new Dictionary<string, object>(Metadata);

        newMetadata[key] = value;
        return this with { Metadata = newMetadata };
    }
}

