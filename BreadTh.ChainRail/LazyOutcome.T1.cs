namespace BreadTh.ChainRail;

internal class Lazyoutcome<VALUE> : LazyOutcomeBase<IOutcome<VALUE>, VALUE>, ILazyOutcome<VALUE>
{
    public Lazyoutcome(Func<Task<IOutcome<VALUE>>> coldTaskGetter, IOutcomeFactory factory)
        : base(coldTaskGetter, factory)
    { }

    public async Task<IOutcome<VALUE>> Execute() =>
        await GetTask();

    public async Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        var result = await GetTask();
        await result.Unpack(onSuccess, onError);
    }

    public async Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        var result = await GetTask();
        await result.Unpack(onSuccess, onError);
    }

    public async Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        var result = await GetTask();
        await result.Unpack(onSuccess, onError);
    }

    public async Task Execute(Action<VALUE> onSuccess, Action<IError> onError)
    {
        var result = await GetTask();
        result.Unpack(onSuccess, onError);
    }



    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, ILazyOutcome<OUTPUT>> nextTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error<OUTPUT>(result.error!);

                return await nextTask(result.result!).Execute();
            },
            factory
        );

    public ILazyOutcome Pipe(Func<VALUE, ILazyOutcome> nextTask) =>
        new LazyOutcome(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error(result.error!);

                return await nextTask(result.result!).Execute();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> nextTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error<OUTPUT>(result.error!);

                return await nextTask(result.result!);
            },
            factory
        );

    public ILazyOutcome Pipe(Func<VALUE, Task<IOutcome>> nextTask) =>
        new LazyOutcome(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error(result.error!);

                return await nextTask(result.result!);
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> nextTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error<OUTPUT>(result.error!);

                return await nextTask(result.result!)();
            },
            factory
        );

    public ILazyOutcome Pipe(Func<VALUE, Func<Task<IOutcome>>> nextTask) =>
        new LazyOutcome(
            async () =>
            {
                var result = await GetTask();
                if (result.error is not null)
                    return factory.Error(result.error!);

                return await nextTask(result.result!)();
            },
            factory
        );


    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return logic(input.result!);
            },
            factory
        );

    public ILazyOutcome Pipe(Func<VALUE, IOutcome> logic) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);

                return logic(input.result!);
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return logic(input.result!)();
            },
            factory
        );

    public ILazyOutcome Pipe(Func<VALUE, Func<IOutcome>> logic) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);

                return logic(input.result!)();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(await logicTask(input.result!));
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(await logicTask(input.result!)());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(logic(input.result!));
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(logic(input.result!)());
            },
            factory
        );

    public ILazyOutcome ForgetResult() =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return factory.Success();
            },
            factory
        );
}
