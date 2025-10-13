using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{

    public Vector3 point => transform.position + new Vector3(0f,0.5f,0f);
    
    public List<GameObject> adjacentPoints = new List<GameObject>();

    void Awake()
    {

        /*notes
        scales children so if pathpoint has no parent pathpoint must be proper scale initially
        only parent and child pathpoints are linked enherently, adjacent path points need to be 
        linked manually
        */

        //linking parent and children pathpoints, deparenting, and scaling to standard-----------------
        //--------copy children into list
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

            //append children and parent to adjacentPoints list
        foreach (Transform child in children)
        {
            
            this.adjacentPoints.Add(child.gameObject);
            if(child.gameObject.TryGetComponent<PathPoint>(out PathPoint childPathPoint)||!childPathPoint.adjacentPoints.Contains(this.gameObject))
            {
                childPathPoint.adjacentPoints.Add(this.gameObject);
            }
            //deparenting
            //Transform parent = child.parent;
            child.parent=null;
            child.localScale = new Vector3 (0.3f,0.3f,1);

            //child.parent = parent;
    
        }
        //---------------------------------------------------------------------------------------------

        //snaping to grid------------------------------------------------------------------------------
        Vector3 position = transform.position;
        transform.position = new Vector3 (Mathf.RoundToInt(position[0]-0.5f)+0.5f,Mathf.RoundToInt(position[1]-0.5f)+0.5f,position[2]);
        //---------------------------------------------------------------------------------------------

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<NPCPerson>(out NPCPerson NPC)&&collision.isTrigger) {
            NPC.whereImAt = this.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<NPCPerson>(out NPCPerson NPC)) {
            NPC.whereIveBeen = this.gameObject;
            NPC.whereImAt = null;
        }
    }




}
