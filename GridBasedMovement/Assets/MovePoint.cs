using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePoint : MonoBehaviour
{


    [SerializeField] private Tilemap tilemap;
    private Vector3Int gridPosition;
    private Vector3Int previousPosition;

    private GameObject parent;
    private Person parentPerson;

    public bool debug;

    #region monobehaviour functions

    void Awake()
    {
        parent = transform.parent.gameObject;
        parent.TryGetComponent<Person>(out parentPerson);
    }

    // Start is called before the first frame update
    void Start()
    {
        //initializing tile position
        gridPosition = tilemap.WorldToCell(transform.position);
        SetPreviousPosition();
        SnapToGridPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,previousPosition)>10) {ResetPosition();}
    }

    #endregion

    #region movepoint position functions

    void SetPreviousPosition()
    {
        previousPosition = gridPosition;
    }

    void ResetPosition()
    {
        gridPosition = previousPosition;
        SnapToGridPosition();
    }

    void SnapToGridPosition()
    {
        
        if (debug) Debug.Log("Tile: " + gridPosition);

        transform.position = tilemap.CellToWorld(gridPosition) + new Vector3(0.5f,0.5f,0f);
    }

    void SetPosition(Vector3Int newPos)
    {
        gridPosition = newPos;
        SnapToGridPosition();
    }

    #endregion

    #region collider checks

    public void CheckForObjects(out Collider2D[] objects,out int objectCount,in Vector3 offset = default,bool includeTriggers = false)
    {
        // radius should match your movepoint collider size (tweak)
        float radius = 0.45f;
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = includeTriggers;            // ignore other triggers if desired
        objects = new Collider2D[8];

        objectCount = Physics2D.OverlapCircle(transform.position - new Vector3(0f,0.5f,0f) + offset, radius, filter, objects);
        
    }

    public bool CanMoveThere(Vector3 offset = default)
    {

        Collider2D[] objects;
        int objectCount;

        CheckForObjects(out objects, out objectCount, in offset);

        for (int i = 0; i < objectCount; i++)
        {
            Collider2D c = objects[i];
            if (c == null) continue; // skip empty slots
            if (c.gameObject == parent) continue; // parent is fine
            return false; // any other collider = invalid
        }
        return true;
    }

    #endregion

}
