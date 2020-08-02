/*
 * https://www.udemy.com/course/complete-arcore-arkit-gaming-developer-augmented-reality/learn/lecture/9748162#overview
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementHelicopter : MonoBehaviour 
{
	Rigidbody helicopter;
	private float forceLiftUp;  // force for lift up helicopter

	//values to move helicopter forward
	public float movementForwardSpeed = 7;
	private float xValueHelicopter = 0;
	private float velocityForward;

	//values to rotate helicopter
	private float wantedYRotation;
	private float yValueHelicopter;
	private float rotateAmoutByKeys = 1.5f;
	private float yRotationVelocity;

	void Awake () 
	{
		helicopter = GetComponent<Rigidbody> ();
	}
	public void Update()
	{
	}

	void FixedUpdate()
	{
			LiftUpDownProcess ();
			MoveProcess ();
			RotationProcess ();
			MaxSpeedLimit ();
			helicopter.AddRelativeForce (Vector3.up * forceLiftUp); // Lift up/down
			helicopter.rotation = Quaternion.Euler (new Vector3(xValueHelicopter, yValueHelicopter, helicopter.rotation.z));  //rotate the helicopter
	}


	//methode for Liftup
	void LiftUpDownProcess()
	{
		if (Input.GetKey (KeyCode.I) || Input.GetButton ("Liftup")) 
		{
			forceLiftUp = 260;
		} 

		else if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K))
		{
			forceLiftUp = 190;
		}
	}

	//methode move forward
	void MoveProcess()
	{
		if (Input.GetAxis ("Vertical") !=0) 
		{
			helicopter.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical") * movementForwardSpeed);
			xValueHelicopter = Mathf.SmoothDamp (xValueHelicopter, 20 * Input.GetAxis ("Vertical"), ref velocityForward, 0.1f);
		}

		if (Input.GetAxis ("Vertical")!=0 || Input.GetAxis ("Horizontal") !=0 ) 
		{
			helicopter.AddRelativeForce (Vector3.forward * (Input.GetAxis ("Vertical")) * movementForwardSpeed);
			xValueHelicopter = Mathf.SmoothDamp (xValueHelicopter, 20 * (Input.GetAxis ("Vertical")), ref velocityForward, 0.1f);
		}
	}

	//methode rotateHelicopter
	void RotationProcess()
	{
		if (Input.GetKey (KeyCode.J) ) 
		{
			wantedYRotation -= rotateAmoutByKeys;
		}


		if (Input.GetAxis ("Horizontal") == 1f || Input.GetAxis ("Vertical") == -1f) 
		{
			wantedYRotation += rotateAmoutByKeys;
		}


		if (Input.GetKey (KeyCode.L))
		{
			wantedYRotation += rotateAmoutByKeys;	
		}

		if (Input.GetAxis ("Horizontal") == -1f || Input.GetAxis ("Vertical") == -1f) 
		{
			wantedYRotation -= rotateAmoutByKeys;
		}
		yValueHelicopter = Mathf.SmoothDamp (yValueHelicopter, wantedYRotation, ref yRotationVelocity, 0.15f);
	}


	// limiting speed
	private Vector3 velocityToSmoothDampToZero;
	void MaxSpeedLimit()
	{
		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) 
		{
			helicopter.velocity = Vector3.ClampMagnitude (helicopter.velocity, Mathf.Lerp (helicopter.velocity.magnitude, 10.0f, Time.deltaTime * 0.4f));
		}

		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.2f) 
		{
			helicopter.velocity = Vector3.ClampMagnitude (helicopter.velocity, Mathf.Lerp (helicopter.velocity.magnitude, 10.0f, Time.deltaTime * 0.4f));
		}

		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) 
		{
			helicopter.velocity = Vector3.ClampMagnitude (helicopter.velocity, Mathf.Lerp (helicopter.velocity.magnitude, 5.0f, Time.deltaTime * 0.4f));
		}

		if (Mathf.Abs (Input.GetAxis ("Vertical")) < 0.2f && Mathf.Abs (Input.GetAxis ("Horizontal")) < 0.2f) 
		{
			helicopter.velocity = Vector3.SmoothDamp (helicopter.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.5f);
		}
	}
}



 
