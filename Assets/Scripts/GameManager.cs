using Model;
using Model.Components;
using Presenter;
using UnityEngine;
using Views;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InventoryTransferView _inventoryTransferView;
        
        private InventoryTransferPresenter _inventoryTransferPresenter;
        private Sprite[] _icons;
        
        
        void Awake()
        {
            _icons = Resources.LoadAll<Sprite>("Images/Icons");

            var inventory = CreateInventory(10, 25);
            var anotherInventory = CreateInventory(10, 25, true);
            
            var model = new InventoryTransferModel(new[]{inventory, anotherInventory});
            
            _inventoryTransferPresenter = new InventoryTransferPresenter(_inventoryTransferView, model);
        }

        private Inventory CreateInventory(int capacity, int maxCapacity, bool isAllEmpty = false)
        {
            var slots = new Slot[maxCapacity];
            for (int i = 0; i < maxCapacity; i++)
            {
                var isClosed = i > capacity - 1;
                var isEmpty = isAllEmpty || Random.Range(0f, 1f) > 0.5f;
                
                var item = isClosed || isEmpty  ? null : new Item(GetRandomIcon());
                slots[i] = new Slot(item, i, isClosed);
            }

            return new Inventory(slots, capacity, maxCapacity);
        }

        private Sprite GetRandomIcon()
        {
            return _icons[Random.Range(0, _icons.Length)];
        }
    }
}
