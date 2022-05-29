
namespace BreadTh.ChainRail;

internal class LazyOutcome : LazyOutcomeBase<IOutcome, Empty>, ILazyOutcome
{
    internal LazyOutcome(Func<Task<IOutcome>> coldTaskGetter, IOutcomeFactory factory)
        : base(coldTaskGetter, factory)
    { }

    public async Task<IOutcome> Execute() =>
        await GetTask();
}
