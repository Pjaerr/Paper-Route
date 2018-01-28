﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

    public GameObject Panel;
    public GameObject Walkie;
    public GameObject DialogueMessage;
    private int i = 0;
    private bool PanelActive = true;


    List<string> chatEvents;

    // Use this for initialization
    void Start()
    {
        chatEvents = new List<string>();

        chatEvents.Add("Hey! Wake up! I need your help!");
        chatEvents.Add("Find the walkie talkie in the room!");
        chatEvents.Add("Good! Now help me escape and I'll show you the way out!");
        chatEvents.Add("I'm locked in the attic, find it and let me out!");
        chatEvents.Add("The crazy old granny locked me in here, don't let her see you!");
        chatEvents.Add("Damn! It looks like you need a key to let me out, find it quick!");
        chatEvents.Add("Great you found the key! Come let me free!");

        gameObject.SetActive(true);
        DialogueMessage.GetComponent<Text>().text = "*Incoming Transmission*";
    }

    public void HasKey()
    {
        Panel.SetActive(true);
        DialogueMessage.GetComponent<Text>().text = chatEvents[6];
    }

    void ClosePanel()
    {
        Panel.SetActive(false);
        PanelActive = false;
    }
    void Alert()
    {

        Panel.SetActive(true);
        DialogueMessage.GetComponent<Text>().text = "*Incoming Transmission*";
    }

    public void AtticDoor()
    {
        if (GameManager.singleton.playerHasAtticKey == false) //player collision at attic door trigger && bool haskey = false
        {
            Panel.SetActive(true);
            DialogueMessage.GetComponent<Text>().text = chatEvents[5];
        }

    }

    void NextText()
    {
        if (PanelActive == true)
        {
            DialogueMessage.GetComponent<Text>().text = chatEvents[i];
            if ((i == 1 && GameManager.singleton.playerHasWalkieTalkie) || i < 1 || i >= 2)
            {
                i++;
            }
        }
        if (GameManager.singleton.playerHasAtticKey)
        {
            DialogueMessage.GetComponent<Text>().text = chatEvents[6];

        }
        if (i == 6)
        {
            ClosePanel();
        }

    }
    public void WomanNearby()
    {
        Panel.SetActive(true);
        DialogueMessage.GetComponent<Text>().text = "She's Nearby! Hide! (Press E to hide in a object)";

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            NextText();
        }


    }


}