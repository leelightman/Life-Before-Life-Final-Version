using UnityEngine;

public class SelectLadder : MonoBehaviour
{
    public Item item;
    public Shader outlineShader;

    private Shader originalShader;
    private Renderer[] rend;

    private void Start()
    {
        //Renderer rend = gameObject.GetComponent<Renderer>();
        //originalShader = rend.material.shader;

        rend = gameObject.GetComponentsInChildren<Renderer>();
        originalShader = rend[0].material.shader;
    }

    public void Collect()
    {
        Inventory.instance.AddItem(item);
    }

    public void ApplyOutline()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material.shader = outlineShader;
        }
    }

    public void ApplyOriginalShader()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material.shader = originalShader;
        }
    }
}
