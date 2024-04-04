using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlaformMovement : MonoBehaviour
{
    
    public float rotationXSpeed = 25.0f; 
    public float rotationZSpeed = 25.0f;
    private Vector3 rotation;
    void FixedUpdate()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 rotationVector = new Vector3(verticalInput, 0f, -horizontalInput);
        rotation += rotationVector;
        transform.rotation = Quaternion.Euler(rotation);

    }

  }
