using UnityEngine;

public class InventoryManagern : MonoBehaviour
{
    public InventorySlotn[] inventorySlots;
    public GameObject InventoryItemPrefab;

    int selectedSlot = -1;

    public void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >=0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

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

    public ItemData GetSelectedItem(bool use)
    {
        if (selectedSlot >= 0)
        {
            InventoryItem item = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();

            if (item != null)
            {
                ItemData itemData = item.itemData;

                if (use)
                {
                    item.count--;

                    if (item.count <= 0)
                    {
                        Destroy(item.gameObject);
                    }
                    else
                    {
                        item.RefreshCount();
                    }
                }

                return itemData;
            }
        }

        return null;
    }


}