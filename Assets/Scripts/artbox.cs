using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artbox : MonoBehaviour
{
    private Transform[] images;

    private int index = 0;

    void Start()
    {
        images = new Transform[transform.GetChild(0).childCount];

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            images[i] = transform.GetChild(0).GetChild(i);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) && index < images.Length - 1)
        {
            images[index].gameObject.SetActive(false);
            index++;
            images[index].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && index > 0)
        {
            images[index].gameObject.SetActive(false);
            index--;
            images[index].gameObject.SetActive(true);
        }
    }
}
