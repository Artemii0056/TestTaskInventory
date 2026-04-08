using System.Collections.Generic;
using Core;

namespace DefaultNamespace.Results
{
    public sealed class AddAmmoResult
    {
        public bool IsSuccess { get; }
        public int RequestedAmount { get; }
        public int AddedAmount { get; }
        public int RemainingAmount { get; }
        public IReadOnlyList<SlotChange> Changes { get; }
    }
}