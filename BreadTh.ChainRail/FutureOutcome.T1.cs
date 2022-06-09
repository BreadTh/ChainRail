namespace BreadTh.ChainRail;

internal class FutureOutcome<VALUE> : FutureOutcomeBase<IOutcome<VALUE>, VALUE>, IFutureOutcome<VALUE>
{
    public FutureOutcome(Func<Task<IOutcome<VALUE>>> lazyInput, IChainRail factory)
        : base(lazyInput, factory)
    { }

    async Task<IOutcome<VALUE>> IFutureOutcome<VALUE>.Execute() =>
        await LazyInput();

    async Task IFutureOutcome<VALUE>.Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    async Task IFutureOutcome<VALUE>.Execute(Action<VALUE> onSuccess, Func<IError, Task> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    async Task IFutureOutcome<VALUE>.Execute(Func<VALUE, Task> onSuccess, Action<IError> onError)
    {
        var input = await LazyInput();
        await input.Switch(onSuccess, onError);
    }

    async Task IFutureOutcome<VALUE>.Execute(Action<VALUE> onSuccess, Action<IError> onError)
    {
        var input = await LazyInput();
        input.Switch(onSuccess, onError);
    }

    IFutureOutcome IFutureOutcome<VALUE>.ForgetResult() =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error(input.Error);

                return factory.Success();
            },
            factory
        );


    private IFutureOutcome<OUTPUT> InnerPipe<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
                await (await LazyInput()).Unify(
                    onError: error => factory.Error<OUTPUT>(error),
                    onSuccess: async result => await next(result)
                ),
            factory
        );

    private IFutureOutcome InnerPipe(Func<VALUE, Task<IOutcome>> next) =>
        new FutureOutcome(
            async () =>
                await (await LazyInput()).Unify(
                    onError: error => factory.Error(error),
                    onSuccess: async result => await next(result)
                ),
            factory
        );

    IFutureOutcome<OUTPUT> IFutureOutcome<VALUE>.Then<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next) =>
        InnerPipe(value => next(value).Execute());

    IFutureOutcome IFutureOutcome<VALUE>.Then(Func<VALUE, IFutureOutcome> next) =>
        InnerPipe(value => next(value).Execute());

    IFutureOutcome<OUTPUT> IFutureOutcome<VALUE>.Then<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        InnerPipe(next);

    IFutureOutcome IFutureOutcome<VALUE>.Then(Func<VALUE, Task<IOutcome>> next) =>
        InnerPipe(next);

    IFutureOutcome<OUTPUT> IFutureOutcome<VALUE>.Then<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next) =>
        InnerPipe(value => Task.FromResult(next(value)));

    IFutureOutcome IFutureOutcome<VALUE>.Then(Func<VALUE, IOutcome> next) =>
        InnerPipe(value => Task.FromResult(next(value)));

    IFutureOutcome<OUTPUT> IFutureOutcome<VALUE>.Then<OUTPUT>(Func<VALUE, Task<OUTPUT>> next) =>
        InnerPipe(async value => factory.Success(await next(value)));

    IFutureOutcome<OUTPUT> IFutureOutcome<VALUE>.Then<OUTPUT>(Func<VALUE, OUTPUT> next) =>
        InnerPipe(value => Task.FromResult(factory.Success(next(value))));

    IFutureOutcome IFutureOutcome<VALUE>.Then(Action<VALUE> next) =>
        InnerPipe(value => 
        { 
            next(value);
            return Task.FromResult(factory.Success());
        });


    IFutureOutcome IFutureOutcome<VALUE>.Then(Func<VALUE, Task> next) =>
        InnerPipe(value => 
        {
            next(value);
            return Task.FromResult(factory.Success());
        });

    private IFutureOutcome<VALUE> InnerTee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return input;

                var output = await next(input.Result!);

                if (output.Error is not null)
                    return factory.Error<VALUE>(output.Error);
                else
                    return input;
            },
            factory
        );

    private IFutureOutcome<VALUE> InnerTee(Func<VALUE, Task<IOutcome>> next) =>
        new FutureOutcome<VALUE>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return input;

                var output = await next(input.Result!);

                if (output.Error is not null)
                    return factory.Error<VALUE>(output.Error);
                else
                    return input;
            },
            factory
        );

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next) =>
        InnerTee(value => next(value).Execute());

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee(Func<VALUE, IFutureOutcome> next) =>
        InnerTee(value => next(value).Execute());

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        InnerTee(next);

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee(Func<VALUE, Task<IOutcome>> next) =>
        InnerTee(next);

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next) =>
        InnerTee(value => Task.FromResult(next(value)));

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee(Func<VALUE, IOutcome> next) =>
        InnerTee(value => Task.FromResult(next(value)));

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee<OUTPUT>(Func<VALUE, Task<OUTPUT>> next) =>
        InnerTee(value => Task.FromResult(factory.Success(next(value))));

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee<OUTPUT>(Func<VALUE, OUTPUT> next) =>
        InnerTee(value => Task.FromResult(factory.Success(next(value))));

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee(Action<VALUE> next) =>
        InnerTee(value => 
        {
            next(value);
            return Task.FromResult(factory.Success());
        });

    IFutureOutcome<VALUE> IFutureOutcome<VALUE>.Tee(Func<VALUE, Task> next) =>
        InnerTee(value => 
        {
            next(value);
            return Task.FromResult(factory.Success());
        });


    private IFutureOutcome<(VALUE, OUTPUT)> InnerAdd<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<(VALUE, OUTPUT)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(VALUE, OUTPUT)>(input.Error);

                var output = await next(input.Result!);

                if (output.Error is not null)
                    return factory.Error<(VALUE, OUTPUT)>(output.Error);
                else
                    return factory.Success((input.Result!, output.Result!));
            },
            factory
        );

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<VALUE, OUTPUT> next) =>
        InnerAdd(value => Task.FromResult(factory.Success(next(value))));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<VALUE, Task<OUTPUT>> next) =>
        InnerAdd(async value => factory.Success(await next(value)));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next) =>
        InnerAdd(value => Task.FromResult(next(value)));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next) =>
        InnerAdd(next);

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next) =>
        InnerAdd(value => next(value).Execute());

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<OUTPUT> next) =>
        InnerAdd(value => Task.FromResult(factory.Success(next())));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<Task<OUTPUT>> next) =>
        InnerAdd(async value => factory.Success(await next()));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<IOutcome<OUTPUT>> next) =>
        InnerAdd(value => Task.FromResult(next()));

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next) =>
        InnerAdd(value => next());

    IFutureOutcome<(VALUE, OUTPUT)> IFutureOutcome<VALUE>.Add<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next) =>
        InnerAdd(value => next().Execute());
}
