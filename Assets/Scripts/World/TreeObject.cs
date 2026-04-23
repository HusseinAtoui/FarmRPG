using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Drop")]
    public ItemData woodItem;
    public InventoryManagern inventory;

    private Vector3 originalScale;

    void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;

        // optional safety auto-find (prevents inspector mistakes)
        if (inventory == null)
            inventory = FindFirstObjectByType<InventoryManagern>();
    }

    public void HitTree()
    {
        currentHealth--;

        Debug.Log("Tree hit! HP: " + currentHealth);

        transform.localScale = originalScale * 1.05f;
        Invoke(nameof(ResetScale), 0.05f);

        if (currentHealth <= 0)
        {
            ChopDown();
        }
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }

    void ChopDown()
    {
        Debug.Log("Tree chopped!");

        if (inventory != null && woodItem != null)
        {
            int amount = Random.Range(woodItem.minYield, woodItem.maxYield + 1);

            for (int i = 0; i < amount; i++)
            {
                inventory.AddItem(woodItem);
            }

            Debug.Log("Gained wood x" + amount);
        }

        Destroy(gameObject);
    }
}