using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    [SerializeField] float destroyTime;

    [SerializeField] GameObject[] spawnObjects;

    [SerializeField] int spawnAmount;

    public void BreakObject()
    {

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
            Broken();

    }

    private void Broken()
    {
        GetComponent<Animator>().SetTrigger("Break");

        for(int cC = 0; cC < spawnAmount; cC++ )
        {
            GameObject newCrystal = GameObject.Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length - 1)], transform);

            newCrystal.transform.parent = null;
        }
        enabled = false;

    }


}
