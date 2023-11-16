using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class WiiMoteTesting : MonoBehaviour
{

    Wiimote wiimote;

    [SerializeField] Transform rotateThis;

    [SerializeField] bool bPressed = false;

    // Start is called before the first frame update
    void Start()
    {

        WiimoteApi.WiimoteManager.FindWiimotes();

        print(WiimoteManager.Wiimotes.Count);

        if(WiimoteManager.Wiimotes.Count > 0)
        {
            wiimote = WiimoteManager.Wiimotes[0];

            wiimote.SendPlayerLED( true, true , false, false);

            wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
        }

    }

    private IEnumerator RumbleTest()
    {
        wiimote.RumbleOn = true;

        yield return new WaitForSeconds(0.5f);
        wiimote.SendStatusInfoRequest();

        yield return new WaitForSeconds(0.5f);
        wiimote.RumbleOn = false;

        yield return new WaitForSeconds(0.5f);
        wiimote.SendStatusInfoRequest();

    }


    private void FollowWiiMote()
    {

        int ret = 0;
        do
        {
            ret = wiimote.ReadWiimoteData();


            float[] accels = wiimote.Accel.GetCalibratedAccelData();

            Vector3 acceleration = (new Vector3(accels[0], accels[1], accels[2])).normalized;

            rotateThis.rotation = Quaternion.LookRotation(acceleration);

        } while (ret > 0);


    }

    private void ButtonPresses()
    {

        if (wiimote.Button.b)
        {
            wiimote.RumbleOn = true;
            if (!bPressed)
            {
                print("Pressed");
                bPressed = true;
            }
        }
        else if(bPressed)
        {
            print("Unpressed");
            bPressed = false;
        }
    }

    private void Update()
    {
        
        if(wiimote != null)
        {
            FollowWiiMote();
            ButtonPresses();
        }
    }

}
