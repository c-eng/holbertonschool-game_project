using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossActivate : MonoBehaviour
{
    public Transform player;
    public float radius;
    public boss script;
    public AudioSource bgm;
    public AudioSource bossBGM;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.position) < radius)
        {
            script.enabled = true;
            bgm.Stop();
            bossBGM.Play();
            this.enabled = false;
        }
    }
}
