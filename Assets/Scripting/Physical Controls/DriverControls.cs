using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DriverControls : MonoBehaviour
{

    [SerializeField] List<GameObject> treads = new();
    [SerializeField] List<Animator> treadAnimation = new();

    [SerializeField] float speed = 4;
    [SerializeField] float rotationSpeedMulti = 1.5f;

    [SerializeField] float maxSpeedTime = 2;

    [SerializeField] float rightStick = 0;
    float rightTime = 0;
    [SerializeField] float leftStick = 0;
    float leftTime = 0;

    [SerializeField] float turningPointAddition = 0.5f;

    private void Controlling()
    {

        if (rightStick == leftStick)
            Move(rightStick);
        else if (leftStick == 0 && rightStick == 0)
            Move(0);
        else
            Rotate(rightStick, leftStick);

    }

    private void Rotate (float r, float l)
    {

        float moveAmount = 0;

        if (l == 0)
        {
            transform.RotateAround(treads[1].transform.position + -transform.right * turningPointAddition, transform.up, -r * rotationSpeedMulti * Time.deltaTime);
        }
        else if (r == 0)
        {
            transform.RotateAround(treads[0].transform.position + transform.right * turningPointAddition, transform.up, l * rotationSpeedMulti * Time.deltaTime);
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

                    moveAmount = l;

                    rotationPivot = treads[1].transform.position;

                }
                else if (r < l && l > 0 ||  r > l && l < 0)
                {

                    percentage = l > 0 ? r / l : -r / -l;

                    rotationMath = l * (1 - percentage) * 2;

                    moveAmount = r;

                    rotationPivot = treads[0].transform.position;

                }

            }
            else 
                rotationMath = l - r;

            transform.RotateAround(rotationPivot, transform.up, rotationMath * rotationSpeedMulti * Time.deltaTime);

        }

        Move(moveAmount);
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

        rightTime += Time.deltaTime;
        leftTime += Time.deltaTime;

        if (rightStick > 0 && rightStick < speed * Input.GetAxis("RightStick") -0.1f || rightStick < 0 && Input.GetAxis("RightStick") * speed + 0.1f < rightStick )
            rightStick = Mathf.Lerp(rightStick, speed * Input.GetAxis("RightStick"), rightTime / maxSpeedTime);
        else
            rightStick = Input.GetAxis("RightStick") * speed;
        if (leftStick > 0 && speed * Input.GetAxis("LeftStick") - 0.1f > leftStick || leftStick < 0 && speed * Input.GetAxis("LeftStick") + 0.1f < leftStick)
            leftStick = Mathf.Lerp(leftStick, speed * Input.GetAxis("LeftStick"), leftTime / maxSpeedTime);
        else
            leftStick = speed * Input.GetAxis("LeftStick");
        


        /*
        if(Input.GetKeyDown(KeyCode.S))
            rightStick = speed;
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            rightStick = 0;
        if (Input.GetKeyDown(KeyCode.D))
            rightStick = -speed;


        if (Input.GetKeyDown(KeyCode.W))
            leftStick = speed;
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W))
            leftStick = 0;
        if (Input.GetKeyDown(KeyCode.A))
            leftStick = -speed;
        */
    }

}
