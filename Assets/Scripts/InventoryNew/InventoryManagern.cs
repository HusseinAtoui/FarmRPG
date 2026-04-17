using UnityEngine;

public class InventoryManagern : MonoBehaviour
{
    public InventorySlotn[] inventorySlots;

    public GameObject InventoryItemPrefab;

   public void AddItem(ItemData itemData)
    {
        Debug.Log("AddItem CALLED");

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlotn slot = inventorySlots[i];

            InventoryItem ItemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (ItemInSlot == null)
            {
                Debug.Log("Spawning item in slot " + i);
                SpawnNewItem(itemData, slot);
                return;
            }
        }

        Debug.Log("No empty slots!");
    }

    void SpawnNewItem(ItemData itemData, InventorySlotn slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(itemData);
    }
}