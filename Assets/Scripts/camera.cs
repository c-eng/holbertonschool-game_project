using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    ///<summary>Player to follow.</summary>
    public GameObject player;
    ///<summary>Offset from player object.</summary>
    private Vector3 offset = new Vector3(0.0f, 15.0f, -5.0f);

    ///<summary>Update function.</summary>
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
