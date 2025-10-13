using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{

    [SerializeField] private GameObject parent;
    public Vector3 previousLocation;
    private Person parentPerson;


    void Awake()
    {
        parent = transform.parent.gameObject;
        parent.TryGetComponent<Person>(out parentPerson);
        //will crash if you try to start in a none walkable tile
        SetPreviousLocation();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
    }

///ChatGPT code-------------------------------------------------------------------
    public static HashSet<Vector3> reservedPositions = new HashSet<Vector3>();

    // Call this before moving to mark the spot as taken
    public static bool ReserveSpot(Vector3 destination)
    {
        // Using Vector3Int helps avoid floating-point precision mismatches
        Vector3Int gridPos = Vector3Int.RoundToInt(destination);

        if (reservedPositions.Contains(gridPos))
            return false;

        reservedPositions.Add(gridPos);
        return true;
    }

    

    void Update()
    {
        if (Vector3.Distance(transform.position,previousLocation)>10) {ResetPosition();}
    }

    void LateUpdate()
    {
        reservedPositions.Clear();
    }

///----------------------------------------------------------------------------------

    public void SetPreviousLocation(){
        previousLocation = transform.position;
    }

    public void ResetPosition(){
        transform.position=previousLocation;
    }  


    public void CheckForObjects(out Collider2D[] objects,out int objectCount,in Vector3 offset = default,bool includeTriggers = false)
    {
        // radius should match your movepoint collider size (tweak)
        float radius = 0.45f;
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = includeTriggers;            // ignore other triggers if desired
        objects = new Collider2D[8];

        objectCount = Physics2D.OverlapCircle(transform.position - new Vector3(0f,0.5f,0f) + offset, radius, filter, objects);
        
    }

    public bool IsPlayer(Vector3 offset = default)
    {
        Collider2D[] objects;
        int objectCount;

        CheckForObjects(out objects, out objectCount, in offset);

        for (int i = 0; i < objectCount; i++)
        {
            Collider2D c = objects[i];
            if (c.gameObject.TryGetComponent<PlayerPerson>(out PlayerPerson Player)) {
                if (parentPerson.debugLogs) Debug.Log(parent.gameObject.name + " detected player");
                return true;}
        }
        return false;
    }


    public bool IsNPC(Vector3 offset = default)
    {
        Collider2D[] objects;
        int objectCount;

        CheckForObjects(out objects, out objectCount, in offset);

        for (int i = 0; i < objectCount; i++)
        {
            Collider2D c = objects[i];
            if (c == null) continue;

            // Ignore self's and parent
            if (c.gameObject == this.gameObject||c.gameObject==parent) continue;

            // Ignore colliders that are children of this NPC
            if (c.transform.IsChildOf(parent.transform)) continue;

            if (c.gameObject.TryGetComponent<NPCPerson>(out NPCPerson npc)) {
                if (parentPerson.debugLogs) Debug.Log(parent.gameObject.name + " detected npc");
                return true;
            }
        }
        return false;
    }

    public bool isInRoom(Room targetRoom, Vector3 offset = default) 
    {
        Collider2D[] objects;
        int objectCount;

        CheckForObjects(out objects, out objectCount, in offset, true);

        for (int i = 0; i < objectCount; i++)
        {
            Collider2D c = objects[i];
            if (c.gameObject.TryGetComponent<Room>(out Room room)) {
                if(room!=targetRoom) {
                    if (parentPerson.debugLogs) Debug.Log(parent.gameObject.name + "tried to enter a different 'room'");
                    return false;
                }
                return true;}
        }
        if (parentPerson.debugLogs) Debug.Log(parent.gameObject.name + "tried to go somewhere that isn't recognised as a 'room'");
        return false;
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

    

    
}
