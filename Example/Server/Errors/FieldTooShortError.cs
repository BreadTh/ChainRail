
namespace BreadTh.ChainRail.Example.Server.Errors;

public class FieldTooShortError : ErrorBase, IDisplayableServerError
{
    public FieldTooShortError(string fieldPathAndName, string value, int minLength) :
        base(
            id: "4451b151-d3e9-4afd-99d2-59ee8f242176",
            message: $"The field, \"{fieldPathAndName}\", must be at least \"{minLength}\" characters in length, but the value \"{value}\" was given.")
    { }
}
