using Model;
using Model.Components;
using Views;
using Views.ViewData;

namespace Presenter
{
    public class InventoryTransferPresenter
    {
        private IInventoryTransferView _view;
        private IInventoryTransferModel _model;
        
        public InventoryTransferPresenter(IInventoryTransferView view, IInventoryTransferModel model)
        {
            _view = view;
            _model = model;

            _view.Init(GetInventoriesDataFromModel());
            
            _view.SlotClicked += OnSlotSelected;
            _view.TransferInvoked += _model.TransferInvoked;
            
            _model.SlotChanged += OnSlotChanged;
        }
        
        public void OnClose()
        {
            _view.SlotClicked -= OnSlotSelected;
            _view.TransferInvoked -= _model.TransferInvoked;
            
            _model.SlotChanged -= OnSlotChanged;
        }

        private InventoryViewData[] GetInventoriesDataFromModel()
        {
            var inventories = _model.Inventories;
            var inventoriesData = new InventoryViewData[inventories.Length];
            
            for (var i = 0; i < inventories.Length; i++)
            {
                inventoriesData[i] = GetInventoryViewData(inventories[i]);
            }

            return inventoriesData;
        }

        private void OnSlotSelected(int index, int inventoryIndex)
        {
            _model.SelectSlot(index, inventoryIndex);
        }
        
        private void OnSlotChanged(Slot slot, int inventoryIndex)
        {
            _view.UpdateSlot(GetSlotViewData(slot), inventoryIndex);
        }

        private SlotViewData GetSlotViewData(Slot slot)
        {
            return new SlotViewData(slot.Item?.Icon, slot.Index, slot.State, slot.IsSelected);
        }

        private InventoryViewData GetInventoryViewData(Inventory inventory)
        {
            var slotViews = new SlotViewData[inventory.Slots.Length];
            
            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                slotViews[i] = GetSlotViewData(inventory.Slots[i]);
            }

            return new InventoryViewData(slotViews);
        }
    }
}