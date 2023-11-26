using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] Animator anim;

    bool grabbed;

    Collectable grabbedObject;

    [SerializeField] Transform grabLocation;

    [SerializeField] float launchForce = 10f;

    public void StartGrab()
    {
        grabbed = true;

        anim.SetBool("Grabbed", grabbed);

        if(grabbedObject != null)
        {

            grabbedObject.PickUp();

            grabbedObject.transform.position = grabLocation.transform.position;

        }
    }

    public void EndGrab()
    {

        if(grabbedObject != null)
        {
            grabbedObject.Drop();

            grabbedObject = null;
        }

        grabbed = false;

        anim.SetBool("Grabbed", grabbed);
    }

    public void Launch()
    {


        if(grabbedObject != null)
        {

            grabbedObject.Drop();

            grabbedObject.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);

            grabbedObject = null;

        }


        grabbed = false;

        anim.SetBool("Grabbed", grabbed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!grabbed && other.TryGetComponent<Collectable>(out grabbedObject))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!grabbed && other.TryGetComponent<Collectable>(out grabbedObject))
        {
            grabbedObject = null;
        }
    }

}