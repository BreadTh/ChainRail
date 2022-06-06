
namespace BreadTh.ChainRail;

public interface ILazyOutcome<VALUE> : ILazyOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();

    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);



    ILazyOutcome ForgetResult();



    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next);

    ILazyOutcome Pipe(Action<VALUE> next);
    ILazyOutcome Pipe(Func<VALUE, Task> next);
    ILazyOutcome Pipe(Func<VALUE, Action> next);
    ILazyOutcome Pipe(Func<VALUE, Func<Task>> next);

    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next);

    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome>>> next);

    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, ILazyOutcome<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<ILazyOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<ILazyOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<ILazyOutcome<OUTPUT>>>> next);

    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, ILazyOutcome> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<ILazyOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<ILazyOutcome>> next);
    ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<ILazyOutcome>>> next);



    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, OUTPUT> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<OUTPUT>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Task<OUTPUT>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<Task<OUTPUT>>> next);

    ILazyOutcome PipeInParallel(Action<VALUE> next);
    ILazyOutcome PipeInParallel(Func<IEnumerable<VALUE>, Task> next);
    ILazyOutcome PipeInParallel(Func<IEnumerable<VALUE>, Action> next);
    ILazyOutcome PipeInParallel(Func<IEnumerable<VALUE>, Func<Task>> next);

    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IOutcome<OUTPUT>>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IOutcome<OUTPUT>>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IOutcome<OUTPUT>>>>> next);

    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IOutcome>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IOutcome>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IOutcome>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IOutcome>>>> next);

    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<ILazyOutcome<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<ILazyOutcome<OUTPUT>>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<ILazyOutcome<OUTPUT>>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<ILazyOutcome<OUTPUT>>>>> next);

    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<ILazyOutcome>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<ILazyOutcome>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<ILazyOutcome>>> next);
    ILazyOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<ILazyOutcome>>>> next);

}