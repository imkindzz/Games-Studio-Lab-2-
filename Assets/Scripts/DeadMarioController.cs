using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeadMarioController : MonoBehaviour
{
    public float jumpHeight = 5f;
    public float speed = 5f;

    private bool reachedJumpHeight = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!reachedJumpHeight)
        {
            if (Vector3.Distance(initialPosition, transform.position) < jumpHeight)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }

            if (Vector3.Distance(initialPosition, transform.position) >= jumpHeight)
            {
                reachedJumpHeight = true;
            }
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
