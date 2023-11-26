using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    [SerializeField] float destroyTime;

    [SerializeField] GameObject spawnObject;

    [SerializeField] int spawnAmount;

    public ParticleSystem particles;

    public void BreakObject()
    {

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
            Broken();

    }

    private void Broken()
    {

        for(int cC = 0; cC < spawnAmount; cC++ )
        {
            GameObject newCrystal = GameObject.Instantiate(spawnObject, transform.position, transform.rotation, null) ;

        }

        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        particles.gameObject.SetActive(true);

        Destroy(gameObject, 1f);

    }


}
