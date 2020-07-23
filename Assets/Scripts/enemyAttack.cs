using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public Collider hitBox;
    public int damage;

    void HitScan()
    {
        Collider[] cols = Physics.OverlapSphere(hitBox.bounds.center, 0.85f, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Player")
                c.SendMessageUpwards("TakeDamage", damage);
        }
    }
}
