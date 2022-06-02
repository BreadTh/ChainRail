using BreadTh.ChainRail;
using Newtonsoft.Json;

public class JsonStringifyError<MODEL> : ErrorBase, IJsonStringifyError
{
    internal readonly JsonException exception;
    internal readonly MODEL model;

    public JsonStringifyError(MODEL model, JsonException exception) 
        : base(
            id: "28df0a01-3af6-4a65-9783-e70d0905bd80",
            message: $"Unable to serialize instance of {typeof(MODEL).Name} to text. Exception type was {exception.GetType().Name}")
    {
        this.exception = exception;
        this.model = model;
    }
}
