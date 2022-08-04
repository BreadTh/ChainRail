
namespace BreadTh.ChainRail;

public interface IChainRail
{
    IOutcome Error(IError error);
    IOutcome Error(IError[] errors);
    IOutcome Error(List<IError> errors);
    IOutcome<VALUE> Error<VALUE>(IError error);
    IOutcome<VALUE> Error<VALUE>(IError[] errors);
    IOutcome<VALUE> Error<VALUE>(List<IError> errors);
    IFutureOutcome StartChain();
    IFutureOutcome<VALUE> StartChain<VALUE>(VALUE startValue);
    IOutcome Success();
    IOutcome<VALUE> Success<VALUE>(VALUE result);
}
