using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    
    [Header ("Sprites")]
    public Sprite idleSprite;
    public Sprite[] animationSprites;
    
    [Header ("Characteristics")]
    public bool loop = true;
    public bool idle = true;
    public float animationTime = 0.25f;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    // Set the sprite renderer on the begging of the game
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Set corresponding sprite on enable
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    // Disable corresponding sprite on enable
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    // Start animating on start
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    // Funtion that corresponds to the next frame of the game
    // Set the next sprite to be active
    private void NextFrame()
    {
        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }

}