using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class enemy : MonoBehaviour
{
    private int hp;
    public int maxHP = 5;
    private bool busy = false;
    private Animator eAnim;
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform[] waypoint;
    private Vector3[] wayposition;
    private int nextpoint = 0;
    private int waypointLen;
    public float detectRad;
    public float retreatRad;
    public float leashRad;
    private Vector3 origin;
    private bool retreat = false;
    private bool pursue = false;
    public Transform player;
    public Collider hitBox;
    public GameObject hurtbox;
    private bool dead = false;
    private float deadTimer;

    void Start()
    {
        hp = maxHP;
        eAnim = GetComponent<Transform>().Find("avatar").GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        origin = transform.position;
        waypointLen = waypoint.Length;
        Array.Resize(ref wayposition, waypointLen);
        for (int i = 0; i < waypointLen; i++)
            wayposition[i] = waypoint[i].position;
    }

    void Update()
    {
        StillBusy();
        StillMoving();
        if (!busy)
            Moveyou();
        if (pursue)
            AttackScan();
        if (dead)
            Dead();
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
        if (atkTrigger)
            eAnim.SetBool("attack", true);
        else
            eAnim.SetBool("attack", false);
    }

    //Moves enemy
    void Moveyou()
    {
        Scanning();
        if (retreat)
            Retreat();
        else if (pursue)
            Pursue();
        else
            Patrol();
    }

    //Detects position and sets movement flags
    void Scanning()
    {
        if (Vector3.Distance(origin, transform.position) > retreatRad)
            retreat = true;
        if (!retreat)
        {
            if (Vector3.Distance(player.position, transform.position) < detectRad)
                pursue = true;
        }
        else
            pursue = false;
    }

    //Patrols Enemy
    void Patrol()
    {
        if (!(agent.hasPath || agent.pathPending))
        {
            if (nextpoint < waypointLen - 1)
                nextpoint += 1;
            else
                nextpoint = 0;
            agent.SetDestination(wayposition[nextpoint]);
        }
    }

    //Pursue logic (?)
    void Pursue()
    {
        if (Vector3.Distance(player.position, transform.position) > leashRad)
            retreat = true;
        else
            agent.SetDestination(player.position);
    }

    //Retreat logic
    void Retreat()
    {
        if (!(agent.destination.Equals(origin)))
        {
            agent.SetDestination(origin);
        }
        if (!(agent.hasPath || agent.pathPending))
            retreat = false;
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
        else
            eAnim.SetTrigger("damage");
    }

    //Death state
    void Die()
    {
        eAnim.SetTrigger("dead");
        dead = true;
        deadTimer = 0f;
        hurtbox.SetActive(false);
    }

    void Dead()
    {
        deadTimer += Time.deltaTime;
        if (deadTimer > 10f)
        {
            Respawn();
        }
    }

    //Respawn logic
    void Respawn()
    {
        hp = maxHP;
        busy = false;
        retreat = false;
        pursue = false;
        dead = false;
        transform.position = origin;
        nextpoint = 0;
        deadTimer = 0f;
        agent.ResetPath();
        eAnim.SetTrigger("respawn");
        hurtbox.SetActive(true);
    }
}
