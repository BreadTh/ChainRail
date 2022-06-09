namespace BreadTh.ChainRail;

internal class Outcome<VALUE> : IOutcome<VALUE>
{
    public VALUE? Result { get; private init; }
    public IError? Error { get; private init; }

    internal Outcome(VALUE? result, IError? error)
    {
        Result = result;
        Error = error;
    }

    async Task IOutcome<VALUE>.Switch(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        if (Error is not null)
            await onError(Error);
        else
            await onSuccess(Result!);
    }

    async Task IOutcome<VALUE>.Switch(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        if (Error is not null)
            await onError(Error);
        else
            await onSuccess();
    }

    async Task IOutcome<VALUE>.Switch(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        if (Error is not null)
            await onError(Error);
        else
            onSuccess(Result!);
    }

    async Task IOutcome<VALUE>.Switch(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        if (Error is not null)
            onError(Error);
        else
            await onSuccess(Result!);
    }

    async Task IOutcome<VALUE>.Switch(Func<Task> onSuccess, Action<IError> onError)
    {
        if (Error is not null)
            onError(Error);
        else
            await onSuccess();
    }

    void IOutcome<VALUE>.Switch(Action<VALUE> onSuccess, Action<IError> onError)
    {
        if (Error is not null)
            onError(Error);
        else
            onSuccess(Result!);
    }

    VALUE IOutcome<VALUE>.Unify(Func<IError, VALUE> transform) 
    {
        if(Error is not null)
            return transform(Error);
        else
            return Result!;
    }

    RESULT IOutcome<VALUE>.Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, RESULT> onError) 
    {
        if(Error is not null)
            return onError(Error);
        else
            return onSuccess(Result!);
    }

    Task<RESULT> IOutcome<VALUE>.Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, RESULT> onError)
    {
        if (Error is not null)
            return Task.FromResult(onError(Error));
        else
            return onSuccess(Result!);
    }

    Task<RESULT> IOutcome<VALUE>.Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, Task<RESULT>> onError)
    {
        if (Error is not null)
            return onError(Error);
        else
            return Task.FromResult(onSuccess(Result!));
    }
    
    Task<RESULT> IOutcome<VALUE>.Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, Task<RESULT>> onError)
    {
        if (Error is not null)
            return onError(Error);
        else
            return onSuccess(Result!);
    }
}