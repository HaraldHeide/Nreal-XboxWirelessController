/*
 * 
 * 
 * https://www.youtube.com/watch?v=3R_V4gqTs_I&list=PLPAgqhxd1Ib1YYqYnZioGyrSUzOwead17
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    private Rigidbody rb;

    private float upForce;

    private float movementForwardSpeed = 500;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;

    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();

        rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * upForce);
        rb.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, rb.rotation.z));
    }

    private void MovementUpDown()
    {
        if (Input.GetKey(KeyCode.I))
        {
            upForce = 450f;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            upForce = -200f;
        }
        else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K))
        {
            upForce = 98.1f;
        }
    }
    private void MovementForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * Time.fixedDeltaTime * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.L))
        {
            wantedYRotation += rotateAmountByKeys;
        }
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }
}
