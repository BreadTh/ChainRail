
namespace BreadTh.ChainRail;

public interface IChainRailFactory
{
    IOutcome Error(IError error);
    IOutcome Error(IError[] errors);
    IOutcome Error(List<IError> errors);
    IOutcome<VALUE> Error<VALUE>(IError error);
    IOutcome<VALUE> Error<VALUE>(IError[] errors);
    IOutcome<VALUE> Error<VALUE>(List<IError> errors);
    ILazyOutcome StartChain();
    IOutcome Success();
    IOutcome<VALUE> Success<VALUE>(VALUE result);
}
