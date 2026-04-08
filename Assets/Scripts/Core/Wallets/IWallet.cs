namespace Core.Wallets
{
    public interface IWallet
    {
        void Increase(int value);
        int Coins { get; }
        bool TrySpend(int amount);
        bool CanSpend(int amount);
    }
}