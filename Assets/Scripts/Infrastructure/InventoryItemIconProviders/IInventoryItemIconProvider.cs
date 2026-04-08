using System.Transactions;
using Infrastructure.StaticData;

namespace Infrastructure.InventoryItemIconProviders
{
    public interface IInventoryItemIconProvider
    {
        
    }
    
    public class InventoryItemIconProvider
    {
        private IStaticDataService _staticDataService;
        
        public InventoryItemIconProvider(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
    }
}