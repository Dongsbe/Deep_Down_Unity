using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateDoor
{
    Stage2Start,
    PublicTollet
}

public class OpenDoor : MonoBehaviour
{

    public StateDoor sDoor;
    //public Animator charAni;
    // public float angle =0 0.0f;
    EventObject doorTarget;
    AudioSource getAudio;
    bool isStage2Run;
    bool isPublicTolletRun;

    bool isBathTub;
    bool IsOpen;
    void Start()
    {
        isStage2Run = false;
        isPublicTolletRun = false;
        this.GetComponent<EventObject>().DrState = DoorState.Open;
    }

    // Update is called once per frame
    void Update()
    {
        switch(sDoor)
        {
            case StateDoor.Stage2Start:
                {
                    if (GameControlManager.GetInstance().isStage2OpenDoor && !isStage2Run)
                    {
                        isStage2Run = true;
                        RotateOpenDoor();
                    }
                }
                break;
            case StateDoor.PublicTollet:
                {
                    if(GameControlManager.GetInstance().curStage == StageInfo.Stage2)
                    {
                        if(GameControlManager.GetInstance().IsStageClear)
                        {
                            if(this.GetComponent<EventObject>().DrState == DoorState.Close)
                            {
                                iTween.RotateAdd(this.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                this.GetComponent<EventObject>().DrState = DoorState.Lock;
                            }
                        }
                    }

                    if (GameControlManager.GetInstance().isPublicTolletClear  == true && !isPublicTolletRun)
                    {
                        isPublicTolletRun = true;
                        RotateOpenDoor();
                    }

                    if(GameControlManager.GetInstance().isPublicTolletClear == false && !isBathTub)
                    {
                        if(isPublicTolletRun)
                        {
                            isBathTub = true;
                            RotateCloseDoor();
                        }
                    }
                }
                break;
            default:
                break;
        }


    }


    void RotateOpenDoor()
    {
        //if (this.GetComponent<EventObject>().DrState == DoorState.Open)
        //{
        //    iTween.RotateAdd(this.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
        //    this.GetComponent<EventObject>().DrState = DoorState.Close;
        //}

        //if (IsOpen == false)
        //{
        //    GameSoundManager.GetInstance().GetAudioSource(getAudio);
        //    GameSoundManager.GetInstance().PlayDoorSound(DoorState.Open);
        //    IsOpen = true;
        //}
        if(this.gameObject.name == "Close03")
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
            GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
            GameSoundManager.GetInstance().PlayDoorSound(DoorState.Stage2_PrivateToilet);
            this.GetComponent<EventObject>().DrState = DoorState.Lock;
        }

        else
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));

            GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
            GameSoundManager.GetInstance().PlayDoorSound(DoorState.Stage2_PrivateToilet);
            this.GetComponent<EventObject>().DrState = DoorState.Close;
        }
    }

    void RotateCloseDoor()
    {
        iTween.RotateAdd(this.gameObject, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));

        GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
        GameSoundManager.GetInstance().PlayDoorSound(DoorState.Stage2_PrivateToilet);
        this.GetComponent<EventObject>().DrState = DoorState.Lock;
    }
}
