using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("Level01", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
