using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockCheck : MonoBehaviour {


    public bool m_bIsHourCheck;
    public bool m_bIsMinCheck;

    // Use this for initialization
    void Start () {

        m_bIsHourCheck = false;
        m_bIsMinCheck = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "HourCorrect")
        {
            if (other.gameObject.name == "Clock_hands_Hour")
            {
                m_bIsHourCheck = true;
            }
        }

        if (gameObject.name == "MinCorrect")
        {
            if (other.gameObject.name == "Clock_hands_Min")
            {
                m_bIsMinCheck = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "HourCorrect")
        {
            if (other.gameObject.name == "Clock_hands_Hour")
            {
                m_bIsHourCheck = false;
            }
        }

        if (gameObject.name == "MinCorrect")
        {
            if (other.gameObject.name == "Clock_hands_Min")
            {
                m_bIsMinCheck = false;
            }
        }
    }
}
