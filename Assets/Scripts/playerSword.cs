using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSword : MonoBehaviour
{
    public Collider hitBox;
    public EndMenu gameOver;

    void HitScan()
    {
        Collider[] cols = Physics.OverlapSphere(hitBox.bounds.center, 2f, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Enemy")
                c.SendMessageUpwards("TakeDamage", 2);
        }
    }

    void GameOver()
    {
        gameOver.Lose();
    }
}
