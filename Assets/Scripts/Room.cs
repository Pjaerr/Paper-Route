using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Transform[] points;
    private Transform trans;


    void Start()
    {
        trans = GetComponent<Transform>();
        storePoints();
    }


    /*Take each child of the room object and store their transforms in points[].*/
    void storePoints()
    {
        points = new Transform[trans.childCount];

        for (int i = 0; i < trans.childCount; i++)
        {
            points[i] = trans.GetChild(i);
        }
    }
}
