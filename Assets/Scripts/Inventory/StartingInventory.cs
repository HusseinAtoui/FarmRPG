using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartingItem
{
    public ItemData item;
    public int amount;
}

public class StartingInventory : MonoBehaviour
{
    public InventorySystem inventory;

    public List<StartingItem> startingItems = new List<StartingItem>();

    void Start()
    {
        foreach (var entry in startingItems)
        {
            if (entry.item != null)
            {
                inventory.AddItem(entry.item, entry.amount);
            }
        }
    }
}