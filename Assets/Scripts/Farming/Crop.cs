using UnityEngine;

public class Crop : MonoBehaviour
{
    public string cropName = "Beetroot";
    public int growthStage = 0;
    public int maxGrowthStage = 3;
    public Sprite[] growthSprites; 

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (growthSprites.Length > 0)
            spriteRenderer.sprite = growthSprites[0];
    }

    public void Grow()
    {
        if (growthStage < maxGrowthStage)
        {
            growthStage++;
            if (growthSprites.Length > growthStage)
                spriteRenderer.sprite = growthSprites[growthStage];
            Debug.Log(cropName + " grew to stage " + growthStage);
        }
    }

    public bool IsFullyGrown()
    {
        return growthStage >= maxGrowthStage;
    }
}