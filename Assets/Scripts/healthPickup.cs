using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public int heal;
    public AudioSource sfx;
    public AudioClip sound;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessageUpwards("HealDamage", heal);
            sfx.PlayOneShot(sound);
            gameObject.SetActive(false);
        }
        //other.SendMessageUpwards("TakeDamage", damage); EquipWeapon(string equip) or EquipTool(string equip)
        //destroy self
    }
}
