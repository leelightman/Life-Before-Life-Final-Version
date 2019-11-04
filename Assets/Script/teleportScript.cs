using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class teleportScript : MonoBehaviour
{
    public AudioClip AudioRewind;
    public AudioClip AudioForward;

    public GameObject MyCamera;
    public Light environmentLight;
    public Material DuskSkyBox;
    public Material DaySkyBox;
    public Color DuskColor;
    public Color DayColor;
    public Image Mask;

    public float MoveTime;//小移动
    private Vector3 MoveTarget;
    private float CurMoveTime;
    private bool IsMoved;

    public AudioSource TeleportAudio;
    public AudioSource AncientAudio;
    public AudioSource ModernAudio;
    public float ChangeTime;
    private float CurChangeTime;
    private bool IsChanged;//按了F以后变化过了没

    public bool IsStartOld;
    public Vector3 StartPoint;
    public Vector3 AfterPoint;
    private bool MyIsOld;

    public bool IsPoolRemoved;
    private bool NeedToChangeEnviLight;
    
    // Start is called before the first frame update
    void Start()
    {
        InitEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        //水下
        if (this.gameObject.transform.position.y < -0.2f && this.gameObject.transform.position.y > -10.0f)
        {
            if (this.gameObject.transform.position.z < 60f)
            {
                Vector3 ResetPoint = StartPoint;
                if (MyIsOld)
                {
                    ResetPoint.x -= 200.0f;
                }
                SetMove(ResetPoint);
            }
            else
            {
                if (!IsPoolRemoved || MyIsOld)
                {
                    Vector3 ResetPoint = AfterPoint;
                    if (MyIsOld)
                    {
                        ResetPoint.x -= 200.0f;
                    }
                    SetMove(ResetPoint);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            StartChangeTimeLine();
        }
        
        TestRenderImage tri = MyCamera.GetComponent<TestRenderImage>();

        if (IsControllable())
        {
            this.gameObject.GetComponent<FirstPersonController>().SetControllable(true);
        }

        if (CurChangeTime > 0)//时间线变化
        {
            tri.TurnOn();
            if (CurChangeTime > ChangeTime / 2.0f)//变化的前半时间
            {
                float ratio = 1.0f - (CurChangeTime / ChangeTime - 0.5f) * 2.0f;//从0变化到1                
                tri.distortFactor = 10f * ratio;
            }
            else//变化的后半时间
            {
                float ratio = (CurChangeTime / ChangeTime) * 2;//从1变化到0
                tri.distortFactor = 10f * ratio;

                if (!IsChanged)//切换时间点所做动作
                {
                    Vector3 pos = this.gameObject.transform.position;
                    IsChanged = true;
                    if (MyIsOld)
                    {
                        pos.x += 200f;
                        pos.y += 0.1f;
                        this.gameObject.transform.position = pos;
                        ChangeTimeLine(false);
                    }
                    else
                    {
                        pos.x -= 200f;
                        pos.y += 0.1f;
                        this.gameObject.transform.position = pos;
                        ChangeTimeLine(true);
                    }
                    Inventory.instance.changeTimeState();
                }
            }
            CurChangeTime -= Time.deltaTime;
        }
        else
        {
            tri.TurnOff();
        }


        if (CurMoveTime > 0)//移动中
        {
            if (CurMoveTime > MoveTime / 2.0f)//变化的前半时间，变暗
            {
                float ratio = 1.0f - (CurMoveTime / MoveTime - 0.5f) * 2.0f;
                Mask.color = new Color(1.0f, 1.0f, 1.0f, ratio);
            }
            else//变化的后半时间，变亮
            {
                float ratio = (CurMoveTime / MoveTime) * 2;
                Mask.color = new Color(1.0f, 1.0f, 1.0f, ratio);
                if (!IsMoved)//切换时间点所做动作
                {
                    IsMoved = true;
                    this.gameObject.transform.position = MoveTarget;
                    if (NeedToChangeEnviLight)
                    {
                        NeedToChangeEnviLight = false;
                        GameObject enviL = environmentLight.gameObject.transform.parent.gameObject;
                        enviL.SetActive(!enviL.activeSelf);
                    }
                }
            }
            CurMoveTime -= Time.deltaTime;
        }

        if (!IsControllable())
        {
            this.gameObject.GetComponent<FirstPersonController>().SetControllable(false);
        }
        

    }


    private void InitEnvironment()
    {
        //init

        CurChangeTime = 0;
        IsChanged = true;

        CurMoveTime = 0;
        IsMoved = true;
        MoveTarget = new Vector3(0, 0, 0);
        
        Mask.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        
        ChangeTimeLine(IsStartOld);

        NeedToChangeEnviLight = false;
    }
    
    public void SetMove(Vector3 Target)
    {
        if (IsControllable())
        {
            CurMoveTime = MoveTime;
            IsMoved = false;
            MoveTarget = Target;
        }
    }

    private void ChangeTimeLine(bool IsOld)
    {
        MyIsOld = IsOld;
        if (MyIsOld)
        {
            environmentLight.color = DayColor;
            RenderSettings.skybox = DaySkyBox;
            ModernAudio.volume = 0;
            AncientAudio.volume = 0.5f;
        }
        else
        {
            environmentLight.color = DuskColor;
            RenderSettings.skybox = DuskSkyBox;
            AncientAudio.volume = 0;
            ModernAudio.volume = 0.5f;
        }
    }

    public void TurnOnOffEnviLight()
    {
        NeedToChangeEnviLight = true;
    }


    private bool StartChangeTimeLine()
    {
        if (IsControllable())
        {
            CurChangeTime = ChangeTime;
            IsChanged = false;//变化中间点

            if (MyIsOld)
            {
                TeleportAudio.clip = AudioForward;
            }
            else
            {
                TeleportAudio.clip = AudioRewind;
            }
            TeleportAudio.Play();
            return true;
        }
        return false;
    }

    public bool IsControllable()
    {
        if (CurChangeTime > 0)
        {
            return false;
        }
        if (CurMoveTime > 0)
        {
            return false;
        }
        return true;
    }
}
