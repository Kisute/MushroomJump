using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public string mushrooms;
    public GameObject[] mushroomObjects;
    public GameObject player;
    public float rythmn;

    int dirctionIndex;
    int[,] directions;
    char[,] mushroomArray;
    GameObject[,] mushroomObjectArray;

    void Start()
    {
        int rowCount =0;
        int bigestCountInTheRow = 0;
        int CountInTheRow = 0;
        StringBuilder stringBuilder = new StringBuilder(mushrooms);


        for (int i = 0; i < stringBuilder.Length; i++)
        {
            if (stringBuilder[i] == ' ')
            {
                rowCount++;
                if (CountInTheRow>bigestCountInTheRow)bigestCountInTheRow=CountInTheRow;
                CountInTheRow=0;
            }
            else
            {
                CountInTheRow++;
            }
        }
        if (CountInTheRow!=0) rowCount++;
        if (CountInTheRow > bigestCountInTheRow) bigestCountInTheRow = CountInTheRow;

        //Debug.Log("taulukon leveys on " + rowCount + " ja pituus on " + bigestCountInTheRow);

        mushroomArray = new char[rowCount, bigestCountInTheRow];
        mushroomObjectArray = new GameObject[rowCount, bigestCountInTheRow];
        
        int row = 0;
        int spot = 0;
        int nextMushroomIndex = 0;
        for (int i = 0; i < stringBuilder.Length; i++)
        {
            if (stringBuilder[i] == ' ')
            {
                while (spot < bigestCountInTheRow)
                {
                    mushroomArray[row, spot] = '0';
                    spot++;
                }
                row++;
                spot = 0;
            }
            else
            {
                mushroomArray[row, spot] = stringBuilder[i];

                Debug.Log("i: " +stringBuilder[i] + " "+ "next mushroomindex :" +nextMushroomIndex);
                if (stringBuilder[i] != '0') 
                {
                    mushroomObjectArray[row, spot] = mushroomObjects[nextMushroomIndex];
                    nextMushroomIndex++;    
                }
                
                //Debug.Log("(" + row + "," + spot + ") " + mushroomArray[row, spot]);
                spot++;
            }

        } 
        
        

        directions = new int[4,2];
        directions[0, 0] = 0; directions[0, 1] = 1; //left
        directions[1, 0] = 1; directions[1, 1] = 0; //down
        directions[2, 0] = 0; directions[2, 1] = -1;//right
        directions[3, 0] = -1; directions[3, 1] = 0;//up

        InvokeRepeating("Changes", 0f, rythmn);

    }

    void Changes()
    {
        if (dirctionIndex < directions.GetLength(0)-1)
        {
            dirctionIndex++;
        }
        else
        {
            dirctionIndex = 0;
        }

        for (int j = 0; j < mushroomObjects.Length; j++)
        {
            mushroomObjects[j].GetComponent<Mushroom>().ChangeColor();
        }
        //player.GetComponent<PlayerScript>().ChangeColor();
    }

    public void PlayerMoves()
    {           Debug.Log("playerMoves");
                int[] playerPosition = player.GetComponent<PlayerScript>().givePosition();

                    Debug.Log("i: "+ dirctionIndex);
                    if (directions[dirctionIndex, 0] + playerPosition[0]>=0 && directions[dirctionIndex, 1] + playerPosition[1] >= 0
                    && directions[dirctionIndex, 0] + playerPosition[0] < mushroomArray.GetLength(0) &&
                    directions[dirctionIndex, 1] + playerPosition[1] < mushroomArray.GetLength(1))
                    {
                        Debug.Log("mushroomArray.GetLength(1) " + mushroomArray.GetLength(1));
                        Debug.Log("mushroomArray.GetLength(0) " + mushroomArray.GetLength(0));

                        Debug.Log("y direction " + directions[dirctionIndex, 1]);
                        player.GetComponent<PlayerScript>().Move(directions[dirctionIndex, 0], directions[dirctionIndex, 1]);
                    
                        //if mushroomObjectArray.getColor()
                    }
                
    }
       
}
