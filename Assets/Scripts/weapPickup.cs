using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapPickup : MonoBehaviour
{
    public string type;
    public AudioSource sfx;
    public AudioClip sound;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessageUpwards("EquipWeapon", type);
            sfx.PlayOneShot(sound);
            gameObject.SetActive(false);
        }
        //other.SendMessageUpwards("TakeDamage", damage); EquipWeapon(string equip) or EquipTool(string equip)
        //destroy self
    }
}
