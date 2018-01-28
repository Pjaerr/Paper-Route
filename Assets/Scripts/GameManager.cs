using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static public GameManager singleton;

    //References
    [SerializeField] private Camera mainCam; //Reference to the main camera.

    //Player Stuff
    public Transform playerTransform; //Reference to the players transform.

    //States
    [HideInInspector] public bool playerIsHidden = false;
    [HideInInspector] public bool playerHasAtticKey = false;
    [HideInInspector] public bool playerAtAtticDoor = false;
    [HideInInspector] public bool playerHasWalkieTalkie = false;
    [HideInInspector] public bool grannyIsChasing = false;

    //Room stuff
    [HideInInspector] public int playerRoomId = 0;
    [HideInInspector] public int grannyRoomId = 1;
    public Transform[] KeySpawn;


    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(this);
        }

        playerRoomId = 0;
        grannyRoomId = 1;
    }

    public void moveCameraTo(Vector3 pos)
    {
        mainCam.transform.position = pos;
    }

    //True if both room ids match.
    public bool isInSameRoomAsPlayer()
    {
        return (grannyRoomId == playerRoomId);
    }

    //Allows other objects to rotate to face the player without access to the player reference.
    public void lookAtPlayer(Transform thisTransform, float rotationSpeed)
    {
        thisTransform.LookAt(playerTransform);
    }


    //Should be called when attic is reached.
    public void gameEndSucceed()
    {
        Debug.Log("Player has reached the attic.");

        //DO END SCENE STUFF HERE.

        SceneManager.LoadScene(2);
    }

    public void gameEndFail()
    {
        Debug.Log("Player has been caught");
        SceneManager.LoadScene(1);
    }
}
