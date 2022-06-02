internal class GetUserListResponse
{
    public Data data { get; set; }

    internal class Data
    {
        public List<User> users { get; set; }
    }
}
