public class ServerResponse<T>
{
    public List<ErrorFromServer> Errors { get; set; }
    public T Data { get; set; }
}