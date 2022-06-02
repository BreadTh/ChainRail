
namespace BreadTh.ChainRail;

public class AggregateError : AggregateErrorBase
{
    public AggregateError(List<IError> errors)
        : base("41be1f2c-36d1-4373-af9c-ca2c925683bd", errors)
    { }

    public AggregateError(IError[] errors)
        : this(errors.ToList())
    { }
}
