using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelected : MonoBehaviour
{
    public Shader outlineShader;

    private Shader originalShader;
    private Renderer rend;

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        originalShader = rend.material.shader;
    }

    public void ApplyOutline()
    {
        rend.material.shader = outlineShader;
        rend.material.color = new Color(1, 164/255f, 0, 1);
    }

    public void ApplyOriginalShader()
    {
        rend.material.shader = originalShader;
        rend.material.color = new Color(1, 164/255f, 0, 0);
    }
}
