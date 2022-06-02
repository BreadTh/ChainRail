using BreadTh.ChainRail;

public class UnexpectedStatusCodeError : ErrorBase, IError
{
    internal UnexpectedStatusCodeError(int actual, int expected)
        : base("", "")
    { }
}
