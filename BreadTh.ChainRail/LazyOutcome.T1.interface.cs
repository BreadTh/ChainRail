
namespace BreadTh.ChainRail;

public interface ILazyOutcome<VALUE> : ILazyOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();
    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);
    ILazyOutcome ForgetResult();
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> logic);
    ILazyOutcome Pipe(Func<VALUE, Func<IOutcome>> logic);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> logic);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> nextTask);
    ILazyOutcome Pipe(Func<VALUE, Func<Task<IOutcome>>> nextTask);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> logicTask);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> logic);
    ILazyOutcome Pipe(Func<VALUE, IOutcome> logic);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, ILazyOutcome<OUTPUT>> nextTask);
    ILazyOutcome Pipe(Func<VALUE, ILazyOutcome> nextTask);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> logic);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> nextTask);
    ILazyOutcome Pipe(Func<VALUE, Task<IOutcome>> nextTask);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> logicTask);
}