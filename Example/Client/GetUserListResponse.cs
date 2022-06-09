internal class GetUserListResponse
{
    public Data data { get; set; } = null!;

    internal class Data
    {
        public List<User> users { get; set; } = null!;
    }
}
