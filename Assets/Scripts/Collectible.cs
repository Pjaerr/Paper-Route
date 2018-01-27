using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    enum Type
    {
        key,
        walkieTalkie
    }

    [SerializeField] private Type type;


    public void pickUp()
    {
        switch (type)
        {
            case Type.key:
                GameManager.singleton.playerHasAtticKey = true;
                break;

            case Type.walkieTalkie:
                GameManager.singleton.playerHasWalkieTalkie = true;
                break;
        }

        Destroy(this.gameObject);
    }
}
