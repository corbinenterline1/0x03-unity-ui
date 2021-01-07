using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Floating point variable to store the player's movement speed.
    /// </summary>
    public float speed = 20;

    private int score = 0;

    private bool isDead;

    /// <summary>
    /// Player health.
    /// </summary>
    public int health = 5;
    AudioSource wallSource;
    AudioSource coinSource;
    AudioSource trapSource;
    AudioSource goalSource;
    AudioSource deathSource;
    AudioSource noSource;

    /// <summary>
    /// Drag for player
    /// </summary>
    public float dragFactor = 0.98f;

    private Rigidbody playerRB;

    // Start is called before the first frame update.
    void Start()
    {
        // Get & store a reference to the Rigidody component so that we an access it.
        playerRB = GetComponent<Rigidbody>();
        // Links audiosource variable to audio source component.
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        wallSource = allMyAudioSources[0];
        trapSource = allMyAudioSources[1];
        coinSource = allMyAudioSources[2];
        goalSource = allMyAudioSources[3];
        deathSource = allMyAudioSources[4];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            wallSource.Play();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score++;
            Debug.Log(string.Format("Score: {0}", score));
            Destroy(other.gameObject);
            coinSource.Play();
        }
        else if (other.tag == "Trap")
        {
            health--;
            Debug.Log(string.Format("Health: {0}", health));
            if (health > 0)
            {
                trapSource.Play();
            }
        }
        else if (other.tag == "Goal")
        {
            Debug.Log(string.Format("You win!"));
            goalSource.Play();
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            CharacterController cc = GetComponent<CharacterController>();
            cc.enabled = false;
        }
        if (!isDead)
        {
            // Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis ("Horizontal");

            // Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis ("Vertical");

            // Use the two store floats to create a new Vector 3variable movement.
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            playerRB.velocity *= dragFactor;

            // Call the AddForce function of our Rigidbody playerRB supplying movement multiplied by speed to move our player.
            playerRB.AddForce (movement * speed);
        }
        if (health <= 0)
        {
            Debug.Log(string.Format("Game Over!"));
            isDead = true;
            deathSource.Play();
            GameObject objectToDisappear = GameObject.Find("Player");
            objectToDisappear.GetComponent<Renderer>().enabled = false;
            StartCoroutine(WaitForSceneLoad());
            health = 5;
            score = 0;
        }
    }
}
