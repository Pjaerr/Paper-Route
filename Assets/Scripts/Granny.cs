using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour
{
    //Attributes
    [SerializeField] private int speed = 1;

    //References
    private Transform trans; //This object's transform.
    private System.Random rand;

    //"AI" Stuff
    [SerializeField] private Transform[] points; //The points (x, y, z) the object will choose from randomly.
    private Vector3 nextPosition; //Where the randomly chosen point is stored until next generated.
    private bool hasReachedPoint = true;

    void Start()
    {
        //Cache the references.
        trans = GetComponent<Transform>();
        rand = new System.Random();
    }

    void Update()
    {
        moveToNextPoint();
    }


    /*
		When raycasting to check if objects exist, if it hits something, go again if the thing it is hitting is 
		tagged as LevelObject or Wall. 
	*/

    bool isRouteClear()
    {
        if (Physics.Raycast(transform.position, nextPosition))
        {
            return false;
        }

        return true;
    }


    void chooseRandomPosition()
    {
        //Generate a number between 0 and the number of elements within the points[] array.
        int index = rand.Next(0, points.Length);

        //Set the next position to be a position within points[] at that randomly generated number.
        nextPosition = points[index].position;
    }

    /**Chooses a point randomly from the points[] array, when chosen set the (x, y, z) as a Vector3
inside of nextPosition and then every frame use Vector3.movetowards() to move this object towards the 
previously generated position and whilst they aren't at that point, set hasReachedPoint to false, and when
they reach the point, set it to true. If hasReachedPoint is true, the cycle goes around again.*/
    void moveToNextPoint()
    {
        float step = speed * Time.deltaTime; //Frame rate independent movement.

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

            while (!isRouteClear())
            {
                chooseRandomPosition();
            }
        }

        //Move this object towards the chosen position over time.
        trans.position = Vector3.MoveTowards(trans.position, nextPosition, step);
    }
}
