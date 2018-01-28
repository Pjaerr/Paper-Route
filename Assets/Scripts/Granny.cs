using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour
{
    //Attributes
    [SerializeField]
    private int speed = 5;

    //References
    private Transform trans; //This object's transform.
    private System.Random rand;

    //"AI" Stuff
    [SerializeField] private Room initialRoom;
    private Transform[] points; //The points (x, y, z) the object will choose from randomly.
    private Vector3 nextPosition; //Where the randomly chosen point is stored until next generated.
    private bool hasReachedPoint = true;

    private bool routeIsClear = false;

    void Start()
    {
        //Cache the references.
        trans = GetComponent<Transform>();
        rand = new System.Random();
        points = initialRoom.points;
    }

    void Update()
    {
        if (points.Length > 0)
        {
            moveToNextPoint();
        }
    }

    void FixedUpdate()
    {
        if (points.Length > 0)
        {
            trans.LookAt(nextPosition);
            if (isRouteClear())
            {
                routeIsClear = true;
            }
            else
            {
                routeIsClear = false;
            }
        }

    }

    /*When raycasting to check if objects exist, if it hits something, go again if the thing it is hitting is 
	tagged as LevelObject or Wall.*/
    Ray ray;

    bool isRouteClear()
    {
        RaycastHit hit;
        ray = new Ray(trans.position, trans.forward);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 500.0f))
        {
            if (hit.collider.tag == "LevelObject" || hit.collider.tag == "Wall")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        Debug.DrawRay(ray.origin, ray.direction);

        return true;
    }


    void chooseRandomPosition()
    {
        //Generate a number between 0 and the number of elements within the points[] array.
        int index = rand.Next(0, points.Length);

        //Set the next position to be a position within points[] at that randomly generated number.
        nextPosition.x = points[index].position.x;
        nextPosition.z = points[index].position.z;
        nextPosition.y = 0;
    }

    /**Chooses a point randomly from the points[] array, when chosen set the (x, y, z) as a Vector3
    inside of nextPosition and then every frame use Vector3.movetowards() to move this object towards the 
    previously generated position and whilst they aren't at that point, set hasReachedPoint to false, and when
    they reach the point, set it to true. If hasReachedPoint is true, the cycle goes around again.*/
    void moveToNextPoint()
    {
        float step = speed * Time.deltaTime; //Frame rate independent movement.

        if (!GameManager.singleton.isInSameRoomAsPlayer() || GameManager.singleton.playerIsHidden)
        {
            GameManager.singleton.grannyIsChasing = false;

            if (trans.position == nextPosition) //This object is at the generated position.
            {
                hasReachedPoint = true;
            }
            else //This object is not at the generated position.
            {
                hasReachedPoint = false;
            }

            if (hasReachedPoint)
            {
                chooseRandomPosition();

                while (!routeIsClear)
                {
                    chooseRandomPosition();
                }
            }

            //Move this object towards the chosen position over time.
            trans.position = Vector3.MoveTowards(trans.position, nextPosition, step);
        }
        else
        {
            GameManager.singleton.grannyIsChasing = true;
            GameManager.singleton.lookAtPlayer(trans, 10.0f);
            trans.position = Vector3.MoveTowards(trans.position, GameManager.singleton.playerTransform.position, step);
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "RoomTrigger") //If the granny has walked through a trigger on the room's entrance
        {
            int chance = rand.Next(0, 2);

            if (chance == 1)
            {
                points = col.transform.parent.GetComponent<Room>().points; //Set the granny's points to the rooms points.
                chooseRandomPosition();     //COULD MESS WITH RAYCASTING IF WE DECIDE TO USE IT.
                GameManager.singleton.grannyRoomId = col.transform.parent.GetComponent<Room>().roomId;
            }
        }

        if (col.gameObject.tag == "Player")
        {
            GameManager.singleton.gameEndFail();
        }
    }
}