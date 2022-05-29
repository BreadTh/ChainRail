namespace BreadTh.ChainRail;

internal class Outcome : Outcome<Empty>, IOutcome
{
    internal Outcome(IError? error) 
        : base(new Empty(), error)
    { }
}
