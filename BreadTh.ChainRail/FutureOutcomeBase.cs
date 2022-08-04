
namespace BreadTh.ChainRail;

internal abstract class FutureOutcomeBase<OUTCOME, RESULT> : IFutureOutcomeBase
    where OUTCOME : IOutcome<RESULT>
{
    protected readonly IChainRail factory;

    protected Func<Task<OUTCOME>> LazyInput { get; init; }
    //Thoughts of caching the result of the task, but how to convey to the user that it will only be executed once?
    //Better to keep simple and stupid for now.

    protected FutureOutcomeBase(Func<Task<OUTCOME>> lazyInput, IChainRail factory)
    {
        LazyInput = lazyInput;
        this.factory = factory;
    }

    async Task IFutureOutcomeBase.Execute(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        var result = await LazyInput();
        await result.Switch(onSuccess, onError);
    }

    async Task IFutureOutcomeBase.Execute(Func<Task> onSuccess, Action<IError> onError)
    {
        var result = await LazyInput();
        await result.Switch(onSuccess, onError);
    }

    private IFutureOutcome<OUTPUT> InnerThen<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next) =>
        new FutureOutcome<OUTPUT>(
            async () =>
                await (await LazyInput()).Unify(
                    onError: error => factory.Error<OUTPUT>(error),
                    onSuccess: async result => await next()
                ),
            factory
        );

    private IFutureOutcome InnerThen(Func<Task<IOutcome>> next) =>
        new FutureOutcome(
            async () =>
                await (await LazyInput()).Unify(
                    onError: error => factory.Error(error),
                    onSuccess: async result => await next()
                ),
            factory
        );


    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(IFutureOutcome<OUTPUT> next) =>
        InnerThen(() => next.Execute());

    IFutureOutcome IFutureOutcomeBase.Then(IFutureOutcome next) =>
        InnerThen(() => next.Execute());

    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next) =>
        InnerThen(() => next().Execute());

    IFutureOutcome IFutureOutcomeBase.Then(Func<IFutureOutcome> next) =>
        InnerThen(() => next().Execute());

    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next) =>
        InnerThen(next);

    IFutureOutcome IFutureOutcomeBase.Then(Func<Task<IOutcome>> next) =>
        InnerThen(next);

    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(Func<IOutcome<OUTPUT>> next) =>
        InnerThen(() => Task.FromResult(next()));

    IFutureOutcome IFutureOutcomeBase.Then(Func<IOutcome> next) =>
        InnerThen(() => Task.FromResult(next()));

    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(Func<Task<OUTPUT>> next) =>
        InnerThen(async () => factory.Success(await next()));

    IFutureOutcome<OUTPUT> IFutureOutcomeBase.Then<OUTPUT>(Func<OUTPUT> next) =>
        InnerThen(() => Task.FromResult(factory.Success(next())));

    IFutureOutcome IFutureOutcomeBase.Then<OUTPUT>(Action next) =>
        InnerThen(() =>
        {
            next();
            return Task.FromResult(factory.Success());
        });


    IFutureOutcome IFutureOutcomeBase.Then<OUTPUT>(Func<Task> next) =>
        InnerThen(async () =>
        {
            await next();
            return factory.Success();
        });

    IFutureOutcome IFutureOutcomeBase.ThenInParallel(IEnumerable<IFutureOutcome> next) =>
        new FutureOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error(input.Error);

                List<Task<IOutcome>> hotTasks = next
                    .Select(chain => chain.Execute())
                    .ToList();

                await Task.WhenAll(hotTasks);

                List<IOutcome> results = hotTasks
                    .Select(task => task.Result)
                    .ToList();

                List<IError> errors = results
                    .Where(x => x.Error is not null)
                    .Select(x => x.Error!)
                    .ToList();

                if (errors.Any())
                    return factory.Error(errors);

                return factory.Success();
            },
            factory
        );


    IFutureOutcome<IEnumerable<OUTPUT>> IFutureOutcomeBase.ThenInParallel<OUTPUT>(IEnumerable<IFutureOutcome<OUTPUT>> next) =>
        new FutureOutcome<IEnumerable<OUTPUT>>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<IEnumerable<OUTPUT>>(input.Error);

                List<Task<IOutcome<OUTPUT>>> hotTasks = next
                    .Select(chain => chain.Execute())
                    .ToList();

                await Task.WhenAll(hotTasks);

                List<IOutcome<OUTPUT>> results = hotTasks
                    .Select(task => task.Result)
                    .ToList();

                List<IError> errors = results
                    .Where(x => x.Error is not null)
                    .Select(x => x.Error!)
                    .ToList();

                if (errors.Any())
                    return factory.Error<IEnumerable<OUTPUT>>(errors);
                else
                    return factory.Success(results.Select(x => x.Result!));
            },
            factory
        );

    IFutureOutcome<(T1, T2)>
        IFutureOutcomeBase.ThenInParallel<T1, T2>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2
    ) =>
        new FutureOutcome<(T1, T2)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2)>(input.Error);

                var (hotTask1, hotTask2) =
                    (next1(), next2());

                await Task.WhenAll(hotTask1, hotTask2);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3
    ) =>
        new FutureOutcome<(T1, T2, T3)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3)>(input.Error);

                var (hotTask1, hotTask2, hotTask3) =
                    (next1(), next2(), next3());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4
    ) =>
        new FutureOutcome<(T1, T2, T3, T4)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4) =
                    (next1(), next2(), next3(), next4());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5) =
                    (next1(), next2(), next3(), next4(), next5());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6) =
                    (next1(), next2(), next3(), next4(), next5(), next6());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11,
        Func<Task<IOutcome<T12>>> next12
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                    hotTask12.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!,
                        hotTask12.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11,
        Func<Task<IOutcome<T12>>> next12,
        Func<Task<IOutcome<T13>>> next13
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                    hotTask12.Result.Error!,
                    hotTask13.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!,
                        hotTask12.Result.Result!,
                        hotTask13.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11,
        Func<Task<IOutcome<T12>>> next12,
        Func<Task<IOutcome<T13>>> next13,
        Func<Task<IOutcome<T14>>> next14
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                    hotTask12.Result.Error!,
                    hotTask13.Result.Error!,
                    hotTask14.Result.Error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!,
                        hotTask12.Result.Result!,
                        hotTask13.Result.Result!,
                        hotTask14.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11,
        Func<Task<IOutcome<T12>>> next12,
        Func<Task<IOutcome<T13>>> next13,
        Func<Task<IOutcome<T14>>> next14,
        Func<Task<IOutcome<T15>>> next15
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14(), next15());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                    hotTask12.Result.Error!,
                    hotTask13.Result.Error!,
                    hotTask14.Result.Error!,
                    hotTask15.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!,
                        hotTask12.Result.Result!,
                        hotTask13.Result.Result!,
                        hotTask14.Result.Result!,
                        hotTask15.Result.Result!
                    ));
            },
            factory
        );

    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>
        IFutureOutcomeBase.ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7,
        Func<Task<IOutcome<T8>>> next8,
        Func<Task<IOutcome<T9>>> next9,
        Func<Task<IOutcome<T10>>> next10,
        Func<Task<IOutcome<T11>>> next11,
        Func<Task<IOutcome<T12>>> next12,
        Func<Task<IOutcome<T13>>> next13,
        Func<Task<IOutcome<T14>>> next14,
        Func<Task<IOutcome<T15>>> next15,
        Func<Task<IOutcome<T16>>> next16
    ) =>
        new FutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.Error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(input.Error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15, hotTask16) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14(), next15(), next16());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15, hotTask16);

                var errors = new List<IError>()
                {
                    hotTask1.Result.Error!,
                    hotTask2.Result.Error!,
                    hotTask3.Result.Error!,
                    hotTask4.Result.Error!,
                    hotTask5.Result.Error!,
                    hotTask6.Result.Error!,
                    hotTask7.Result.Error!,
                    hotTask8.Result.Error!,
                    hotTask9.Result.Error!,
                    hotTask10.Result.Error!,
                    hotTask11.Result.Error!,
                    hotTask12.Result.Error!,
                    hotTask13.Result.Error!,
                    hotTask14.Result.Error!,
                    hotTask15.Result.Error!,
                    hotTask16.Result.Error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.Result!,
                        hotTask2.Result.Result!,
                        hotTask3.Result.Result!,
                        hotTask4.Result.Result!,
                        hotTask5.Result.Result!,
                        hotTask6.Result.Result!,
                        hotTask7.Result.Result!,
                        hotTask8.Result.Result!,
                        hotTask9.Result.Result!,
                        hotTask10.Result.Result!,
                        hotTask11.Result.Result!,
                        hotTask12.Result.Result!,
                        hotTask13.Result.Result!,
                        hotTask14.Result.Result!,
                        hotTask15.Result.Result!,
                        hotTask16.Result.Result!
                    ));
            },
            factory
        );
}
