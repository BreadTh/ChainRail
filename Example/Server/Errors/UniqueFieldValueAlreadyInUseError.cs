
namespace BreadTh.ChainRail.Example.Server.Errors;

public class UniqueFieldValueAlreadyInUseError : ErrorBase, IDisplayableServerError 
{
    public UniqueFieldValueAlreadyInUseError(string fieldPathAndName, string value) 
        : base(
            id: "15c8d77c-0693-4ac7-ba06-ca6304b14caf",
            message: $"The field, \"{fieldPathAndName}\", must be unique, but The value of \"{value}\" is already taken.")
    { }
}
