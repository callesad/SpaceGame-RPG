using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePoint : MonoBehaviour
{


    [SerializeField] private Tilemap tilemap;
    private Vector3Int gridPosition;
    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        //initializing tile position
        gridPosition = tilemap.WorldToCell(transform.position);
        SnapToGridPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SnapToGridPosition()
    {
        
        if (debug) Debug.Log("Tile: " + gridPosition);

        transform.position = tilemap.CellToWorld(gridPosition) + new Vector3(0.5f,0.5f,0f);
    }

    


}
