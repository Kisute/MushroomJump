using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class ChangeColorInRythmn : MonoBehaviour
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
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials; 
        materials[indexOfMaterial] = alternativeColors[indexOfStartingmaterial];
        rend.sharedMaterials = materials;
        i = indexOfStartingmaterial;
        InvokeRepeating("NextColor", 0f, speed);
    }

    public void NextColor()
    {
        if (i < alternativeColors.Length-1)
        {
            i++;
        }
        else
        {
            i = 0;
        }
        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[i];
        rend.sharedMaterials = materials;
    }
}


