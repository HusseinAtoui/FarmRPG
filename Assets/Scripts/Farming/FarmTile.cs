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

    public void PlantSeed(ItemData seedData, GameObject cropPrefab)
    {
        if (!isHoed)
        {
            Debug.Log("Tile must be hoed first!");
            return;
        }

        if (currentCrop != null)
        {
            Debug.Log("Tile already has a crop!");
            return;
        }

        if (seedData.itemType != ItemType.Seed)
        {
            Debug.Log("Not a seed!");
            return;
        }

        // Spawn crop prefab
        currentCrop = Instantiate(cropPrefab, transform.position, Quaternion.identity, transform)
            .GetComponent<Crop>();

        Debug.Log("Seed planted: " + seedData.itemName);
    }
}