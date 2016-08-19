using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    public float Speed = 1f;

    public Looper looper;

    bool moveLeft = false;
    bool moveRight = false;

    bool dead = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0))
        {
            if (moveLeft)
            {
                moveRight = true;
                moveLeft = false;
            }
            else
            {
                moveRight = false;
                moveLeft = true;
            }
        }        
    }

    void FixedUpdate()
    {
        if (dead == false)
        {
            if (moveRight)
            {
                rb.AddForce(new Vector2(-0.1f, 0) * Speed);
            }
            if (moveLeft)
            {
                rb.AddForce(new Vector2(0.1f, 0) * Speed);
            }
        }
    }

    //On Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && dead == false)
        {
            dead = true;

            rb.AddForce(new Vector2(0, 2f) * Speed);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(2f);

            rb.gravityScale = 1;
        }

        if (collision.gameObject.tag == "Wall" && dead == false)
        {
            dead = true;

            rb.AddForce(new Vector2(0, 2f) * Speed);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(2f);

            rb.gravityScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Crate" && dead == false)
        {
            looper.cratesPool.Enqueue(collider.gameObject);
            collider.gameObject.SetActive(false);
        }
    }
}
