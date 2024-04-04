using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileMapManager : MonoBehaviour
{
    [Header("Destrucatable =")]
    public Tilemap destructableTilemap;
    public TileBase destructableTile;
    public int numberOfDestructables = 70;
    [Header("Undestructable")]
    public Tilemap undestructableTilemap;
    
    private List<Vector3Int> tilesWithColliders = new List<Vector3Int>();

    // At the start of the game generate the destrutable boxes
    private void Start()
    {
        GenrateDestructables();
    }

    // Get positions of the undestructables in the undesrtuctable tilemap
    void GetTileVertices()
    {
        BoundsInt bounds = undestructableTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            Vector3 tileCenter = undestructableTilemap.GetCellCenterWorld(position);

            // Use Physics2D.OverlapPointAll to find colliders at the world position
            Collider2D[] colliders = Physics2D.OverlapPointAll(tileCenter);

            if (colliders.Length > 0)
            {
                tilesWithColliders.Add(position);
            }
        }
    }

    // Generate destructable tiles in the destructable tilemap
    // The destructable cannot overlap with the undestructable and
    // the generated tile cannot restrict first mvoe of the players
    private void GenrateDestructables()
    {
        Vector3Int[] forbiddenTiles = new Vector3Int[]{
            new Vector3Int(-6, 4, 0),
            new Vector3Int(-5, 4, 0),
            new Vector3Int(-6, 3, 0),
            new Vector3Int(6, -5, 0),
            new Vector3Int(6, -6, 0),
            new Vector3Int(5, -6, 0)

        };
        GetTileVertices();
        int numberOfGenerated = 0;
        while (numberOfGenerated < numberOfDestructables)
        {
            int randomX = Random.Range(-6, 7);
            int randomY = Random.Range(-6, 5);
            Vector3Int newPosition = new Vector3Int(randomX, randomY, 0);
            // If the position collides with undestructable
            if (tilesWithColliders.Contains(newPosition))
            {
                continue;
            }
            // If the postion collides with player first moves
            if (forbiddenTiles.Contains(newPosition))
            {
                continue;
            }

            destructableTilemap.SetTile(new Vector3Int(randomX, randomY, 0), destructableTile);
            numberOfGenerated++;
            tilesWithColliders.Add(newPosition);
        }
  
    }
}