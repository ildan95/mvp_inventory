using UnityEngine;

namespace Model.Components
{
    public class Item
    {
        public Sprite Icon { get; }

        public Item(Sprite sprite)
        {
            Icon = sprite;
        }
    }
}