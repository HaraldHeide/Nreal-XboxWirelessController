/*
 * 
 * 
 */ 
using UnityEngine;
public class GamePadInputHandler : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 2;
    [SerializeField]
    private float RotateSpeed = 100;

    void Update()
    {
        if (Input.GetButtonDown("AButton"))
        {
            transform.localScale *= 1.1f;
        }
        if (Input.GetButtonDown("BButton"))
        {
            transform.localScale /= 1.1f;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") != 0
            || Input.GetAxis("LeftJoystickVertical") != 0
            || Input.GetAxis("RightJoystickVertical") != 0)
        {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis("LeftJoystickHorizontal") * Time.deltaTime * MoveSpeed;
            inputDirection.z = Input.GetAxis("LeftJoystickVertical") * Time.deltaTime * MoveSpeed;
            inputDirection.y = Input.GetAxis("RightJoystickVertical") * Time.deltaTime * MoveSpeed;
            //transform.position += inputDirection;

            transform.Translate(inputDirection, Space.World);
        }

        //Rotate
        if (Input.GetAxis("RightJoystickHorizontal") != 0)
        {
            Vector3 RotateDirection = Vector3.zero;
            RotateDirection.y = Input.GetAxis("RightJoystickHorizontal") * Time.deltaTime * RotateSpeed;

            transform.rotation *= Quaternion.Euler(RotateDirection);
            //transform.Rotate(RotateDirection, Space.World);
        }
    }
}
