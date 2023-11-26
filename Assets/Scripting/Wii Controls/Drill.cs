using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{

    [SerializeField] Animator anim;

    [SerializeField] bool drillActive;

    private void OnTriggerStay(Collider other)
    {
        if (drillActive && other.TryGetComponent<Breakable>(out Breakable breakable))
            breakable.BreakObject();
    }

    public void DrillActivate()
    {
        drillActive = true;

        anim.SetBool("DrillActive", drillActive);

    }

    public void DrillShutdown()
    {
        drillActive = false;

        anim.SetBool("DrillActive", drillActive);
    }
}
