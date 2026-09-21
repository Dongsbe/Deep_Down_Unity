using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockNumber : MonoBehaviour {

    public int unlock;
    bool isClickState;
    int ClickCnt;
    private Transform tr;

    // Use this for initialization
    void Start () {
        isClickState = true;
        ClickCnt = 0;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void KeyDownU()
    {
        if(isClickState)
        {
            ClickCnt++;
            ClickCnt = ClickCnt % 10;
            iTween.RotateBy(this.gameObject, iTween.Hash("y", 0.1f, "easeType", iTween.EaseType.linear, "speed", 100.0f));
            ReturnNum();
            isClickState = false;
            StartCoroutine("ResetState");

            GameSoundManager.GetInstance().GetCharacterSound();
            GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.LockSpinSound);
        }
    }

    public int ReturnNum()
    {
        return ClickCnt;
    }

    public int ReturnSuccNum()
    {
        return unlock;
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(0.5f);
        isClickState = true;
    }
}
