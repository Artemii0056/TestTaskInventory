using System;

namespace Core
{
    public class Wallet : IWallet
    {
        public int Coins { get; private set; }

        public Wallet(int coins) =>
            Coins = coins;

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new Exception("Cannot increase amount of coins");

            Coins += amount;
        }

        public void Decrease(int amount)
        {
            if (Coins < amount)
                throw new Exception("Cannot decrease amount of coins");

            if (amount <= 0)
                throw new Exception("Cannot increase amount of coins");

            Coins -= amount;
        }

        public bool Have(int amount) => 
            Coins >= amount;
    }

    public interface IWallet
    {
        void Increase(int value);
    }
}