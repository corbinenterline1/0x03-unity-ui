using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Private variable to store the offset distance between the player & camera.
    private Vector3 offset;
    /// <summary>
    /// Public variable to store a reference to the player game object.
    /// </summary>
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // Calculate & store the offset value by getting the distance between the player's position & camera's position.
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
