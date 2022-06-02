
namespace BreadTh.ChainRail;

public abstract class AggregateErrorBase : IError, IAggregateError
{
    public List<IError> Inner { get; init; }
    public string Id { get; init; }
    public string Message { get => $"{GetType().Name}: " + string.Join("; ", Inner.Select(x => x.Message)); }

    public AggregateErrorBase(string id, List<IError> inner) 
    {
        Id = id;

        var flattened = new List<IError>();
        foreach (var error in inner)
        {
            if (error is not IAggregateError)
                flattened.Add(error);
            else
            {
                foreach (var innerError in ((IAggregateError)error).Inner)
                    flattened.Add(innerError);
            }
        }

        Inner = flattened;
    }

    public List<IError> Flatten() =>
        Inner;
}
