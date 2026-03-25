using UnityEngine;

public class FarmTile : MonoBehaviour
{
    public bool isHoed = false;
    public bool isWatered = false;

    public Crop currentCrop;

    [Header("Soil Sprites")]
    public Sprite normalSoil;   // unhoed
    public Sprite hoedSoil;     // hoed
    public Sprite wateredSoil;  // watered (optional for later)

    public bool CanPlantSeed => isHoed && currentCrop == null;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSoil;
    }

    public void HoeTile()
    {
        if (!isHoed)
        {
            isHoed = true;
            spriteRenderer.sprite = hoedSoil;  // swap sprite
            Debug.Log("Tile hoed: " + gameObject.name);
        }
    }

    public void WaterTile()
    {
        if (!isHoed) return;

        isWatered = true;
        Debug.Log("Tile watered: " + gameObject.name);

        if (currentCrop != null && !currentCrop.IsFullyGrown())
        {
            currentCrop.Grow();
        }
    }

    public bool PlantSeed(ItemData seedData, GameObject cropPrefab)
    {
        if (!isHoed)
        {
            Debug.Log("Tile must be hoed first!");
            return false;
        }

        if (currentCrop != null)
        {
            Debug.Log("Tile already has a crop!");
            return false;
        }

        if (seedData.itemType != ItemType.Seed)
        {
            Debug.Log("Not a seed!");
            return false;
        }

        if (cropPrefab == null)
        {
            Debug.LogWarning("No crop prefab assigned for seed: " + seedData.itemName);
            return false;
        }

        // Spawn crop prefab as a child of this tile
        currentCrop = Instantiate(cropPrefab, transform.position, Quaternion.identity, transform)
            .GetComponent<Crop>();

        Debug.Log("Seed planted: " + seedData.itemName);
        return true;
    }
}