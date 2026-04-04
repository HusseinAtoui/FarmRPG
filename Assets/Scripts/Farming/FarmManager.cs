using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [Header("Inventory & Seed Selection")]
    public InventorySystem inventory;

    [Header("Player Stamina")]
    public PlayerStamina playerStamina;

    void Update()
    {
        HandleTileClick();
        HandleHotbarSelection();
        HandleMockSleep();
    }

    void HandleTileClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider == null) return;

        FarmTile tile = hit.collider.GetComponent<FarmTile>();
        if (tile == null) return;

        ItemData selectedItem = inventory.GetSelectedItem();
        if (selectedItem == null)
        {
            Debug.Log("No item selected");
            return;
        }

        // TOOL ACTIONS
        if (selectedItem.itemType == ItemType.Tool)
        {
            switch (selectedItem.itemName)
            {
                case "Hoe":
                    if (!tile.isHoed && playerStamina.UseStamina(selectedItem.staminaCost))
                    {
                        tile.HoeTile();
                    }
                    break;

                case "Watering Can":
                    if (tile.isHoed && playerStamina.UseStamina(selectedItem.staminaCost))
                    {
                        tile.WaterTile();
                    }
                    break;

                case "Sickle":
                    if (tile.currentCrop != null && tile.currentCrop.IsFullyGrown() && playerStamina.UseStamina(selectedItem.staminaCost))
                    {
                        tile.HarvestCrop(inventory);
                    }
                    break;

                // Add Axe, Pickaxe etc. here later
            }
        }
        // SEED ACTION
        else if (selectedItem.itemType == ItemType.Seed)
        {
            if (tile.CanPlantSeed && inventory.HasItem(selectedItem))
            {
                bool planted = tile.PlantSeed(selectedItem, selectedItem.cropPrefab);
                if (planted)
                {
                    inventory.RemoveFromSelectedSlot(1);
                }
            }
        }
    }

    void HandleHotbarSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) inventory.SelectSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) inventory.SelectSlot(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) inventory.SelectSlot(7);
    }

    void HandleMockSleep()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        playerStamina.RestoreFull();
        Debug.Log("Player slept. Stamina restored. (mock)");

        FarmTile[] tiles = FindObjectsOfType<FarmTile>();

        foreach (FarmTile tile in tiles)
        {
            // only reset if no crop
            if (tile.currentCrop == null)
            {
                tile.ResetWater();
            }
        }
    }
}
}