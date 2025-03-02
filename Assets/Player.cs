using Unity.VisualScripting;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded;
    [SerializeField] float moveSpeed;
    [SerializeField] float airMoveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float squadSpeed;
    [SerializeField] float health;
    private float horizontalInput;
    float count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        count = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
            Jump();
        }
        if (isGrounded)
        {
            count = 2;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Squad();
        }
    }
    internal bool getGrounded()
    {
        return isGrounded;
    }


    private void FixedUpdate()
    {
        //rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
        //if (isGrounded)
        //{
        //    rb.linearVelocityX = moveSpeed;
        //}
        //else
        //{
        //    rb.linearVelocityX = airMoveSpeed;
        //}
    }

    bool canJump()
    {
        return isGrounded || count > 0;
    }

    void Squad()
    {
        rb.linearVelocityY = -squadSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        count--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ground");
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            takeDamage();
            rb.linearVelocityX = 0;
        }
    }

    void takeDamage()
    {
        health--;
    }
}
