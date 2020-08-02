/*
 * https://www.youtube.com/watch?v=J6QR4KzNeJU
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 25f;
    [SerializeField]
    private float strafeSpeed = 7.5f;
    [SerializeField]
    private float hoverSpeed = 5f;
    [SerializeField]
    private float rollSpeed = 90f;

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float activeRollSpeed;

    private float forwardAcceleration = 2.5f;
    private float strafeAcceleration = 2.0f;
    private float hoverAcceleration = 2.0f;
    private float rollAcceleration = 3.5f;

    public float lookRateSpeed = 90f;

    private Vector2 lookInput, screenCenter, mouseDistance;


    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);


        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed,
                                        Input.GetAxisRaw("Vertical") * forwardSpeed,
                                        forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed,
                                        Input.GetAxisRaw("Horizontal") * strafeSpeed,
                                        strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed,
                                        Input.GetAxisRaw("Hover") * hoverSpeed,
                                        hoverAcceleration * Time.deltaTime);
        activeRollSpeed = Mathf.Lerp(activeRollSpeed, 
                                        Input.GetAxisRaw("Roll"),
                                        rollAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime,
                            mouseDistance.x * lookRateSpeed * Time.deltaTime,
                            activeRollSpeed * rollSpeed * Time.deltaTime, Space.Self);
    }
}
