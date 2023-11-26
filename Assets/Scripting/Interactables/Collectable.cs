using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();   
    }

    public void PickUp()
    {
        body.isKinematic = true;
    }

    public void Drop()
    {
        body.isKinematic = false;
    }
}
