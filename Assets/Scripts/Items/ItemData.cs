using UnityEngine;

public enum ItemType
{
    Tool,
    Seed,
    Crop,
    Resource
}

[CreateAssetMenu(fileName = "New Item", menuName = "FarmRPG/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;

    public ItemType itemType;

    [Header("Stack Settings")]
    public bool stackable = true;
    public int maxStack = 99;

    [Header("Economy")]
    public int sellPrice;

    [Header("Seed Settings")]
    public GameObject cropPrefab;  // only used if itemType == Seed
}