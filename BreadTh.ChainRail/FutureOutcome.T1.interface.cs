
namespace BreadTh.ChainRail;

public interface IFutureOutcome<VALUE> : IFutureOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();

    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);



    IFutureOutcome ForgetResult();



    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome Then(Action<VALUE> next);
    IFutureOutcome Then(Func<VALUE, Task> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);

    IFutureOutcome Then(Func<VALUE, IOutcome> next);
    IFutureOutcome Then(Func<VALUE, Task<IOutcome>> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome Then(Func<VALUE, IFutureOutcome> next);



    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee(Action<VALUE> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, Task> next);

    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, IOutcome> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, Task<IOutcome>> next);

    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, IFutureOutcome> next);


    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);

    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<OUTPUT> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<Task<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<IOutcome<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next);
}