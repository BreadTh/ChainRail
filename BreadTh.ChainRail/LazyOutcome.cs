
namespace BreadTh.ChainRail;

internal class LazyOutcome : LazyOutcomeBase<IOutcome, Empty>, ILazyOutcome
{
    internal LazyOutcome(Func<Task<IOutcome>> lazyInput, IChainRailFactory factory)
        : base(lazyInput, factory)
    { }

    public async Task<IOutcome> Execute() =>
        await LazyInput();
}
