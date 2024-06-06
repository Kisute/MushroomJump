using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float speed;
    Vector3[] directions;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        directions = new Vector3[4];
        directions[0] = Vector3.forward;
        directions[1] = Vector3.right;
        directions[2] = Vector3.back;
        directions[3] = Vector3.left;

        InvokeRepeating("ChangeDirection", 0f, speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(directions[i] * 2);
        }
    }
    void ChangeDirection()
    {
        if (i < directions.Length - 1)
        {
            i++;
        }
        else
        {
            i = 0;
        }
    }
}
