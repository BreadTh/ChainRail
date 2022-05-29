
namespace BreadTh.ChainRail.Example.Server.Errors;

public class UnluckyProcessError : ErrorBase, IDisplayableServerError
{
    public UnluckyProcessError()
        : base(
            id: "f4bea808-c120-404b-96de-692654d4dc75",
            message: "Oh no, you were unlucky! Please try again.")
    { }
}
