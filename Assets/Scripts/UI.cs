using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    /*
		0: Main menu << Starts and is triggered via end scene.
		1: Start Scene << Triggered via main menu scene, and launches Game scene when GetGrabbedTrigger is entered
		2: Game << The game, launches ReachedAttic scene when you enter a trigger called Attic
		3: ReachedAttic << An image, button triggers End Scene
		4: End Scene << Triggers main menu, or artbook scene.
		5: ArtBook << stuck for life.
	*/
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

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void grabbedIntoHouse()
    {
        SceneManager.LoadScene(2);
    }
}
