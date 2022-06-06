
namespace BreadTh.ChainRail;

public interface IFutureOutcome : IFutureOutcomeBase
{
    Task<IOutcome> Execute();
}
