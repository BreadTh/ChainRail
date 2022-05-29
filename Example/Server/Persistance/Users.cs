using BreadTh.ChainRail.Example.Server.Errors;
using BreadTh.ChainRail.Example.Server.Persistance.Models;
using BreadTh.ChainRail.Example.Server.Requests;

namespace BreadTh.ChainRail.Example.Server.Persistance;

internal class Users
{
    private readonly OutcomeFactory outcome;

    private readonly Dictionary<Guid, User> users = new();

    public Users(OutcomeFactory outcome)
    {
        this.outcome = outcome;
    }

    public ILazyOutcome<Guid> Create(UserRequest request) =>
        outcome
            .StartChain()
            .Then(CheckWithMagicEightball)
            .Then(() => Validate(request))
            .Pipe(user =>
            { 
                users.Add(user.Id, user);
                return user.Id;
            });

    public ILazyOutcome<List<User>> GetAll() =>
        outcome
            .StartChain()
            .Then(CheckWithMagicEightball)
            .Then(() => users.Values.ToList());

    private IOutcome<User> Validate(UserRequest request) 
    {
        const int UsernameMinLength = 5;

        var errors = new List<IError>();

        if(request.Username is null)
            errors.Add(new MandetoryFieldOmittedError("Username"));
        else if(request.Username.Length < UsernameMinLength)
            errors.Add(new FieldTooShortError("Username", request.Username, UsernameMinLength));

        if(errors.Any())
            return outcome.Error<User>(errors);

        var user = new User(Guid.NewGuid(), request.Username!);
        return outcome.Success(user);
    }

    private IOutcome CheckWithMagicEightball() 
    {
        if(new Random().Next(0, 100) < 10)
            return outcome.Error(new UnluckyProcessError());
        else
            return outcome.Success();
    }
}
