using BreadTh.ChainRail;
using Newtonsoft.Json;

internal class JsonParseError : ErrorBase
{
    internal readonly JsonException? exception;
    internal readonly string text;

    internal JsonParseError(Type modelType, JsonException exception, string text)
        : base(
            id: "3f23921d-812c-4ca4-b8b8-ce66f5a4ede3",
            message: $"Unable to deserialize test to instance of {modelType.Name}. Exception type was {exception.GetType().Name}")
    {
        this.exception = exception;
        this.text = text;
    }

    internal JsonParseError(Type modelType, string text)
    : base(
        id: "3f23921d-812c-4ca4-b8b8-ce66f5a4ede3",
        message: $"Unable to deserialize test to instance of {modelType.Name}.")
    {
        this.text = text;
    }
}
