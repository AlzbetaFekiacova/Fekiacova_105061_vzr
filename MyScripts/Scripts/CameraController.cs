using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private bool isClicked = false;
    private Vector3 initialPosition = new Vector3(2, 4, -3);
    private Vector3 intitalRotation = new Vector3(47, 0, 0);    

    public Transform targetObject;
    public float speed;
    public float translationSpeed = 0.5f;
    public float translationSpeedKeyboard = 0.05f;
    public float zoomSpeed;
    public float rotateSpeed = 2;
    public TMP_Text ButtonText;
    public PlayableDirector timeline;
    public Button RotateButton;
    public Button AnimationButton;

    public void RototeOnClick()
    {
 
        if (!isClicked)
        {
            transform.position = initialPosition;
            transform.eulerAngles = intitalRotation;
            ButtonText.text = "Manualne";
            AnimationButton.interactable = false;

        }
        else
        {
            ButtonText.text = "Otacanie";
            AnimationButton.interactable = true;
        }
        isClicked = !isClicked;

    }

    public void PlayAnimation()
    {
        if (!isClicked)
        {
            timeline.Play();
            RotateButton.interactable = false;
            AnimationButton.interactable = false;
        }
    }

    void Update()
    {
        // Check if rotation is om
        if (isClicked)
        {
            transform.RotateAround(targetObject.position, Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            // Check if the timeline has finished
            if (timeline.state == PlayState.Paused && !timeline.extrapolationMode.Equals(DirectorWrapMode.Loop))
            {
                RotateButton.interactable = true;
                AnimationButton.interactable = true;
            }

            if (Input.GetMouseButton(2))
            {
                float pointerX = Input.GetAxis("Mouse X");
                float pointerY = Input.GetAxis("Mouse Y");
                transform.Translate(-pointerX * translationSpeed, -pointerY * translationSpeed, 0);
            }
            else if (Input.GetMouseButton(1))
            {
                float pointerX = Input.GetAxis("Mouse X");
                float pointerY = Input.GetAxis("Mouse Y");
                transform.Rotate(-pointerY * rotateSpeed, pointerX * rotateSpeed, 0);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
            }

            Quaternion originalRot = transform.rotation;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);

            // Keyborard move

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            transform.Translate(movement * translationSpeedKeyboard);
        }
    }
}

