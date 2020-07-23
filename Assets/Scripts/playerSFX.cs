using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSFX : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip swing;
    // Start is called before the first frame update

    public void SwordSFX()
    {
        sfx.PlayOneShot(swing);
    }
}
