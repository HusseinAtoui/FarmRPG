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
    public string itemName;
    public Sprite icon;

    public ItemType itemType;

    public bool stackable = true;
    public int maxStack = 99;

    public int sellPrice;
}