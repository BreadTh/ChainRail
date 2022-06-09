using BreadTh.ChainRail.Example.Server.Errors;
using BreadTh.ChainRail.Example.Server.Persistance.Models;
using BreadTh.ChainRail.Example.Server.Requests;

namespace BreadTh.ChainRail.Example.Server.Persistance;

internal class Users
{
    private readonly ChainRail chainRail;

    private readonly Dictionary<string, User> users = new();

    public Users(ChainRail chainRail)
    {
        this.chainRail = chainRail;
    }

    public IFutureOutcome<User> Create(UserRequest request) =>
        chainRail
            .StartChain(request)
            .Tee(ValidateRequest)
            .Then(TranslateToUser)
            .Tee(VerifyUsernameAvailable)
            .Tee(InsertUser);

    public IOutcome<User> GetUserById(string userId)
    {
        if(users.TryGetValue(userId, out var user))
            return chainRail.Success(user);
        else
            return chainRail.Error<User>(new EntityNotFoundError("User", "Id", userId));
    }
    
    private User TranslateToUser(UserRequest request) =>
        new User(Guid.NewGuid().ToString(), request.Username);

    private IOutcome VerifyUsernameAvailable(User candidate) =>
        users.Values.Any(user => user.Username == candidate.Username)
        ? chainRail.Error(new UniqueFieldValueAlreadyInUseError("Username", candidate.Username))
        : chainRail.Success();

    private void InsertUser(User user) =>
        users.Add(user.Id, user);

    public IFutureOutcome<List<User>> GetAll() =>
        chainRail
            .StartChain()
            .Then(() => users.Values.ToList());

    private IOutcome ValidateRequest(UserRequest request) 
    {
        const int UsernameMinLength = 5;

        var errors = new List<IError>();

        if(request.Username is null)
            errors.Add(new MandetoryFieldOmittedError("Username"));
        
        else if(request.Username.Length < UsernameMinLength)
            errors.Add(new FieldTooShortError("Username", request.Username, UsernameMinLength));

        if(errors.Any())
            return chainRail.Error(errors);
        else
            return chainRail.Success();
    }
}
