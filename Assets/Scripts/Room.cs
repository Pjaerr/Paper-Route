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
        points = new Transform[trans.childCount - 1];

        for (int i = 0; i < trans.childCount; i++)
        {
            if (!(trans.GetChild(i).tag == "RoomTrigger"))
            {
                points[i] = trans.GetChild(i);
            }
        }
    }
}
