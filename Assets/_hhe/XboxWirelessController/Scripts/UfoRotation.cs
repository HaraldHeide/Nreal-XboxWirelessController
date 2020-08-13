/*
 * UFO Rotation
 * 
 */
using UnityEngine;
public class UfoRotation: MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed = 100;
    void Update()
    {
        //Rotate always face Player
        if (Input.GetAxis("RightJoystickHorizontal") != 0)
        {
            Vector3 RotateDirection = Vector3.zero;
            RotateDirection.y = Input.GetAxis("RightJoystickHorizontal") * Time.deltaTime * RotateSpeed;

            transform.rotation *= Quaternion.Euler(RotateDirection);
            //transform.Rotate(RotateDirection, Space.World);
        }
    }
}