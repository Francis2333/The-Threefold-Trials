using Unity.VisualScripting;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public Camera mainCamera; // Assign in Inspector (often just Camera.main)
    public GameObject floor;
    float x, y;

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;
        x = mainCamera.WorldToViewportPoint(new Vector3(0, 0, 0)).x;
        y = mainCamera.WorldToViewportPoint(new Vector3(0, 0, 0)).y;
        spawnBackground();
        Instantiate(floor, new Vector2(mainCamera.WorldToViewportPoint(new Vector3(0.5f, 0, 0)).x, y), transform.rotation);
    }

    void Update()
    {
        if (needNewBackground())
        {
            spawnBackground();
        }
    }

    void spawnBackground()
    {
        GameObject newBg = Instantiate(backgroundPrefab, new Vector2(x, y), transform.rotation);
        x = newBg.GetComponent<Renderer>().bounds.max.x + 12.4f;
    }

    bool needNewBackground()
    {
        if (mainCamera.WorldToViewportPoint(new Vector3(1,0,0)).x <= x)
        {
            return true;
        }
        return false;
    }
}
