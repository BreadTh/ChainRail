
namespace BreadTh.ChainRail.Example.Server.Errors;

//This interface indicates that this error is safe to show to API caller. 
//If an error does not "implement" this interface, it should not be shown to the API caller.
public interface IDisplayableServerError : IError
{ }