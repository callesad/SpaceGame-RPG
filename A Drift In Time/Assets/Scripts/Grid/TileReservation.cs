using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;   

public class TileReservation : MonoBehaviour 

{
    static Tilemap tilemap;
    [SerializeField]TileBase marker;

    public static HashSet<Vector3Int> reservedPositions = new HashSet<Vector3Int>();
/*
    public TileReservation(Tilemap tilemap , TileBase marker)
    {
        this.tilemap = tilemap;
        this.marker = marker;
    }*/

    void Awake()
    {
        if (!this.gameObject.TryGetComponent<Tilemap>(out Tilemap tm)) {
            throw new System.InvalidOperationException("<TileReservation> Script needs to be attached to a <GameObject> with a <Tilemap> component");
        }

        tilemap = tm; // assign to static field
    }
    
    void Update() 
    {
        PrintReservedGrid(reservedPositions);
    }

    void LateUpdate()
    {
        ClearGrid(reservedPositions);

    }

    // Call this before moving to mark the spot as taken
    public static bool ReserveSpot(Vector3 destination/*, Tilemap tilemap*/)
    {
        // Using Vector3Int helps avoid floating-point precision mismatches
        Vector3Int gridPos = tilemap.WorldToCell(destination + new Vector3Int(0,-1,0));

        if (reservedPositions.Contains(gridPos))
            return false;

        reservedPositions.Add(gridPos);
        return true;
    }

    private void PrintReservedGrid(HashSet<Vector3Int> grid)
    {
        foreach(Vector3Int tilePos in grid) {
            tilemap.SetTile(tilePos, marker); // replace tile
        }
    }

    void ClearGrid(HashSet<Vector3Int> grid) {
        foreach(Vector3Int tilePos in grid) {
            tilemap.SetTile(tilePos, null); // replace tile
        }
        grid.Clear();
    }
}
