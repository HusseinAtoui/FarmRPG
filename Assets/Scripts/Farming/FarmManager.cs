using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [Header("Inventory & Seed Selection")]
    public InventoryManagern inventory;

    [Header("Player Stamina")]
    public PlayerStamina playerStamina;

    void Update()
    {
        HandleTileClick();
        HandleMockSleep();
    }

    void HandleTileClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider == null) return;

        // 1. CHECK TREE FIRST
        TreeObject tree = hit.collider.GetComponent<TreeObject>();
        if (tree != null)
        {
            TryHitTree(tree);
            return;
        }

        // 2. FARM TILE LOGIC
        FarmTile tile = hit.collider.GetComponent<FarmTile>();
        if (tile == null) return;

        ItemData item = inventory.GetSelectedItem(false);

        if (item == null)
        {
            Debug.Log("No item selected");
            return;
        }

        if (item.itemType == ItemType.Tool)
        {
            HandleToolOnTile(item, tile);
        }
        else if (item.itemType == ItemType.Seed)
        {
            HandleSeedOnTile(item, tile);
        }
    }

    // TREE LOGIC
    void TryHitTree(TreeObject tree)
    {
        ItemData item = inventory.GetSelectedItem(false);

        if (item == null) return;

        if (item.itemType == ItemType.Tool && item.itemName == "Axe")
        {
            if (playerStamina.UseStamina(item.staminaCost))
            {
                tree.HitTree();
            }
        }
    }

    // TILE TOOL LOGIC
    void HandleToolOnTile(ItemData item, FarmTile tile)
    {
        switch (item.itemName)
        {
            case "Hoe":
                if (!tile.isHoed && playerStamina.UseStamina(item.staminaCost))
                    tile.HoeTile();
                break;

            case "Watering Can":
                if (tile.isHoed && playerStamina.UseStamina(item.staminaCost))
                    tile.WaterTile();
                break;

            case "Sickle":
                if (tile.currentCrop != null &&
                    tile.currentCrop.IsFullyGrown() &&
                    playerStamina.UseStamina(item.staminaCost))
                {
                    tile.HarvestCrop(inventory);
                }
                break;
        }
    }

    // TILE SEED LOGIC
    void HandleSeedOnTile(ItemData item, FarmTile tile)
    {
        if (tile.CanPlantSeed && inventory.HasItem(item))
        {
            bool planted = tile.PlantSeed(item, item.cropPrefab);

            if (planted)
            {
                inventory.RemoveFromSelectedSlot(1);
            }
        }
    }

    // SLEEP TEST
    void HandleMockSleep()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerStamina.RestoreFull();
            Debug.Log("Player slept. Stamina restored.");

            FarmTile[] tiles = FindObjectsOfType<FarmTile>();

            foreach (FarmTile tile in tiles)
            {
                if (tile.currentCrop == null)
                {
                    tile.ResetWater();
                }
            }
        }
    }
}