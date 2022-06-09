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

    internal IFutureOutcome<string> Create(string username) =>
        chainRail
            .StartChain()
            .Then(() => 
                apiClient
                .Call<CreateUserResponse>(Method.Post, "/users", new { username = username }))
            .Then(response => response.id);

    internal IFutureOutcome<List<User>> GetAll() =>
        chainRail
            .StartChain()
            .Then(() => apiClient.Call<List<User>>(Method.Get, "/users"));
}
