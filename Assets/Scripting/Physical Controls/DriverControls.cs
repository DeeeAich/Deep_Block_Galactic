using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverControls : MonoBehaviour
{

    [SerializeField] List<GameObject> treads = new();

    [SerializeField] float speed = 4;
    [SerializeField] float rotationSpeedMulti = 1.5f;

    [SerializeField] float maxSpeedTime = 2;

    float rightStick = 0;
    float rightTime = 0;
    float leftStick = 0;
    float leftTime = 0;

    private void Controlling()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            rightTime = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A))
            leftTime = 0;

        rightTime += Time.deltaTime;
        leftTime += Time.deltaTime;

        rightStick = Mathf.Lerp(rightStick, speed * Input.GetAxis("RightStick"), rightTime / maxSpeedTime);
        leftStick = Mathf.Lerp(leftStick, speed * Input.GetAxis("LeftStick"), leftTime / maxSpeedTime);

        if (rightStick == leftStick)
            Move(rightStick);
        else
            Rotate(rightStick, leftStick);

    }

    private void Rotate (float r, float l)
    {

        if (l == 0)
        {
            transform.RotateAround(treads[0].transform.position, transform.up, r * rotationSpeedMulti * Time.deltaTime);
        }
        else if (r == 0)
        {
            transform.RotateAround(treads[1].transform.position, transform.up, -l * rotationSpeedMulti * Time.deltaTime);
        }
        else
        {

            float combined = r - l;

            transform.Rotate(transform.up, combined * rotationSpeedMulti * Time.deltaTime);

        }
    }

    private void Move(float r)
    {

        GetComponent<Rigidbody>().velocity = transform.forward * r + new Vector3( 0, GetComponent<Rigidbody>().velocity.y, 0) ;

    }



    private void FixedUpdate()
    {

        Controlling();

    }

    
    

}
