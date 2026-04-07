using Core.Architecture;

namespace Core
{
    public class Wallet
    {
        public int Coins { get; private set; }

        public void Increase(int amount)
        {
            Coins += amount;
        }
    }
}