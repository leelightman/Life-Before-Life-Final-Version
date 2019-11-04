using UnityEngine;

public class Selected : MonoBehaviour
{
    //public GameObject selectedObject;
    //public int red;
    //public int green;
    //public int blue;
    //public bool lookingAtObject = false;
    //public bool flashingIn = true;
    //public bool startFlashing = false;
    //public int flashSpeed = 2;
    //public GameObject instructionText;
    public Item item;
    public Shader outlineShader;
    public GameObject itemInOtherTime;

    private Shader originalShader;
    private Renderer rend;

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        originalShader = rend.material.shader;
    }
    

    public void Collect()
    {
        if (item.itemName.Contains("Puzzle"))
        {
            PuzzleController pc = GameObject.Find("PuzzleManager").GetComponent<PuzzleController>();
            pc.puzzleStatue[item.itemName[6] - '1'] = false;
        }
        Inventory.instance.AddItem(item);
        if (itemInOtherTime != null)
        {
            itemInOtherTime.SetActive(!itemInOtherTime.activeSelf);
        }
    }

    public void ApplyOutline()
    {
        rend.material.shader = outlineShader;
    }

    public void ApplyOriginalShader()
    {
        rend.material.shader = originalShader;
    }
}
