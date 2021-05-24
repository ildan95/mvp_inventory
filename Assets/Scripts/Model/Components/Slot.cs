using Data;

namespace Model.Components
{
    public class Slot
    {
        public Item Item { get; private set; }
        public int Index { get; }
        public SlotState State { get; private set; }
        public bool IsSelected { get; private set; } = false;

        public Slot(Item item, int index, bool isClosed)
        {
            Item = item;
            Index = index;

            State = isClosed ? SlotState.Closed
                : item == null ? SlotState.Empty
                : SlotState.WithItem;
        }
        
        public void ToggleSelect()
        {
            IsSelected = !IsSelected;
        }

        public bool TrySetItem(Item item)
        {
            if (State != SlotState.Empty)
            {
                return false;
            }
            
            Item = item;
            State = SlotState.WithItem;
            return true;
        }

        public void Clear()
        {
            if (State != SlotState.WithItem)
            {
                return;
            }

            IsSelected = false;
            Item = null;
            State = SlotState.Empty;
        }
    }
}