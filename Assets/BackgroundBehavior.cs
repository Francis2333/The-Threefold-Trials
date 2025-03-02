using UnityEngine;
using System;

public class BackgroundBehavior : MonoBehaviour
{
    Camera cam;
    Rigidbody2D rb;
    public float moveSpeed;
    public float inAirSpeed;
    public GameObject Hero;
    Player player;
    bool isGrounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        player = Hero.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < (cam.transform.position.x - cam.orthographicSize * 5))
        {
            Destroy(gameObject);
        }
        //if (player.getGrounded()) // Ensure player is assigned before calling method
        //{
        //    isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}
    }

    private void FixedUpdate()
    {
        //if (isGrounded)
        //{
        //    rb.linearVelocityX = -moveSpeed;
        //}
        //else
        //{
        //    rb.linearVelocityX = -inAirSpeed;
        //}
        rb.linearVelocityX = -moveSpeed;
    }
}
