using BreadTh.ChainRail;
using System.Net;

public class UnknownResponseError : ErrorBase, IError
{
    public UnknownResponseError(string path, HttpStatusCode statusCode, string body)
        : base(
            id: "2b320d63-5e71-452d-aea6-5603057b7726",
            message: $"Could not parse response from server, nor any error messages. path: {path}, statusCode: {statusCode}, body: {body}")
    { }
}
