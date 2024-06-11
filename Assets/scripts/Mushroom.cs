using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] Material[] alternativeColors;
    [SerializeField] Material deadMushroom;
    int colorIndex;
    [SerializeField] GameObject mushroom;
    [SerializeField] int indexOfMaterial;
    [SerializeField] int indexOfSecondMaterial;
    [SerializeField] Animator animator;
    Renderer rend;

    void Start()
    {
        ChangeColor();
    }

    // Vaihtaa sienen v‰rin sattumanvaraisesti
    public void ChangeColor()
    {
        colorIndex = Random.Range(0, alternativeColors.Length);

        rend = mushroom.GetComponentInChildren<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = alternativeColors[colorIndex];
        rend.sharedMaterials = materials;
    }

    // Huolehtii siit‰ milt‰ sienen tuhoutuminen n‰ytt‰‰
    internal void Die()
    {
        animator.Play("die");
        rend = mushroom.GetComponentInChildren<Renderer>();
        Material[] materials = rend.sharedMaterials;
        materials[indexOfMaterial] = deadMushroom;
        materials[indexOfSecondMaterial] = deadMushroom;
        rend.sharedMaterials = materials;
    }

    internal int GetColor()
    {
        return colorIndex;
    }
}


