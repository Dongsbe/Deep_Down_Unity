using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class EventObject : MonoBehaviour
{
    public EventObj ObjectType = EventObj.Unknown;
    public EventAction ObjectAction = EventAction.Unknown;
    public GameObject TargetObject;
    public TargetPos ActingBy;
    public DoorState DrState;
    public float ActionScale;
    public bool IsVideoUsed;

    public bool IsUsed;

    // Use this for initialization
    void Start()
    {
        IsUsed = false;

        switch (this.ObjectType)
        {
            case EventObj.Character:
                {
                }
                break;

            case EventObj.Door:
                {
                }
                break;

            case EventObj.Item:
                {
                }
                break;

            case EventObj.JumpScare:
                {
                    this.gameObject.SetActive(false);
                }
                break;

            case EventObj.Unknown:
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        EventObject colTarget = other.GetComponent<EventObject>();
        switch (ObjectType)
        {
            case EventObj.Character:
                {
                    if (colTarget == null)
                    {
                        return;
                    }

                    switch (colTarget.ObjectType)
                    {
                        case EventObj.Item:
                            {
                                switch (colTarget.ObjectAction)
                                {
                                    case EventAction.Move:
                                        {
                                            if (colTarget.tag == "Untagged")
                                            {
                                                if (colTarget.TargetObject)
                                                {
                                                    if (colTarget.GetComponent<iTween>() == null)
                                                    {
                                                        iTween.MoveBy(colTarget.TargetObject.gameObject, iTween.Hash(colTarget.ActingBy.ToString(), colTarget.ActionScale, "easeType", "linear", "speed", 0.1f));
                                                        GameSoundManager.GetInstance().GetAudioSource(colTarget.TargetObject.GetComponent<AudioSource>());
                                                        GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.BookShelf);
                                                    }
                                                }

                                                else
                                                {
                                                    if (colTarget.GetComponent<iTween>() == null)
                                                    {
                                                        iTween.MoveBy(colTarget.gameObject, iTween.Hash(colTarget.ActingBy.ToString(), colTarget.ActionScale, "easeType", "easeInOutExpo", "delay", 1));
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case EventAction.Rotate:
                                        {
                                            if (colTarget.GetComponent<iTween>() == null)
                                            {
                                                iTween.RotateBy(colTarget.gameObject, iTween.Hash(colTarget.ActingBy.ToString(), colTarget.ActionScale, "easeType", "easeInOutBack", "delay", 0));
                                            }
                                        }
                                        break;

                                    case EventAction.Drop:
                                        {
                                            if (colTarget.IsUsed == false)
                                            {
                                                colTarget.GetComponent<Rigidbody>().useGravity = true;
                                                GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.DropFrame);
                                                colTarget.gameObject.GetComponent<BoxCollider>().enabled = false;
                                                colTarget.IsUsed = true;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;

                        case EventObj.Area:
                            {
                                if (colTarget == null)
                                {
                                    return;
                                }

                                if (colTarget.name == "StageTutorial")
                                {
                                    GameControlManager.GetInstance().curStage = StageInfo.StageTutorial;
                                    if (colTarget.IsUsed == false)
                                    {
                                        GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                        GameSoundManager.GetInstance().PlayAreaSound(AreaSound.Tutorial);
                                        colTarget.IsUsed = true;
                                    }
                                }

                                if (colTarget.name == "Stage1")
                                {
                                    GameControlManager.GetInstance().curStage = StageInfo.Stage1;
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayAreaSound(AreaSound.Stage1);
                                }

                                if (colTarget.name == "Stage2")
                                {
                                    GameControlManager.GetInstance().curStage = StageInfo.Stage2;
                                    GameControlManager.GetInstance().IsStageClear = false;
                                    GameControlManager.GetInstance().IsStageObjClear = false;
                                }

                                if (colTarget.name == "Stage1_Hidden")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayAreaSound(AreaSound.Stage1_Hidden_BGM);
                                    GameSoundManager.GetInstance().PlayAreaSound(AreaSound.Stage1_Hidden_BABY);
                                }

                                if(colTarget.name == "ToiletArea")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.GetWallet1);
                                }
                            }
                            break;
                    }
                }
                break;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        EventObject colTarget = other.GetComponent<EventObject>();

        if (colTarget == null)
        {
            return;
        }

        if (colTarget.enabled == false)
        {
            return;
        }

        colTarget.IsUsed = false;

        switch (ObjectType)
        {
            case EventObj.Character:
                {
                    if (colTarget == null)
                    {
                        return;
                    }

                    switch (colTarget.ObjectType)
                    {
                        case EventObj.Item:
                            {
                                switch (colTarget.ObjectAction)
                                {
                                    case EventAction.Animation:
                                        {
                                            //if (Input.GetKeyDown(KeyCode.E))
                                            //{
                                            //    if (colTarget.GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying)
                                            //    {
                                            //        colTarget.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
                                            //    }

                                            //    else
                                            //    {
                                            //        colTarget.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                                            //    }
                                            //}
                                        }
                                        break;
                                }
                            }
                            break;

                        case EventObj.Door:
                            {
                                switch (colTarget.DrState)
                                {
                                    case DoorState.Open:
                                        {
                                            if (colTarget.GetComponent<iTween>() == null)
                                            {
                                                switch (colTarget.name)
                                                {
                                                    case "ToiletDoor2":
                                                        {
                                                            if (Input.GetKeyDown(KeyCode.E))
                                                            {
                                                                GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                                GameSoundManager.GetInstance().PlayDoorSound(DoorState.Stage2_PublicToilet);
                                                                iTween.RotateAdd(colTarget.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                                colTarget.DrState = DoorState.Close;
                                                            }
                                                        }
                                                        break;

                                                    case "EndingDoor":
                                                        {
                                                            GameSoundManager.GetInstance().StopAudio();
                                                            GameControlManager.GetInstance().EndingVid.enabled = true;

                                                            if ((ulong)GameControlManager.GetInstance().EndingVid.frame >= GameControlManager.GetInstance().EndingVid.frameCount - 3)
                                                            {
                                                                SceneManager.LoadScene("IntroScene");
                                                            }
                                                        }
                                                        break;

                                                    case "Private_Toilet":
                                                        {
                                                            if (Input.GetKeyDown(KeyCode.E))
                                                            {
                                                                GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                                GameSoundManager.GetInstance().PlayDoorSound(DoorState.Stage2_PrivateToilet);
                                                                iTween.RotateAdd(colTarget.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                                colTarget.DrState = DoorState.Close;
                                                            }
                                                        }
                                                        break;

                                                    case "Stage2ExitDoor":
                                                        {
                                                            if(GameControlManager.GetInstance().IsStageClear)
                                                            {
                                                                if (colTarget.IsUsed == false)
                                                                {
                                                                    //GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                                    //GameSoundManager.GetInstance().PlayDoorSound(DoorState.Open);
                                                                    iTween.RotateAdd(colTarget.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                                    colTarget.DrState = DoorState.Close;
                                                                    colTarget.IsUsed = true;
                                                                }
                                                            }
                                                        }
                                                        break;

                                                    default:
                                                        {
                                                            if (Input.GetKeyDown(KeyCode.E))
                                                            {
                                                                GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                                GameSoundManager.GetInstance().PlayDoorSound(colTarget.DrState);
                                                                iTween.RotateAdd(colTarget.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                                colTarget.DrState = DoorState.Close;
                                                            }
                                                        }
                                                        break;
                                                }                                                
                                            }
                                        }
                                        break;

                                    case DoorState.Close:
                                        {
                                            if (colTarget.GetComponent<iTween>() == null)
                                            {
                                                if (Input.GetKeyDown(KeyCode.E))
                                                {

                                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                    GameSoundManager.GetInstance().PlayDoorSound(colTarget.DrState);
                                                    iTween.RotateAdd(colTarget.gameObject, iTween.Hash("y", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                    colTarget.DrState = DoorState.Open;
                                                }
                                            }
                                        }
                                        break;

                                    case DoorState.Locked:
                                        {
                                            if (Input.GetKeyDown(KeyCode.E))
                                            {
                                                GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                GameSoundManager.GetInstance().PlayDoorSound(colTarget.DrState);
                                            }
                                        }
                                        break;

                                    case DoorState.Lock:
                                        {
                                            if (GameControlManager.GetInstance().IsStageObjClear == false)
                                            {
                                                if (Input.GetKeyDown(KeyCode.E))
                                                {
                                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                                    GameSoundManager.GetInstance().PlayDoorSound(colTarget.DrState);
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            break;

                        case EventObj.Area:
                            {
                                if (colTarget.name == "Carpet")
                                {
                                    this.gameObject.GetComponent<FirstPersonController>().m_bOnCarpet = true;
                                }

                                if (colTarget.name == "Stage1")
                                {
                                    if (GameControlManager.GetInstance().curStage == StageInfo.StageTutorial)
                                    {
                                        GameControlManager.GetInstance().curStage = StageInfo.Stage1;
                                        GameControlManager.GetInstance().IsStageClear = false;
                                    }
                                }

                                if(colTarget.name == "XrayArea")
                                {
                                    if (GameControlManager.GetInstance().XrayGhost == true)
                                    {
                                        GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                        GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.XrayLightOff);
                                    }
                                }

                                // 0930 YSM (2스테이지 콜라이더 만들면서 추가)
                                //if(colTarget.name == "Stage2")
                                //{
                                //    if(GameControlManager.GetInstance().curStage == StageInfo.Stage1)
                                //    {
                                //        GameControlManager.GetInstance().curStage = StageInfo.Stage2;
                                //        GameControlManager.GetInstance().IsStageClear = false;
                                //        GameControlManager.GetInstance().IsStageObjClear = false;
                                //    }
                                //}
                            }
                            break;
                    }
                }
                break;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        EventObject colTarget = other.GetComponent<EventObject>();
        switch (ObjectType)
        {
            case EventObj.Character:
                {
                    if (colTarget == null)
                    {
                        return;
                    }

                    switch (colTarget.ObjectType)
                    {
                        case EventObj.Area:
                            {
                                if (colTarget.name == "StageTutorial")
                                {
                                    Destroy(colTarget);
                                    //
                                }

                                if (colTarget.name == "Stage1")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().StopAudio();
                                    Destroy(colTarget);
                                }

                                if (colTarget.name == "Stage1_Hidden")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().StopAudio();
                                }

                                if(colTarget.name == "ToiletArea")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().StopAudio();
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.Mannequinn);
                                    Destroy(colTarget);
                                }

                                if(colTarget.name == "XrayArea")
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(colTarget.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().StopAudio();
                                    Destroy(colTarget);
                                }

                                if (colTarget.name == "Carpet")
                                {
                                    if (this.ObjectType == EventObj.Character)
                                    {
                                        this.gameObject.GetComponent<FirstPersonController>().m_bOnCarpet = false;
                                    }
                                }
                            }
                            break;
                    }
                }
                break;
        }
    }
}
