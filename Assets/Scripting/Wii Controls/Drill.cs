using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{

    [SerializeField] Animator anim;

    [SerializeField] bool drillActive;

    [SerializeField] ParticleSystem paricles;

    private void OnTriggerStay(Collider other)
    {
        if (drillActive && other.TryGetComponent<Breakable>(out Breakable breakable))
        {
            paricles.GetComponent<ParticleSystemRenderer>().material = breakable.particles.GetComponent<ParticleSystemRenderer>().material;
            paricles.gameObject.SetActive(true);
            breakable.BreakObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        paricles.gameObject.SetActive(false);

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
