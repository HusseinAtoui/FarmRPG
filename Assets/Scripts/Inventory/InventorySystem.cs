using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int inventorySize = 20;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryChanged;

    void Awake()
    {
        InitializeInventory();
    }

    public void InitializeInventory()
    {
        slots.Clear();

        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item && item.stackable)
            {
                slot.quantity += amount;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.quantity = amount;
                OnInventoryChanged?.Invoke();
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

                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }
}