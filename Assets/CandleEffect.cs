using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleEffect : MonoBehaviour
{
    public float Interval;
    public float RandomSeed;
    [Range(0f, 1.0f)]
    public float Influence;
    [Range(0f,0.5f)]
    public float DarkRatio;
    [Range(0f, 0.5f)]
    public float LightRatio;
    private float CurTime;

    private Light MyLight;
    private float InitIntensity;
    // Start is called before the first frame update
    void Start()
    {
        MyLight = this.gameObject.GetComponent<Light>();
        InitIntensity = MyLight.intensity;
        CurTime = Interval + Random.Range(0,RandomSeed);
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = 1.0f;
        if (CurTime > 0)//前100%到90%变暗,90%到70%变亮
        {
            if (CurTime > (1 - DarkRatio - LightRatio) * Interval) 
            {
                if (CurTime > (1 - DarkRatio) * Interval)  //变化的前半时间，变暗
                {
                    ratio = (CurTime - (1 - DarkRatio) * Interval) / (DarkRatio * Interval);
                }
                else //变化的后半时间，变亮
                {
                    ratio = 1.0f - (CurTime - (1 - DarkRatio - LightRatio) * Interval) / (LightRatio * Interval);
                }
            }
            CurTime -= Time.deltaTime;
        }
        else
        {
            CurTime = Interval + Random.Range(0, RandomSeed);
        }
        MyLight.intensity = ((1 - Influence) + Influence * ratio) * InitIntensity;
    }
}
