
namespace BreadTh.ChainRail;

public class OutcomeFactory : IOutcomeFactory
{
    public IOutcome Error(IError error) =>
        new Outcome(error);

    public IOutcome Error(IError[] errors) =>
        new Outcome(new OutcomeAggregateError(errors));

    public IOutcome Error(List<IError> errors) =>
        new Outcome(new OutcomeAggregateError(errors.ToArray()));

    public IOutcome Success() =>
        new Outcome(null);


    public IOutcome<VALUE> Error<VALUE>(IError error) =>
        new Outcome<VALUE>(default, error);

    public IOutcome<VALUE> Error<VALUE>(IError[] errors) =>
        new Outcome<VALUE>(default, new OutcomeAggregateError(errors));

    public IOutcome<VALUE> Error<VALUE>(List<IError> errors) =>
        new Outcome<VALUE>(default, new OutcomeAggregateError(errors.ToArray()));

    public IOutcome<VALUE> Success<VALUE>(VALUE result) =>
        new Outcome<VALUE>(result, default);


    public ILazyOutcome StartChain() =>
        new LazyOutcome(() => Task.FromResult((IOutcome)new Outcome(null!)), this);
}
