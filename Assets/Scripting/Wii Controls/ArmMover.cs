using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WiimoteApi;

public class ArmMover : MonoBehaviour
{

    Wiimote myMote = null;
    [SerializeField] bool rightMote;

    [SerializeField] UnityEvent bDown;
    [SerializeField] UnityEvent bUp;
    [SerializeField] UnityEvent aPress;

    [SerializeField] Transform changeTransform;

    [SerializeField] bool bPressed = false;

    [SerializeField] float turnSpeed = 2f;

    private void Start()
    {

        WiimoteManager.FindWiimotes();

        if (WiimoteManager.Wiimotes.Count > 1)
        {
            myMote = WiimoteManager.Wiimotes[rightMote ? 1 : 0];

            myMote.SendPlayerLED(!rightMote, !rightMote, rightMote, rightMote);

            myMote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);

            print("move connected" + (rightMote ? " right" : " left" ) + ". Battery status is " + myMote.Status.battery_low);
        }
        else
            print("Not enough remotes connected");
    }

    private void FixedUpdate()
    {
        if (myMote == null)
            return;

        int ret = 0;
        do
        {
            ret = myMote.ReadWiimoteData();

            float[] accels = myMote.Accel.GetCalibratedAccelData();

            Vector3 acceleration = (new Vector3(-accels[0], -accels[1], accels[2])).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(acceleration);


            changeTransform.localRotation = lookRotation;

        } while (ret > 0);
    }

    private void Update()
    {

        if(myMote != null && myMote.Button.b && !bPressed)
        {
            bPressed = true;

            bDown.Invoke();

            myMote.RumbleOn = !rightMote;

            myMote.SendStatusInfoRequest();

        }
        else if (myMote != null && bPressed && !myMote.Button.b)
        {
            bPressed = false;

            bUp.Invoke();

            myMote.RumbleOn = false;

            myMote.SendStatusInfoRequest();
        }

        if(myMote != null && myMote.Button.a)
        {
            aPress.Invoke();
        }
    }

}
