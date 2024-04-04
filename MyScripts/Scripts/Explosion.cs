using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Renderers")]
    public AnimatedSpriteRenderer startRendered;
    public AnimatedSpriteRenderer middleRendered;
    public AnimatedSpriteRenderer endRendered;

    // Function to set active rendered according to which part of the flame is rendered
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        startRendered.enabled = renderer == startRendered;
        middleRendered.enabled = renderer == middleRendered;
        endRendered.enabled = renderer == endRendered;
    }

    // Function to set direction of the flame
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle*Mathf.Rad2Deg, Vector3.forward);
    }

    // Function to destroy after some seconds
    public void DestroyAfterSeconds(float seconds) 
    {
        Destroy(gameObject, seconds);
    }


}
