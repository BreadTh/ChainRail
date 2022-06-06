
namespace BreadTh.ChainRail;

internal class FutureOutcome : FutureOutcomeBase<IOutcome, Empty>, IFutureOutcome
{
    internal FutureOutcome(Func<Task<IOutcome>> lazyInput, IChainRail factory)
        : base(lazyInput, factory)
    { }

    public async Task<IOutcome> Execute() =>
        await LazyInput();
}
