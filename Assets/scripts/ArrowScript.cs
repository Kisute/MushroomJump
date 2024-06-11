using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] Material[] alternativeColors;
    [SerializeField] int indexOfMaterial;
    Renderer rend;

    // muuttaa nuolen värin kirkkaaksi kertomaan siitä että nuoli on aktiivinen
    public void GoOn()
    {
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[0];
        rend.sharedMaterials = materials;
    }

    // muuttaa nuolen värin tummaksi kertomaan siitä että nuoli ei ole aktiivinen
    public void GoOff()
    {
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[1];
        rend.sharedMaterials = materials;
    }
}


