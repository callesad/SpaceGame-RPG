using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{

    //GAMEOBJECT MUST START INACTIVE IN SCENE FOR DIALOGUE WINDOW TO WORK PROPERLY
    //TD: make a gameobject manager that makes sure everything is in the write initial state before starting the game

    Controller controller;
    

    void Awake()
    {
        controller = new DialogueController(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
