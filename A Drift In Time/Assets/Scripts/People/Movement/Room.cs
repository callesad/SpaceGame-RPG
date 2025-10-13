using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{


    public int maxWalkAroundSpace = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Person>(out Person person)&&collision.isTrigger) {
            person.whatRoomImIn = this;
        }
    }

    
    void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.gameObject.TryGetComponent<Person>(out Person person)) {
            if (person.whatRoomImIn==this)
            {
                person.whatRoomImIn = null;
            }
        }
    }
    
}
