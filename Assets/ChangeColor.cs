using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Random;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material[] materials;
    int i;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(0, materials.Length);
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[i];
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = materials[i];
    }

    public void NextColor()
    {
        if (i < materials.Length)
        {
            i++;
        }
        else
        {
            i = 0;
        }
    }
}


