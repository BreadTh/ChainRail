
namespace BreadTh.ChainRail;

//Exists to signal to AggregateError (and other implementations) that the error
//itself does not contain any important information.
//But instead that its .Inner is where the real information is at.
public interface IAggregateError : IError
{ }
