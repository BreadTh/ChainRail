
namespace BreadTh.ChainRail;

public interface ILazyOutcomeBase
{
    Task Execute(Func<Task> onSuccess, Action<IError> onError);
    Task Execute(Func<Task> onSuccess, Func<IError, Task> onError);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<IOutcome<OUTPUT>>> next);
    ILazyOutcome Then(Func<Func<IOutcome>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<IOutcome<OUTPUT>>>> next);
    ILazyOutcome Then(Func<Func<Task<IOutcome>>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Func<Task<OUTPUT>>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<IOutcome<OUTPUT>> next);
    ILazyOutcome Then(Func<IOutcome> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<ILazyOutcome<OUTPUT>> next);
    ILazyOutcome Then(Func<ILazyOutcome> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<OUTPUT> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next);
    ILazyOutcome Then(Func<Task<IOutcome>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(Func<Task<OUTPUT>> next);
    ILazyOutcome<OUTPUT> Then<OUTPUT>(ILazyOutcome<OUTPUT> next);
    ILazyOutcome Then(ILazyOutcome next);

    ILazyOutcome ThenInParallel(List<ILazyOutcome> next);
    ILazyOutcome ThenInParallel(ILazyOutcome[] next);

    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14, Func<Task<IOutcome<T15>>> next15, Func<Task<IOutcome<T16>>> next16);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14, Func<Task<IOutcome<T15>>> next15);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13, Func<Task<IOutcome<T14>>> next14);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12, Func<Task<IOutcome<T13>>> next13);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11, Func<Task<IOutcome<T12>>> next12);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10, Func<Task<IOutcome<T11>>> next11);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9, Func<Task<IOutcome<T10>>> next10);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8, Func<Task<IOutcome<T9>>> next9);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7, T8)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7, T8>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7, Func<Task<IOutcome<T8>>> next8);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6, T7)> ThenInParallel<T1, T2, T3, T4, T5, T6, T7>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6, Func<Task<IOutcome<T7>>> next7);
    ILazyOutcome<(T1, T2, T3, T4, T5, T6)> ThenInParallel<T1, T2, T3, T4, T5, T6>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5, Func<Task<IOutcome<T6>>> next6);
    ILazyOutcome<(T1, T2, T3, T4, T5)> ThenInParallel<T1, T2, T3, T4, T5>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4, Func<Task<IOutcome<T5>>> next5);
    ILazyOutcome<(T1, T2, T3, T4)> ThenInParallel<T1, T2, T3, T4>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3, Func<Task<IOutcome<T4>>> next4);
    ILazyOutcome<(T1, T2, T3)> ThenInParallel<T1, T2, T3>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2, Func<Task<IOutcome<T3>>> next3);
    ILazyOutcome<(T1, T2)> ThenInParallel<T1, T2>(Func<Task<IOutcome<T1>>> next1, Func<Task<IOutcome<T2>>> next2);
}