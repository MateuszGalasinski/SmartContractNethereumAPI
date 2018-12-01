namespace ContractManagement.Models
{
    public class NetworkInfo
    {
        public string Url { get; }
        public string Port { get; }

        public string Address => $"{Url}:{Port}";
    }
}
