using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    public boss script;
    public Collider hitBox1;
    public Collider hitBox2;
    public float hitRad1;
    public float hitRad2;
    public int punchDamage;
    public int slamDamage;

    void HitScan1()
    {
        script.attackCounter += 1;
        Collider[] cols = Physics.OverlapSphere(hitBox1.bounds.center, hitRad1, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Player")
            {
                c.SendMessageUpwards("TakeDamage", punchDamage);
            }
        }
    }

    void HitScan2()
    {
        Collider[] cols = Physics.OverlapSphere(hitBox2.bounds.center, hitRad2, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Player")
                c.SendMessageUpwards("TakeDamage", slamDamage);
        }
    }
}
