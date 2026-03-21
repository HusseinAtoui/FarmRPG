using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;

    public void SetSlot(ItemData item, int quantity)
    {
        if (item != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = item.icon;
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
        else
        {
            iconImage.enabled = false;
            quantityText.text = "";
        }
    }
}