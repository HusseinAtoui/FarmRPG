using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int inventorySize = 20;

    [Header("Slots")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void InitializeInventory()
    {
        slots.Clear();
        for (int i = 0; i < inventorySize; i++)
            slots.Add(new InventorySlot(null, 0));
    }

    void Awake()
    {
        InitializeInventory();
    }

    public bool AddItem(ItemData item, int amount)
    {
        // Try stacking first
        foreach (var slot in slots)
        {
            if (slot.item == item && item.stackable)
            {
                slot.quantity += amount;
                return true;
            }
        }

        // Find empty slot
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = amount;
                return true;
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }

    public void RemoveItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.quantity -= amount;

                if (slot.quantity <= 0)
                {
                    slot.item = null;
                    slot.quantity = 0;
                }

                return;
            }
        }
    }
}