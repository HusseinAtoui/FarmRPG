using UnityEngine;

public class InventoryToggleUI : MonoBehaviour
{
    public GameObject inventoryPanel;

    private bool isOpen = false;

    void Start()
    {
        inventoryPanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }
}