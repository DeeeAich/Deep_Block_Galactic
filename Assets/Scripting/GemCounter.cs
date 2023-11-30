using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private int gemCount = 0;
    private int currentGemCount = 0;

    private void Start()
    {

        foreach (Breakable breakable in FindObjectsOfType<Breakable>())
            gemCount += breakable.spawnAmount;

        text.text = "0/" + gemCount;
    }

    public void GemFound()
    {
        currentGemCount++;

        text.text = currentGemCount + "/" + gemCount;
    }
}
