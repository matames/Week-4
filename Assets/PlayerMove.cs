using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour

{
    public float speed;
    public float jumpHeight;
    public float gravityMultiplier;
    public float afterjumpVelocity;

    private float speedValue;
   

    bool onFloor;
    bool onWall;

    Rigidbody2D myBody;
    SpriteRenderer myRenderer;

    public Animator anim;
    public Sprite jumpSprite;
    public Sprite walkSprite;
    public Sprite wallSprite;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Walking", false);
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(onFloor && myBody.velocity.y > 0)
        {
            onFloor = false;
        }

        if(onFloor && myBody.velocity.x == 0)
        {
            anim.SetBool("Walking", false);
        }

        if(!onFloor && onWall)
        {
            anim.SetBool("Walking", false);
            myRenderer.sprite = wallSprite;
        }

        CheckKeys();
        JumpPhysics();
        
    }

    void CheckKeys()
    {
        if(Input.GetKey(KeyCode.D))
        {
            myRenderer.flipX = false;
            anim.SetBool("Walking", true);
            HandleLRMovement(speed);
            speedValue = 1;
            //myBody.velocity += Vector2.right * Physics2D.gravity * (accelerationMultiplier - 1f) * Time.deltaTime;


        } else if (Input.GetKey(KeyCode.A))
        {
            myRenderer.flipX = true;
            anim.SetBool("Walking", true);
            HandleLRMovement(-speed);
            speedValue = -1;
            //myBody.velocity += Vector2.left * -Physics2D.gravity * (accelerationMultiplier - 1f) * Time.deltaTime;

        }

        if(Input.GetKeyDown(KeyCode.W) && onFloor || Input.GetKeyDown(KeyCode.W) && onWall)
        {
            anim.SetBool("Walking", false);
            myRenderer.sprite = jumpSprite;
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);

        }


        else if(!Input.GetKeyDown(KeyCode.W) && !onFloor)
        {
            myBody.velocity += Vector2.up * (Physics2D.gravity.y) / 4 * (jumpHeight - 1f) * Time.deltaTime;
        }
    }

    void JumpPhysics()
    {
        if(myBody.velocity.y < 0)
        {
            anim.SetBool("Walking", false);
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1f) * Time.deltaTime;
        } 
    }

    void HandleLRMovement(float dir)
    {
        myBody.velocity = new Vector3(dir, myBody.velocity.y);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "floor")
        {
            myRenderer.sprite = walkSprite;
            onFloor = true;
            myBody.velocity = new Vector2(speedValue * afterjumpVelocity, myBody.velocity.y);

        }
        if(collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "wall")
        {
            anim.SetBool("Walking", false);
            onWall = true;
        }
    }
}
