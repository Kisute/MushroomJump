using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    public MapController mapController;
    public Material[] alternativeColors;
    public int[] position = new int[2];
    int i;
    public int indexOfMaterial;
    Renderer rend;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pelaajaa haluaa liikkua");
            mapController.GetComponent<MapController>().PlayerMoves();
        }
    }

    public void ChangeColor()
    {
        //i = Random.Range(0, alternativeColors.Length);

        rend = GetComponent<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[i];
        rend.sharedMaterials = materials;
    }
    
    public int[] givePosition()
    {
        Debug.Log("position: "+ position[0] + ","+ position[1] );
        return position;
    }

    public void Move(int x, int y)
    {
        transform.Translate(Vector3.back * (-x) * 2);
        transform.Translate(Vector3.right * (-y) * 2);

        position[0] = position[0] + x;
        position[1] = position[1] + y;

        Debug.Log("new position: " + position[0] + "," + position[1]);
    }
}
