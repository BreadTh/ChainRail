using BreadTh.ChainRail;
using RestSharp;

internal class ApiClient 
{
    readonly RestClient restClient;
    readonly IChainRailFactory chainRail;
    readonly Json json;
    internal ApiClient(RestClient restClient, IChainRailFactory chainRail, Json json)
    {
        this.restClient = restClient;
        this.chainRail = chainRail;
        this.json = json;
    }

    public async Task<IOutcome<MODEL>> Call<MODEL>(Method method, string url, object? body = null)
    {
        var request = new RestRequest(url, method);
        if(body is not null)
            request.AddJsonBody(body);

        var response = await restClient.ExecuteAsync(request);
        
        return json.Parse<ServerResponse<MODEL>>(response.Content)
            .Unify(
                onSuccess: response => 
                {
                    if(response.Errors.Count == 0)
                        return chainRail.Success(response.Data);
                    else
                        return chainRail.Error<MODEL>(response.Errors.Select(x => (IError)x).ToList());
                },
                onError: _ => 
                    chainRail.Error<MODEL>(new UnknownResponseError(url, response.StatusCode, response.Content ?? ""))
            );
        

    }
}
