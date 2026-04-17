using UnityEngine;

public class DemoScript : MonoBehaviour
{
   public InventoryManagern inventoryManager;
   public ItemData[] itemsToPickup;

   public void PickupItem(int id)
    {
        inventoryManager.AddItem(itemsToPickup[id]);
    }
}
