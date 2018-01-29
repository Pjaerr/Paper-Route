using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*Dialogue was made by Benjamin Sparkes with input from Aaron Teleghani.*/

public class Dialogue : MonoBehaviour
{

    private Text DialogueMessageText;
    public GameObject Panel;
    public GameObject Walkie;
    public GameObject DialogueMessage;
    private int i = 0;
    private bool PanelActive = true;


    List<string> chatEvents;

    // Use this for initialization
    void Start()
    {
        DialogueMessageText = DialogueMessage.GetComponent<Text>();

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
        DialogueMessageText.text = chatEvents[6];
    }

    void ClosePanel()
    {
        Panel.SetActive(false);
        PanelActive = false;
    }
    void Alert()
    {

        Panel.SetActive(true);
        DialogueMessageText.text = "*Incoming Transmission*";
    }

    public void AtticDoor()
    {
        if (GameManager.singleton.playerHasAtticKey == false) //player collision at attic door trigger && bool haskey = false
        {
            Panel.SetActive(true);
            DialogueMessageText.text = chatEvents[5];
        }

    }

    void NextText()
    {
        if (PanelActive == true)
        {
            DialogueMessageText.text = chatEvents[i];
            if ((i == 1 && GameManager.singleton.playerHasWalkieTalkie) || i < 1 || i >= 2)
            {
                i++;
            }
        }
        if (GameManager.singleton.playerHasAtticKey)
        {
            DialogueMessageText.text = chatEvents[6];

        }
        if (i == 6)
        {
            ClosePanel();
        }

    }

    public void WomanNearby()
    {
        Panel.SetActive(true);
        DialogueMessageText.text = "She's Nearby! Hide! (Press E to hide in a object)";

    }

    public void WomanNoLongerNearby()
    {
        Panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextText();
        }


    }


}
