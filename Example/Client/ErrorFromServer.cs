using BreadTh.ChainRail;

public class ErrorFromServer : IError
{
    public string Id { get; set; } = null!;
    public string Message { get; set; } = null!;

    public List<IError> Inner { get; set; } = new();

    public List<IError> Flatten() =>
        new List<IError>() { this };

}
