using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Material[] alternativeColors;
    public int indexOfStartingmaterial;
    int i;
    public int indexOfMaterial;
    Renderer rend;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GoOn()
    {
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[0];
        rend.sharedMaterials = materials;
    }

    public void GoOff()
    {
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[1];
        rend.sharedMaterials = materials;
    }
}


