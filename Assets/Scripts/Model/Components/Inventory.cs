using Data;
using UnityEngine;

namespace Model.Components
{
    public class Inventory
    {
        public Slot[] Slots { get; }
        public int MaxCapacity { get; }
        public int CurrentCapacity { get; }

        public Inventory(Slot[] slots, int capacity)
        {
            MaxCapacity = capacity;
            CurrentCapacity = capacity;
            Slots = slots;
        }

        public Inventory(Slot[] slots, int currentCapacity, int maxCapacity) : this(slots, currentCapacity)
        {
            if (maxCapacity < currentCapacity)
            {
                Debug.LogWarning($"maxCapacity({maxCapacity}) set bigger then currentCapacity({currentCapacity})");
            }
            else
            {
                MaxCapacity = maxCapacity;
            }
        }

        public Slot GetSlotOrNull(int index)
        {
            return index >= Slots.Length ? null : Slots[index];
        }

        public Slot TryAddItem(Item item)
        {
            var slot = GetEmptySlotOrNull();
            return (slot?.TrySetItem(item) ?? false) ? slot : null;
        }
        
        private Slot GetEmptySlotOrNull()
        {
            foreach (var slot in Slots)
            {
                if (slot.State == SlotState.Empty)
                {
                    return slot;
                }
            }

            return null;
        }
    }
}