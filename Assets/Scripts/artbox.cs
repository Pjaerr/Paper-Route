using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artbox : MonoBehaviour
{
    private Transform[] images;

    private int index = 0;

    void Start()
    {
        images = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            images[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        if (index < images.Length)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                images[index].gameObject.SetActive(false);
                index++;
                images[index].gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                images[index].gameObject.SetActive(false);
                index--;
                images[index].gameObject.SetActive(true);
            }
        }

    }
}
