
namespace BreadTh.ChainRail;

internal abstract class LazyOutcomeBase<OUTCOME, RESULT> : ILazyOutcomeBase
    where OUTCOME : IOutcome<RESULT>
{
    protected readonly IOutcomeFactory factory;

    protected Func<Task<OUTCOME>> GetTask { get; init; }
    //Thoughts of caching the result of the task, but how to convey to the user that it will only be executed once?
    //Better to keep simple and stupid for now.

    protected LazyOutcomeBase(Func<Task<OUTCOME>> coldTaskGetter, IOutcomeFactory factory)
    {
        GetTask = coldTaskGetter;
        this.factory = factory;
    }

    public async Task Execute(Func<Task> onSuccess, Func<IError, Task> onError)
    {
        var result = await GetTask();
        await result.Unpack(onSuccess, onError);
    }

    public async Task Execute(Func<Task> onSuccess, Action<IError> onError)
    {
        var result = await GetTask();
        await result.Unpack(onSuccess, onError);
    }

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<ILazyOutcome<OUTPUT>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await logic().Execute();
            },
            factory
        );

    public ILazyOutcome Then(Func<ILazyOutcome> logic) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await logic().Execute();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(ILazyOutcome<OUTPUT> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await logicTask.Execute();
            },
            factory
        );

    public ILazyOutcome Then(ILazyOutcome logicTask) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await logicTask.Execute();
            },
            factory
        );


    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await logicTask();
            },
            factory
        );

    public ILazyOutcome Then(Func<Task<IOutcome>> logicTask) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await logicTask();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IOutcome<OUTPUT>>>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return await logicTask()();
            },
            factory
        );

    public ILazyOutcome Then(Func<Func<Task<IOutcome>>> logicTask) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return await logicTask()();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return logic();
            },
            factory
        );

    public ILazyOutcome Then(Func<IOutcome> logic) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return logic();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IOutcome<OUTPUT>>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return logic()();
            },
            factory
        );

    public ILazyOutcome Then(Func<Func<IOutcome>> logic) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);
                else
                    return logic()();
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(await logicTask());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<OUTPUT>>> logicTask) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(await logicTask()());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(logic());
            },
            factory
        );

    public ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<OUTPUT>> logic) =>
        new Lazyoutcome<OUTPUT>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<OUTPUT>(input.error);
                else
                    return factory.Success(logic()());
            },
            factory
        );

    public ILazyOutcome ThenInParallel(List<ILazyOutcome> logicTasks) =>
        new LazyOutcome(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error(input.error);

                List<Task<IOutcome>> hotTasks = logicTasks
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

    public ILazyOutcome ThenInParallel(ILazyOutcome[] logicTasks) =>
        ThenInParallel(logicTasks.ToList());


    public ILazyOutcome<(T1, T2)>
        ThenInParallel<T1, T2>
    (
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2
    ) =>
        new Lazyoutcome<(T1, T2)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2)>(input.error);

                var (hotTask1, hotTask2) =
                    (func1(), func2());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3
    ) =>
        new Lazyoutcome<(T1, T2, T3)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3)>(input.error);

                var (hotTask1, hotTask2, hotTask3) =
                    (func1(), func2(), func3());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4) =
                    (func1(), func2(), func3(), func4());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5) =
                    (func1(), func2(), func3(), func4(), func5());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6) =
                    (func1(), func2(), func3(), func4(), func5(), func6());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11,
        Func<Task<IOutcome<T12>>> func12
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11(), func12());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11,
        Func<Task<IOutcome<T12>>> func12,
        Func<Task<IOutcome<T13>>> func13
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11(), func12(), func13());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11,
        Func<Task<IOutcome<T12>>> func12,
        Func<Task<IOutcome<T13>>> func13,
        Func<Task<IOutcome<T14>>> func14
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11(), func12(), func13(), func14());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11,
        Func<Task<IOutcome<T12>>> func12,
        Func<Task<IOutcome<T13>>> func13,
        Func<Task<IOutcome<T14>>> func14,
        Func<Task<IOutcome<T15>>> func15
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11(), func12(), func13(), func14(), func15());

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
        Func<Task<IOutcome<T1>>> func1,
        Func<Task<IOutcome<T2>>> func2,
        Func<Task<IOutcome<T3>>> func3,
        Func<Task<IOutcome<T4>>> func4,
        Func<Task<IOutcome<T5>>> func5,
        Func<Task<IOutcome<T6>>> func6,
        Func<Task<IOutcome<T7>>> func7,
        Func<Task<IOutcome<T8>>> func8,
        Func<Task<IOutcome<T9>>> func9,
        Func<Task<IOutcome<T10>>> func10,
        Func<Task<IOutcome<T11>>> func11,
        Func<Task<IOutcome<T12>>> func12,
        Func<Task<IOutcome<T13>>> func13,
        Func<Task<IOutcome<T14>>> func14,
        Func<Task<IOutcome<T15>>> func15,
        Func<Task<IOutcome<T16>>> func16
    ) =>
        new Lazyoutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(
            async () =>
            {
                var input = await GetTask();
                if (input.error is not null)
                    return factory.Error<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)>(input.error);

                var (hotTask1, hotTask2, hotTask3, hotTask4, hotTask5, hotTask6, hotTask7, hotTask8, hotTask9, hotTask10, hotTask11, hotTask12, hotTask13, hotTask14, hotTask15, hotTask16) =
                    (func1(), func2(), func3(), func4(), func5(), func6(), func7(), func8(), func9(), func10(), func11(), func12(), func13(), func14(), func15(), func16());

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
