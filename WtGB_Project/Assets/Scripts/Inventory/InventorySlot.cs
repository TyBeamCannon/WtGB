using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem { get; set; }

    public SlotTag myTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryManager.carriedItem == null)
                return;
            
            SetItem(InventoryManager.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        InventoryManager.carriedItem = null;

        if (item != null && item.activeSlot != null)
            item.activeSlot.myItem = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        
    }
   
}
