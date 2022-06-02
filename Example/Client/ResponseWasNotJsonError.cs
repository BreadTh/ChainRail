using BreadTh.ChainRail;

public class ResponseWasNotJsonError : ErrorBase, IError
{
    internal ResponseWasNotJsonError()
        : base("", "")
    { }
}
