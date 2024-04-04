using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructuble : MonoBehaviour
{
    [Header("Spawnable")]
    public float destructionTime = 1f;
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.8f;
    public GameObject[] spawnableItems;

    // We destroy the object over the destruction of the time
    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    // When we are destroying the object we spawn the collectable if the generated number is less than the spawn chance
    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
