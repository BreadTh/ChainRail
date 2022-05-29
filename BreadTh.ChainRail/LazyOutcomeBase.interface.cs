
namespace BreadTh.ChainRail;

public interface ILazyOutcomeBase
{
    Task Execute(Func<Task> onSuccess, Action<IError> onError);
    Task Execute(Func<Task> onSuccess, Func<IError, Task> onError);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IOutcome<OUTPUT>>> logic);
    ILazyOutcome Then(Func<Func<IOutcome>> logic);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<OUTPUT>> logic);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IOutcome<OUTPUT>>>> logicTask);
    ILazyOutcome Then(Func<Func<Task<IOutcome>>> logicTask);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<OUTPUT>>> logicTask);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> logic);
    ILazyOutcome Then(Func<IOutcome> logic);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<ILazyOutcome<OUTPUT>> logic);
    ILazyOutcome Then(Func<ILazyOutcome> logic);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> logic);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> logicTask);
    ILazyOutcome Then(Func<Task<IOutcome>> logicTask);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> logicTask);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(ILazyOutcome<OUTPUT> logicTask);
    ILazyOutcome Then(ILazyOutcome logicTask);

    ILazyOutcome ThenInParallel(List<ILazyOutcome> logicTasks);
    ILazyOutcome ThenInParallel(ILazyOutcome[] logicTasks);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11, Func<Task<IOutcome<T12>>> func12, Func<Task<IOutcome<T13>>> func13, Func<Task<IOutcome<T14>>> func14, Func<Task<IOutcome<T15>>> func15, Func<Task<IOutcome<T16>>> func16);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11, Func<Task<IOutcome<T12>>> func12, Func<Task<IOutcome<T13>>> func13, Func<Task<IOutcome<T14>>> func14, Func<Task<IOutcome<T15>>> func15);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11, Func<Task<IOutcome<T12>>> func12, Func<Task<IOutcome<T13>>> func13, Func<Task<IOutcome<T14>>> func14);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11, Func<Task<IOutcome<T12>>> func12, Func<Task<IOutcome<T13>>> func13);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11, Func<Task<IOutcome<T12>>> func12);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10, Func<Task<IOutcome<T11>>> func11);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9, Func<Task<IOutcome<T10>>> func10);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8, Func<Task<IOutcome<T9>>> func9);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7, Func<Task<IOutcome<T8>>> func8);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6, Func<Task<IOutcome<T7>>> func7);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6)> ThenInParallel<T1, T2, T3, T4, T5, T6>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5, Func<Task<IOutcome<T6>>> func6);
    ILazyOutcome<(T1, T2, T3, T4, T5)> ThenInParallel<T1, T2, T3, T4, T5>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4, Func<Task<IOutcome<T5>>> func5);
    ILazyOutcome<(T1, T2, T3, T4)> ThenInParallel<T1, T2, T3, T4>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3, Func<Task<IOutcome<T4>>> func4);
    ILazyOutcome<(T1, T2, T3)> ThenInParallel<T1, T2, T3>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2, Func<Task<IOutcome<T3>>> func3);
    ILazyOutcome<(T1, T2)> ThenInParallel<T1, T2>(Func<Task<IOutcome<T1>>> func1, Func<Task<IOutcome<T2>>> func2);
}