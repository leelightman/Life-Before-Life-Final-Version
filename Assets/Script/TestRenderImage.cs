using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderImage : MonoBehaviour
{

    #region Variables
    public Shader UseShader;
    private Shader curShader;
    private Material curMaterial;


    [Range(0, 1f)]
    public float distortFactor = 1.0f;
    [Range(0, 2f)]
    public float distortStrength = 1.0f;
    //扭曲中心（0-1）屏幕空间，默认为中心点
    public Vector2 distortCenter = new Vector2(0.5f, 0.5f);

    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion
    
    void Start()
    {
        /*
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
        }

        if (!curShader && !curShader.isSupported)
        {
            enabled = false;
        }
        */
    }

    void OnRenderImage(RenderTexture source, RenderTexture target)
    {
        if (curShader != null)
        {
            //shader1
            /*
            material.SetFloat("_DistortFactor", distortFactor);
            material.SetVector("_DistortCenter", distortCenter);
            */

            //shader2
            material.SetFloat("_DistortFactor", distortFactor);
            material.SetVector("_DistortCenter", distortCenter);
            material.SetFloat("_DistortStrength", distortStrength);


            Graphics.Blit(source, target, material);
            //Debug.Log("OnRenderImage: " + grayScaleAmout);
        }
        else
        {
            Graphics.Blit(source, target);
        }
    }

    public void TurnOn()
    {
        curShader = UseShader;
    }

    public void TurnOff()
    {
        curShader = null;
    }

}