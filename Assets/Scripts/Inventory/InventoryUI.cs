using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventory;   // Assign InventoryManager
    public GameObject slotPrefab;       // Assign InventorySlot prefab
    public Transform slotParent;        // Assign InventoryPanel

    private List<GameObject> slotObjects = new List<GameObject>();

    void Start()
    {
        DrawInventory();
    }

    void OnEnable()
    {
        inventory.OnInventoryChanged += DrawInventory;
    }

    void OnDisable()
    {
        inventory.OnInventoryChanged -= DrawInventory;
    }

    public void DrawInventory()
    {
        // Clear previous slots
        foreach (var obj in slotObjects)
        {
            Destroy(obj);
        }
        slotObjects.Clear();

        // Create new slots
        foreach (var slot in inventory.slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            slotObjects.Add(newSlot);

            // Get the InventorySlotUI component and set its visuals
            var slotUI = newSlot.GetComponent<InventorySlotUI>();
            slotUI.SetSlot(slot.item, slot.quantity);
        }
    }
}