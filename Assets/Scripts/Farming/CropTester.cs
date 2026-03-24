using UnityEngine;

public class CropTester : MonoBehaviour
{
    public Crop crop;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            crop.Grow(); 
        }
    }
}