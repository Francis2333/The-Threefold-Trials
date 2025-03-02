using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Background;
    public GameObject floor; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void updateBackground()
    {
        Instantiate(Background, transform.position, transform.rotation);
        
    }
}
