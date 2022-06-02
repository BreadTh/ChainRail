
namespace BreadTh.ChainRail;

internal abstract class LazyOutcomeBase<OUTCOME, RESULT> : ILazyOutcomeBase
    where OUTCOME : IOutcome<RESULT>
{
    protected readonly IChainRailFactory factory;

    protected Func<Task<OUTCOME>> LazyInput { get; init; }
    //Thoughts of caching the result of the task, but how to convey to the user that it will only be executed once?
    //Better to keep simple and stupid for now.

    protected LazyOutcomeBase(Func<Task<OUTCOME>> lazyInput, IChainRailFactory factory)
    {
        LazyInput = lazyInput;
        this.factory = factory;
    }

    public async Task Execute(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        var result = await LazyInput();
        await result.Switch(onSuccess, onError);
    }

    public async Task Execute(Func<Task> onSuccess, Action<IError> onError)
    {
        var result = await LazyInput();
        await result.Switch(onSuccess, onError);
    }

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<ILazyOutcome<OUTPUT>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await next().Execute();
            },
            factory
        );

    public ILazyOutcome Then(Func<ILazyOutcome> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await next().Execute();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(ILazyOutcome<OUTPUT> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await next.Execute();
            },
            factory
        );

    public ILazyOutcome Then(ILazyOutcome next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await next.Execute();
            },
            factory
        );


    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await next();
            },
            factory
        );

    public ILazyOutcome Then(Func<Task<IOutcome>> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await next();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IOutcome<OUTPUT>>>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await next()();
            },
            factory
        );

    public ILazyOutcome Then(Func<Func<Task<IOutcome>>> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await next()();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return next();
            },
            factory
        );

    public ILazyOutcome Then(Func<IOutcome> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return next();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IOutcome<OUTPUT>>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return next()();
            },
            factory
        );

    public ILazyOutcome Then(Func<Func<IOutcome>> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return next()();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(await next());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<OUTPUT>>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(await next()());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(next());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<OUTPUT>> next) =>
        new LazyOutcome<OUTPUT>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(next()());
            },
            factory
        );

    public ILazyOutcome ThenInParallel(List<ILazyOutcome> next) =>
        new LazyOutcome(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error(input.error);

                List<Task<IOutcome>> hotTasks = next
                    .Select(chain => chain.Execute())
                    .ToList();

                await Task.WhenAll(hotTasks);

                List<IOutcome> results = hotTasks
                    .Select(task => task.Result)
                    .ToList();

                List<IError> errors = results
                    .Where(x => x.error is not null)
                    .Select(x => x.error!)
                    .ToList();

                if (errors.Any())
                    return factory.Error(errors);

                return factory.Success();
            },
            factory
        );

    public ILazyOutcome ThenInParallel(ILazyOutcome[] next) =>
        ThenInParallel(next.ToList());


    public ILazyOutcome<(T1, T2)>
        ThenInParallel<T1, T2>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2
    ) =>
        new LazyOutcome<(T1, T2)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2)>(input.error);

                var (hotTask1, hotTask2) =
                    (next1(), next2());

                await Task.WhenAll(hotTask1, hotTask2);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3)>
        ThenInParallel<T1, T2, T3>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3
    ) =>
        new LazyOutcome<(T1, T2, T3)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3)>(input.error);

                var (hotTask1, hotTask2, hotTask3) =
                    (next1(), next2(), next3());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4)>
        ThenInParallel<T1, T2, T3, T4>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4
    ) =>
        new LazyOutcome<(T1, T2, T3, T4)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4) =
                    (next1(), next2(), next3(), next4());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5)>
        ThenInParallel<T1, T2, T3, T4, T5>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5
    ) =>
        new LazyOutcome<(T1, T2, T3, T4, T5)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5) =
                    (next1(), next2(), next3(), next4(), next5());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6)>
        ThenInParallel<T1, T2, T3, T4, T5, T6>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6
    ) =>
        new LazyOutcome<(T1, T2, T3, T4, T5, T6)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6) =
                    (next1(), next2(), next3(), next4(), next5(), next6());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7>
    (
        Func<Task<IOutcome<T1>>> next1,
        Func<Task<IOutcome<T2>>> next2,
        Func<Task<IOutcome<T3>>> next3,
        Func<Task<IOutcome<T4>>> next4,
        Func<Task<IOutcome<T5>>> next5,
        Func<Task<IOutcome<T6>>> next6,
        Func<Task<IOutcome<T7>>> next7
    ) =>
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                    hotTask12.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!,
                        hotTask12.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                    hotTask12.Result.error!,
                    hotTask13.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!,
                        hotTask12.Result.result!,
                        hotTask13.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                    hotTask12.Result.error!,
                    hotTask13.Result.error!,
                    hotTask14.Result.error!,
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!,
                        hotTask12.Result.result!,
                        hotTask13.Result.result!,
                        hotTask14.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14(), next15());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                    hotTask12.Result.error!,
                    hotTask13.Result.error!,
                    hotTask14.Result.error!,
                    hotTask15.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!,
                        hotTask12.Result.result!,
                        hotTask13.Result.result!,
                        hotTask14.Result.result!,
                        hotTask15.Result.result!
                    ));
            },
            factory
        );

    public ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>
        ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
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
        new LazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(
            async () =>
            {
                var input = await LazyInput();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15, hotTask16) =
                    (next1(), next2(), next3(), next4(), next5(), next6(), next7(), next8(), next9(), next10(), next11(), next12(), next13(), next14(), next15(), next16());

                await Task.WhenAll(hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15, hotTask16);

                var errors = new List<IError>()
                {
                    hotTask1.Result.error!,
                    hotTask2.Result.error!,
                    hotTask3.Result.error!,
                    hotTask4.Result.error!,
                    hotTask5.Result.error!,
                    hotTask6.Result.error!,
                    hotTask7.Result.error!,
                    hotTask8.Result.error!,
                    hotTask9.Result.error!,
                    hotTask10.Result.error!,
                    hotTask11.Result.error!,
                    hotTask12.Result.error!,
                    hotTask13.Result.error!,
                    hotTask14.Result.error!,
                    hotTask15.Result.error!,
                    hotTask16.Result.error!
                }.Where(error => error is not null);

                if (errors.Any())
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(errors.ToList());
                else
                    return factory.Success((
                        hotTask1.Result.result!,
                        hotTask2.Result.result!,
                        hotTask3.Result.result!,
                        hotTask4.Result.result!,
                        hotTask5.Result.result!,
                        hotTask6.Result.result!,
                        hotTask7.Result.result!,
                        hotTask8.Result.result!,
                        hotTask9.Result.result!,
                        hotTask10.Result.result!,
                        hotTask11.Result.result!,
                        hotTask12.Result.result!,
                        hotTask13.Result.result!,
                        hotTask14.Result.result!,
                        hotTask15.Result.result!,
                        hotTask16.Result.result!
                    ));
            },
            factory
        );
}
