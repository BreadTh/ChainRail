
namespace BreadTh.ChainRail;

public class OutcomeAggregateError : AggregateErrorBase
{
    public OutcomeAggregateError(List<IError> errors)
        : base("41be1f2c-36d1-4373-af9c-ca2c925683bd", errors)
    { }

    public OutcomeAggregateError(IError[] errors)
        : this(errors.ToList())
    { }
}
