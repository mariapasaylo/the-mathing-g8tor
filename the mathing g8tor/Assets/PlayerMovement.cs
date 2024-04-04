using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * 7f, player.velocity.y);

        //Jump this is checked for every frame
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector2(player.velocity.x, 14);
        }

        if (player.velocity.y < 0)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        else
        {
            if (player.velocity.x >= 0)
            {
                spriteRenderer.sprite = spriteArray[0];
            }
            if (player.velocity.x < 0)
            {
                spriteRenderer.sprite = spriteArray[1];
            }
        }
     
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
}
