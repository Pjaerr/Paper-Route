using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int roomCount = 0;
    public Room[] rooms = new Room[6];

    public Dialogue DialogueMessage;
    static public GameManager singleton;

    //References
    [SerializeField] private GameObject PauseMenuPanel; //Reference to the panel for pause.
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
    [HideInInspector] public int grannyRoomId = 2;
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
        grannyRoomId = 2;
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


    public bool playerDanger = false;
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].roomId == playerRoomId)
            {
                for (int j = 0; j < rooms[i].adjacentRoomIds.Length; j++)
                {
                    if (grannyRoomId == rooms[i].adjacentRoomIds[j])
                    {
                        if (!playerDanger)
                        {

                            playerDanger = true;
                            DialogueMessage.WomanNearby();
                        }
                    }
                    else
                    {
                        playerDanger = false;
                    }
                }
            }
        }

        if (playerHasAtticKey && playerAtAtticDoor)
        {
            gameEndSucceed();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0;

        }
    }
    //Should be called when attic is reached.
    public void gameEndSucceed()
    {
        Debug.Log("Player has reached the attic.");

        //DO END SCENE STUFF HERE.

        SceneManager.LoadScene(3);
    }

    public void gameEndFail()
    {
        Debug.Log("Player has been caught");
        SceneManager.LoadScene(2);
    }
}
