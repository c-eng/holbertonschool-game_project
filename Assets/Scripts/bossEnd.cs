using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossEnd : MonoBehaviour
{
    public EndMenu gameOver;

    public void GameOver()
    {
        gameOver.Win();
    }
}
