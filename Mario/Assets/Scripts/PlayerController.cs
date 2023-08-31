using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    public GameObject pauseMenu;

    public AudioSource jumpAudio;
    public AudioSource hurtAudio;

    public GameObject life1, life2, life3;
    public int lifes = 6;

    private Rigidbody2D myRigbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    public bool isHurt;

    void Start()
    {
        myRigbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
            Run();
            Filp();
            Jump();
            CheckGrounded();
            SwitchAnimation();
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isGround);
    }

    void Filp()
    {
        bool playerHasXAixsSpeed = Mathf.Abs(myRigbody.velocity.x) > Mathf.Epsilon;
        if(playerHasXAixsSpeed)
        {
            if(myRigbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVe1 = new Vector2(moveDir * runSpeed, myRigbody.velocity.y);
        myRigbody.velocity = playerVe1;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                jumpAudio.Play();
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump)
                {
                    jumpAudio.Play();
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVe1 = new Vector2(0.0f, doubleJumpSpeed);
                    myRigbody.velocity = Vector2.up * doubleJumpVe1;
                    canDoubleJump = false;
                }
            }
        }
    }

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if(myAnim.GetBool("Jump"))
        {
            if(myRigbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
                if(myAnim.GetBool("Hurt"))
                {
                    myAnim.SetBool("Hurt", true);
                }
            }
        }
        else if(isHurt)
        {
            hurtAudio.Play();
            myAnim.SetBool("Hurt", true);
            if (Mathf.Abs(myRigbody.velocity.x) < 0.1f)
            {
                myAnim.SetBool("Idle", true);
                myAnim.SetBool("Hurt", false);
                isHurt = false;
            }
        }
        else if(isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
        if(myAnim.GetBool("DoubleJump"))
        {
            if(myRigbody.velocity.y < 0.6f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if(isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }
    public  void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Object")
        {
            Destroy(collision.gameObject);
            lifes++;
            Life();
        }

        if(collision.tag == "Die")
        {
            Invoke("Restart", 1f);
            lifes--;
            Life();
        }

        if (collision.tag == "End")
        {
            //Debug.Log(1);
            End();
        }

        if (collision.gameObject.tag == "Spike")
        {
            isHurt = true;
            lifes--;
            Life();
        }
            if (collision.gameObject.tag == "Linedie")
        {
            isHurt = true;
            lifes--;
            Life();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void End()
    {
        SceneManager.LoadScene("End");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            if(myAnim.GetBool("Fall"))
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigbody.velocity = Vector2.up * jumpVel;
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                myRigbody.velocity = new Vector2(-10, myRigbody.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                myRigbody.velocity = new Vector2(10, myRigbody.velocity.y);
                isHurt = true;
            }
        }
    }

    public void Life()
    {
        if(lifes == 6)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        else if (lifes == 4)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        else if (lifes == 2)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        else if (lifes < 1)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        /*if(siHurt)
          {
              lifes--;
              life();
          }
         */
    }
}
