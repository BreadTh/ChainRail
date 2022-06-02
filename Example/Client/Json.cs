using BreadTh.ChainRail;
using Newtonsoft.Json;

internal class Json 
{
    private readonly ChainRail chainRail;

    internal Json(ChainRail chainRail)
    {
        this.chainRail = chainRail;
    }

    public IOutcome<string> Stringify<MODEL>(MODEL model) 
    {
        try 
        {
            string json = JsonConvert.SerializeObject(model);
            return chainRail.Success(json);
        }
        catch(JsonException exception) 
        {
            return chainRail.Error<string>(new JsonStringifyError<MODEL>(model, exception));
        }
    }

    public IOutcome<MODEL> Parse<MODEL>(string? json) 
    {
        try
        {
            var model = JsonConvert.DeserializeObject<MODEL>(json!);
            if(model is null)
                return chainRail.Error<MODEL>(new JsonParseError(typeof(MODEL), json));
            else
                return chainRail.Success(model);
        }
        catch(JsonException exception) 
        {
            return chainRail.Error<MODEL>(new JsonParseError(typeof(MODEL), exception, json));
        }
    }
}
