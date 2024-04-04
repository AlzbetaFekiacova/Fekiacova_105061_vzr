using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Transform platformTransform;
    private Vector3 lastVelocity;
    private Rigidbody rb;
    public float speed;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winningText;
    public TextMeshProUGUI failText;
    [SerializeField]
    private int numberOfCollectibles;
    public AudioSource deathSound;
    public AudioSource scoreSound;
    public AudioSource bounceSound;
    public AudioSource winningSound;
    private bool won;
    public Button restartButton;
    public GameObject platform;

    private void Start()
    {
        platformTransform = platform.transform;
        rb = GetComponent<Rigidbody>();
        score = 0;
        numberOfCollectibles = 10;
        won = false;
    }

    private void FixedUpdate()
    {
        if(transform.position.y < - 25 && !won) 
        {
            this.GetComponent<Renderer>().enabled = false;
            deathSound.Play();
            failText.text = "Prehral si";
            restartButton.gameObject.SetActive(true);
        }
        lastVelocity = rb.velocity;

        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("BouncyWall"))
        {
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
            bounceSound.Play();
        }

        else if (collision.gameObject.CompareTag("Collectable"))
        {
            
            collision.gameObject.SetActive(false);
            score++;
            Debug.Log("score = " + score);
            scoreText.text = "Score = " + score.ToString();
            scoreSound.Play();

            if(score == numberOfCollectibles)
            {
                winningText.text = "Vyhral si!\n\n Gratulujem!";
                winningSound.Play();
                won = true;
                restartButton.gameObject.SetActive(true);
            }
            
        }
        
        else if (collision.gameObject.CompareTag("Poisson") && !won)
        {

            this.GetComponent<Renderer>().enabled = false;
            failText.text = "Prehral si";
            deathSound.Play();
            restartButton.gameObject.SetActive(true);

        }
    }
}
