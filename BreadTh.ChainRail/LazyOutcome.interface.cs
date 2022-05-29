
namespace BreadTh.ChainRail;

public interface ILazyOutcome : ILazyOutcomeBase
{
    Task<IOutcome> Execute();
}
