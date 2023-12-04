using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == FindObjectOfType<DriverControls>().name)
        {
            GetComponent<Animator>().SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == FindObjectOfType<DriverControls>().name)
        {
            GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
