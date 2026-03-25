using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public int inventorySize = 20;

    public int selectedSlotIndex = 0;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryChanged;

    void Awake()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        slots.Clear();

        for (int i = 0; i < inventorySize; i++)
        {
            slots.Add(new InventorySlot(null, 0));
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        if (item == null) return false;

        // STACK FIRST
        if (item.stackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    int spaceLeft = item.maxStack - slot.quantity;
                    int addAmount = Mathf.Min(spaceLeft, amount);

                    slot.quantity += addAmount;
                    amount -= addAmount;

                    if (amount <= 0)
                    {
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }
        }

        // ADD NEW SLOT
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                int addAmount = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;

                slot.item = item;
                slot.quantity = addAmount;

                amount -= addAmount;

                if (amount <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }

    public bool RemoveItem(ItemData item, int amount)
    {
        if (item == null) return false;

        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                int removeAmount = Mathf.Min(slot.quantity, amount);

                slot.quantity -= removeAmount;
                amount -= removeAmount;

                if (slot.quantity <= 0)
                {
                    slot.item = null;
                    slot.quantity = 0;
                }

                if (amount <= 0)
                {
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        return false;
    }

    public bool HasItem(ItemData item, int amount = 1)
    {
        int count = 0;

        foreach (var slot in slots)
        {
            if (slot.item == item)
                count += slot.quantity;
        }

        return count >= amount;
    }

    public int GetItemCount(ItemData item)
    {
        int count = 0;

        foreach (var slot in slots)
        {
            if (slot.item == item)
                count += slot.quantity;
        }

        return count;
    }

    public InventorySlot GetSelectedSlot()
    {
        if (selectedSlotIndex < 0 || selectedSlotIndex >= slots.Count)
            return null;

        return slots[selectedSlotIndex];
    }

    public ItemData GetSelectedItem()
    {
        var slot = GetSelectedSlot();
        return slot != null ? slot.item : null;
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;

        selectedSlotIndex = index;

        InventorySlot slot = GetSelectedSlot();

        if (slot != null && slot.item != null)
            Debug.Log("Selected: " + slot.item.itemName + " x" + slot.quantity);
        else
            Debug.Log("Selected: Empty Slot");

        OnInventoryChanged?.Invoke();
    }

    public bool RemoveFromSelectedSlot(int amount = 1)
    {
        var slot = GetSelectedSlot();
        if (slot == null || slot.item == null) return false;

        int removeAmount = Mathf.Min(slot.quantity, amount);
        slot.quantity -= removeAmount;

        if (slot.quantity <= 0)
        {
            slot.item = null;
            slot.quantity = 0;
        }

        OnInventoryChanged?.Invoke();
        return true;
    }
}