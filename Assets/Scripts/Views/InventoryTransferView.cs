using System;
using UnityEngine;
using UnityEngine.UI;
using Views.ViewData;

namespace Views
{
    public class InventoryTransferView : MonoBehaviour, IInventoryTransferView
    {
        private const int SourceInventoryIndex = 0; 
        private const int DestinationInventoryIndex = 1;
        private const int ViewInventoriesCount = 2;
        
        [SerializeField] private InventoryView _sourceInventoryView; 
        [SerializeField] private InventoryView _destinationInventoryView;
        [SerializeField] private Button _transferBtn;

        public event Action<int, int> SlotClicked;
        public event Action<int> TransferInvoked;

        public void Start()
        {
            _sourceInventoryView.SlotClicked += OnSourceSlotClicked;
            _destinationInventoryView.SlotClicked += OnDestinationSlotClicked;
        }
        
        public void OnDestroy()
        {
            OnClose();
        }

        public void Init(InventoryViewData[] inventories)
        {
            if (inventories.Length != ViewInventoriesCount)
            {
                Debug.LogError($"This view works with {ViewInventoriesCount} inventories only");
            }
            _sourceInventoryView.Init(inventories[SourceInventoryIndex].Slots);
            _destinationInventoryView.Init(inventories[DestinationInventoryIndex].Slots);
        }

        public void UpdateSlot(SlotViewData slot, int inventoryIndex)
        {
            _transferBtn.interactable = slot.IsSelected;
            
            var inventoryView = inventoryIndex == SourceInventoryIndex ? _sourceInventoryView : _destinationInventoryView;
            inventoryView.UpdateSlot(slot);
        }
        
        public void OnClose()
        {
            _sourceInventoryView.SlotClicked -= OnSourceSlotClicked;
            _destinationInventoryView.SlotClicked -= OnDestinationSlotClicked;
        }

        public void OnTransfer()
        {
            TransferInvoked?.Invoke(-1);
        }
        
        private void OnDestinationSlotClicked(int index)
        {
            SlotClicked?.Invoke(index, DestinationInventoryIndex);
        }

        private void OnSourceSlotClicked(int index)
        {
            SlotClicked?.Invoke(index, SourceInventoryIndex);
        }
    }
}