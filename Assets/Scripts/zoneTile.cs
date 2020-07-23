using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneTile : MonoBehaviour
{
    public fadeControl fade;
    public Transform target;
    public GameObject to;
    public GameObject from;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //trigger transition script
            fade.FadeOut(target, to, from);
            //fix losing health on zoning (probably due to falling through the level and respawning)
            /*
            player player = other.gameObject.GetComponent<player>();
            player.busyFlag = true;
            dEnemy.SetActive(true);
            dInteract.SetActive(true);
            player.start = target.position;
            other.transform.position = target.position;
            oEnemy.SetActive(false);
            oInteract.SetActive(false);
            player.busyFlag = false;
            */
        }
    }

/*
    void FadeOut()
    {
        player.ctrlLock = true;
        fade.SetTrigger("out");
        to.SetActive(true);
    }

    public void FadeIn()
    {
        player.transform.position = target.position;
        fade.SetTrigger("in");
    }

    public void FadeClean()
    {
        from.SetActive(false);
        player.ctrlLock = false;
    }
    */
}
