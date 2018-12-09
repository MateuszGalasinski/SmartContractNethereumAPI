namespace Core.Options
{
    public class NetworkOptions
    {
        public NetworkOptions()
        {
        }

        public string Url { get; set; }
        public int Port { get; set; }

        public string Address => $"{Url}:{Port}";
    }
}
