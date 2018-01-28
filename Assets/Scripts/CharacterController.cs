using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Scene Control
    [SerializeField] private bool isMainScene = true;
    [SerializeField] private UI uiReference;


    //Attributes
    [SerializeField] private int speed = 5;
    private bool isAbleToHide = false; //Set to true if touching a "Hideable" object.

    //References
    private Rigidbody rb; //This player's rigidbody.
    private Transform trans; //This player's transform.
    private Animator playerAnimator; //This player's animator
    private Transform model; //The object that holds the model of the boy. (Child of player).
    [SerializeField] private SkinnedMeshRenderer meshRenderer; //Disabling hides the model.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        model = trans.GetChild(0);
    }

    void Update()
    {
        if (isMainScene)
        {
            /*If the player is colliding with a hideable object and are pressing E */
            if (isAbleToHide && Input.GetKeyDown(KeyCode.E))
            {
                hidePlayer(); //Hide them.
            }
            /*Else, if they are hidden and pressing E, unhide them*/
            else if (GameManager.singleton.playerIsHidden && Input.GetKeyDown(KeyCode.E))
            {
                hidePlayer(); //Unhide them.
            }
            //If the player isn't hidden, allow them to move.
            if (!GameManager.singleton.playerIsHidden)
            {
                meshRenderer.enabled = true;

                playerMovement();
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }
        else
        {
            playerMovement();
        }
    }


    void playerMovement()
    {
        //Getting axis
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float Animspeed = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");
        if ((Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == -1) || (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 1))
            Animspeed = 1;

        playerAnimator.SetFloat("Speed", Animspeed);
        //Rotation of model
        if (Input.GetAxis("Horizontal") < 0)
        {
            model.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            model.transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            model.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            model.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
            model.transform.eulerAngles = new Vector3(0, 45, 0);
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0)
            model.transform.eulerAngles = new Vector3(0, 135, 0);
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
            model.transform.eulerAngles = new Vector3(0, -45, 0);
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0)
            model.transform.eulerAngles = new Vector3(0, -135, 0);

        //Moving the player
        transform.Translate(x, 0, z);
    }
    void hidePlayer()
    {
        GameManager.singleton.playerIsHidden = !GameManager.singleton.playerIsHidden;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hideable")
        {
            isAbleToHide = true;
        }

        if (col.gameObject.tag == "AtticDoor")
        {
            GameManager.singleton.playerAtAtticDoor = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Hideable")
        {
            isAbleToHide = false;
        }

        if (col.gameObject.tag == "AtticDoor")
        {
            GameManager.singleton.playerAtAtticDoor = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Room")
        {
            GameManager.singleton.moveCameraTo(col.gameObject.GetComponent<Room>().cameraPoint.position);
            GameManager.singleton.playerRoomId = col.gameObject.GetComponent<Room>().roomId;
        }

        /*If player walks over a key, call its pickup function which sets playerHasAtticKey to true
        and destroys the key object.*/
        if (col.gameObject.tag == "Key")
        {
            col.gameObject.GetComponent<Collectible>().pickUp();
        }

        if (col.gameObject.tag == "GetGrabbedTrigger")
        {
            uiReference.grabbedIntoHouse();
        }
    }
}
