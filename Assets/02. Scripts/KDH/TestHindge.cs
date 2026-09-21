using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHindge : MonoBehaviour {

    public bool isOpen;
    bool firstOpen;

	// Use this for initialization
	void Start () {
           iTween.RotateAdd(this.gameObject, iTween.Hash("x", 5, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
           isOpen = false;
           firstOpen = true;
        }
        //HingeState = RotateHinge.Unknown;


	
	// Update is called once per frame
	void Update () {

        if(isOpen && firstOpen)
        {
            iTween.RotateAdd(this.gameObject, iTween.Hash("x", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
            firstOpen = false;
        }

    }
}
