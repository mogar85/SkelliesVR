using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    float speed = 10;
    [SerializeField]
    float runMultiplyer = 2;
    [SerializeField]
    float jumpForce = 10;
    [SerializeField]
    LayerMask mask;

    Rigidbody body;
    bool grounded = false;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundControl();
        Movement();
        Jump();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void GroundControl()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.3f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
            }
        }
    }

    private void Movement()
    {
        if (!grounded)
            return;
        float forwardMovement = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");
        Vector3 v3 = transform.forward * forwardMovement * speed;
        v3 += transform.right * strafe * speed;
        if (Input.GetKey(KeyCode.LeftShift))
            v3 *= runMultiplyer;

        v3 *= Time.deltaTime;
        v3.y = body.velocity.y;
        body.velocity = v3;
        //transform.position = transform.position + v3.normalized * Time.deltaTime;
        //body.AddForce(v3 * speed);
    }
}
