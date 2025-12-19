using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPerson : Person, Iinteractable
{

    //global variables//

    public string DialogueKnotName = "npc";

//path point references-------------------------------------------

    private GameObject pathGrid;

    public GameObject whereILive;

    public GameObject whereImGoing;

    public GameObject whereImAt;

    public GameObject whereIveBeen;

    public GameObject whereIWantToBe;

    public List<GameObject> path = null;

    //public GameObject targetLocation;

//----------------------------------------------------------------

//movement vectors------------------------------------------------

    public Vector3 targetPosition;
    private Vector3 direction;

    [SerializeField]private int numberOfTimesHitNPC = 0;
    [SerializeField]private int numberOfTimesHitPlayer = 0;
    //[SerializeField]private int numberOfTimesHitAnything = 0;
    public int numberOfTimesNPCHasNotMoved = -1;

    public float meanderingWaitTime = 5f;
    public int meanderingWalkDistance = 3;

    
//----------------------------------------------------------------    

//state machine global variables------------------------------

    private PersonStateMachine stateMachine; //state machine

    //initializing states:
    public PersonRandomlyWalkingState RandomlyWalking;
    public PersonWalkingWithPurposeState WalkingWithPurpose;
    public PersonIdleState Idle;
    public PersonMeanderingState Meandering;
    public PersonDialogueWithPlayerState Dialogue;

//------------------------------------------------------------

//trigger-bools------------------------------------------------

    public bool walkRandomly = false;
    public bool isStuck = false;
    public bool isScrewed = false;
    public bool meander = false;




//-------------------------------------------------------------
    
    //functions//

//monobehaviour functions-------------------------------------

    #region subscribing to events
    void OnEnable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnEnterDialogue+=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue+=OnExitDialogue;
        }
    }
    

    void OnDisable()
    {
        if (EventManager.Instance != null) {
            EventManager.Instance.OnEnterDialogue-=OnEnterDialogue;
            EventManager.Instance.OnExitDialogue-=OnExitDialogue;
        }
    }
    #endregion

    #region initializonf actions 
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PersonStateMachine();
        pathGrid = GameObject.FindGameObjectWithTag("PathGrid");
    
        #region initializing states
        RandomlyWalking = new PersonRandomlyWalkingState(this,stateMachine);
        WalkingWithPurpose = new PersonWalkingWithPurposeState(this,stateMachine);
        Idle = new PersonIdleState(this,stateMachine);
        Meandering = new PersonMeanderingState(this,stateMachine);
        Dialogue = new PersonDialogueWithPlayerState(this,stateMachine);
        #endregion
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(Idle);
    }
    #endregion

    #region update actions
    protected override void Update()
    {
        base.Update();
        if(isStuck) {
            /*whereImGoing=whereIveBeen; //tries to get unstuck, will expand upon later
            numberOfTimesNPCHasNotMoved=0;
            isStuck=false;*/
            stateMachine.currentState.GetUnstuck();

        }
        stateMachine.currentState.FrameUpdate();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixedFrameUpdate();

    }
    #endregion

//------------------------------------------------------------

