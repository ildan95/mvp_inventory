using System;
using UnityEngine;
using Views.ViewData;

namespace Views
{
    public class InventoryView : MonoBehaviour
    {
        private const int SlotInRowCount = 5;
        
        [SerializeField] private GameObject _parent;
        [SerializeField] private GameObject _slotsRow;
        [SerializeField] private SlotView _slot;
        
        public event Action<int> SlotClicked;
        
        private SlotView[] _slotViews;
        
        public void Init(SlotViewData[] slots)
        {
            GameObject slotParent = null;
            
            _slotViews = new SlotView[slots.Length];
            for (int index = 0; index < slots.Length; index++)
            {
                CreateSlotRowIfNeed(index, ref slotParent);
                _slotViews[index] = CreateSlotView(slots, slotParent, index);;
            }
        }
        
        public void UpdateSlot(SlotViewData slot)
        {
            if (slot.Index >= _slotViews.Length)
            {
                Debug.LogWarning("Slot index out of range");
                return;
            }
            _slotViews[slot.Index].UpdateSlot(slot);
        }

        public void OnDestroy()
        {
            foreach (var slotView in _slotViews)
            {
                slotView.Clicked -= OnClicked;
            }
        }

        private void CreateSlotRowIfNeed(int index, ref GameObject slotParent)
        {
            if (index % SlotInRowCount == 0)
            {
                slotParent = Instantiate(_slotsRow, _parent.transform);
            }
        }

        private SlotView CreateSlotView(SlotViewData[] slots, GameObject slotParent, int index)
        {
            var slotView = Instantiate(_slot, slotParent.transform);
            slotView.Init(slots[index]);
            slotView.Clicked += OnClicked;
            return slotView;
        }

        private void OnClicked(int index)
        {
            SlotClicked?.Invoke(index);
        }
    }
}