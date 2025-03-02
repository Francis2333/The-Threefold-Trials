using Unity.Cinemachine;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed;
    public Camera cam;
    public GameObject obstacles;
    private bool move;
    Rigidbody2D rb;
    public Transform target2D;
    bool hitHero;

    void Start()
    {
        moveSpeed = -10;
        obstacles = gameObject;
        target2D = transform;
        rb = GetComponent<Rigidbody2D>();
        move = false;
        hitHero = false;
    }
    

    void Update()
    {
        // 1) Get the actual Camera (usually the Main Camera with CinemachineBrain).
        //    If you have a dedicated camera object, reference it instead of Camera.main.
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("No main camera found!");
            return;
        }

        // 2) Convert the target’s position to the camera’s viewport space
        Vector3 viewportPos = cam.WorldToViewportPoint(target2D.position);

        // 3) Check if it’s within the viewport bounds
        //    z > 0 ensures the object isn't behind an orthographic camera (unlikely, but good practice).
        bool isVisible = (viewportPos.z > 0 &&
                          viewportPos.x >= 0 && viewportPos.x <= 1 &&
                          viewportPos.y >= 0 && viewportPos.y <= 1);

        if (isVisible)
        {
            move = true;
        }
        else
        {
            move = false;
        }

        if (hitHero || gameObject.transform.position.x < (cam.transform.position.x - cam.orthographicSize*2))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.linearVelocityX = moveSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            hitHero = true;
        }
        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
