using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] Animator anim;

    bool grabbed;

    Collectable grabbedObject;

    [SerializeField] Transform grabLocation;

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
        grabbed = false;

        anim.SetBool("Grabbed", grabbed);

        if(grabbedObject != null)
        {

        }
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
