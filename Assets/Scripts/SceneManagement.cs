using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void toGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitTheGame()
    {
        Application.Quit();
        Debug.Log("gameQuit");
    }


}
