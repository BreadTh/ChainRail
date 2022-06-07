using BreadTh.ChainRail;
using Newtonsoft.Json;

internal class JsonParseError : ErrorBase
{
    internal readonly JsonException? exception;
    internal readonly string? json;
 
    internal JsonParseError(Type modelType, JsonException? exception = null, string? description = null, string? json = null)
        : base(
            id: "3f23921d-812c-4ca4-b8b8-ce66f5a4ede3",
            message: 
                $"Unable to deserialize test to instance of {modelType.Name}." +
                (exception is null ? "" : $"\nException type was {exception.GetType().Name}") +
                (description is null ? "" : $"\nDescription was: {description}")
        )
    {
        this.exception = exception;
        this.json = json;
    }
}
