using System.Collections.Generic;
using Core;

namespace Results
{
    public class AddAmmoResult
    {
        public AddAmmoResult(int requestedAmount, int addedAmount, int remainingAmount, IReadOnlyList<SlotChange> changes)
        {
            RequestedAmount = requestedAmount;
            AddedAmount = addedAmount;
            RemainingAmount = remainingAmount;
            Changes = changes;
        }

        public int RequestedAmount { get; }
        public int AddedAmount { get; }
        public int RemainingAmount { get; }
        public IReadOnlyList<SlotChange> Changes { get; }

        public bool IsSuccess => AddedAmount > 0;
        public bool IsFullSuccess => RemainingAmount == 0;
    }
}