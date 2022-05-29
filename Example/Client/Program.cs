using BreadTh.ChainRail;
using RestSharp;
using System.Net;
using System.Text.Json;

var outcome = new OutcomeFactory();
var restClient = new RestClient("http://localhost:80");

Console.Write("Enter username to add: ");
var username = Console.ReadLine() ?? "";

await outcome
    .StartChain()
    .Then(() => CreateUser(username))
    .Pipe(userId =>
        outcome
            .StartChain()
            .Then(GetUserList)
            .Pipe(users => (users: users, newUserId: userId))
    )
    .Execute(
        onError: (error) => Console.WriteLine($"Oh no, something went wrong!! {error.Message}"),
        onSuccess: result => PrintUsers(result.users, result.newUserId)
    );


async Task<IOutcome<string>> CreateUser(string username) 
{
    var request = new RestRequest("/users", Method.Post);
    request.AddJsonBody(new { username = username });
    var response = await restClient.ExecuteAsync(request);

    if(response.StatusCode != HttpStatusCode.OK)
        return outcome.Error<string>(new UnexpectedStatusCodeError((int)response.StatusCode, 200));

    try 
    {
        var parsed = JsonSerializer.Deserialize<CreateUserResponse>(response.Content);
        return outcome.Success(parsed.data.userId);
    }
    catch 
    {
        return outcome.Error<string>(new ResponseWasNotJsonError());
    }
}

async Task<IOutcome<List<User>>> GetUserList()
{
    var request = new RestRequest("/users", Method.Get);
    var response = await restClient.ExecuteAsync(request);

    if (response.StatusCode != HttpStatusCode.OK)
        return outcome.Error<List<User>>(new UnexpectedStatusCodeError((int)response.StatusCode, 200));

    try
    {   
        var parsed = JsonSerializer.Deserialize<GetUserListResponse>(response.Content);
        return outcome.Success(parsed.data.users);
    }
    catch
    {
        return outcome.Error<List<User>>(new ResponseWasNotJsonError());
    }
}


Task PrintUsers(List<User> users, string newUserId)
{
    foreach (var user in users)
        if (user.id == newUserId)
            Console.WriteLine($"==> {user.id}: {user.username}");
        else
            Console.WriteLine($"    {user.id}: {user.username}");

    return Task.CompletedTask;
}


public record User(string id, string username);

internal class CreateUserResponse
{
    public Data data { get; set; }
    internal class Data 
    {
        public string userId { get; set; }
    }
}

internal class GetUserListResponse
{
    public Data data { get; set; }

    internal class Data
    {
        public List<User> users { get; set; }
    }
}

internal class UnexpectedStatusCodeError : ErrorBase, IError
{
    internal UnexpectedStatusCodeError(int actual, int expected)
        : base("", "")
    { }
}

internal class ResponseWasNotJsonError : ErrorBase, IError
{
    internal ResponseWasNotJsonError()
        : base("", "")
    { }
}