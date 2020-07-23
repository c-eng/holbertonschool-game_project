using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeControl : MonoBehaviour
{
    public player player;
    public Transform target;
    public GameObject to;
    public GameObject from;
    private Animator fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<Animator>();
    }

    public void FadeOut(Transform tgt, GameObject t, GameObject f)
    {
        player.ctrlLock = true;
        fade.SetTrigger("out");
        target = tgt;
        to = t;
        from = f;
        to.SetActive(true);
    }

    public void FadeIn()
    {
        player.start = target.position;
        player.transform.position = target.position;
        fade.SetTrigger("in");
    }

    public void FadeClean()
    {
        from.SetActive(false);
        player.ctrlLock = false;
    }
}
