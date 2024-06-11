using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapController : MonoBehaviour
{

    [SerializeField] string mushrooms;
    [SerializeField] GameObject[] mushroomObjects;
    [SerializeField] GameObject player;
    [SerializeField] float rythmn;
    [SerializeField] GameObject menu;

    int dirctionIndex;
    int[,] directions;
    char[,] mushroomArray;
    GameObject[,] mushroomObjectArray;
    bool playerIsUnableToMove = false;

    // alustetaan tarvittavat asiat
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
                    mushroomArray[spot, row] = '0';
                    spot++;
                }
                row++;
                spot = 0;
            }
            else
            {
                mushroomArray[spot, row] = stringBuilder[i];

                Debug.Log("i: " +stringBuilder[i] + " "+ "next mushroomindex :" +nextMushroomIndex);
                if (stringBuilder[i] != '0' && nextMushroomIndex<mushroomObjects.Length) 
                {
                    mushroomObjectArray[spot, row] = mushroomObjects[nextMushroomIndex];
                    nextMushroomIndex++;    
                }
                spot++;
            }
        } 

        directions = new int[4,2];
        directions[0, 0] = 0; directions[0, 1] = -1; //up
        directions[1, 0] = 1; directions[1, 1] = 0;  //right
        directions[2, 0] = 0; directions[2, 1] = 1;  //down
        directions[3, 0] = -1; directions[3, 1] = 0; //left

        InvokeRepeating("Changes", 0f, rythmn);
    }

    // Suoritetaan kaikki muutokset jotka tapahtuvat säännöllisesti
    void Changes()
    {
        Debug.Log("Direction " + dirctionIndex);

        FindGoodDirection();

        for (int j = 0; j < mushroomObjectArray.GetLength(0); j++)
        {
            for (int k = 0; k < mushroomObjectArray.GetLength(1); k++)
            {
                if (mushroomObjectArray[j, k]!=null) mushroomObjectArray[j,k].GetComponent<Mushroom>().ChangeColor();
            }
        }
        player.GetComponent<PlayerScript>().ChangeColor();
    }

    // Etsitään sopiva suunta jonne pelaaja voi seuraavaksi mennä jos sopivaa ei löydy tarkistetaan onko pelaaja voittanut vai hävinnyt pelin
    private void FindGoodDirection()
    {
        int[] playerPosition = player.GetComponent<PlayerScript>().givePosition();

        int checkedDirections = 0;
        bool found = false;
        while (checkedDirections <4)
        {
            
            if (dirctionIndex < (directions.GetLength(0) - 1))
            {
                dirctionIndex++;
            }
            else
            {
                dirctionIndex = 0;
            }
            int[] newPlayerPosition = new int[2] { directions[dirctionIndex, 0] + playerPosition[0], directions[dirctionIndex, 1] + playerPosition[1] };
            if (newPlayerPosition[0] >= 0 && newPlayerPosition[1] >= 0
            && newPlayerPosition[0] < mushroomObjectArray.GetLength(0)
            && newPlayerPosition[1] < mushroomObjectArray.GetLength(1)
            && mushroomObjectArray[newPlayerPosition[0], newPlayerPosition[1]] != null) { found = true; checkedDirections = 4; }
            checkedDirections++;
        }
        if (found) player.GetComponent<PlayerScript>().ChangeArrowDirections(dirctionIndex);
        
        else
        {
            playerIsUnableToMove = true;
            if (mushroomObjectArray[playerPosition[0], playerPosition[1]] != null)
            {
                mushroomObjectArray[playerPosition[0], playerPosition[1]].GetComponent<Mushroom>().Die();
                mushroomObjectArray[playerPosition[0], playerPosition[1]] = null;
            }
            for (int i = 0; i<mushroomObjectArray.GetLength(0); i++) 
            {
                for (int j = 0; j < mushroomObjectArray.GetLength(0); j++)
                {

                    if (mushroomObjectArray[i, j] != null) { this.Invoke("Restart", 1f); }
                }

            }
            this.Invoke("NextLevel", 1f);
        }
    }

    // käynnistetään seuraava taso
    void NextLevel()
    {
        menu.GetComponent<MenuManager>().OpenMenu();
    }

    // Käynnistetään nykyinen taso uusiksi
    void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    // Huolehditaan siitä miten pelaajan liikkuminen vaikuttaa sieniin ja sienet pelaajaan
    public void PlayerMoves()
    {
        if (playerIsUnableToMove && menu.GetComponent<MenuManager>().isOpen())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        else if (playerIsUnableToMove) return;

        int[] playerPosition = player.GetComponent<PlayerScript>().givePosition();

        mushroomObjectArray[playerPosition[0], playerPosition[1]].GetComponent<Mushroom>().Die();
        mushroomObjectArray[playerPosition[0], playerPosition[1]] =null;

        player.GetComponent<PlayerScript>().Move(directions[dirctionIndex, 0], directions[dirctionIndex, 1]);
        int[] newPlayerPosition = new int[2] { directions[dirctionIndex, 0] + playerPosition[0], directions[dirctionIndex, 1] + playerPosition[1] };

        // Jos nykyinen nuolen suunta ei ole hyvä etsitään uusi
        if (!(newPlayerPosition[0] >= 0 && newPlayerPosition[1] >= 0
        && newPlayerPosition[0] < mushroomObjectArray.GetLength(0)
        && newPlayerPosition[1] < mushroomObjectArray.GetLength(1)
        && mushroomObjectArray[newPlayerPosition[0], newPlayerPosition[1]] != null))
        {
            FindGoodDirection();
        }
    }         
}
       