using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventory;
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    void Start()
    {
        CreateSlots();

        inventory.OnInventoryChanged += UpdateInventoryUI;

        UpdateInventoryUI();
    }

    void CreateSlots()
    {
        for (int i = 0; i < inventory.inventorySize; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);

            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();

            slotUIs.Add(slotUI);
        }
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            var slot = inventory.slots[i];

            slotUIs[i].SetSlot(slot.item, slot.quantity);

            // hightlights selected slot
            slotUIs[i].SetSelected(i == inventory.selectedSlotIndex);
        }
    }

    void OnEnable()
    {
        inventory.OnInventoryChanged += UpdateInventoryUI;
    }

    void OnDisable()
    {
        inventory.OnInventoryChanged -= UpdateInventoryUI;
    }
}