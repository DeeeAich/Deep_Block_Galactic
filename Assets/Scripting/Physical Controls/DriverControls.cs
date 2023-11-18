using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverControls : MonoBehaviour
{

    [SerializeField] List<GameObject> treads = new();

    [SerializeField] float speed = 4;
    [SerializeField] float rotationSpeed = 15;

    private void Controlling()
    {

        float rightStick = Input.GetAxis("RightStick");
        float leftStick = Input.GetAxis("LeftStick");


        if (rightStick == leftStick)
            Move(rightStick, leftStick);
        else
            Rotate(rightStick, leftStick);

    }

    private void Rotate (float r, float l)
    {

        if (l == 0)
        {
            transform.RotateAround(treads[0].transform.position, transform.up, r * rotationSpeed * Time.deltaTime);
        }
        else if (r == 0)
        {
            transform.RotateAround(treads[1].transform.position, transform.up, l * rotationSpeed * Time.deltaTime);
        }
        else
        {

            transform.Rotate(transform.up, rotationSpeed * 2 * (r > 0 ? -1 : 1) * Time.deltaTime);

        }
    }

    private void Move(float r, float l)
    {

        GetComponent<Rigidbody>().velocity = transform.forward * (r + l) * Time.deltaTime;

    }



    private void FixedUpdate()
    {

        Controlling();

    }

    
    

}
