using System;

namespace Core.Wallets
{
    public class Wallet : IWallet
    {
        public Wallet(int coins) =>
            Coins = coins;

        public int Coins { get; private set; }

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new Exception("Cannot increase amount of coins");

            Coins += amount;
        }

        public bool TrySpend(int amount)
        {
            if (CanSpend(amount) == false)
                return false;

            Coins -= amount;
            return true;
        }

        public bool CanSpend(int amount)
        {
            if (amount <= 0)
                return false;

            return Coins >= amount;
        }
    }
}