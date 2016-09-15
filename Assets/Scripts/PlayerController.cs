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
                    flip();
                    animator.SetInteger("AnimNo", 1);
                }
                else
                {
                    moveRight = false;
                    moveLeft = true;
                    flip();
                    animator.SetInteger("AnimNo", 1);
                }

                source.PlayOneShot(moveSound, vol);
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

                GameOver();
            }

            if (collision.gameObject.tag == "Wall")
            {
                rb.AddForce(new Vector2(0, 2f) * Speed);
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddTorque(2f);
                rb.gravityScale = 1;

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

        //Save only when in Playing State
        if(GameController.instance.currentState == GameState.Play )
            GameController.instance.Save();

        UIManager.instance.UpdatePlayerStats();
        GameController.instance.SetCurrentState(GameState.Gameover);
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
