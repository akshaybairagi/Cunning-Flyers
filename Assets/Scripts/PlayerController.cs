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

    //Animation
    public Animator gameOverAnimator;
    public Animator statsAnimator;
    public Animator menuAnimator;
    private Animator animator;

    //Audio Clips
    public AudioClip pickUpSound;
    public AudioClip hitSound;
    public AudioClip moveSound;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    //Training Button
    public Animator traningBtn;

    //Start Tap Image
    public GameObject startBtn;
    public Animator startBtnAnimator;

    //Training Back Button
    public Animator backBtn;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LoadPlayerStats();
        setGameState();        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.currentState == GameState.Play
                || GameController.instance.currentState == GameState.Training)
        {

            if (InputManager.instance.currentInput == InputState.Tap)
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

            if (InputManager.instance.currentInput == InputState.SwipeDown)
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

            if (collider.gameObject.tag == "Training")
            {
                collider.gameObject.GetComponent<Animator>().SetBool("IsActive", true);
            }
        }
    }

    private void UpdatePlayerStats()
    {
        GameController.instance.score++;

        //scoreText.text = GameController.instance.score.ToString(); 
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

            statsAnimator.SetBool("IsActive", true);
        }
        else
        {
            gameOverAnimator.SetBool("IsActive", true);
        }

        menuAnimator.SetBool("IsActive", true);

        GameController.instance.Save();
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
