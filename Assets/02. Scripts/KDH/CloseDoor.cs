using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour {

    EventObject doorTarget;
    AudioSource getAudio;

    bool isCloseRun;
    bool isOpenRun;
	// Use this for initialization
	void Start () {
        isCloseRun = false;
        isOpenRun = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(GameControlManager.GetInstance().isTolletCloseDoor && !isCloseRun)
        {
            isCloseRun = true;
            //GameSoundManager.GetInstance().GetAudioSource(getAudio);
            //GameSoundManager.GetInstance().PlayDoorSound(DoorState.Close);
            iTween.RotateAdd(this.gameObject, iTween.Hash("y", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
            //doorTarget.DrState = DoorState.Open;
            this.GetComponent<EventObject>().DrState = DoorState.Locked;
        }

        if(GameControlManager.GetInstance().isTolletOpenDoor && !isOpenRun)
        {
            isOpenRun = true;
            this.GetComponent<EventObject>().DrState = DoorState.Open;
        }


    }
}
