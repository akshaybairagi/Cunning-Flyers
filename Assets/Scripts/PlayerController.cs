using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    public float Speed = 1f;

    public Looper looper;

    bool moveLeft = false;
    bool moveRight = false;

    bool dead = false;

    public Text scoreText;
    public Text highScoreText;
    public Text coinText;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        LoadPlayerStats();
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

            SavePlayerStats();
        }

        if (collision.gameObject.tag == "Wall" && dead == false)
        {
            dead = true;

            rb.AddForce(new Vector2(0, 2f) * Speed);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(2f);
            rb.gravityScale = 1;

            SavePlayerStats();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Crate" && dead == false)
        {
            looper.cratesPool.Enqueue(collider.gameObject);
            collider.gameObject.SetActive(false);

            UpdatePlayerStats();
        }
    }

    private void UpdatePlayerStats()
    {
        GameController.control.score++;
        GameController.control.coins++;

        scoreText.text = GameController.control.score.ToString();
        coinText.text = GameController.control.coins.ToString() + "$";  
    }

    private void LoadPlayerStats()
    {
        GameController.control.Load();
        DisplayPlayerData();
    }

    private void SavePlayerStats()
    {
        if (GameController.control.score > GameController.control.highScore)
        {
            GameController.control.highScore = GameController.control.score;
        }

        DisplayPlayerData();
        GameController.control.Save();
    }

    private void DisplayPlayerData()
    {
        scoreText.text = GameController.control.score.ToString();
        highScoreText.text = "High " + GameController.control.highScore.ToString();
        coinText.text = GameController.control.coins.ToString() + "$";
    }
}
