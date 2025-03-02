using UnityEngine;

public class FloorBehavior : MonoBehaviour
{
    Camera cam;
    Rigidbody2D rb;
    public GameObject Hero;
    Player player;
    public GameObject background;
    BackgroundBehavior b;
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        player = Hero.GetComponent<Player>();
        b = background.GetComponent<BackgroundBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < (cam.transform.position.x - cam.orthographicSize * 5))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = -b.moveSpeed;
    }
}
