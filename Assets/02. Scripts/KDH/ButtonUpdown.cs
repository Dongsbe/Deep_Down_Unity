using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpdown : MonoBehaviour {

    private bool isClickState;

    // Use this for initialization
    void Start () {
        isClickState = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void isClick()
    {
        if(isClickState)
        {
            iTween.MoveAdd(this.gameObject, iTween.Hash("y", -0.2f, "easeType", iTween.EaseType.linear, "speed", 50.0f));
            StartCoroutine("ResetState");
        }

    }


    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(0.2f);
        iTween.MoveAdd(this.gameObject, iTween.Hash("y", 0.2f, "easeType", iTween.EaseType.linear, "speed", 50.0f));
        isClickState = true;
        
    }
}
