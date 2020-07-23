using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 displace;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        displace = origin;
        displace.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.25f;
        transform.position = displace;
    }
}
