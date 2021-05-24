using System;
using Data;
using Model.Components;
using UnityEngine;

namespace Model
{
    public class InventoryTransferModel : IInventoryTransferModel
    {
        public Inventory[] Inventories { get; }

        public event Action<Slot, int> SlotChanged;
        
        private Slot _selectedSlot;
        private int _selectedInventoryIndex;

        public InventoryTransferModel(Inventory[] inventories)
        {
            Inventories = inventories;
        }
        
        public void SelectSlot(int slotIndex, int inventoryIndex)
        {
            ToggleAndInvokeSlot();
            UpdateSelectedInfo(slotIndex, inventoryIndex);
            ToggleAndInvokeSlot();
        }

        public void TransferInvoked(int destinationInventoryIndex)
        {
            if (!HasSelectedSlot())
            {
                return;
            }

            destinationInventoryIndex = ConvertDestinationIndexIfNeed(destinationInventoryIndex);
            
            if (TryAddSelectedToDestinationInventory(destinationInventoryIndex))
            {
                ClearSelectedSlot();
            }
        }
        
        private bool HasSelectedSlot()
        {
            return _selectedSlot != null && _selectedSlot.State == SlotState.WithItem;
        }
        
        private int ConvertDestinationIndexIfNeed(int index)
        {
            return index == -1 ? GetNotSelectedIndex() : index;
        }
        
        private int GetNotSelectedIndex()
        {
            return (_selectedInventoryIndex + 1) % Inventories.Length;
        }

        private bool TryAddSelectedToDestinationInventory(int destinationInventoryIndex)
        {
            var destinationInventory = GetInventory(destinationInventoryIndex);
            var destinationSlot = destinationInventory.TryAddItem(_selectedSlot.Item);
            var isAdded = destinationSlot != null;

            if (isAdded)
            {
                SlotChanged?.Invoke(destinationSlot, destinationInventoryIndex);
            }
            
            return isAdded;
        }
        
        private void ClearSelectedSlot()
        {
            _selectedSlot.Clear();
            SlotChanged?.Invoke(_selectedSlot, _selectedInventoryIndex);
            _selectedSlot = null;
        }

        private void ToggleAndInvokeSlot()
        {
            if (_selectedSlot == null)
            {
                return;
            }
            
            _selectedSlot.ToggleSelect();
            SlotChanged?.Invoke(_selectedSlot, _selectedInventoryIndex);
        }

        private void UpdateSelectedInfo(int slotIndex, int inventoryIndex)
        {
            var inventory = GetInventory(inventoryIndex);
            _selectedSlot = _selectedSlot?.Index == slotIndex && _selectedInventoryIndex == inventoryIndex
                ? null
                : inventory?.GetSlotOrNull(slotIndex);
            _selectedInventoryIndex = inventoryIndex;
        }

        private Inventory GetInventory(int inventoryIndex)
        {
            if (inventoryIndex >= Inventories.Length || inventoryIndex < 0)
            {
                Debug.LogWarning($"Inventory index({inventoryIndex}) out of range");
                return null;
            }
            
            return Inventories[inventoryIndex];
        }
    }
}