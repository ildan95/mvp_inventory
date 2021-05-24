using System;
using Data;
using UnityEngine;
using UnityEngine.UI;
using Views.ViewData;

namespace Views
{
    [RequireComponent(typeof(Image))]
    public class SlotView : MonoBehaviour
    {
        private const float ClosedAlpha = 0.4f;
        
        [SerializeField] private Image _item;
        [SerializeField] private Image _selectedMask;

        private Image _image;
        private SlotViewData _slot;
        
        public event Action<int> Clicked;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Init(SlotViewData slot)
        {
            UpdateSlot(slot);
        }

        public void UpdateSlot(SlotViewData slot)
        {
            _slot = slot;

            switch (slot.State)
            {
                case SlotState.Closed:
                    _image.color = GetColorWithAlpha(_image.color, ClosedAlpha);
                    Clear();
                    break;
                case SlotState.Empty:
                    Clear();
                    break;
                case SlotState.WithItem:
                    _item.color = GetColorWithAlpha( _item.color, 1f);
                    _item.sprite = slot.ItemIcon;
                    SetSelected(slot.IsSelected);
                    break;
            }
        }

        private void SetSelected(bool isSelected)
        {
            var alpha = isSelected ? 1 : 0;
            _selectedMask.color = GetColorWithAlpha(_selectedMask.color, alpha);
        }
        
        public void OnClick()
        {
            Clicked?.Invoke(_slot.Index);
        }
        
        private void Clear()
        {
            _item.color = GetColorWithAlpha(_item.color, 0f);
            SetSelected(false);
        }

        private Color GetColorWithAlpha(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}