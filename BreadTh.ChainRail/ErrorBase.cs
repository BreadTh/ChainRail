
namespace BreadTh.ChainRail;

public abstract class ErrorBase : IError
{
    public string Id { get; init; }
    public string Message { get; init; }
    public List<IError> Inner { get; init; }

    public ErrorBase(string id, string message, List<IError>? inner = null)
    {
        Id = id;
        Message = message;
        Inner = inner ?? new List<IError>();
    }

    public ErrorBase(string id, string message, IError inner)
    {
        Id = id;
        Message = message;
        Inner = new List<IError>() { inner };
    }

    public List<IError> Flatten() =>
        new List<IError>() { this };
}
