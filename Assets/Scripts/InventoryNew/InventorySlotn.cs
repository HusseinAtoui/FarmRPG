using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotn : MonoBehaviour, IDropHandler
{

    public Image image;
    public Color selectedColor, notSelectedColor;
    
    public void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
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