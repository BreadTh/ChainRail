
namespace BreadTh.ChainRail.Example.Server.Extensions;

internal static class LazyOutcomeExtensions 
{
    internal static Task WriteToHttpResonse<VALUE>(this ILazyOutcome<VALUE> outcome, HttpResponse response) =>
        outcome.Execute(HandleSuccess<VALUE>(response), HandleError(response));

    private static Func<IError, Task> HandleError(HttpResponse response) =>
        async (IError error) =>
        {
            var errors = error.Flatten();
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(
                new
                {
                    errors = errors.Select(err =>
                        new
                        {
                            id = err.Id,
                            message = err.Message
                        }
                    ),
                    data = new
                    {
                    }
                }
            );
        };

    private static Func<VALUE, Task> HandleSuccess<VALUE>(HttpResponse response) =>
        async (VALUE data) =>
        {
            response.StatusCode = 200;
            await response.WriteAsJsonAsync(
                new
                {
                    errors = new List<object>(),
                    data = data
                }
            );
        };
}



