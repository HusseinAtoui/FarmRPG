using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotn : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (draggedItem == null)
            return;

        // If slot already has an item == swap
        if (transform.childCount > 0)
        {
            Transform currentItem = transform.GetChild(0);

            currentItem.SetParent(draggedItem.parentAfterDrag);
        }

        draggedItem.parentAfterDrag = transform;
    }

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }
}