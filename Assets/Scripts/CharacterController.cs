using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Attributes
    [SerializeField] private int speed = 5;


    //References
    private Rigidbody rb;

    private Transform trans;

    private Animator playerAnimator;

    public Transform model;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        //Setting Model as child
        model = this.gameObject.transform.GetChild(0);

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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Room")
        {
            GameManager.singleton.moveCameraTo(col.gameObject.GetComponent<Room>().cameraPoint.position);
        }
    }
}
