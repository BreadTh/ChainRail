
namespace BreadTh.ChainRail.Example.Server.Errors;

public class EntityNotFoundError : ErrorBase, IDisplayableServerError
{
    public EntityNotFoundError(string entityName, string lookupField, string lookupValue)
        : base(
            id: "1bae883b-700f-48fd-914d-7bc76bdcd668",
            message: $"A \"{entityName}\" entity could not be found where \"{lookupField}\" equals \"{lookupValue}\"")
    { }
}