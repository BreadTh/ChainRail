
namespace BreadTh.ChainRail;

public interface IOutcome<VALUE>
{
    IError? error { get; }
    VALUE? result { get; }

    void Switch(Action<VALUE> onSuccess, Action<IError> onError);
    Task Switch(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Switch(Func<Task> onSuccess, Action<IError> onError);
    Task Switch(Func<Task> onSuccess, Func<IError, Task> onError);
    Task Switch(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Switch(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);


    RESULT Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, RESULT> onError);
    Task<RESULT> Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, RESULT> onError);
    Task<RESULT> Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, Task<RESULT>> onError);
    Task<RESULT> Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, Task<RESULT>> onError);

    VALUE Unify(Func<IError, VALUE> transform);
}