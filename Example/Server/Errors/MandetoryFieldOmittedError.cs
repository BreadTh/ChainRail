
namespace BreadTh.ChainRail.Example.Server.Errors;

public class MandetoryFieldOmittedError : ErrorBase, IDisplayableServerError
{
    public MandetoryFieldOmittedError(string fieldPathAndName) :
        base(
            id: "cbc133b4-77fd-48a1-8d34-12aee9d1b64a",
            message: $"The field, \"{fieldPathAndName}\", may not be omitted.")
    { }
}
