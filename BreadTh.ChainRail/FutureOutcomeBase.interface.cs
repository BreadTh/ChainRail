
using System.Collections.Generic;

namespace BreadTh.ChainRail;

public interface IFutureOutcomeBase
{
    Task Execute(Func<Task> onSuccess, Action<IError> onError);
    Task Execute(Func<Task> onSuccess, Func<IError, Task> onError);

    //.Then overload combinations
    //  actual value
    //      OUTPUT 
    //      Nothing             (returns ILazyOutcome instead of IFutureOutcome<T>)
    //      IOutcome<OUTPUT>
    //      IOutcome            (returns ILazyOutcome instead of IFutureOutcome<T>)
    //      ILazyOutcome<OUTPUT>
    //      ILazyOutcome        (returns ILazyOutcome instead of IFutureOutcome<T>)
    //
    //  value wrapper
    //      Func<T>
    //      Func<Task<T>>
    //      Func<Func<T>>
    //      Func<Func<Task<T>>
    //      no wrapper          (Only with IFutureOutcome/IFutureOutcome<T> - too easy to accidentally execute
    //                          early if it allowed for taking e.g. OUTPUT directly)


    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<OUTPUT>>> next);

    IFutureOutcome Then<OUTPUT>(Action next);
    IFutureOutcome Then<OUTPUT>(Func<Task> next);
    IFutureOutcome Then<OUTPUT>(Func<Action> next);
    IFutureOutcome Then<OUTPUT>(Func<Func<Task>> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IOutcome<OUTPUT>>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IOutcome<OUTPUT>>>> next);

    IFutureOutcome Then(Func<IOutcome> next);
    IFutureOutcome Then(Func<Task<IOutcome>> next);
    IFutureOutcome Then(Func<Func<IOutcome>> next);
    IFutureOutcome Then(Func<Func<Task<IOutcome>>> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IFutureOutcome>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IFutureOutcome>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IFutureOutcome>>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(IFutureOutcome next);

    IFutureOutcome Then(Func<IFutureOutcome> next);
    IFutureOutcome Then(Func<Task<IFutureOutcome>> next);
    IFutureOutcome Then(Func<Func<IFutureOutcome>> next);
    IFutureOutcome Then(Func<Func<Task<IFutureOutcome>>> next);
    IFutureOutcome Then(IFutureOutcome next);



    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<OUTPUT>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<Task<OUTPUT>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<OUTPUT>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<Task<OUTPUT>>>> next);

    IFutureOutcome ThenInParallel<OUTPUT>(IEnumerable<Action> next);
    IFutureOutcome ThenInParallel<OUTPUT>(Func<IEnumerable<Task>> next);
    IFutureOutcome ThenInParallel<OUTPUT>(Func<IEnumerable<Action>> next);
    IFutureOutcome ThenInParallel<OUTPUT>(Func<Func<IEnumerable<Task>>> next);

    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<IOutcome<OUTPUT>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<Task<IOutcome<OUTPUT>>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<IOutcome<OUTPUT>>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<Task<IOutcome<OUTPUT>>>>> next);

    IFutureOutcome ThenInParallel(Func<IEnumerable<IOutcome>> next);
    IFutureOutcome ThenInParallel(Func<IEnumerable<Task<IOutcome>>> next);
    IFutureOutcome ThenInParallel(Func<Func<IEnumerable<IOutcome>>> next);
    IFutureOutcome ThenInParallel(Func<Func<IEnumerable<Task<IOutcome>>>> next);

    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<IFutureOutcome<OUTPUT>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<IEnumerable<Task<IFutureOutcome>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<IFutureOutcome>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(Func<Func<IEnumerable<Task<IFutureOutcome>>>> next);
    IFutureOutcome<IEnumerable<OUTPUT>> ThenInParallel<OUTPUT>(IEnumerable<IFutureOutcome> next);

    IFutureOutcome ThenInParallel(Func<IEnumerable<IFutureOutcome>> next);
    IFutureOutcome ThenInParallel(Func<IEnumerable<Task<IFutureOutcome>>> next);
    IFutureOutcome ThenInParallel(Func<Func<IEnumerable<IFutureOutcome>>> next);
    IFutureOutcome ThenInParallel(Func<Func<IEnumerable<Task<IFutureOutcome>>>> next);
    IFutureOutcome ThenInParallel(IEnumerable<IFutureOutcome> next);



    //TODO: When more set in the way it all should work, all combinations
    //  should be mixed with wrappers and values. Until then it's a lot of repeated code to lock into.
    //  Also, add itemized PipleInParallel
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