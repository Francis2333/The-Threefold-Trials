using UnityEngine;

public class OstacleCreatorScript : MonoBehaviour
{
    public GameObject obstacle;
    Camera cam;
    public float HighY, MidY, LowY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void obstacleHigh()
    {
        Instantiate(obstacle, cam.ViewportToWorldPoint(new Vector3(1, HighY, 0)), transform.rotation);
    }

    public void obstacleMid()
    {
        Instantiate(obstacle, cam.ViewportToWorldPoint(new Vector3(1, MidY, 0)), transform.rotation);
    }

    public void obstacleLow()
    {
        Instantiate(obstacle, cam.ViewportToWorldPoint(new Vector3(1, LowY, 0)), transform.rotation);
    }
}
