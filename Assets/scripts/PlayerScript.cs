using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("variables")] 
    [SerializeField] MapController mapController;
    [SerializeField] Material[] alternativeColors;
    [SerializeField] int[] position = new int[2];
    [SerializeField] int indexOfMaterial;
    Renderer rend;
    int colorIndex = 0;
    bool moving = false;
    Vector3 directionToMove = Vector3.zero;
    int timesMoved = 0;

    [SerializeField] GameObject[] arrows = new GameObject[4];
    [SerializeField] GameObject playerModel;

    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource dyingSound;

    [SerializeField] Animator animator;

    void Start()
    {
        rend = GetComponent<Renderer>();
        Debug.Log("position: " + position[0] + "," + position[1]);
        arrows[0].GetComponent<ArrowScript>().GoOn();
        ChangeColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pelaajaa haluaa liikkua");
            mapController.GetComponent<MapController>().PlayerMoves();
        }

        if (moving)
        {
            if (timesMoved == 10)
            {
                moving = false;
                return;
            }

            else timesMoved++;
            transform.localPosition += directionToMove;
        }
    }

    // Vaihtaa pelaajan värin sattumanvaraisesti
    public void ChangeColor()
    {
        rend = playerModel.GetComponentInChildren<Renderer>();
        colorIndex = Random.Range(0, alternativeColors.Length);

        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[colorIndex];
        rend.sharedMaterials = materials;
    }
    
    // Antaa pelaajan sienimatriisia vastaavat koordinaatit
    public int[] givePosition()
    {
        Debug.Log("position: "+ position[0] + ","+ position[1] );
        return position;
    }

    // liikuttaa pelaajaa ja tallentaa uudet koordinaatit
    public void Move(int x, int y)
    {
        timesMoved = 0;
        moving= true;
        jumpSound.Play();
        animator.Play("jump");
        directionToMove = ((Vector3.forward * (y) * 2 + Vector3.left * (x) * 2)*0.1f);
        //transform.Translate(Vector3.forward * (y) * 2);
        //transform.Translate(Vector3.left * (x) * 2);
        
        position[0] = position[0] + x;
        position[1] = position[1] + y;
    }

    public void Die()
    {
        dyingSound.Play();
    }

        // muuttaa pelaajan nuolet
        public void ChangeArrowDirections(int direction)
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<ArrowScript>().GoOff();
        }
        arrows[direction].GetComponent<ArrowScript>().GoOn();
    }

    internal int GetColor()
    {
        return colorIndex;
    }
}
