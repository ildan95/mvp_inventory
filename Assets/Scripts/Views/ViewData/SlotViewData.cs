using Data;
using UnityEngine;

namespace Views.ViewData
{
    public class SlotViewData
    {
        public Sprite ItemIcon { get; }
        public int Index { get; }
        public SlotState State { get; }
        public bool IsSelected { get; }
        
        public SlotViewData(Sprite itemIcon, int index, SlotState state, bool isSelected)
        {
            ItemIcon = itemIcon;
            Index = index;
            State = state;
            IsSelected = isSelected;
        }
    }
}