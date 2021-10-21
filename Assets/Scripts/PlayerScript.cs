using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    private int scoreValue = 0;
    private bool gameOver = false;

    private int livesCount = 3;
    public Text livesText;

    public AudioSource musicSource;
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    Animator anim;

    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        SetScoreValue ();
        winText.text = "";
        SetLivesCount ();

        musicSource.clip = musicClip1;
        musicSource.Play();
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
            else if (facingRight == true && hozMovement < 0)
                {
                    Flip();
                }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreValue ();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(50.0f, 0.0f);
                livesCount = 3;
                SetLivesCount ();
            }

            if (scoreValue > 7 && gameOver == false)
            {
                winText.text = "You Win! Created by David Trefry";
                musicSource.clip = musicClip2;
                musicSource.Play();
                gameOver = true;
            }
        }

        if (collision.collider.tag == "Enemy" && gameOver == false)
        {
            livesCount -= 1;
            SetLivesCount ();
            Destroy(collision.collider.gameObject);

            if (livesCount <1)
            {
                winText.text = "You Lose! Created by David Trefry";
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {

            if (rd2d.velocity.magnitude > 0)
            {
            anim.SetInteger("MovementState", 1);
            }

            if (rd2d.velocity.magnitude == 0)
            {
            anim.SetInteger("MovementState", 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                    rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                    anim.SetInteger("MovementState", 5);
            }
        }
    }

    void SetScoreValue ()
    {
        score.text = "Score: " + scoreValue.ToString();
    }

    void SetLivesCount ()
    {
        livesText.text = "Lives: " + livesCount.ToString();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}