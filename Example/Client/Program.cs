using BreadTh.ChainRail;
using RestSharp;

var chainRail = new ChainRail();
var json = new Json(chainRail);
var restClient = new RestClient("http://localhost:80");
var apiClient = new ApiClient(restClient, chainRail, json);
var users = new Users(apiClient, chainRail);

while(true)
{
    Console.WriteLine("Options:");
    Console.WriteLine("    1: Add a user");
    Console.WriteLine("    2: See list of users");
    Console.Write("Enter a number: ");
    var choice = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (choice) 
    {
        case "1":
            Console.Write("Enter username to add: ");
            var username = Console.ReadLine() ?? "";
            Console.WriteLine();
            await chainRail
                .StartChain(username)
                .Pipe(users.Create)
                .Execute(
                    onError: PrintError,
                    onSuccess: userId => Console.WriteLine($"New user has ID: {userId}")
                );
            break;

        case "2":

            Console.WriteLine("Fetching users..");
            await chainRail
                .StartChain()
                .Then(users.GetAll)
                .Execute(
                    onError: PrintError,
                    onSuccess: entries => 
                        entries.ForEach(user => Console.WriteLine($"    {user.id}: {user.username}"))
                );
            Console.WriteLine();
            break;

        default:
            Console.WriteLine("I didn't quite understand that. Please try again.");
            break;
    }
 }

void PrintError(IError error) =>
    Console.WriteLine($"Errors:\n {string.Join("\n", error.Flatten().Select(e => $"    Id: {e.Id}: {e.Message}"))}");