//movement functions-(Non-Eventfull-PathFinding)-------------------------------

    public int MoveMovePointTo(Vector3 destination)  //returns int based on what was hit, 1=player, 2=npc, 3=terrain, 0=nothing
    {

        //testing to see if NPC is stuck---------------------------------------------------------------
        //keeping track of how long since npc has been able to move
        if (movePoint.previousLocation == movePoint.transform.position) {
            numberOfTimesNPCHasNotMoved+=1; //this means that in the previous iteration the movepoint was reset to its previous location, thus nether have changed
        } else {
            numberOfTimesNPCHasNotMoved=0; //reset counter
        }

        //this line is subject to change
        if(numberOfTimesNPCHasNotMoved>600) {
            //whereImGoing=whereIveBeen;
            if (debugLogs) Debug.Log(this.gameObject.name + " is stuck");
            isStuck=true;
            return 0; //since nothing has been done in this script yet, 0 just resets all the counts
        }
        //------------------------------

        //---------------------------------------------------------------------------------------------


        //caching current location as previous location
        movePoint.SetPreviousLocation();

        //numberOfTimesHitAnything+=1; //increases hit count by 1 every frame, gets reset if nothing is hit

        //Try to reserve destination, if fails means other person is going to move there
        if (!MovePoint.ReserveSpot(destination))
        {
            // Someone else already claimed it this frame
            movePoint.ResetPosition(); //revert location of movepoint
            
            return 1; // Treat as player collision just cuz
        }
        //--------------------------------------------------------------------------------
    
        movePoint.transform.position = destination; //moving location of movepoint

        //bypasses-------------------------

        if(numberOfTimesHitNPC>100) {
            return 0;
        }

        if(numberOfTimesHitPlayer>300) {
            return 0;
        }

        //----------------------------------

        if(movePoint.CanMoveThere()) { //if movepoint is in empty space, resets hit counters and returns 0     
            return 0; //hit nothing

            
        }

        if(movePoint.IsPlayer()) {
            movePoint.ResetPosition(); //reset position of movepoint
            return 1; //hit player
        } 
        
        if (movePoint.IsNPC()) {
            movePoint.ResetPosition(); //reset position of movepoint
            return 2; //hit npc
        } 
        
        else {
            movePoint.ResetPosition(); //reset position of movepoint
            return 3; //hit non person object
        }
    }
    

    public void MoveHorizontal(int numberOfSpaces,Vector3 path)
    {
        int hitCode = MoveMovePointTo(movePoint.transform.position + new Vector3Int(numberOfSpaces,0,0));
        switch (hitCode) {
            case 0: //hit nothing

                //reset checks
                numberOfTimesHitNPC=0;
                numberOfTimesHitPlayer=0;
                //numberOfTimesHitAnything=0;

                break;
            case 1: //hit player

                numberOfTimesHitNPC=0; //reset npc check

                numberOfTimesHitPlayer+=1;

                break;
            case 2: //hit npc

                numberOfTimesHitPlayer=0; //reset player check 

                MoveMovePointTo(movePoint.transform.position + new Vector3Int(0,numberOfSpaces,0));
                //MoveMovePointTo(movePoint.transform.position + new Vector3(0f,numberOfSpaces,0f));

                numberOfTimesHitNPC+=1;

                break;

            case 3: //hit terrain
                MoveMovePointTo(movePoint.transform.position + new Vector3Int(0,Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),0));
                break;
        }
    }

    public void MoveVertical(int numberOfSpaces,Vector3 path)
    {
        int hitCode = MoveMovePointTo(movePoint.transform.position + new Vector3Int(0,numberOfSpaces,0));
        switch (hitCode) {
            case 0: //hit nothing

                //reset checks
                numberOfTimesHitNPC=0;
                numberOfTimesHitPlayer=0;
                //numberOfTimesHitAnything=0;

                break;
            case 1: //hit player

                numberOfTimesHitNPC=0; //reset npc check

                numberOfTimesHitPlayer+=1;

                break;
            case 2: //hit npc

                numberOfTimesHitPlayer=0; //reset player check

                MoveMovePointTo(movePoint.transform.position + new Vector3Int(numberOfSpaces,0,0));
                //MoveMovePointTo(movePoint.transform.position + new Vector3(0f,numberOfSpaces,0f));

                numberOfTimesHitNPC+=1;

                break;

            case 3:
                MoveMovePointTo(movePoint.transform.position + new Vector3Int(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),0,0));
                break;
        }
    }

    

    /*
    public void MoveTowards(Vector3 destination)
    {

        //testing if more distnce in x or y
        Vector3 path = destination - transform.position;
        Vector3Int direction;

        if (Mathf.Abs(path[0])>Mathf.Abs(path[1])) {
            //move horizontally
            //MoveHorizontal(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),path);
            //MoveNPCpath(new Vector3Int(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),0,0),path);
            direction = new Vector3Int(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),0,0);
        } else {
            //move vertically
            //MoveVertical(Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),path);
            //MoveNPCpath(new Vector3Int(0,Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),0),path);
            direction = new Vector3Int(0,Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),0);
        }




        Vector3Int inverseDirection = new Vector3Int(direction[1],direction[0],0);

        for (int i = 0 ; i < 2 ; i++){
            if (inverseDirection[i]!=0) {
                inverseDirection[i]=Mathf.RoundToInt(path[i]/Mathf.Abs(path[i]));
            }
        }

        int hitCode = MoveMovePointTo(movePoint.transform.position + direction);
        
        switch (hitCode) {
            case 0: //hit nothing

                //reset checks
                numberOfTimesHitNPC=0;
                numberOfTimesHitPlayer=0;
                //numberOfTimesHitAnything=0;

                break;
            case 1: //hit player
                Debug.Log(this.gameObject.name + " returned 1");

                numberOfTimesHitNPC=0; //reset npc check

                numberOfTimesHitPlayer+=1;

                break;
            case 2: //hit npc
                Debug.Log(this.gameObject.name + " returned 2");

                numberOfTimesHitPlayer=0; //reset player check

                numberOfTimesHitNPC+=1;

                MoveMovePointTo(movePoint.transform.position + inverseDirection);
                //MoveMovePointTo(movePoint.transform.position + new Vector3(0f,numberOfSpaces,0f));
                break;

            case 3: //hit terrain
                Debug.Log(this.gameObject.name + " returned 3");

                

                MoveMovePointTo(movePoint.transform.position + inverseDirection);
                break;
        }
    }*/
    
    //this is the function to use to go to places, everything else is just to accomplish this
    public void MoveTowards(Vector3 destination)
    {


        if (Vector3.Distance(movePoint.transform.position,destination)<0.05) {return;}//bypass if already at where is trying to go

        //testing if more distnce in x or y
        Vector3 path = destination - transform.position;

        if (Mathf.Abs(path[0])>Mathf.Abs(path[1])) {
            //move horizontally
            MoveHorizontal(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),path);
            //MoveNPCpath(new Vector3Int(Mathf.RoundToInt(path[0]/Mathf.Abs(path[0])),0,0),path);
        } else {
            //move vertically
            MoveVertical(Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),path);
            //MoveNPCpath(new Vector3Int(0,Mathf.RoundToInt(path[1]/Mathf.Abs(path[1])),0),path);
        }
    }

    
    

