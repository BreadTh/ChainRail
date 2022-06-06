
namespace BreadTh.ChainRail;

public class ChainRail : IChainRail
{
    public IOutcome Error(IError error) =>
        new Outcome(error);

    public IOutcome Error(IError[] errors) =>
        new Outcome(new AggregateError(errors));

    public IOutcome Error(List<IError> errors) =>
        new Outcome(new AggregateError(errors.ToArray()));

    public IOutcome Success() =>
        new Outcome(null);


    public IOutcome<VALUE> Error<VALUE>(IError error) =>
        new Outcome<VALUE>(default, error);

    public IOutcome<VALUE> Error<VALUE>(IError[] errors) =>
        new Outcome<VALUE>(default, new AggregateError(errors));

    public IOutcome<VALUE> Error<VALUE>(List<IError> errors) =>
        new Outcome<VALUE>(default, new AggregateError(errors.ToArray()));

    public IOutcome<VALUE> Success<VALUE>(VALUE result) =>
        new Outcome<VALUE>(result, default);


    public ILazyOutcome StartChain() =>
        new LazyOutcome(() => Task.FromResult((IOutcome)new Outcome(null!)), this);

    public ILazyOutcome<VALUE> StartChain<VALUE>(VALUE startValue) =>
        new LazyOutcome<VALUE>(() => Task.FromResult((IOutcome<VALUE>)new Outcome<VALUE>(startValue, null!)), this);
}
