using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    

    
    [Header("Player attribudes")]
    public float speed = 5f;
    public Boolean kickBomb = false;
    public int playerNumber = 0;

    [Header("Player codes")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    [Header("Player sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer activeSpriteRenderer;
    public AnimatedSpriteRenderer spriteRendererDeath;

    [Header("Sounds")]
    public AudioClip deathSound;
    public AudioClip pickUpSound;
    private AudioSource audioSource;

    [Header("Game manager")]
    public GameManager manager;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.down;

    // Function to set variables on the begginig of the game
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        audioSource = GetComponent<AudioSource>();
    }

    // Funtion to be performed on each frame
    // Check which direction to be set
    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    // Update related to the change of the player's position
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;

        rb.MovePosition(position + translation);
    }

    // Function to set the player dirrection and the corresponding sprite
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    
    
    // Check if the player collides whether he collided wih flame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            audioSource.clip = deathSound;
            audioSource.Play();
            DeathSequence();
        }
    }

    // Make player die
    public void PickUpDeathBox()
    {
       
        DeathSequence();
    }

    // Start the player death, set the death sprite
    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.5f);
    }

    // When user dies disable the player game object
    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        manager.CheckWinState();

    }

    // Make player be able to kick bombs
    public void SetKickBomb()
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
        kickBomb = true;
    }

    // Add speed to the player
    public void AddSpeed()
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
        speed++;
    }


}