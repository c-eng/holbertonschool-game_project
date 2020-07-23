using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject over;
    public GameObject win;
    public GameObject lose;
    public AudioSource winSFX;
    public AudioSource loseSFX;
    public AudioSource bgm;
    public AudioSource bossBGM;

    public void Win()
    {
        Time.timeScale = 0f;
        over.SetActive(true);
        win.SetActive(true);
        bgm.Stop();
        bossBGM.Stop();
        winSFX.Play();
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        over.SetActive(true);
        lose.SetActive(true);
        bgm.Stop();
        bossBGM.Stop();
        loseSFX.Play();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
