using BreadTh.ChainRail;
using RestSharp;

internal class Users
{
    private readonly ApiClient apiClient;
    private readonly ChainRail chainRail;

    internal Users(ApiClient apiClient, ChainRail chainRail)
    {
        this.apiClient = apiClient;
        this.chainRail = chainRail;
    }

    internal ILazyOutcome<string> Create(string username) =>
        chainRail
            .StartChain()
            .Then(() => 
                apiClient
                .Call<CreateUserResponse>(Method.Post, "/users", new { username = username }))
            .Pipe(response => response.id);

    internal ILazyOutcome<List<User>> GetAll() =>
        chainRail
            .StartChain()
            .Then(() => apiClient.Call<List<User>>(Method.Get, "/users"));
}
