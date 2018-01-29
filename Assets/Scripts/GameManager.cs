using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static public GameManager singleton;

    /*References*/
    [SerializeField] private Camera mainCam; //Reference to the main camera.

    //Audio
    [SerializeField] private AudioSource normalAudio;
    [SerializeField] private AudioSource chasingAudio;
    [SerializeField] private AudioSource sadAudio;

    //UI
    [SerializeField] private GameObject pauseMenu;
    public Dialogue DialogueMessage;
    private bool messageActive = false; //Is the dialogue box open?
    public GameObject walkieTalkieImage;

    /*Player*/
    public Transform playerTransform; //Reference to the players transform.
    public Room currentPlayerRoom;
    [HideInInspector] public bool playerIsHidden = false;
    [HideInInspector] public bool playerHasAtticKey = false;
    [HideInInspector] public bool playerAtAtticDoor = false;
    [HideInInspector] public bool playerHasWalkieTalkie = false;

    /*Granny*/
    public Room currentGrannyRoom;
    [HideInInspector] public bool grannyIsChasing = false;

    /*Collectibles*/
    public Transform[] KeySpawn;


    /*End Scene Stuff*/
    public Transform atticCameraPoint;

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
    }

    public void moveCameraTo(Vector3 pos)
    {
        mainCam.transform.position = pos;
    }

    //True if both room ids match.
    public bool isInSameRoomAsPlayer()
    {
        return (currentGrannyRoom.roomId == currentPlayerRoom.roomId);
    }

    //Allows other objects to rotate to face the player without access to the player reference.
    public void lookAtPlayer(Transform thisTransform, float rotationSpeed)
    {
        thisTransform.LookAt(playerTransform);
    }

    bool chaseAudioActive = false;

    void Update()
    {
        if (isEndScene)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, atticCameraPoint.position, 0.5f * Time.deltaTime);
        }

        if (grannyIsChasing && !chaseAudioActive)
        {
            chasingAudio.Play();
            normalAudio.Stop();
            chaseAudioActive = true;
        }
        else if (chaseAudioActive && !grannyIsChasing)
        {
            normalAudio.Play();
            chasingAudio.Stop();
            chaseAudioActive = false;
        }

        /*Loop through all adjacencies on the player's current room and if the room the
        granny is currently in matches any of those rooms, she is nearby.*/
        for (int i = 0; i < currentPlayerRoom.adjacentRoomIds.Length; i++)
        {
            if (currentGrannyRoom.roomId == currentPlayerRoom.adjacentRoomIds[i])
            {
                //If the dialogue box isn't open already
                if (!messageActive)
                {
                    messageActive = true; //Tell other code it is open.
                    DialogueMessage.WomanNearby(); //Trigger the granny nearby dialogue message.
                }
            }
            else if (messageActive) //If dialogue box is open but the granny isn't near.
            {
                messageActive = false; //Tell other code the box is closed.
                DialogueMessage.WomanNoLongerNearby(); //Close the dialogue box.
            }
        }

        if (playerHasAtticKey && playerAtAtticDoor)
        {
            gameEndSucceed();
            playerHasAtticKey = false;
        }

        //PAUSE MENU

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf); //Reverse pause menu's enabled/disabled state.

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }
    }

    bool isEndScene = false;
    //Should be called when attic is reached.
    public void gameEndSucceed()
    {
        normalAudio.Stop();
        chasingAudio.Stop();
        sadAudio.Play();
        isEndScene = true;
        walkieTalkieImage.SetActive(false);



        playerTransform.position = new Vector3(0, 200, 0);
        StartCoroutine(exitGame());
    }

    public IEnumerator exitGame()
    {
        yield return new WaitForSeconds(14);
        SceneManager.LoadScene(3);
    }

    public void gameEndFail()
    {
        Debug.Log("Player has been caught");
        SceneManager.LoadScene(2);
    }
}
