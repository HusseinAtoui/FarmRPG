using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [Header("Inventory & Seed Selection")]
    public InventorySystem inventory;
    public ItemData selectedSeed;
    public GameObject cropPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                if (tile != null)
                {
                    // Step 1: Hoe only if NOT hoed
                    if (!tile.isHoed)
                    {
                        tile.HoeTile();
                        return; // stop here, no planting on same click
                    }

                    // Step 2: Plant only if tile was already hoed BEFORE this click
                    if (tile.CanPlantSeed && selectedSeed != null && HasSeedInInventory(selectedSeed))
                    {
                        tile.PlantSeed(selectedSeed, cropPrefab);
                        inventory.RemoveItem(selectedSeed, 1);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) // press W to water the clicked tile
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                if (tile != null)
                {
                    tile.WaterTile();
                }
            }
        }
    }

    private bool HasSeedInInventory(ItemData seed)
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.item == seed && slot.quantity > 0)
                return true;
        }
        return false;
    }
}