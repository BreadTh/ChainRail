public class ServerResponse<T>
{
    public List<ErrorFromServer> Errors { get; set; } = null!;
    public T Data { get; set; } = default!;
}