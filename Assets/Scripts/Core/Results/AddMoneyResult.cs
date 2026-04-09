namespace Core.Results
{
    public class AddMoneyResult
    {
        public AddMoneyResult(int amount) => 
            Amount = amount;

        public int Amount { get; }
    }
}