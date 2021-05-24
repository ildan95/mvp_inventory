namespace Views.ViewData
{
    public class InventoryViewData
    {
        public SlotViewData[] Slots { get; }
    
        public InventoryViewData(SlotViewData[] slots)
        {
            Slots = slots;
        }
    }
}