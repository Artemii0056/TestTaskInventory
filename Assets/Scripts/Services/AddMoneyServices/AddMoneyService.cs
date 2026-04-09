using Core.Results;
using Core.Wallets;
using Services.RandomServices;

namespace Services.AddMoneyServices
{
    public class AddMoneyService : IAddMoneyService
    {
        private readonly IWallet _wallet;
        private readonly IRandomService _randomService;

        public AddMoneyService(
            IWallet wallet,
            IRandomService randomService)
        {
            _wallet = wallet;
            _randomService = randomService;
        }

        public AddMoneyResult AddRandom()
        {
            int amount = _randomService.GenerateValue(9, 99); //TODO Вынести чиселки

            _wallet.Increase(amount);

            return new AddMoneyResult(amount);
        }
    }
}