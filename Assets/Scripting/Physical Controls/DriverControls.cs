using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverControls : MonoBehaviour
{

    [SerializeField] List<GameObject> treads = new();

    [SerializeField] float speed = 4;
    [SerializeField] float rotationSpeedMulti = 1.5f;

    [SerializeField] float maxSpeedTime = 2;

    [SerializeField] float rightStick = 0;
    float rightTime = 0;
    [SerializeField] float leftStick = 0;
    float leftTime = 0;

    private void Controlling()
    {

        if (rightStick == leftStick)
            Move(rightStick);
        else
            Rotate(rightStick, leftStick);

    }

    private void Rotate (float r, float l)
    {

        if (l == 0)
        {
            transform.RotateAround(treads[1].transform.position, transform.up, -r * rotationSpeedMulti * Time.deltaTime);
        }
        else if (r == 0)
        {
            transform.RotateAround(treads[0].transform.position, transform.up, l * rotationSpeedMulti * Time.deltaTime);
        }
        else
        {

            float rotationMath = 0;

            Vector3 rotationPivot = transform.position;

            if(r > 0 && l > 0|| r < 0 && l < 0)
            {
                float percentage = 0;

                if (r > l && r > 0 || r < l && r < 0 )
                {
                    percentage = r > 0 ? l / r : -l / -r;

                    rotationMath = -r * (1 - percentage) * 2;

                    Move(l + r * percentage);

                    //rotationPivot += -Vector3.Distance(transform.position, treads[0].transform.position) * transform.right * (1 - percent);

                }
                else if (r < l && l > 0 ||  r > l && l < 0)
                {

                    percentage = l > 0 ? r / l : -r / -l;

                    rotationMath = l * (1 - percentage) * 2;

                    Move(r);

                    //rotationPivot += Vector3.Distance(transform.position, treads[0].transform.position) * transform.right * (1 - percent);

                }

            }
            else
                rotationMath = l - r;

            transform.RotateAround(rotationPivot, transform.up, rotationMath * rotationSpeedMulti * Time.deltaTime);

        }
    }

    private void Move(float moveAmount)
    {

        GetComponent<Rigidbody>().velocity = transform.forward * moveAmount + new Vector3( 0, GetComponent<Rigidbody>().velocity.y, 0) ;

    }



    private void FixedUpdate()
    {

        Controlling();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rightTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A))
            leftTime = 0;


        rightStick = Mathf.Lerp(rightStick, speed * Input.GetAxis("RightStick"), rightTime / maxSpeedTime);
        leftStick = Mathf.Lerp(leftStick, speed * Input.GetAxis("LeftStick"), leftTime / maxSpeedTime);

        rightTime += Time.deltaTime;
        leftTime += Time.deltaTime;
    }

}
