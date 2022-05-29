
namespace BreadTh.ChainRail;

public interface IError
{
    string Id { get; }
    string Message { get; }
    List<IError> Inner { get; }
    List<IError> Flatten();
}
