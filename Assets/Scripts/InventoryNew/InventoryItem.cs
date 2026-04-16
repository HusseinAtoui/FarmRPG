using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;
    private Canvas canvas;

    void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;

        parentAfterDrag = transform.parent;

        transform.SetParent(canvas.transform); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        // If not dropped on slot → return back
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(parentAfterDrag);
        }
    }
}