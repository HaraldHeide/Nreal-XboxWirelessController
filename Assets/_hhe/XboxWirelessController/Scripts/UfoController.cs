/*
 * UFO controller
 * 
 */
using UnityEngine;
public class UfoController : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 2;
    [SerializeField]
    private float RotateSpeed = 100;


    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        #region Buttons A, B X, Y
        if (Input.GetButtonDown("AButton"))
        {
            transform.localScale *= 1.1f;
        }
        if (Input.GetButtonDown("BButton"))
        {
            transform.localScale /= 1.1f;
        }
        if (Input.GetButtonDown("XButton"))
        {
            anim.SetBool("LandingGearUp", !anim.GetBool("LandingGearUp"));
        }
        #endregion

        #region Movement Instant
        if (Input.GetAxis("LeftJoystickHorizontal") != 0
            || Input.GetAxis("LeftJoystickVertical") != 0
            || Input.GetAxis("RightJoystickVertical") != 0)
        {
            Vector3 inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxis("LeftJoystickHorizontal") * Time.deltaTime * MoveSpeed;
            inputDirection.z = Input.GetAxis("LeftJoystickVertical") * Time.deltaTime * MoveSpeed;
            inputDirection.y = Input.GetAxis("RightJoystickVertical") * Time.deltaTime * MoveSpeed;
            //transform.position += inputDirection;
            transform.Translate(inputDirection, Space.Self);
        }
        #endregion

        transform.LookAt(Camera.main.transform);
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
        transform.localRotation *= Quaternion.Euler(0,  180f, 0);
        //Rotate always face Player
        //if (Input.GetAxis("RightJoystickHorizontal") != 0)
        //{
        //    Vector3 RotateDirection = Vector3.zero;
        //    RotateDirection.y = Input.GetAxis("RightJoystickHorizontal") * Time.deltaTime * RotateSpeed;

        //    transform.rotation *= Quaternion.Euler(RotateDirection);
        //    //transform.Rotate(RotateDirection, Space.World);
        //}
    }
}