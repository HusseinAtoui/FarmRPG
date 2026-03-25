using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [Header("Inventory & Seed Selection")]
    public InventorySystem inventory;

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
                    ItemData selectedItem = inventory.GetSelectedItem();

                    if (tile.CanPlantSeed && selectedItem != null && selectedItem.itemType == ItemType.Seed &&
                        inventory.HasItem(selectedItem))
                    {
                        // try planting and check if successful
                        bool planted = tile.PlantSeed(selectedItem, selectedItem.cropPrefab);

                        if (planted)
                        {
                            inventory.RemoveFromSelectedSlot(1);
                        }
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.SelectSlot(4);
    }
}