using Core.Options;

namespace Core.Components
{
    public interface IAccountProvider
    {
        AccountOptions GetAccount();
    }
}