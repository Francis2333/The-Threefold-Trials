using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class ObstacleGeneratorScript : MonoBehaviour
{
    public GameObject obstacle;   // The prefab you want to spawn
    public float spawnInterval = 2f;   // Time gap between spawns (in seconds)
    public int numberToSpawn = 3;      // Optional: how many to spawn in total
    public float Y;      // The position where new objects appear
    float timer = 0;
    Vector3 position;
    Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        position.x = camera.ViewportToWorldPoint(new Vector3(0.99f,0,0)).x;
        position.y = Random.value * 3f - 5.5f;
        position.z = -0.1f;
    }

    private void FixedUpdate()
    {
        if (timer >= spawnInterval)
        {
            Instantiate(obstacle, position, transform.rotation);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
