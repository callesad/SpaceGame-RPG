using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_motherboard : MonoBehaviour
{

    public List<NPCPerson> NPCList = new List<NPCPerson>();

    public void AllWalkRandomly()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].walkRandomly = true;
        }
    }

    public void AllStopWalkRandomly()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].walkRandomly = false;
        }
    }

    public void AllMeander()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].meander = true;
        }
    }

    public void AllStopMeandering()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].meander = false;
        }
    }

    public void AllDebugLogsOn()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].debugLogs = true;
        }
    }

    public void AllDebugLogsOff()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].debugLogs = false;
        }
    }

    public void AllGoHome()
    {
        for (int i = 0 ; i < NPCList.Count ; i++) {
            NPCList[i].whereIWantToBe = NPCList[i].whereILive;
        }
    }

    [ContextMenu("All Walk Randomly")]
    private void CallAllWalkRandomly()
    {
        AllWalkRandomly();
    }

    [ContextMenu("All Stop Walking Randomly")]
    private void CallAllStopWalkRandomly()
    {
        AllStopWalkRandomly();
    }

    [ContextMenu("All Meander")]
    private void CallAllMeander()
    {
        AllMeander();
    }

    [ContextMenu("All Stop Meandering")]
    private void CallAllStopMeandering()
    {
        AllStopMeandering();
    }

    [ContextMenu("All DebugLogs On")]
    private void CallAllDebugLogsOn()
    {
        AllDebugLogsOn();
    }

    [ContextMenu("All DebugLogs Off")]
    private void CallAllDebugLogsOff()
    {
        AllDebugLogsOff();
    }

    [ContextMenu("All Go Home")]
    private void CallAllGoHome()
    {
        AllGoHome();
    }
}
