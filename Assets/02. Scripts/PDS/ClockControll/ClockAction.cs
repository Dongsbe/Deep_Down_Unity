using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAction : MonoBehaviour {

    public GameObject Hour;
    public GameObject Min;

    ClockCheck HourCheck;
    ClockCheck MinCheck;

    public GameObject Target;

	// Use this for initialization
	void Start ()
    {
        HourCheck = Hour.GetComponent<ClockCheck>();
        MinCheck = Min.GetComponent<ClockCheck>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(HourCheck.m_bIsHourCheck)
        {
            if(MinCheck.m_bIsMinCheck)
            {
                GameControlManager.GetInstance().m_bIsClockOn = true;
                GameControlManager.GetInstance().Target = Target;
                //if (m_bAction == false)
                //{
                //    if(GameControlManager.GetInstance().m_bIsOnGame)
                //    {
                //        iTween.MoveBy(Target.gameObject, iTween.Hash("y", -2, "easeType", "linear", "speed", 0.1f));
                //        GameSoundManager.GetInstance().GetAudioSource(Target.GetComponent<AudioSource>());
                //        GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.BookShelf);

                //        m_bAction = true;
                //    }
                //}
            }
        }
	}
}
