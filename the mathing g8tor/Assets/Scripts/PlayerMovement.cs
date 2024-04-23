using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D player;
    private BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    private Animator anim;
    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        GlobalVariables.friendCount = 3;

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        //Jump this is checked for every frame
        if (Input.GetButtonDown("Jump") && IsGrounded())
        { //Adjust the strenght of the jump
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }

        //Up and down movement
        if (player.velocity.y < 0)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        else
        {  //Left and right movement
            if (player.velocity.x >= 0)
            {
                spriteRenderer.sprite = spriteArray[0];
            }
            if (player.velocity.x < 0)
            {
                spriteRenderer.sprite = spriteArray[1];
           }
        }
       
        UpdateAnimationState();
 
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        //running and idle animations
        if (dirX > 0f) //right
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f) //left
        {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else //idle
        {
            state = MovementState.idle;
        }

        //do not do if else because you don't want idle and running to occur at the same time as jump and fall
        //Jumping animation
        if (player.velocity.y > .1f) //check for upward velocity. It is not exactly zero when you are not applying any force
        {
            state = MovementState.jumping;
        }else if (player.velocity.y < -.1f) { //check for downward velocity
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);

    }

    private bool IsGrounded() 
    {   //This function is meant to stop us from jumping an unlimited number of times
        //Checks if you are overlapping with jumpable ground
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "VOID")
        {
            // Starting position of player character
            player.position = new Vector2(-13, -2);
        }

        // Once the friends are free, door will be set with
        // an active tag "Exit" and this will trigger the next scene
        if (other.tag == "Exit")
        {
            SceneManager.LoadScene(2);
        }
    }
    

    // Load player position from saved game data
    public void LoadData(GameData data)
    {
        // Move the player to the last saved location
        this.transform.position = data.position;
    }

    // Save player position to game data
    public void SaveData(ref GameData data)
    {
        // save the player's current location
        data.position = this.transform.position;
    }
}


