using BreadTh.ChainRail;

public class ErrorFromServer : IError
{
    public string Id { get; set; }
    public string Message { get; set; }

    public List<IError> Inner { get; set; } = new();

    public List<IError> Flatten() =>
        new List<IError>() { this };

}
