namespace BreadTh.ChainRail;

internal class Outcome<VALUE> : IOutcome<VALUE>
{
    public VALUE? result { get; private init; }
    public IError? error { get; private init; }

    internal Outcome(VALUE? result, IError? error)
    {
        this.result = result;
        this.error = error;
    }

    public async Task Switch(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            await onSuccess(result!);
    }

    public async Task Switch(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            await onSuccess();
    }

    public async Task Switch(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            onSuccess(result!);
    }

    public async Task Switch(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            await onSuccess(result!);
    }

    public async Task Switch(Func<Task> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            await onSuccess();
    }

    public void Switch(Action<VALUE> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            onSuccess(result!);
    }

    public VALUE Unify(Func<IError, VALUE> transform) 
    {
        if(error is not null)
            return transform(error);
        else
            return result!;
    }

    public RESULT Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, RESULT> onError) 
    {
        if(error is not null)
            return onError(error);
        else
            return onSuccess(result!);
    }

    public Task<RESULT> Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, RESULT> onError)
    {
        if (error is not null)
            return Task.FromResult(onError(error));
        else
            return onSuccess(result!);
    }

    public Task<RESULT> Unify<RESULT>(Func<VALUE, RESULT> onSuccess, Func<IError, Task<RESULT>> onError)
    {
        if (error is not null)
            return onError(error);
        else
            return Task.FromResult(onSuccess(result!));
    }
    
    public Task<RESULT> Unify<RESULT>(Func<VALUE, Task<RESULT>> onSuccess, Func<IError, Task<RESULT>> onError)
    {
        if (error is not null)
            return onError(error);
        else
            return onSuccess(result!);
    }
}