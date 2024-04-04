using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Define Enum to differentiate different types of pickups
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
        Death,
        KickBomb,
    }

    [Header ("Pick up attributes")]
    public float destoryDuration = 1f;
    public ItemType type;

    // Function which handles what happens when user collects the pick up 
    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                int numberOfBombs = player.GetComponent<BombController>().bombAmout;
                player.GetComponent<PlayerTxtManager>().SetBombText(numberOfBombs);
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().IncreaseExplosionSize();
                int explosionSize = player.GetComponent<BombController>().explosionSize;
                player.GetComponent<PlayerTxtManager>().SetFlameText(explosionSize);
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().AddSpeed();
                float speed = player.GetComponent<MovementController>().speed;
                player.GetComponent<PlayerTxtManager>().SetSpeedText(Mathf.FloorToInt(speed));
                
                break;

            case ItemType.Death:
                player.GetComponent<MovementController>().PickUpDeathBox();
               
                break;
            case ItemType.KickBomb:
                player.GetComponent<MovementController>().SetKickBomb();
                player.GetComponent<PlayerTxtManager>().SetKickText();
              
                break;
        }

        Destroy(gameObject);
    }

    // To differentiate if the user has picked up the item
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }

    }

}