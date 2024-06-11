using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pelaajaa haluaa seuraavaan");
        }
    }

    public void OpenMenu()
    {
        panel.SetActive(true);
        open = true;
    }

    public bool isOpen()
    {
        return open;
    }
}
