using System.Collections;
using System.Transactions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKeyCode = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmout = 1;
    private int remaingBombs;

    [Header("Sounds")]
    public AudioClip timer;
    public AudioClip bombSound;
    public AudioClip pickUpSound;
    public AudioSource audioSource;
    

    [Header ("Explosion")]
    public Explosion explosionPrefab;
    public float explostionDuration = 1f;
    public int explosionSize = 1;
    public LayerMask explosionLayerMask;

    [Header("Destructuble")]
    public Tilemap destrictubleTiles;
    public Destructuble destructubiblePrefab;
    public LayerMask pickUpLayer;
    private int putBy = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    // At the beging set the number of remaing bombs to be bomb amount
    private void OnEnable()
    {
        remaingBombs = bombAmout;
    }

    // On each frame check the number of remaing bombs and key input
    private void Update()
    {
        if (Input.GetKeyDown(inputKeyCode) && remaingBombs > 0)
        {
            StartCoroutine(PlaceBomb());
        }
    }

    // Recursion funtion to place a bomb and explode the bomb
    private IEnumerator PlaceBomb()
    {
        audioSource.clip = timer;
        audioSource.Play();
        // Round the position of the bomb to be centered 
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // Create a bomb
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        putBy = GetComponent<MovementController>().playerNumber;
        remaingBombs--;

        // Suspend the bomb explosion
        yield return new WaitForSeconds(bombFuseTime);

        // Set the bomb position
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // Create explostion in the bomb position
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.startRendered);
        explosion.DestroyAfterSeconds(explostionDuration);

        // Explode in each direction
        Expode(position, Vector2.up, explosionSize);
        Expode(position, Vector2.down, explosionSize);
        Expode(position, Vector2.right, explosionSize);
        Expode(position, Vector2.left, explosionSize);
        bomb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        audioSource.clip = bombSound;
        audioSource.Play();
        Destroy(bomb);
        remaingBombs++;
    }

    // Function to prevent the user from walking over bomb
    // When the bom is put, the bomb is no longer a trigger
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            if(other.attachedRigidbody.bodyType.Equals(RigidbodyType2D.Static) && other.isTrigger)
            {
                other.isTrigger = false;
            }
            if (gameObject.GetComponent<MovementController>().kickBomb && other.attachedRigidbody.bodyType.Equals(RigidbodyType2D.Static))
            {
                if (putBy == gameObject.GetComponent<MovementController>().playerNumber)
                {
                    other.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
    }
         
    // Recursive funtion to perform explosion
    private void Expode(Vector2 position, Vector2 direction, int explosionLenght )
    {
        // End condition
        if (explosionLenght <=0)
        {
            return;
        }

        // Calculate new position
        position += direction;

        // Check whether the direction is clear (if there is no destructable object)
        if (Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        // Check whether the direction is clear (if there is no destructable object)
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, pickUpLayer))
        {
            ClearPickUp(position);
            return;
        }

        // Create new explosion in given direction
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosionLenght > 1 ? explosion.middleRendered : explosion.endRendered );
        explosion.SetDirection(direction);
        explosion.DestroyAfterSeconds(explostionDuration);
        Expode(position, direction, explosionLenght-1);

       
    }

    private void ClearPickUp(Vector2 position)
    {
        // Find the object at the specified position
        Collider2D[] colliders = Physics2D.OverlapPointAll(position);

        // Loop through all colliders at the position and destroy them
        foreach (Collider2D collider in colliders)
        {
            Destroy(collider.gameObject);
        }
    }
    // Function to explode the destructable in the path of the flame
    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destrictubleTiles.WorldToCell(position);
        TileBase tile = destrictubleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructubiblePrefab, position, Quaternion.identity);
            destrictubleTiles.SetTile(cell, null);
        }
    }

    // Function to add bomb to the player
    public void AddBomb()
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
        bombAmout++;
        remaingBombs++;
    }

    public void IncreaseExplosionSize()
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
        explosionSize++;
    }

 }