//-------------------------------------------------

//AI functions-(PathFinding)-------------------------------------

    
    public Vector3 ChooseRandomSpotInRoom(Room room,int numberOfSpacesToMove = 3)
    {

        int x;
        int y;

        

        x = (Random.Range(0,numberOfSpacesToMove));
        y = (numberOfSpacesToMove - x);

        x *= Random.Range(0,2) == 0 ? -1 : 1;
        y *= Random.Range(0,2) == 0 ? -1 : 1;
        
        Vector3 destination = movePoint.transform.position + new Vector3 (x,y,0f);

        if (!movePoint.isInRoom(room,new Vector3 (x,y,0f))||!MovePoint.ReserveSpot(destination)||!movePoint.CanMoveThere(new Vector3 (x,y,0f))) {
            return movePoint.transform.position; //failed
        }

        if (debugLogs) Debug.Log(this.gameObject.name + " chose spot " + destination);

        return destination;
    }


    public GameObject ChooseRandomNewAdjacentPathPoint()
    {
        if (whereImAt==null) {return null;}
        if(whereImAt.TryGetComponent<PathPoint>(out PathPoint currentPoint)) {

            int numberOfAdjacentPoints = currentPoint.adjacentPoints.Count;

            int index = Random.Range(0, numberOfAdjacentPoints);

            return currentPoint.adjacentPoints[index];
        
        }
        return null;
    }

    public List<GameObject> FindPathToDestination(GameObject A, GameObject B)
    {

        List<GameObject> path = new List<GameObject>();
        path.Add(B);

        
        while(A!=B) {
            B = SearchAdjacentPoints(A,B);
            if (B==null) {return null;}
            path.Add(B);
        }

        path.Reverse();
        return path;
            
        
    }

    public GameObject SearchAdjacentPoints(GameObject currentPoint, GameObject targetPoint, HashSet<GameObject> visited = null)
    {
        if (visited == null) visited = new HashSet<GameObject>();
        if (visited.Contains(currentPoint)) return null;
        visited.Add(currentPoint);

        if (currentPoint.TryGetComponent<PathPoint>(out PathPoint currentPathPoint)) {
            for (int i = 0; i < currentPathPoint.adjacentPoints.Count; i++) {
                var neighbor = currentPathPoint.adjacentPoints[i];
                if (neighbor == targetPoint) {
                    return currentPoint;
                } else {
                    GameObject dum = SearchAdjacentPoints(neighbor, targetPoint, visited);
                    if (dum != null) return dum;
                }
            }
        }
        return null;
    }

//-------------------------------------------------

//public class implimentations------------------------------
    #region Iinteractable interface
    public string Interact(int interactType)
    {
        if (interactType == 1) { //To DO: change to enum at one point 

            //trigger EnterDialogue Event
            EventManager.Instance.EnterDialogue(); //triggers OnEnterDialogue
            EventManager.Instance.StartDialogue(name);

        }
        return this.name;
    }
    #endregion

    #region OnEnterDialogue/OnExitDialogue
    void OnEnterDialogue()
    {
        stateMachine.ChangeState(Dialogue); //puts all npc in dialogue mode, freezing them and making them interact with the Dialogue object
    }

    void OnExitDialogue()
    {
        stateMachine.ChangeState(stateMachine.previousState); //returning npcs to whatever state they were last in 
    }
    #endregion
//---------------------------------------------------------------
}
