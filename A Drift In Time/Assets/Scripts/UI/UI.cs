using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    void Awake()
    {
        DeactivateAllChildren();
    }

    public void DeactivateAllChildren()
    {
        // Iterate through each child transform of the parent object
        foreach (Transform child in transform)
        {
            // Deactivate the GameObject associated with the child transform
            child.gameObject.SetActive(false);
        }
    }
}
