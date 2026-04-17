using UnityEngine;

public class InventoryManagern : MonoBehaviour
{
    public InventorySlotn[] inventorySlots;

    public GameObject InventoryItemPrefab;

   public bool AddItem(ItemData itemData)
    {
        Debug.Log("AddItem CALLED");

        // check for stackable and existing item
        for (int i = 0; i < inventorySlots.Length; i++)
                {
                    InventorySlotn slot = inventorySlots[i];

                    InventoryItem ItemInSlot = slot.GetComponentInChildren<InventoryItem>();

                    if (ItemInSlot != null && itemData.stackable && ItemInSlot.itemData == itemData && ItemInSlot.count < itemData.maxStack)
                    {
                        ItemInSlot.count++;
                        ItemInSlot.RefreshCount();    
                        return true;
                    }
                }

        // look for empty slots
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlotn slot = inventorySlots[i];

            InventoryItem ItemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (ItemInSlot == null)
            {
                SpawnNewItem(itemData, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(ItemData itemData, InventorySlotn slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);
    }
}