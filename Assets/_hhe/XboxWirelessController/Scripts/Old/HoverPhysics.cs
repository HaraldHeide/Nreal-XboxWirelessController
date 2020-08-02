/*
 * 
 * https://www.santoshnalla.com/post/unity-physics-simple-ufo-controls
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPhysics : MonoBehaviour
{
    RaycastHit _hit;
    Rigidbody _rigidBody;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HoverUpwardForce();
        TiltSidewaysTorque();
        TiltForwardTorque();
    }

    void HoverUpwardForce()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out _hit, 21.0f))
        {
            _rigidBody.AddForce(transform.up.normalized * (_rigidBody.mass * Physics.gravity.magnitude) * (20 / _hit.distance), ForceMode.Force);
        }
    }

    void TiltSidewaysTorque()
    {
        _rigidBody.AddRelativeTorque(transform.forward * -Input.GetAxis("Horizontal"), ForceMode.Acceleration);
    }


    void TiltForwardTorque()
    {
        _rigidBody.AddRelativeTorque(transform.right * Input.GetAxis("Vertical"), ForceMode.Acceleration);
    }
}