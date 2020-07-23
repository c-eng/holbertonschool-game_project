using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Control tightening measures:
>Change GetAxis values to -1, 0, 1 values for true 8 way directional control (for gamepad)
>Add a key buffer for storing attack/tool input (maybe)
*/

///<summary>General Class for majority of player control</summary>
public class player : MonoBehaviour
{
    private CharacterController controller;
    private Transform pc;
    public int speed;
    public float xMove;
    public float zMove;
    private bool attack;
    private bool tool;
    public bool busy = false;
    public bool ctrlLock = false;
    public int hp = 5;
    public int maxHP = 5;
    public string weapEquip = "Sword";
    public string toolEquip = "None";
    private Animator pcAnim;
    private Vector3 gravity = Vector3.down * 9.8f;
    public Vector3 start;
    private bool dash = false;
    private float dashTimer = 0.12f;
    private float dashTime = 0f;
    public playUI gooey;
    public pauseUI pause;
    public AudioSource sfx;
    public AudioClip dashSound;

    public Collider[] attackHitboxes;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        pc = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        pcAnim = pc.Find("avatar").GetComponent<Animator>();
    }

    //Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    void Start()
    {
        start = pc.position;
        gooey.UpdateHealth(hp, maxHP);
        gooey.UpdateWeap(weapEquip);
        gooey.UpdateTool(toolEquip);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            pause.Pause();
        StillBusy();
        if (!ctrlLock)
        {
            //For tighted controls use GetKey or GetButton for remappable keys.
            xMove = Input.GetAxisRaw("Horizontal");
            zMove = Input.GetAxisRaw("Vertical");
            //Change these to GetButtonDown
            attack = Input.GetButton("Attack");
            //attack = Input.GetKey(",");
            tool = Input.GetButton("Tool");
            //tool = Input.GetKey(".");
        }
        else
        {
            xMove = 0;
            zMove = 0;
            attack = false;
            tool = false;
        }
    }
    void FixedUpdate()
    {
        if (dash)
            Dash();
        if (pc.position.y < -2.0f)
        {
            pc.position = start;
            TakeDamage(1);
        }
        else
            Move();
        //Attack/tool activation here
        if (attack && !busy)
            Attack();
        else
            pcAnim.SetBool(weapEquip, false);
        if (tool && !busy)
            Tool();
        else
            pcAnim.SetBool(toolEquip, false);
    }

    //Player Move
    void Move()
    {
        Vector3 pcMove = new Vector3(xMove, 0, zMove);
        bool grounded = controller.isGrounded;
        if (pcMove != Vector3.zero)
        {
            pcAnim.SetBool("move", true);
            if (!busy)
            {
                Vector3 pcClamp = Vector3.ClampMagnitude(pcMove, 1f);
                if (grounded)
                    controller.Move(pcClamp * Time.fixedDeltaTime * speed);
                else
                    controller.Move(((pcClamp * speed) + gravity) * Time.fixedDeltaTime);
                transform.LookAt(transform.position + pcMove);
            }
            else
            {
                if (grounded)
                    controller.Move(Vector3.zero);
                else
                    controller.Move(gravity * Time.fixedDeltaTime);
            }
        }
        else
        {
            pcAnim.SetBool("move", false);
            if (!grounded)
                controller.Move(gravity * Time.fixedDeltaTime);
        }
    }

    //Player Attack
    void Attack()
    {
        Debug.Log("You Attack");
        //Figure out Attack handling
        //check weapon type (from gui)?
        //check weapEquip for weapon type, trigger appropriate animation
        switch (weapEquip)
        {
            case "Sword":
                Debug.Log("Sword");
                pcAnim.SetBool("Sword", true);
                break;
            case "Wand":
                Debug.Log("Wand");
                pcAnim.SetBool("Wand", true);
                break;
        }
    }

    //Player Tool
    void Tool()
    {
        Debug.Log("You Tool");
        //Figure out Tool handling
        //check toolEquip string for equipment type, trigger appripriate animation
        switch (toolEquip)
        {
            case "Dash":
                Debug.Log("Dash");
                sfx.PlayOneShot(dashSound);
                dash = true;
                break;
            case "Hook":
                Debug.Log("Hook");
                pcAnim.SetBool("hook", true);
                break;
        }
    }

    //Dash Tool (provisional)
    void Dash()
    {
        busy = true;
        if (dashTime > dashTimer)
        {
            gravity = Vector3.down * 9.8f;
            dash = false;
            pcAnim.SetBool("Dash", false);
            dashTime = 0f;
        }
        else
        {
            gravity = Vector3.zero;
            pcAnim.SetBool("Dash", true);
            controller.Move((pc.forward * (speed * 6)) * Time.fixedDeltaTime);
            dashTime += Time.fixedDeltaTime;
        }
    }

    //Get player HP value
    public int GetHP()
    {
        return (hp);
    }

    //Damage player
    public void TakeDamage(int damage)
    {
        if (damage < 0)
            damage = 0;
        int newHP = hp - damage;
        ValidateHP(newHP);
    }

    //Heal player
    public void HealDamage(int heal)
    {
        if (heal < 0)
            heal = 0;
        int newHP = hp + heal;
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
        gooey.UpdateHealth(hp, maxHP);
        CheckStatus();
    }

    //Checks player status
    void CheckStatus()
    {
        if (hp == 0)
            GameOver();
    }

    void GameOver()
    {
        Debug.Log("You Died.");
        pcAnim.SetTrigger("dead");
    }

    public void EquipWeapon(string equip)
    {
        weapEquip = equip;
        gooey.UpdateWeap(weapEquip);
    }

    public void EquipTool(string equip)
    {
        toolEquip = equip;
        gooey.UpdateTool(toolEquip);
    }

    //Determines busyness depending on animator state
    void StillBusy()
    {
        if (ctrlLock)
            busy = true;
        else if (pcAnim.GetCurrentAnimatorStateInfo(0).IsTag("busy"))
            busy = true;
        else if (pcAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            busy = true;
        else
            busy = false;
    }

    void HitScan(Collider atk)
    {
        Collider[] cols = Physics.OverlapSphere(atk.bounds.center, 2f, LayerMask.GetMask("Hurtbox"));
        foreach (Collider c in cols)
        {
            if (c.tag == "Enemy")
                c.SendMessageUpwards("TakeDamage", 2);
        }
    }
}
