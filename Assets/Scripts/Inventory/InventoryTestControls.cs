using UnityEngine;

public class InventoryTestControls : MonoBehaviour
{
    public InventorySystem inventory;
    public ItemData beetroot;
    public ItemData beetroot2;

    void Update()
    {
        // Press 1 to add 1 beetroot
        if (Input.GetKeyDown(KeyCode.Alpha7))
            inventory.AddItem(beetroot, 1);

        // Press 2 to remove 1 beetroot
        if (Input.GetKeyDown(KeyCode.Alpha8))
            inventory.RemoveItem(beetroot, 1);

        // Press 3 to add 5 beetroot2
        if (Input.GetKeyDown(KeyCode.Alpha9))
            inventory.AddItem(beetroot2, 5);

        // Press 4 to remove 5 beetroot2
        if (Input.GetKeyDown(KeyCode.Alpha0))
            inventory.RemoveItem(beetroot2, 5);
    }
}