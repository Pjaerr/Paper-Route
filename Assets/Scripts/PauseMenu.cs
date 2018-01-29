using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}

