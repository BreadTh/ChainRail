
namespace BreadTh.ChainRail;

public interface IOutcome<VALUE>
{
    IError? error { get; }
    VALUE? result { get; }

    void Unpack(Action<VALUE> onSuccess, Action<IError> onError);
    Task Unpack(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Unpack(Func<Task> onSuccess, Action<IError> onError);
    Task Unpack(Func<Task> onSuccess, Func<IError, Task> onError);
    Task Unpack(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Unpack(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);

    VALUE TransformError(Func<IError, VALUE> transform);
}