using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playUI : MonoBehaviour
{
    public Transform iconTargets;
    public Image heartEmpty;
    public Image heartFull;
    public Image weapIcon;
    public Image toolIcon;
    public Image sword;
    public Image wand;
    public Image boot;
    public Image empty;


    public void UpdateHealth(int hp, int maxHP)
    {
        int hpCount = 0;
        int targetCount = iconTargets.childCount;
        for (; hpCount < maxHP; hpCount++)
        {
            Image icon = iconTargets.GetChild(hpCount).GetComponent<Image>();
            if (hpCount < hp)
                icon.sprite = heartFull.sprite;
            else
                icon.sprite = heartEmpty.sprite;
        }
    }

    public void UpdateWeap(string weap)
    {
        switch (weap)
        {
            case "Sword":
                weapIcon.sprite = sword.sprite;
                break;
            case "Wand":
                weapIcon.sprite = wand.sprite;
                break;
            case "None":
                weapIcon.sprite = empty.sprite;
                break;
        }
    }

    public void UpdateTool(string tool)
    {
        switch (tool)
        {
            case "Dash":
                toolIcon.sprite = boot.sprite;
                break;
            case "Hook":
                break;
            case "None":
                toolIcon.sprite = empty.sprite;
                break;
        }
    }
}
