namespace BreadTh.ChainRail;

internal class FutureOutcome<VALUE> : FutureOutcomeBase<IOutcome<VALUE>, VALUE>, IFutureOutcome<VALUE>
{
    public FutureOutcome(Func<Task<IOutcome<VALUE>>> lazyInput, IChainRail factory)
        : base(lazyInput, factory)
    { }

    public async Task<IOutcome<VALUE>> Execute() =>
        await LazyInput();

    public async Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    public async Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    public async Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    public async Task Execute(Action<VALUE> onSuccess, Action<IError> onError)
    {
        var input = await LazyInput();
        input.Switch(onSuccess, onError);
    }



    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error!);

                return await next(input.result!).Execute();
            },
            factory
        );

    public IFutureOutcome Pipe(Func<VALUE, IFutureOutcome> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error!);

                return await next(input.result!).Execute();
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error!);

                return await next(input.result!);
            },
            factory
        );

    public IFutureOutcome Pipe(Func<VALUE, Task<IOutcome>> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error!);

                return await next(input.result!);
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error!);

                return await next(input.result!)();
            },
            factory
        );

    public IFutureOutcome Pipe(Func<VALUE, Func<Task<IOutcome>>> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error!);

                return await next(input.result!)();
            },
            factory
        );


    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return next(input.result!);
            },
            factory
        );

    public IFutureOutcome Pipe(Func<VALUE, IOutcome> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);

                return next(input.result!);
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return next(input.result!)();
            },
            factory
        );

    public IFutureOutcome Pipe(Func<VALUE, Func<IOutcome>> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);

                return next(input.result!)();
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Task<OUTPUT>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(await next(input.result!));
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(await next(input.result!)());
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, OUTPUT> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(next(input.result!));
            },
            factory
        );

    public IFutureOutcome<OUTPUT> Pipe<OUTPUT>(Func<VALUE, Func<OUTPUT>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);

                return factory.Success(next(input.result!)());
            },
            factory
        );

    public IFutureOutcome ForgetResult() =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                
                return factory.Success();
            },
            factory
        );





    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!).Execute();
                
                if(output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Func<VALUE, IFutureOutcome> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!).Execute();
                
                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!);

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Func<VALUE, Task<IOutcome>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!);

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<Task<IOutcome<OUTPUT>>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!)();

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Func<VALUE, Func<Task<IOutcome>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = await next(input.result!)();
                
                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );


    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = next(input.result!);

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Func<VALUE, IOutcome> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = next(input.result!);

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = next(input.result!)();

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Func<VALUE, Func<IOutcome>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                var output = next(input.result!)();

                if (output.error is not null)
                    return factory.Error<VALUE>(output.error);
                else
                    return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<OUTPUT>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                _ = await next(input.result!);
                return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<Task<OUTPUT>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                _ = await next(input.result!)();

                return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, OUTPUT> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return input;

                _ = next(input.result!);
                return input;
            },
            factory
        );

    public IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Func<OUTPUT>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();

                if(input.error is not null)
                    return input;

                _ = next(input.result!)();

                return input;
                

            },
            factory
        );

    public IFutureOutcome<VALUE> Tee(Action<VALUE> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();

                if(input.error is not null)
                    return input;

                next(input.result!);

                return input;
                

            },
            factory
        );
}
