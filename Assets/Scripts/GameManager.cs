using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager singleton;

    //References
    [SerializeField] private Camera mainCam;

    //Player States
    [HideInInspector] public bool playerIsHidden = false;
    [HideInInspector] public bool playerHasBasementKey = false;
    [HideInInspector] public bool playerHasAtticKey = false;

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
}
