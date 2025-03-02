using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //private
    private Vector2 movementVector;
    private Animator animator;                                // for animation
    private SpriteRenderer sprite_r;                          // for sprite flip
    private Rigidbody2D body;
    private bool isGrounded = false;
    private bool jump = false;
    private float fireRate = 0.3f;
    private float nextFire = 0;
    private bool facingRight = true;
    private AudioSource audioSource;

    //public
    public GameObject fire;
    public Transform firePoint;
    public float speed = 3;
    public float jumpForce = 250;
    public float maxSpeed = 7f;
    public float gravityMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();                 // for animation
        sprite_r = GetComponent<SpriteRenderer>();           // for sprite flip
        body = GetComponent<Rigidbody2D>();                  // to apply force to player
    }

    // Note that we changed this from Update to FixedUpdate as we are now working directly
    // with the player physics
    void FixedUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movementVector.x));   // for animation
       
        if (movementVector.x > 0 && body.velocity.x < maxSpeed)
            //transform.Translate(Vector2.right * speed * Time.deltaTime);  // vector is (1,0)
            body.AddForce(Vector2.right * speed);
        else if (movementVector.x < 0 && Mathf.Abs(body.velocity.x) < maxSpeed)
            //transform.Translate(Vector2.left * speed * Time.deltaTime); // vector is (-1, 0)
            body.AddForce(Vector2.left * speed);

        if (jump) //if the player pressed jump 
        {
            //transform.Translate(Vector2.up * jumpForce * Time.deltaTime);
            //StartCoroutine("LerpJump");
            body.AddForce(Vector2.up * jumpForce);
            jump = false;
            isGrounded = false;
        }

        // used this to get rid of player slowing floating down
        if (body.velocity.y < 0) //player is falling
        {
            body.gravityScale = gravityMultiplier; //speed up drop
        }
        else
        {
            body.gravityScale = 1;
        }
    }

    public void Update()
    {
        // to make sure player is facing direction they are heading
        if (movementVector.x < 0 && facingRight){ // if we are walking to the left
            Flip();
            facingRight = false;
        }
        else if (movementVector.x > 0 && !facingRight){ //if we are walking to the right
            Flip();
            facingRight = true;
    }
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        if (isGrounded) // to avoid the player jumping while not on the ground
            jump = true; 
        //Debug.Log("Jumping!!!");
        //transform.Translate(Vector2.up * jumpForce * Time.deltaTime);
    }

    public void OnFire(InputValue movementValue)
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("isShooting");
            Instantiate(fire, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0));
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            Debug.Log("Touching ground");
        }


        if (collision.gameObject.CompareTag("door"))
        {
            GameManager.instance.Win();
            GameManager.instance.WinLose();
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            if (GameManager.instance.GetLives() > 0)
            {
                GameManager.instance.DecreaseLives();
                if (GameManager.instance.GetLives() <= 0)
                {
                    GameManager.instance.WinLose();
                }
            }
            else if (GameManager.instance.GetLives() <= 0)
            {
                GameManager.instance.WinLose();
            }
        }
    }

    IEnumerator LerpJump()
    {
        float desired = transform.position.y + 3; //how high to go

        while(transform.position.y < desired) //while not that high
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    // Needed to restart the game if the player leaves the platform (or falls in lava)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("boundary"))
        {
            if (GameManager.instance.GetLives() > 0)
            {
                GameManager.instance.DecreaseLives();
                if (GameManager.instance.GetLives() <= 0)
                    {
                    GameManager.instance.WinLose();
                    }
                else
                    SceneManager.LoadScene(0);
            }
            else if (GameManager.instance.GetLives() <= 0)
            {
                GameManager.instance.WinLose();
            }
        }
    }

    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }

}
