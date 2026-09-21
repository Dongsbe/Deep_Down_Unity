using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnView : MonoBehaviour {

    bool isClickState;

    // Use this for initialization
    void Start () {
        isClickState = true;
    }
	
	// Update is called once per frame
	void Update () {

        
	}
    public void ChangeNum()
    {
        if(isClickState)
        {
            iTween.RotateBy(this.gameObject, iTween.Hash("y", 0.1f, "easeType", iTween.EaseType.linear, "speed", 100.0f));
            isClickState = false;
            StartCoroutine("ResetState");
        }
            
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(0.5f);
        isClickState = true;
    }
}
 