using System;
using Model.Components;

namespace Model
{
    public interface IInventoryTransferModel
    {
        Inventory[] Inventories { get; }
        
        void TransferInvoked(int destinationInventoryIndex);
        void SelectSlot(int slotIndex, int inventoryIndex);

        event Action<Slot, int> SlotChanged;
    }
}