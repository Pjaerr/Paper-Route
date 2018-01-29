using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Transform[] points;
    private Transform trans;
    [HideInInspector] public Transform cameraPoint;

    public int roomId;

    public int[] adjacentRoomIds;

    void Awake()
    {
        trans = GetComponent<Transform>();
        cameraPoint = trans.GetChild(1);
        storePoints();
    }

    void Start()
    {
        GameManager.singleton.rooms[GameManager.singleton.roomCount] = this;
        GameManager.singleton.roomCount++;
    }


    /*Take each child of the room object and store their transforms in points[].*/
    void storePoints()
    {
        points = new Transform[trans.GetChild(0).childCount];

        for (int i = 0; i < trans.GetChild(0).childCount; i++)
        {
            points[i] = trans.GetChild(0).GetChild(i);
        }
    }
}
