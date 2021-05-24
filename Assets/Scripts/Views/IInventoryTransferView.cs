using System;
using Views.ViewData;

namespace Views
{
    public interface IInventoryTransferView
    {
        void Init(InventoryViewData[] inventories);
        
        event Action<int, int> SlotClicked;
        event Action<int> TransferInvoked;
        
        void UpdateSlot(SlotViewData slot, int inventoryIndex);
    }
}