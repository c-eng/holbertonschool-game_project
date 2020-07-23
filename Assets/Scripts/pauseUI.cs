using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseUI : MonoBehaviour
{
    public player player;
    public GameObject pause;
    public Text atkType;
    public Text atkValue;
    public Text toolType;
    public Text hpValue; //This is a string "hp / maxHp"

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            {
                Resume();
            }
    }

    void OnEnable()
    {
        //Switch statement on player.weapEquip, player.toolEquip, player.hp, player.maxHP
        atkType.text = player.weapEquip;
        toolType.text = player.toolEquip;
        hpValue.text = player.hp.ToString() + " / " + player.maxHP.ToString();
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
