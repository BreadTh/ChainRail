
namespace BreadTh.ChainRail;

public interface ILazyOutcome<VALUE> : ILazyOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();
    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);
    ILazyOutcome ForgetResult();
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next);
    ILazyOutcome Pipe(Func<VALUE, Func<IOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next);
    ILazyOutcome Pipe(Func<VALUE, Func<Task<IOutcome>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    ILazyOutcome Pipe(Func<VALUE, IOutcome> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, ILazyOutcome<OUTPUT>> next);
    ILazyOutcome Pipe(Func<VALUE, ILazyOutcome> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    ILazyOutcome Pipe(Func<VALUE, Task<IOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);



    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next);
    ILazyOutcome<VALUE> Tee(Func<VALUE, Func<IOutcome>> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<OUTPUT>> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next);
    ILazyOutcome<VALUE> Tee(Func<VALUE, Func<Task<IOutcome>>> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    ILazyOutcome<VALUE> Tee(Func<VALUE, IOutcome> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, ILazyOutcome<OUTPUT>> next);
    ILazyOutcome<VALUE> Tee(Func<VALUE, ILazyOutcome> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, OUTPUT> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    ILazyOutcome<VALUE> Tee(Func<VALUE, Task<IOutcome>> next);
    ILazyOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);


    //what about other versions with just action instead of func
    //And what about task?
    ILazyOutcome<VALUE> Tee(Action<VALUE> next);


}