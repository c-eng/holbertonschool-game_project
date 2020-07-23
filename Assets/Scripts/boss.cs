using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class boss : MonoBehaviour
{
    private int hp;
    public int maxHP = 20;
    private bool busy = false;
    private Animator eAnim;
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public Collider hitBox;
    public GameObject hurtbox;
    public int attackCounter = 0;

    void Start()
    {
        hp = maxHP;
        eAnim = GetComponent<Transform>().Find("avatar").GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        StillBusy();
        StillMoving();
        if (!busy)
            Moveyou();
        AttackScan();
    }

    //Checks NavMeshAgent velocity and sets movement trigger
    void StillMoving()
    {
        if (agent.velocity.Equals(Vector3.zero))
            eAnim.SetBool("move", false);
        else
            eAnim.SetBool("move", true);
    }

    //Checks animation state for busyness
    void StillBusy()
    {
        if (eAnim.GetCurrentAnimatorStateInfo(0).IsTag("busy"))
            busy = true;
        else if (eAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            busy = true;
        else
            busy = false;
        if (busy == true)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
        else
            agent.isStopped = false;
    }

    //Scans for player in attack range
    void AttackScan()
    {
        bool atkTrigger = false;
        Collider[] cols = Physics.OverlapSphere(hitBox.bounds.center, 1.6f, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Player")
            {
                atkTrigger = true;
            }
        }
        //more logic here for attack types (punch vs slam) when should it punch vs when should it slam (punch punch slam) use attack counter
        if (atkTrigger)
        {
            if (attackCounter < 3)
            {
                eAnim.SetBool("attack1", true);
                eAnim.SetBool("attack2", false);
            }
            else
            {
                eAnim.SetBool("attack2", true);
                eAnim.SetBool("attack1", false);
                attackCounter = 0;
            }
        }
        else
        {
            eAnim.SetBool("attack1", false);
            eAnim.SetBool("attack2", false);
        }
    }

    //Moves enemy
    void Moveyou()
    {
        Pursue();
    }

    //Detects position and sets movement flags

    //Pursue logic (?)
    void Pursue()
    {
        agent.SetDestination(player.position);
    }

    //Damage enemy
    public void TakeDamage(int damage)
    {
        if (damage < 0)
            damage = 0;
        int newHP = hp - damage;
        ValidateHP(newHP);
    }

    //Validates HP changes
    void ValidateHP(int newHP)
    {
        if (newHP < 0)
            hp = 0;
        else if (newHP > maxHP)
            hp = maxHP;
        else
            hp = newHP;
        CheckStatus();
    }

    //Checks enemy status
    void CheckStatus()
    {
        if (hp == 0)
            Die();
    }

    //Death state
    void Die()
    {
        eAnim.SetTrigger("dead");
        hurtbox.SetActive(false);
        // you win (for now) or put this at the end of death animation
    }
}
