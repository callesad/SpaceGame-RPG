using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars1Paralax : MonoBehaviour
{
    public GameObject player;            // Reference to the player object
    public float parallaxFactor = 0.5f;  // The speed factor (less than 1 for slower, greater than 1 for faster)

    private Vector3 lastPlayerPosition;  // To store the player's previous position

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the previous position of the player
        lastPlayerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate how much the player has moved in this frame
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;

        // Move the background based on the player's movement but slower using parallaxFactor
        transform.position += playerMovement * parallaxFactor;

        // Update last player position for the next frame
        lastPlayerPosition = player.transform.position;
    }
}