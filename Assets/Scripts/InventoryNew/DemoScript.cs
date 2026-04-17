using UnityEngine;

public class DemoScript : MonoBehaviour
{
   public InventoryManagern inventoryManager;
   public ItemData[] itemsToPickup;

   public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
            Debug.Log("Picked up " + itemsToPickup[id].itemName);
        else
            Debug.Log("Failed to pick up " + itemsToPickup[id].itemName);
    }
}
