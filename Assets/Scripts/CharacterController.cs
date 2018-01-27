using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Attributes
    [SerializeField] private int speed = 1;


    //References
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
