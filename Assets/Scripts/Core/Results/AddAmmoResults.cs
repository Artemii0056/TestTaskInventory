using System.Collections.Generic;
using Core.Slots;

namespace Core.Results
{
    public class AddAmmoResult
    {
        public AddAmmoResult(int requestedAmount, 
            int addedAmount, 
            int remainingAmount, 
            IReadOnlyList<SlotChange> changes)
        {
            RequestedAmount = requestedAmount;
            AddedAmount = addedAmount;
            RemainingAmount = remainingAmount;
            Changes = changes;
        }

        public IReadOnlyList<SlotChange> Changes { get; }
        public int RequestedAmount { get; }
        public int AddedAmount { get; }
        public int RemainingAmount { get; }
    }
}