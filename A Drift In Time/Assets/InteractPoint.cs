using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{


    [SerializeField] private GameObject parent;
    private Person parentPerson;


    void Awake()
    {
        parent = transform.parent.gameObject;
        parent.TryGetComponent<Person>(out parentPerson);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(int interactType){
        Debug.Log("pp");
        if (IsInteractable(out Iinteractable interactable)) {
            interactable.Interact(interactType);
            Debug.Log("Interacted");
        }
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

    public bool IsInteractable(out Iinteractable subject ,Vector3 offset = default)
    {
        Collider2D[] objects;
        int objectCount;
        subject = null;

        CheckForObjects(out objects, out objectCount, in offset);

        for (int i = 0; i < objectCount; i++)
        {
            Collider2D c = objects[i];
            if (c == null) continue;

            // Ignore self's and parent
            if (c.gameObject == this.gameObject||c.gameObject==parent) continue;

            // Ignore colliders that are children of this NPC
            if (c.transform.IsChildOf(parent.transform)) continue;

            if (c.gameObject.TryGetComponent<Iinteractable>(out Iinteractable interactable)) {
                if (parentPerson.debugLogs) Debug.Log(parent.gameObject.name + " interacted with" + interactable);
                subject = interactable;
                return true;
            }
        }
        return false;
    }
}
