using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material[] alternativeColors;
    int i;
    public int indexOfMaterial;
    Renderer rend;
    public float speed;

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
    void RandomColor()
    {
        i = Random.Range(0, alternativeColors.Length);

        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[i];
        rend.sharedMaterials = materials;
    }

    public void NextColor()
    {
        if (i < alternativeColors.Length - 1)
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


