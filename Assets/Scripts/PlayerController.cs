using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Player Movement & Physics
    private Rigidbody2D rb;

    public float Speed = 1f;
    public Looper looper;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveDown = false;
    bool applyMoveForce = false;

    //player animator
    private Animator animator;

    //Audio Clips
    public AudioClip pickUpSound;
    public AudioClip hitSound;
    public AudioClip moveSound;

    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;    

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LoadPlayerStats();
        UIManager.instance.UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        setGameState();

        if (GameController.instance.currentState == GameState.Play
            || GameController.instance.currentState == GameState.Training)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0))
            {
                float vol = Random.Range(volLowRange, volHighRange);

                if (moveLeft)
                {
                    moveRight = true;
                    moveLeft = false;
                    moveDown = false;
                    flip();
                    animator.SetInteger("AnimNo", 1);
                }
                else
                {
                    moveRight = false;
                    moveLeft = true;
                    moveDown = false;
                    flip();
                    animator.SetInteger("AnimNo", 1);
                }

                source.PlayOneShot(moveSound, vol);
            }

            if (SwipeManager.IsSwipingDown())
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(moveSound, vol);

                moveRight = false;
                moveLeft = false;
                moveDown = true;
                applyMoveForce = true;
                animator.SetInteger("AnimNo", 0);
            }
        }
         
    }

    void FixedUpdate()
    {
        if (GameController.instance.currentState == GameState.Play
            || GameController.instance.currentState == GameState.Training)
        {
            if (moveRight)
            {
                rb.AddForce(new Vector2(-0.1f, 0) * Speed);
            }

            if (moveLeft)
            {
                rb.AddForce(new Vector2(0.1f, 0) * Speed);
            }

            if (moveDown)
            {
                if (applyMoveForce)
                {
                    applyMoveForce = false;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(new Vector2(0, -1f) * Speed);
                }
            }
        }
        
    }

    //On Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameController.instance.currentState == GameState.Play
            || GameController.instance.currentState == GameState.Training)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                rb.AddForce(new Vector2(0, 2f) * Speed);
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddTorque(2f);
                rb.gravityScale = 1;

                GameController.instance.SetCurrentState(GameState.Gameover);
                GameOver();
            }

            if (collision.gameObject.tag == "Wall")
            {
                rb.AddForce(new Vector2(0, 2f) * Speed);
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddTorque(2f);
                rb.gravityScale = 1;

                GameController.instance.SetCurrentState(GameState.Gameover);
                GameOver();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameController.instance.currentState == GameState.Play
            || GameController.instance.currentState == GameState.Training)
        {
            if (collider.gameObject.tag == "Crate")
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(pickUpSound, vol);

                looper.cratesPool.Enqueue(collider.gameObject);
                collider.gameObject.SetActive(false);

                UpdatePlayerStats();
            }
        }        
    }

    private void UpdatePlayerStats()
    {
        GameController.instance.score++;
        UIManager.instance.UpdatePlayerStats();
    }

    private void LoadPlayerStats()
    {
        GameController.instance.score = 0;
        GameController.instance.Load();
    }

    private void GameOver()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(hitSound, vol);

        if (GameController.instance.score > GameController.instance.highScore)
        {
            GameController.instance.highScore = GameController.instance.score;
        }

        GameController.instance.Save();

        UIManager.instance.UpdatePlayerStats();
        UIManager.instance.MenuController(GameState.Gameover);
    }

    void setGameState()
    {
        switch (GameController.instance.currentState)
        {
            case GameState.Training:
                Speed = 120;
                rb.gravityScale = 0.36f;
                break;

            case GameState.Play:
                Speed = 200;
                rb.gravityScale = 0.6f;
                break;

            case GameState.PauseBeforeStart:
                Speed = 0;
                rb.gravityScale = 0f;
                break;

            default:
                Speed = 200;
                rb.gravityScale = 1f;
                break;
        }
    }

    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
