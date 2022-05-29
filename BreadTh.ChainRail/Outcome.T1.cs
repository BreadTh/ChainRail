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

    public async Task Unpack(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            await onSuccess(result!);
    }

    public async Task Unpack(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            await onSuccess();
    }

    public async Task Unpack(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        if (error is not null)
            await onError(error);
        else
            onSuccess(result!);
    }

    public async Task Unpack(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            await onSuccess(result!);
    }

    public async Task Unpack(Func<Task> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            await onSuccess();
    }

    public void Unpack(Action<VALUE> onSuccess, Action<IError> onError)
    {
        if (error is not null)
            onError(error);
        else
            onSuccess(result!);
    }

    public VALUE TransformError(Func<IError, VALUE> transform) 
    {
        if(error is not null)
            return transform(error);
        else
            return result!;
    }
}