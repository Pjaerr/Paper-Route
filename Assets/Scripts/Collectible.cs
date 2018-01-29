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


    int index;

    void Start()

    {
        if (type == Type.key)
        {
            index = Random.Range(0, GameManager.singleton.KeySpawn.Length);
            GetComponent<Transform>().position = GameManager.singleton.KeySpawn[index].position;
        }
    }


    public void pickUp()
    {
        switch (type)
        {
            case Type.key:
                {
                    GameManager.singleton.playerHasAtticKey = true;
                    GameManager.singleton.DialogueMessage.HasKey();
                    break;
                }
            case Type.walkieTalkie:
                GameManager.singleton.playerHasWalkieTalkie = true;
                break;
        }

        Destroy(this.gameObject);
    }
}
