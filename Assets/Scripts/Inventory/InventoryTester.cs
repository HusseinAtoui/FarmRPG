using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public InventorySystem inventory;
    public ItemData beetroot;
    public ItemData beetroot2;

    void Start()
    {
        if (inventory != null)
        {
            inventory.AddItem(beetroot, 5);
            inventory.AddItem(beetroot2, 99);
        }
    }
}