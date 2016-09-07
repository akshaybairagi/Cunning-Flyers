using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Player Movement
    private Rigidbody2D rb;

    public float Speed = 1f;
    public Looper looper;

    bool moveLeft = false;
    bool moveRight = false;
    bool moveDown = false;
    bool applyMoveForce = false;

    //UI
    public Text scoreText;
    public Text highScoreText;
    public Text coinText;

    public Text goScoreText;
    public Text goHighText;

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

    private bool BeginPlay = false;

    //Training Button
    public Animator traningBtn;

    //Start Tap Image
    public GameObject startBtn;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LoadPlayerStats();

        if(GameController.control.trainingMode == true)
        {
            Speed = 120;
            rb.gravityScale = 0.36f;
        }

        StartPlay(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.control.isDead == true && GameController.control.contPlay == true)
        {
            moveLeft = false;
            moveRight = false;
            GameController.control.isDead = false;
            GameController.control.contPlay = false;
            transform.position = GameController.control.lastPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);

            //gameOverAnimator.SetBool("IsActive", false);
            menuAnimator.SetBool("IsActive", false);

            rb.gravityScale = 0.6f;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.control.trainingMode)
            {
                GameController.control.trainingMode = false;
            }
            SceneManager.LoadScene("StartScreen");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(0))
        {
            if(BeginPlay == false)
            {
                BeginPlay = true;
                StartPlay(true);
                traningBtn.SetBool("IsActive", true);
                if(GameController.control.trainingMode == false)
                    startBtn.SetActive(false);
            }

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

            if(GameController.control.isDead == false && BeginPlay == true)
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

    void FixedUpdate()
    {
        if (GameController.control.isDead == false)
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
        if (collision.gameObject.tag == "Obstacle" && GameController.control.isDead == false)
        {
            GameController.control.isDead = true;

            rb.AddForce(new Vector2(0, 2f) * Speed);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(2f);
            rb.gravityScale = 1;

            GameOver();
        }

        if (collision.gameObject.tag == "Wall" && GameController.control.isDead == false)
        {
            GameController.control.isDead = true;

            rb.AddForce(new Vector2(0, 2f) * Speed);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddTorque(2f);
            rb.gravityScale = 1;

            GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Crate" && GameController.control.isDead == false)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(pickUpSound, vol);

            looper.cratesPool.Enqueue(collider.gameObject);
            collider.gameObject.SetActive(false);

            UpdatePlayerStats();
        }

        if (collider.gameObject.tag == "Training" && GameController.control.isDead == false)
        {
            collider.gameObject.GetComponent<Animator>().SetBool("IsActive", true);
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
        GameController.control.score = 0;
        GameController.control.Load();
        DisplayPlayerData();
    }

    private void DisplayPlayerData()
    {
        scoreText.text = GameController.control.score.ToString();
        highScoreText.text = "High " + GameController.control.highScore.ToString();
        coinText.text = GameController.control.coins.ToString() + "$";

        goScoreText.text = GameController.control.score.ToString();
        goHighText.text = GameController.control.highScore.ToString();
    }

    private void GameOver()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(hitSound, vol);

        Vector3 nextPos = transform.position;

        if(nextPos.x > 0)
        {
            nextPos.x = -2f;
        }
        else
        {
            nextPos.x = 2f;
        }

        GameController.control.lastPosition = nextPos;
        GameController.control.isDead = true;

        if (GameController.control.score > GameController.control.highScore)
        {
            GameController.control.highScore = GameController.control.score;

            statsAnimator.SetBool("IsActive", true);
        }
        else
        {
            gameOverAnimator.SetBool("IsActive", true);
        }

        menuAnimator.SetBool("IsActive", true);

        DisplayPlayerData(); 
        
        if(GameController.control.trainingMode)
        {
            GameController.control.trainingMode = false;
        }

        GameController.control.Save();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void StartPlay(bool play)
    {
        if (play == false)
        {
            Speed = 0;
            rb.gravityScale = 0f;
        }
        else
        {
            if (GameController.control.trainingMode == true)
            {
                Speed = 120;
                rb.gravityScale = 0.36f;
            }
            else
            {
                Speed = 200;
                rb.gravityScale = 0.6f;
            }
        }
    }
}
