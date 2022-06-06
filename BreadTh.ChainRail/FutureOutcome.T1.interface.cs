
namespace BreadTh.ChainRail;

public interface IFutureOutcome<VALUE> : IFutureOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();

    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);



    IFutureOutcome ForgetResult();



    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next);

    IFutureOutcome Pipe(Action<VALUE> next);
    IFutureOutcome Pipe(Func<VALUE, Task> next);
    IFutureOutcome Pipe(Func<VALUE, Action> next);
    IFutureOutcome Pipe(Func<VALUE, Func<Task>> next);

    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next);

    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome>>> next);

    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IFutureOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IFutureOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IFutureOutcome<OUTPUT>>>> next);

    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IFutureOutcome> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IFutureOutcome>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IFutureOutcome>> next);
    IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IFutureOutcome>>> next);



    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, OUTPUT> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<OUTPUT>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Task<OUTPUT>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<Task<OUTPUT>>> next);

    IFutureOutcome PipeInParallel(Action<VALUE> next);
    IFutureOutcome PipeInParallel(Func<IEnumerable<VALUE>, Task> next);
    IFutureOutcome PipeInParallel(Func<IEnumerable<VALUE>, Action> next);
    IFutureOutcome PipeInParallel(Func<IEnumerable<VALUE>, Func<Task>> next);

    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IOutcome<OUTPUT>>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IOutcome<OUTPUT>>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IOutcome<OUTPUT>>>>> next);

    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IOutcome>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IOutcome>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IOutcome>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IOutcome>>>> next);

    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IFutureOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IFutureOutcome<OUTPUT>>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IFutureOutcome<OUTPUT>>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IFutureOutcome<OUTPUT>>>>> next);

    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<IFutureOutcome>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<IFutureOutcome>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, IEnumerable<Task<IFutureOutcome>>> next);
    IFutureOutcome<OUTPUT> PipeInParallel<OUTPUT>(Func<IEnumerable<VALUE>, Func<IEnumerable<Task<IFutureOutcome>>>> next);

}