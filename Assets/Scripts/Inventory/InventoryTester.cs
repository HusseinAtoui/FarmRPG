using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public InventorySystem inventory;
    public ItemData beetroot;

    void Start()
    {
        inventory.AddItem(beetroot, 5);
    }
}