
namespace BreadTh.ChainRail;

public interface IFutureOutcomeBase
{
    Task Execute(Func<Task> onSuccess, Action<IError> onError);
    Task Execute(Func<Task> onSuccess, Func<IError, Task> onError);

    //.Then overload combinations
    //  actual value
    //      OUTPUT
    //      Nothing/Action      (returns ILazyOutcome instead of IFutureOutcome<T>)
    //      IOutcome<OUTPUT>
    //      IOutcome            (returns ILazyOutcome instead of IFutureOutcome<T>)
    //      ILazyOutcome<OUTPUT>(Not with Func<Task<T>> wrapper, because why would getting an unfulfilled future be async)
    //      ILazyOutcome        (returns ILazyOutcome instead of IFutureOutcome<T>, Not with Func<Task<T>> wrapper)
    //
    //  value wrapper
    //      Func<T>
    //      Func<Task<T>>
    //      no wrapper          (Only with IFutureOutcome/IFutureOutcome<T> - too easy to accidentally execute
    //                          early if it allowed for taking e.g. OUTPUT directly)


    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> next);
    IFutureOutcome Then<OUTPUT>(Action next);
    IFutureOutcome Then<OUTPUT>(Func<Task> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome Then(Func<IOutcome> next);
    IFutureOutcome Then(Func<Task<IOutcome>> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(IFutureOutcome<OUTPUT> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next);
    IFutureOutcome Then(IFutureOutcome next);
    IFutureOutcome Then(Func<IFutureOutcome> next);



    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(IEnumerable<IFutureOutcome<OUTPUT>> next);
    IFutureOutcome ThenInParallel(IEnumerable<IFutureOutcome> next);



    IFutureOutcome<(T1, T2)> ThenInParallel<T1, T2>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2);
    IFutureOutcome<(T1, T2, T3)> ThenInParallel<T1, T2, T3>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3);
    IFutureOutcome<(T1, T2, T3, T4)> ThenInParallel<T1, T2, T3, T4>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4);
    IFutureOutcome<(T1, T2, T3, T4, T5)> ThenInParallel<T1, T2, T3, T4, T5>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6)> ThenInParallel<T1, T2, T3, T4, T5, T6>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14, Func<Task<IOutcome<T15>>> next15);
    IFutureOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14, Func<Task<IOutcome<T15>>> next15, Func<Task<IOutcome<T16>>> next16);
}