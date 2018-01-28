using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void loadArtbook()
    {
        SceneManager.LoadScene(4);
    }

    public void loadEndScene()
    {
        SceneManager.LoadScene(3);
    }
}
