using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Material[] alternativeColors;
    int i;
    public int indexOfMaterial;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(0, alternativeColors.Length);

        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials; 
        materials[indexOfMaterial] = alternativeColors[i];
        rend.sharedMaterials = materials;
    }

    // Update is called once per frame
    public void ChangeColor()
    {
        i = Random.Range(0, alternativeColors.Length);

        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[i];
        rend.sharedMaterials = materials;
    }
}


