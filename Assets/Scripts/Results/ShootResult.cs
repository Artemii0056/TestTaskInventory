namespace Results
{
    using Inventories;

    namespace DefaultNamespace.Results
    {
        public class ShootResult
        {
            public ShootResult(
                bool isSuccess,
                InventoryItemType weaponType,
                InventoryItemType ammoType,
                float damage,
                int changedSlotId = -1)
            {
                IsSuccess = isSuccess;
                WeaponType = weaponType;
                AmmoType = ammoType;
                Damage = damage;
                ChangedSlotId = changedSlotId;
            }

            public bool IsSuccess { get; }
            public InventoryItemType WeaponType { get; }
            public InventoryItemType AmmoType { get; }
            public float Damage { get; }
            public int ChangedSlotId { get; }
        }
    }
}