using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapScript : MonoBehaviour
{
    public Tilemap tilemap = null;
    public GameObject particlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) )
            {
                Vector3 newPos = new Vector3(position.x + 0.5f, position.y + 0.5f, -1);
                Instantiate(particlePrefab, newPos, Quaternion.identity);
            }            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        /*foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            Gizmos.color = new Color(1, 1, 0, 0.75F);

            Gizmos.DrawSphere(position, 0.1f);
        }
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        */
    }
}
